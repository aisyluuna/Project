using System.Linq;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Identity;
using QueueForChildren.Web.Interfaces;

namespace QueueForChildren.Services.School
{
    public interface IAppendToQueueService
    {
        string AppendToQueue(User user, Child child, Data.Entities.School school);
    }
    
    internal sealed class AppendToQueueService : IAppendToQueueService
    {
        private readonly IRepository<SchoolFillInfo> _fillInfoRepository;

        private readonly IRepository<QueueToSchool> _queueToSchoolRepository;

        private readonly IDataCheckService _dataCheckService;

        public AppendToQueueService(IRepository<SchoolFillInfo> fillInfoRepository,
            IRepository<QueueToSchool> queueToSchoolRepository, IDataCheckService dataCheckService)
        {
            _fillInfoRepository = fillInfoRepository;
            _queueToSchoolRepository = queueToSchoolRepository;
            _dataCheckService = dataCheckService;
        }

        public string AppendToQueue(User user, Child child, Data.Entities.School school)
        {
            var fillInfo = _fillInfoRepository
                .GetAll()
                .SingleOrDefault(info => info.SchoolId == school.Id);

            if (!HasFreePlace(fillInfo))
            {
                return "В выбранной школе пока нет свободных мест.";
            }

            var childExistInQueue = _queueToSchoolRepository
                .GetAll()
                .Where(queue => queue.SchoolId == school.Id)
                .Any(queue => queue.ChildId == child.Id);

            if (childExistInQueue)
            {
                return "Ваш ребенок уже поставлен в очередь!";
            }

            var msg = _dataCheckService.CheckBeforeAppend(user, child);

            if (msg != null)
            {
                return msg;
            }

            var queueToSchool = new QueueToSchool
            {
                ChildId = child.Id,
                SchoolId = school.Id
            };

            _queueToSchoolRepository.Create(new[] {queueToSchool});
            fillInfo.InQueueCount++;
            _fillInfoRepository.Update(new[] {fillInfo});

            return "Ваш ребенок успешно оставлен в очередь!";
        }

        private static bool HasFreePlace(SchoolFillInfo fillInfo)
        {
            return fillInfo.FreePlaceCount - fillInfo.FilledCount - fillInfo.InQueueCount > 0;
        }
    }
}