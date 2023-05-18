using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QueueForChildren.Data.Dtos.KindergartenQueue;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Identity;
using QueueForChildren.Services.Kindergarten;
using QueueForChildren.Web.Interfaces;

namespace QueueForChildren.Controllers
{
    [Authorize]
    public class KindergartenQueueController : Controller
    {
        private readonly IRepository<KindergartenLanguage> _kindergartenLanguageRepository;

        private readonly IRepository<Kindergarten> _kindergartenRepository;

        private readonly IRepository<Address> _addressRepository;

        private readonly IRepository<Parent> _parentRepository;

        private readonly IRepository<Child> _childRepository;

        private readonly UserManager<User> _userManager;

        private readonly IKindergartenListGetService _kindergartenListGetService;

        private readonly IAppendToQueueService _appendToQueueService;

        private readonly IKindergartenQueueGetService _kindergartenQueueGetService;

        public KindergartenQueueController(IRepository<KindergartenLanguage> kindergartenLanguageRepository,
            IRepository<Kindergarten> kindergartenRepository, IRepository<Address> addressRepository,
            IRepository<Parent> parentRepository, IRepository<Child> childRepository, UserManager<User> userManager,
            IKindergartenListGetService kindergartenListGetService, IAppendToQueueService appendToQueueService, IKindergartenQueueGetService kindergartenQueueGetService)
        {
            _kindergartenLanguageRepository = kindergartenLanguageRepository;
            _kindergartenRepository = kindergartenRepository;
            _addressRepository = addressRepository;
            _parentRepository = parentRepository;
            _childRepository = childRepository;
            _userManager = userManager;
            _kindergartenListGetService = kindergartenListGetService;
            _appendToQueueService = appendToQueueService;
            _kindergartenQueueGetService = kindergartenQueueGetService;
        }
        
        [HttpGet]
        public IActionResult FindKindergarten()
        {
            var regions = _kindergartenRepository.GetAll()
                .Where(kindergarten => kindergarten.Address.MicroRegion != null)
                .Select(kindergarten => kindergarten.Address.MicroRegion)
                .Distinct()
                .ToArray();

            ViewBag.Regions = new SelectList(regions);
            ViewBag.Languages = new SelectList(_kindergartenLanguageRepository.GetAll()
                .Select(lang => lang.Name)
                .Distinct()
            );

            return PartialView("FindKindergarten");
        }

        public IActionResult KindergartenList()
        {
            var kindergartenList = _kindergartenListGetService.KindergartenList();
            return PartialView("KindergartenList", new KindergartenSelectDto()
            {
                Kindergartens = kindergartenList
            });
        }

        [HttpPost]
        public async Task<IActionResult> FindKindergarten(KindergartenFindDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            var list = _kindergartenListGetService.FindKindergarten(user, dto);

            var children = user.Parent.Children.Select(child => new
                {
                    child.Id,
                    Name = child.LastName + " " + child.Name[0] + ". " + child.MiddleName?[0] + ".",
                })
                .ToArray();

            ViewBag.Children = new SelectList(children, "Id", "Name");

            return PartialView("SelectKindergarten", new KindergartenSelectDto()
            {
                Kindergartens = list
            });
        }

        [HttpPost]
        public async Task<IActionResult> SendApplication([FromBody]SelectedDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            long.TryParse(dto.childId, out var _childId);
            long.TryParse(dto.kindergartenId, out var _kindergartenId);
            var child = _childRepository.GetById(_childId);
            var kindergarten = _kindergartenRepository.GetById(_kindergartenId);
            
            var msg = _appendToQueueService.AppendToQueue(user, child, kindergarten);

            return Json(new {success = true, data = msg});
        }
        
        public IActionResult SelectKindergartenQueue()
        {
            var schoolList = _kindergartenRepository.GetAll()
                .Select(kindergarten => new
                {
                    kindergarten.Id,
                    kindergarten.Name
                });
            
            ViewBag.KindergartenList = new SelectList(schoolList, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> QueueList(long schoolId)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.ParentId = user.ParentId;

            var list = _kindergartenQueueGetService.GetQueueList(schoolId);
            return PartialView("_queueList", list);
        }
    }
}