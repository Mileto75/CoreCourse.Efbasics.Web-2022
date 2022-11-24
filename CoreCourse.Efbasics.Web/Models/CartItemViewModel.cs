using CoreCourse.Efbasics.Web.ViewModels;

namespace CoreCourse.Efbasics.Web.Models
{
    public class CartItemViewModel : BaseViewModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
