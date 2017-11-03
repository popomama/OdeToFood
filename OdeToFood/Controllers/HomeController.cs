using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;
using OdeToFood.ViewModels;


namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData restaurantData;
        private IGreeter greeter;

        //injection automatially , it is instantiated by the service registered
        public HomeController(IRestaurantData data, IGreeter currentGreeter)
        {
            restaurantData = data;
            greeter = currentGreeter;

        }
        public IActionResult Index()
        {
            // var model = restaurantData.GetAll(); //new Restaurant { Id = 1, Name = "The House of Good" };
            var model = new HomePageViewModel();
            model.Restaurants = restaurantData.GetAll();
            model.CurrentMessage = greeter.GetGreeting();
            return View(model);
        }


        public IActionResult  Details(int id)
        {
            var model = restaurantData.Get(id);

            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
    }

   
}
