using System.Linq;
using Castle.Components.DictionaryAdapter;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Entities.FNS;
using QueueForChildren.Data.Entities.Zags;
using QueueForChildren.Data.Identity;
using QueueForChildren.Web.Interfaces;

namespace QueueForChildren.Services.School
{
    public interface IDataCheckService
    {
        string CheckBeforeAppend(User user, Child child);
    }

    internal sealed class DataCheckService : IDataCheckService
    {
        private readonly IRepository<ZagsChild> _zagsChildRepositroy;

        private readonly IRepository<ZagsParent> _zagsParentRepositroy;

        private readonly IRepository<Applicant> _applicantRepository;

        private readonly IRepository<Estate> _estateRepository;

        public DataCheckService(IRepository<ZagsChild> zagsChildRepositroy,
            IRepository<ZagsParent> zagsParentRepositroy,
            IRepository<Applicant> applicantRepository,
            IRepository<Estate> estateRepository)
        {
            _zagsChildRepositroy = zagsChildRepositroy;
            _zagsParentRepositroy = zagsParentRepositroy;
            _applicantRepository = applicantRepository;
            _estateRepository = estateRepository;
        }

        public string CheckBeforeAppend(User user, Child child)
        {
            var childExistInZags = _zagsChildRepositroy
                .GetAll()
                .Where(ch => ch.Serial == child.Serial)
                .Any(ch => ch.Number == child.Number);

            if (!childExistInZags)
            {
                return $"Ребенок со свидетельством о рождении {child.Serial} {child.Number} "+
                       "не найден в записях ЗАГСа. Проверьте достоверность данных.";
            }

            var parentExistInZags = _zagsParentRepositroy.GetAll()
                .Any(p => p.INN == user.Parent.INN);

            if (!parentExistInZags)
            {
                return $"Пользователь с ИНН: {user.Parent.INN} "+
                       "не найден в записях ЗАГСа. Проверьте достоверность данных.";
            }

            var applicant = _applicantRepository
                .GetAll()
                .SingleOrDefault(app => app.ParentId == user.ParentId);

            if (applicant is null)
            {
                return "Пользователь не найден в ФНС. Обратитесь к администратору системы.";
            }

            if (applicant.AverageIncome > 14000m && !applicant.IsBenefit)
            {
                return "Ваш доход на 1 члена семьи превышает 14000р.";
            }

            return null;
        }
    }
}