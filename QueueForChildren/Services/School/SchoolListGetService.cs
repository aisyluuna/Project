using System;
using System.Collections.Generic;
using System.Linq;
using QueueForChildren.Data.Dtos;
using QueueForChildren.Data.Dtos.SchoolQueue;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Identity;
using QueueForChildren.Web.Extensions;
using QueueForChildren.Web.Interfaces;

namespace QueueForChildren.Services.School
{
    public interface ISchoolListGetService
    {
        IReadOnlyList<SchoolDto> SchoolList();

        IReadOnlyList<SchoolDto> FindSchool(User user, SchoolFindDto dto);
    }

    internal sealed class SchoolListGetService : ISchoolListGetService
    {
        private readonly IRepository<SchoolLanguage> _schoolLanguageRepository;

        private readonly IRepository<Parent> _parentRepository;

        private readonly IRepository<SchoolFillInfo> _schoolFillInfoRepository;

        public SchoolListGetService(IRepository<SchoolLanguage> schoolLanguageRepository,
            IRepository<Parent> parentRepository, IRepository<SchoolFillInfo> schoolFillInfoRepository)
        {
            _schoolLanguageRepository = schoolLanguageRepository;
            _parentRepository = parentRepository;
            _schoolFillInfoRepository = schoolFillInfoRepository;
        }

        public IReadOnlyList<SchoolDto> SchoolList()
        {
            var schoolList = _schoolFillInfoRepository.GetAll()
                .Select(info => new SchoolDto()
                {
                    Id = info.SchoolId,
                    Name = info.School.Name,
                    Address = "ул. " + info.School.Address.Street + ", д. " + info.School.Address.HouseNumber +
                              info.School.Address.AdditionalChar,
                    MicroRegion = info.School.Address.MicroRegion,
                    Rating = info.School.Rating,
                    Phone = info.School.Phone,
                    FreePlaceCount = info.FreePlaceCount - info.FilledCount - info.InQueueCount,
                    Languages = info.School.Languages
                })
                .ToArray();

            return schoolList;
        }

        public IReadOnlyList<SchoolDto> FindSchool(User user, SchoolFindDto dto)
        {
            var schools = _schoolLanguageRepository.GetAll()
                .Where(lang => lang.Name == dto.Language)
                .Where(lang => lang.School.Address.MicroRegion == dto.Region)
                .Select(lang => new
                {
                    lang.SchoolId,
                    Longitude = lang.School.Address.Longitude ?? 0,
                    Latitude = lang.School.Address.Latitude ?? 0
                })
                .ToArray();

            var parentAddress = _parentRepository.GetAll()
                .Where(p => p.Id == user.ParentId)
                .Select(p => new Address
                {
                    Latitude = p.Address.Latitude,
                    Longitude = p.Address.Longitude
                })
                .SingleOrDefault();

            var isByCoords = parentAddress.Latitude != null && parentAddress.Longitude != null && dto.Radius > 0;

            var around = KoordRange(parentAddress, dto.Radius);

            var schoolsByRadius = schools
                .WhereIf(isByCoords, school => parentAddress.Latitude - around.aroundLat <= school.Latitude)
                .WhereIf(isByCoords, school => school.Latitude <= parentAddress.Latitude + around.aroundLat)
                .WhereIf(isByCoords, school => parentAddress.Longitude - around.aroundLon <= school.Longitude)
                .WhereIf(isByCoords, school => school.Longitude <= parentAddress.Longitude + around.aroundLon)
                .ToArray();

            var schoolIds = schoolsByRadius.Select(school => school.SchoolId);

            var schoolList = _schoolFillInfoRepository.GetAll()
                .Where(info => schoolIds.Contains(info.SchoolId))
                .Select(info => new SchoolDto()
                {
                    Id = info.SchoolId,
                    Name = info.School.Name,
                    Address = "ул. " + info.School.Address.Street + ", д. " + info.School.Address.HouseNumber +
                              info.School.Address.AdditionalChar,
                    MicroRegion = info.School.Address.MicroRegion,
                    Rating = info.School.Rating,
                    Phone = info.School.Phone,
                    FreePlaceCount = info.FreePlaceCount - info.FilledCount - info.InQueueCount,
                    Languages = info.School.Languages
                })
                .ToArray();

            return schoolList;
        }

        private (double aroundLat, double aroundLon) KoordRange(Address address, double radius)
        {
            const double EARTH_READIUS = 6371210;

            double computeDelta(double degrees)
            {
                return Math.PI / 180 * EARTH_READIUS * Math.Cos(deg2rad(degrees));
            }

            double deg2rad(double degrees)
            {
                return degrees * Math.PI / 180;
            }

            var aroundLat = radius / computeDelta(address.Latitude.Value);
            var aroundLon = radius / computeDelta(address.Longitude.Value);

            return (aroundLat, aroundLon);
        }
    }
}