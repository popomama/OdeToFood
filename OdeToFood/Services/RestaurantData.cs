using OdeToFood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Services
{

    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant restData);
    }

    public class SqlRestaurantData: IRestaurantData
    {
        private OdeToFoodDbContext _context;

        public SqlRestaurantData( OdeToFoodDbContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant restData)
        {
            _context.Add(restData);
            _context.SaveChanges();
            return restData;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        private static List<Restaurant> restaurants;

        static InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant{Id =1,Name="The House of Good"},
                new Restaurant{Id =2,Name="The House of Good 2"},
                new Restaurant{Id =3,Name="The House of Good 3"}
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(newRestaurant);

            return newRestaurant;
        }

        public Restaurant Get(int id)
        {
            return restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants;
        }
    }
}
