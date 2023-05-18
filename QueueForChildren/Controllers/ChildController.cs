using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QueueForChildren.Data.Dtos;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Identity;
using QueueForChildren.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueForChildren.Controllers
{
    [Authorize]
    public class ChildController : Controller
    {
        private readonly IRepository<Parent> _parentRepository;

        private readonly IRepository<Child> _childRepository;

        private readonly IRepository<Address> _addressRepository;

        private readonly UserManager<User> _userManager;

        public ChildController(IRepository<Child> childRepository, IRepository<Address> addressRepository, UserManager<User> userManager, IRepository<Parent> parentRepository)
        {
            _childRepository = childRepository;
            _addressRepository = addressRepository;
            _userManager = userManager;
            _parentRepository = parentRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChildDto childDto)
        {
            var parent = await GetParentAsync();
            var address = _addressRepository.GetById(parent.AddressId);

            var child = new Child
            {
                Name = childDto.Name,
                LastName = childDto.LastName,
                MiddleName = childDto.MiddleName,
                BirthDate = childDto.BirthDate,
                Serial = childDto.Serial,
                Number = childDto.Number,
                IssuedBy = childDto.IssuedBy,
                IssuedDate = childDto.IssuedDate,
                ActNumber = childDto.ActNumber,
                AddressId = address.Id,
                ParentId = parent.Id
            };

            _childRepository.Create(new[] { child });

            return Json(new { success = true });
        }

        private async Task<Parent> GetParentAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var parent = _parentRepository.GetById(currentUser.ParentId.Value);

            return parent;
        }
    }
}
