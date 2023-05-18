using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueueForChildren.Data.Dtos;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Identity;
using QueueForChildren.Web.Interfaces;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QueueForChildren.Data.Dtos.SchoolQueue;
using QueueForChildren.Services.School;

namespace QueueForChildren.Controllers
{
    [Authorize]
    public class SchoolQueueController : Controller
    {
        private readonly IRepository<SchoolLanguage> _schoolLanguageRepository;

        private readonly IRepository<School> _schoolRepository;

        private readonly IRepository<Address> _addressRepository;

        private readonly IRepository<Parent> _parentRepository;

        private readonly IRepository<Child> _childRepository;

        private readonly UserManager<User> _userManager;

        private readonly ISchoolListGetService _schoolListGetService;

        private readonly IAppendToQueueService _appendToQueueService;

        private readonly ISchoolQueueGetService _schoolQueueGetService;

        public SchoolQueueController(IRepository<SchoolLanguage> schoolLanguageRepository,
            IRepository<School> schoolRepository, IRepository<Address> addressRepository,
            IRepository<Parent> parentRepository, UserManager<User> userManager,
            ISchoolListGetService schoolListGetService, IAppendToQueueService appendToQueueService,
            IRepository<Child> childRepository, ISchoolQueueGetService schoolQueueGetService)
        {
            _schoolLanguageRepository = schoolLanguageRepository;
            _schoolRepository = schoolRepository;
            _addressRepository = addressRepository;
            _parentRepository = parentRepository;
            _userManager = userManager;
            _schoolListGetService = schoolListGetService;
            _appendToQueueService = appendToQueueService;
            _childRepository = childRepository;
            _schoolQueueGetService = schoolQueueGetService;
        }

        [HttpGet]
        public IActionResult FindSchool()
        {
            var regions = _schoolRepository.GetAll()
                .Include(school => school.Address)
                .Where(school => school.Address.MicroRegion != null)
                .Select(school => school.Address.MicroRegion)
                .Distinct()
                .ToArray();

            ViewBag.Regions = new SelectList(regions);
            ViewBag.Languages = new SelectList(_schoolLanguageRepository.GetAll()
                .Select(lang => lang.Name)
                .Distinct()
            );

            return PartialView("FindSchool");
        }

        public IActionResult SchoolList()
        {
            var schoolList = _schoolListGetService.SchoolList();

            return PartialView("SchoolList", new SchoolSelectDto()
            {
                Schools = schoolList
            });
        }

        [HttpPost]
        public async Task<IActionResult> FindSchool(SchoolFindDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            var list = _schoolListGetService.FindSchool(user, dto);

            var children = user.Parent.Children.Select(child => new
                {
                    child.Id,
                    Name = child.LastName + " " + child.Name[0] + ". " + child.MiddleName?[0] + ".",
                })
                .ToArray();

            ViewBag.Children = new SelectList(children, "Id", "Name");

            return PartialView("SelectSchool", new SchoolSelectDto()
            {
                Schools = list
            });
        }

        [HttpPost]
        public async Task<IActionResult> SendApplication([FromBody]SelectedDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            long.TryParse(dto.childId, out var _childId);
            long.TryParse(dto.schoolId, out var _schoolId);
            var child = _childRepository.GetById(_childId);
            var school = _schoolRepository.GetById(_schoolId);
            
            var msg = _appendToQueueService.AppendToQueue(user, child, school);

            return Json(new {success = true, data = msg});
        }
        
        public IActionResult SelectSchoolQueue()
        {
            var schoolList = _schoolRepository.GetAll()
                .Select(school => new
                {
                    school.Id,
                    school.Name
                });
            
            ViewBag.SchoolList = new SelectList(schoolList, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> QueueList(long schoolId)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.ParentId = user.ParentId;

            var list = _schoolQueueGetService.GetQueueList(schoolId);
            return PartialView("_queueList", list);
        }
    }
}