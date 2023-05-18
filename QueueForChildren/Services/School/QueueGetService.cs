using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QueueForChildren.Data.Dtos.Queue;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Entities.FNS;
using QueueForChildren.Web.Interfaces;

namespace QueueForChildren.Services.School
{
    public interface ISchoolQueueGetService
    {
        IReadOnlyList<QueueBy> GetQueueList(long schoolId);
    }
    
    
    internal sealed class SchoolQueueGetService : ISchoolQueueGetService
    {
        private readonly IRepository<QueueToSchool> _queueRepository;

        private readonly IRepository<Applicant> _applicantRepository;
        
        public SchoolQueueGetService(IRepository<QueueToSchool> queueRepository, IRepository<Applicant> applicantRepository)
        {
            _queueRepository = queueRepository;
            _applicantRepository = applicantRepository;
        }

        public IReadOnlyList<QueueBy> GetQueueList(long schoolId)
        {
            var queue = _queueRepository.GetAll()
                .Where(q => q.SchoolId == schoolId)
                .Include(q => q.Child)
                .ToArray();

            var applicantIds = queue.Select(q => q.Child.ParentId)
                .Distinct();

            var applicantArray = _applicantRepository.GetAll()
                .Where(a => applicantIds.Contains(a.ParentId))
                .ToArray();
            
            var list = (from q in queue
                    join a in applicantArray on q.Child.ParentId equals a.ParentId
                    where q.SchoolId == schoolId
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