using System.Collections.Generic;
using System.Linq;
using QueueForChildren.Data.Dtos.Queue;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Entities.FNS;
using QueueForChildren.Services.School;
using QueueForChildren.Web.Interfaces;

namespace QueueForChildren.Services.Kindergarten
{
    public interface IKindergartenQueueGetService
    {
        IReadOnlyList<QueueBy> GetQueueList(long kindergartenId);
    }
    
    internal sealed class KindergartenQueueGetService : IKindergartenQueueGetService
    {
        private readonly IRepository<QueueToKindergarten> _queueRepository;

        private readonly IRepository<Applicant> _applicantRepository;
        
        public KindergartenQueueGetService(IRepository<QueueToKindergarten> queueRepository, IRepository<Applicant> applicantRepository)
        {
            _queueRepository = queueRepository;
            _applicantRepository = applicantRepository;
        }

        public IReadOnlyList<QueueBy> GetQueueList(long kindergartenId)
        {
            var queue = _queueRepository.GetAll()
                .Where(q => q.KindergartenId == kindergartenId)
                .ToArray();

            var applicantIds = queue.Select(q => q.Child.ParentId)
                .Distinct();

            var applicantArray = _applicantRepository.GetAll()
                .Where(a => applicantIds.Contains(a.ParentId))
                .ToArray();
            
            var list = (from q in queue
                    join a in applicantArray on q.Child.ParentId equals a.ParentId
                    where q.KindergartenId == kindergartenId
                    select new
                    {
                        Name = GetChildName(q.Child),
                        a.IsBenefit,
                        AverageIncome = a.FamilyIncomeForYear / a.FamilyMembersCount / 12,
                        q.CreateDate,
                        q.ChildId,
                        q.Child.ParentId
                    })
                .OrderBy(l => l.IsBenefit)
                .ThenBy(l => l.AverageIncome)
                .ThenByDescending(l => l.CreateDate)
                .Select(l => new QueueBy
                {
                    ChildName = l.Name,
                    IsBenefit = l.IsBenefit ? "Да" : "Нет",
                    Date = l.CreateDate.ToString("dd.MM.yyyy"),
                    ParentId = l.ParentId
                })
                .ToArray();

            return list;
        }
        
        private string GetChildName(Child child)
        {
            var name = child.LastName + " " + child.Name[0] + ". ";
            return string.IsNullOrEmpty(child.MiddleName)
                ? name
                : name + child.MiddleName[0] + '.';
        }
    }
}