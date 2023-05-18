using System.Linq;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Identity;
using QueueForChildren.Services.School;
using QueueForChildren.Web.Interfaces;

namespace QueueForChildren.Services.Kindergarten
{
    public interface IAppendToQueueService
    {
        string AppendToQueue(User user, Child child, Data.Entities.Kindergarten kindergarten);
    }

    internal sealed class AppendToQueueService : IAppendToQueueService
    {
        private readonly IRepository<KindergartenFillInfo> _fillInfoRepository;

        private readonly IRepository<QueueToKindergarten> _queueToKindergartenRepository;

        private readonly IDataCheckService _dataCheckService;

        public AppendToQueueService(IRepository<KindergartenFillInfo> fillInfoRepository,
            IRepository<QueueToKindergarten> queueToKindergartenRepository, IDataCheckService dataCheckService)
        {
            _fillInfoRepository = fillInfoRepository;
            _queueToKindergartenRepository = queueToKindergartenRepository;
            _dataCheckService = dataCheckService;
        }

        public string AppendToQueue(User user, Child child, Data.Entities.Kindergarten kindergarten)
        {
            var fillInfo = _fillInfoRepository
                .GetAll()
                .SingleOrDefault(info => info.KindergartenId == kindergarten.Id);

            if (!HasFreePlace(fillInfo))
            {
                return "В выбранном дестком саду пока нет свободных мест.";
            }

            var childExistInQueue = _queueToKindergartenRepository
                .GetAll()
                .Where(queue => queue.KindergartenId == kindergarten.Id)
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

            var queueTokindergarten = new QueueToKindergarten
            {
                ChildId = child.Id,
                KindergartenId = kindergarten.Id
            };

            _queueToKindergartenRepository.Create(new[] {queueTokindergarten});
            fillInfo.InQueueCount++;
            _fillInfoRepository.Update(new[] {fillInfo});

            return "Ваш ребенок успешно оставлен в очередь!";
        }

        private static bool HasFreePlace(KindergartenFillInfo fillInfo)
        {
            return fillInfo.FreePlaceCount - fillInfo.FilledCount - fillInfo.InQueueCount > 0;
        }
    }
}