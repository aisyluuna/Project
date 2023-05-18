using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueueForChildren.Data.Dtos;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Identity;
using QueueForChildren.Web.Extensions;
using QueueForChildren.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueForChildren.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IRepository<Parent> _parentRepository;

        private readonly IRepository<Child> _childRepository;

        private readonly IRepository<Address> _addressRepository;

        private readonly UserManager<User> _userManager;

        public AccountController(IRepository<Parent> parentRepository, IRepository<Child> childRepository, UserManager<User> userManager, IRepository<Address> addressRepository)
        {
            _parentRepository = parentRepository;
            _childRepository = childRepository;
            _userManager = userManager;
            _addressRepository = addressRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Account");
        }
        
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return RedirectToAction("Login", "User", returnUrl);
        }

        [Authorize]
        public async Task<IActionResult> PersonalData()
        {
            var user = await _userManager.GetUserAsync(User);

            CreateParentDto dto = new CreateParentDto();

            if (!user.IsParentNull())
            {
                var parent = _parentRepository
                    .GetAll()
                    .Where(p => p.Id == user.ParentId)
                    .Include(p => p.Address)
                    .SingleOrDefault();

                dto.Address = new AddressDto
                {
                    MicroRegion = parent.Address.MicroRegion,
                    Street = parent.Address.Street,
                    HouseNumber = parent.Address.HouseNumber,
                    AdditionalChar = parent.Address.AdditionalChar,
                    Settlement = parent.Address.Settlement
                };

                dto.BirthDate = parent.BirthDate;
                dto.Serial = parent.Serial;
                dto.Number = parent.Number;
                dto.IssuedBy = parent.IssuedBy;
                dto.IssuedDate = parent.IssuedDate;
                dto.SubdivisionCode = parent.SubdivisionCode;
                dto.Phone = parent.Phone;
                dto.INN = parent.INN;
                dto.Name = parent.Name;
                dto.LastName = parent.LastName;
                dto.MiddleName = parent.MiddleName;
            }

            return PartialView("PersonalData", dto);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateParent(CreateParentDto parentDto)
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (!ModelState.IsValid)
            {
                return PartialView("PersonalData", parentDto);
            }

            var parent = user.Parent;
            var address = parent.Address;

            parent.LastName = parentDto.LastName;
            parent.Name = parentDto.Name;
            parent.MiddleName = parentDto.MiddleName;
            parent.BirthDate = parentDto.BirthDate;
            parent.Serial = parentDto.Serial;
            parent.Number = parentDto.Number;
            parent.IssuedBy = parentDto.IssuedBy;
            parent.Phone = parentDto.Phone;
            parent.SubdivisionCode = parentDto.SubdivisionCode;
            parent.INN = parentDto.INN;

            address.Settlement = parentDto.Address.Settlement;
            address.MicroRegion = parentDto.Address.MicroRegion;
            address.Street = parentDto.Address.Street;
            address.HouseNumber = parentDto.Address.HouseNumber;
            address.AdditionalChar = parentDto.Address.AdditionalChar;
            
            _parentRepository.Update(new [] { parent });
            _addressRepository.Update(new [] { address });

            return PartialView("PersonalData", parentDto);
        }

        public async Task<IActionResult> Children()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var parent = _parentRepository.GetById(currentUser.ParentId.Value);
            if(parent is null)
            {
                return PartialView("Children");
            }

            var children = _childRepository.GetAll()
                .Where(child => child.ParentId == parent.Id)
                .Select(child => new ChildDto
                {
                    Id = child.Id,
                    Name = child.Name,
                    MiddleName = child.MiddleName,
                    LastName = child.LastName,
                    BirthDate = child.BirthDate
                })
                .ToArray();

            return PartialView("Children", children);
        }       
    }
}
