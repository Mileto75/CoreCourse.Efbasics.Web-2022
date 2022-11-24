using CoreCourse.Efbasics.Web.Models;

namespace CoreCourse.Efbasics.Web.ViewModels
{
    public class CartIndexViewModel
    {
        public List<CartItemViewModel> Items { get; set; }
        public decimal Total { get; set; }
    }
}
