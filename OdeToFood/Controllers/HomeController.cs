using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData restaurantData;

        public HomeController(IRestaurantData data)
        {
            restaurantData = data;

        }
        public IActionResult Index()
        {
            var model = restaurantData.GetAll(); //new Restaurant { Id = 1, Name = "The House of Good" };
            return View(model);
        }
    }
}
