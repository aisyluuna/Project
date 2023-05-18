using System;
using System.Collections.Generic;
using System.Linq;
using QueueForChildren.Data.Dtos.KindergartenQueue;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Identity;
using QueueForChildren.Web.Extensions;
using QueueForChildren.Web.Interfaces;

namespace QueueForChildren.Services.Kindergarten
{
    public interface IKindergartenListGetService
    {
        IReadOnlyList<KindergartenDto> KindergartenList();

        IReadOnlyList<KindergartenDto> FindKindergarten(User user, KindergartenFindDto dto);
    }

    internal sealed class KindergartenListGetService : IKindergartenListGetService
    {
        private readonly IRepository<KindergartenLanguage> _kindergartenLanguageRepository;

        private readonly IRepository<Parent> _parentRepository;

        private readonly IRepository<KindergartenFillInfo> _kindergartenFillInfoRepository;

        public KindergartenListGetService(IRepository<KindergartenLanguage> kindergartenLanguageRepository,
            IRepository<Parent> parentRepository, IRepository<KindergartenFillInfo> kindergartenFillInfoRepository)
        {
            _kindergartenLanguageRepository = kindergartenLanguageRepository;
            _parentRepository = parentRepository;
            _kindergartenFillInfoRepository = kindergartenFillInfoRepository;
        }

        public IReadOnlyList<KindergartenDto> KindergartenList()
        {
            var kindergartenList = _kindergartenFillInfoRepository.GetAll()
                .Select(info => new KindergartenDto()
                {
                    Id = info.KindergartenId,
                    Name = info.Kindergarten.Name,
                    Address = "ул. " + info.Kindergarten.Address.Street + ", д. " + info.Kindergarten.Address.HouseNumber +
                              info.Kindergarten.Address.AdditionalChar,
                    MicroRegion = info.Kindergarten.Address.MicroRegion,
                    Rating = info.Kindergarten.Rating,
                    Phone = info.Kindergarten.Phone,
                    FreePlaceCount = info.FreePlaceCount - info.FilledCount - info.InQueueCount,
                    Languages = info.Kindergarten.Languages
                })
                .ToArray();

            return kindergartenList;
        }

        public IReadOnlyList<KindergartenDto> FindKindergarten(User user, KindergartenFindDto dto)
        {
            var kindergartens = _kindergartenLanguageRepository.GetAll()
                .Where(lang => lang.Name == dto.Language)
                .Where(lang => lang.Kindergarten.Address.MicroRegion == dto.Region)
                .Select(lang => new
                {
                    lang.KindergartenId,
                    Longitude = lang.Kindergarten.Address.Longitude ?? 0,
                    Latitude = lang.Kindergarten.Address.Latitude ?? 0
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

            var kindergartensByRadius = kindergartens
                .WhereIf(isByCoords, kindergarten => parentAddress.Latitude - around.aroundLat <= kindergarten.Latitude)
                .WhereIf(isByCoords, kindergarten => kindergarten.Latitude <= parentAddress.Latitude + around.aroundLat)
                .WhereIf(isByCoords, kindergarten => parentAddress.Longitude - around.aroundLon <= kindergarten.Longitude)
                .WhereIf(isByCoords, kindergarten => kindergarten.Longitude <= parentAddress.Longitude + around.aroundLon)
                .ToArray();

            var kindergartenIds = kindergartensByRadius.Select(kindergarten => kindergarten.KindergartenId);

            var kindergartenList = _kindergartenFillInfoRepository.GetAll()
                .Where(info => kindergartenIds.Contains(info.KindergartenId))
                .Select(info => new KindergartenDto()
                {
                    Id = info.KindergartenId,
                    Name = info.Kindergarten.Name,
                    Address = "ул. " + info.Kindergarten.Address.Street + ", д. " + info.Kindergarten.Address.HouseNumber +
                              info.Kindergarten.Address.AdditionalChar,
                    MicroRegion = info.Kindergarten.Address.MicroRegion,
                    Rating = info.Kindergarten.Rating,
                    Phone = info.Kindergarten.Phone,
                    FreePlaceCount = info.FreePlaceCount - info.FilledCount - info.InQueueCount,
                    Languages = info.Kindergarten.Languages
                })
                .ToArray();

            return kindergartenList;
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