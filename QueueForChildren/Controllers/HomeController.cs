using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QueueForChildren.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QueueForChildren.Data.Entities;
using QueueForChildren.Web.Interfaces;

namespace QueueForChildren.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRepository<Address> _addressRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<Address> addressRepository)
        {
            _logger = logger;
            _addressRepository = addressRepository;
        }

        public IActionResult Index()
        {
            var addresses = _addressRepository.GetAll()
                .ToArray();
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
