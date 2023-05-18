using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueueForChildren.Data.Dtos;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Identity;
using QueueForChildren.Web.Extensions;
using QueueForChildren.Web.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace QueueForChildren.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Parent> _parentRepository;

        public UserController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            IRepository<Address> addressRepository,
            IRepository<Parent> parentRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _addressRepository = addressRepository;
            _parentRepository = parentRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginDto {ReturnUrl = returnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Login, loginDto.Password, false, false);
            if (result.Succeeded)
            {
                // проверяем, принадлежит ли URL приложению
                if (!string.IsNullOrEmpty(loginDto.ReturnUrl) && Url.IsLocalUrl(loginDto.ReturnUrl))
                {
                    return Redirect(loginDto.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }

            return View(loginDto);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = registerDto.EMail,
                    UserName = registerDto.EMail,
                    Name = registerDto.Name,
                    LastName = registerDto.LastName,
                    MiddleName = registerDto.MiddleName
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("CreateParent");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(registerDto);
        }

        [HttpGet]
        public async Task<IActionResult> CreateParent()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.IsParentNull())
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateParent(CreateParentDto parentDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.IsParentNull())
            {
                return RedirectToAction("Index", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(parentDto);
            }

            var latitudeStr = parentDto.Address.Latitude?.Replace('.', ',') ?? string.Empty;
            var longitudeStr = parentDto.Address.Longitude?.Replace('.', ',') ?? string.Empty;

            var address = new Address
            {
                Latitude = double.TryParse(latitudeStr, out var latitude) ? latitude : null,
                Longitude = double.TryParse(longitudeStr, out var longitude) ? longitude : null,
                RegionType = Data.Enums.RegionType.Republic,
                RegionName = "Татарстан",
                SettlementType = Data.Enums.SettlementType.City,
                Settlement = parentDto.Address.Settlement,
                MicroRegion = parentDto.Address.MicroRegion,
                Street = parentDto.Address.Street,
                HouseNumber = parentDto.Address.HouseNumber,
                AdditionalChar = parentDto.Address.AdditionalChar
            };


            _addressRepository.Create(new[] {address});


            var parent = new Parent
            {
                Name = user.Name,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                BirthDate = parentDto.BirthDate,

                Serial = parentDto.Serial,
                Number = parentDto.Number,
                Address = address,
                IssuedBy = parentDto.IssuedBy,
                IssuedDate = parentDto.IssuedDate,
                Phone = parentDto.Phone,
                EMail = user.Email,
                SubdivisionCode = parentDto.SubdivisionCode,
                INN = parentDto.INN
            };

            _parentRepository.Create(new[] {parent});

            user.Parent = parent;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Account");
        }
    }
}