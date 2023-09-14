namespace FormsApp.Models
{
    public class ProductViewModel
    {
        public List<Product> Products{set;get;} = null!;
        public List<Category> Categories{set;get;} = null!;

        public string? SelectedCategory{set;get;}
    }
}