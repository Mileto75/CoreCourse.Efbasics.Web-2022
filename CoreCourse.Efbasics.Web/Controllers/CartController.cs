using CoreCourse.Efbasics.Web.Data;
using CoreCourse.Efbasics.Web.Models;
using CoreCourse.Efbasics.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoreCourse.Efbasics.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly SchoolDbContext _schoolDbContext;

        public CartController(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        public IActionResult Index()
        {
            CartIndexViewModel cartIndexViewModel = new();
            cartIndexViewModel.Items = new();
            //check if session exists
            //and get the data from the sessio
            if (HttpContext.Session.Keys.Contains("ShoppingCart"))
            {
                //get the serialized viewmodel from the session
                var serializedCartIndexViewModel = HttpContext
                    .Session.GetString("ShoppingCart");
                cartIndexViewModel =
                    JsonConvert
                    .DeserializeObject<CartIndexViewModel>(serializedCartIndexViewModel);
            }
            //calculate total
            cartIndexViewModel.Total = cartIndexViewModel.Items
                .Sum(i => i.Price);
            //pass to view
            return View(cartIndexViewModel);
        }
        public async Task<IActionResult> Add(int id) 
        {
            //get the course and check if exists
            var course = await _schoolDbContext
                .Courses
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                return RedirectToAction("Index","Courses");
            }
            //course exists
            CartIndexViewModel cartIndexViewModel = new();
            cartIndexViewModel.Items = new();
            string serializedCartIndexViewModel = "";
            //check if session exists
            if(HttpContext.Session.Keys.Contains("ShoppingCart"))
            {
                //get the serialized viewmodel from the session
                serializedCartIndexViewModel = HttpContext
                    .Session.GetString("ShoppingCart");
                cartIndexViewModel =
                    JsonConvert
                    .DeserializeObject<CartIndexViewModel>(serializedCartIndexViewModel);
            }

            //add product to viewmodel list
            //check if item already in shoppingCart
            if(cartIndexViewModel.Items.Any(c => c.Id == course.Id)) 
            {
                //increment quantity 
                var item = cartIndexViewModel
                    .Items
                    .FirstOrDefault(i => i.Id == course.Id);
                item.Quantity++;
                item.Price += course.Price;
            }
            else
            {
                //add new item
                cartIndexViewModel.Items
                .Add(new CartItemViewModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    Quantity = 1,
                    Price = course.Price
                }
                );
            }
            //serialize viewmodel
            serializedCartIndexViewModel = 
                JsonConvert.SerializeObject( cartIndexViewModel );
            //add to session => overwrite session with new shoppingCart
            HttpContext.Session.SetString("ShoppingCart", serializedCartIndexViewModel);
            //update counter
            HttpContext.Session.SetInt32("CartCounter", cartIndexViewModel
                .Items.Sum(i => i.Quantity));
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id) 
        {
            //check if course exists
            //get the course and check if exists
            //may be redundant check
            var course = await _schoolDbContext
                .Courses
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                return RedirectToAction("Index", "Courses");
            }
            //course exists
            CartIndexViewModel cartIndexViewModel = new();
            cartIndexViewModel.Items = new();
            string serializedCartIndexViewModel = "";
            //check if session exists
            if (!HttpContext.Session.Keys.Contains("ShoppingCart"))
            {
                return RedirectToAction("Index", "Courses");
            }
            //get the serialized viewmodel from the session
            serializedCartIndexViewModel = HttpContext
                .Session.GetString("ShoppingCart");
            cartIndexViewModel =
                JsonConvert
                .DeserializeObject<CartIndexViewModel>(serializedCartIndexViewModel);
            //get the item
            var item = cartIndexViewModel.Items.FirstOrDefault(i => i.Id == id);
            //check if exists in shoppingCart
            if(item == null ) 
            {
                RedirectToAction("Index", "Courses");
            }
            //remove item
            //check for quantity
            if(item.Quantity > 1)
            {
                item.Quantity--;
                item.Price = (course.Price * item.Quantity);
            }
            else
            {
                //remove from list
                cartIndexViewModel.Items.Remove(item);
            }
            //update session
            //serialize viewmodel
            serializedCartIndexViewModel =
                JsonConvert.SerializeObject(cartIndexViewModel);
            //add to session => overwrite session with new shoppingCart
            HttpContext.Session.SetString("ShoppingCart", serializedCartIndexViewModel);
            //update counter
            HttpContext.Session.SetInt32("CartCounter", cartIndexViewModel
                .Items.Sum(i => i.Quantity));
            return RedirectToAction("Index");
        }
    }
}
