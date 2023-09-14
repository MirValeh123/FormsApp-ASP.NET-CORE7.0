namespace FormsApp.Models
{
    public class Repository
    {

        private static readonly List<Product> _products = new();


        private static readonly List<Category> _categories = new();

        static Repository()
        {
            _categories.Add(new Category { CategoryId = 1, Name = "Telefon" });
            _categories.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });

            _products.Add(new Product { ProductId = 1, Name = "Iphone 14", Price = 4000, IsActive = true, Image = "1.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 2, Name = "ASUS TUF Gaming F17", Price = 4000, IsActive = true, Image = "2.jpg", CategoryId = 2 });
            _products.Add(new Product { ProductId = 3, Name = "Iphone 14 Pro", Price = 4300, IsActive = false, Image = "3.webp", CategoryId = 1 });
            _products.Add(new Product { ProductId = 4, Name = "Iphone 14 Pro Max", Price = 5000, IsActive = true, Image = "4.jpeg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 5, Name = "Macbook Air", Price = 7000, IsActive = false, Image = "5.webp", CategoryId = 2 });
            _products.Add(new Product { ProductId = 6, Name = "Macbook Pro", Price = 8000, IsActive = true, Image = "6.jpeg", CategoryId = 2 });
        }
        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }
        public static void CreateProduct(Product entity)
        {
            _products.Add(entity);
        }
        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
        public static void UpdateProduct(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
            if (entity != null)
            {
                if (string.IsNullOrEmpty(updatedProduct.Name))
                {
                    entity.Name = updatedProduct.Name;

                }
                entity.Price = updatedProduct.Price;
                entity.Image = updatedProduct.Image;
                entity.CategoryId = updatedProduct.CategoryId;
                entity.IsActive = updatedProduct.IsActive;

            }
        }
        public static void EditIsActive(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
            if (entity != null)
            {
               
                entity.IsActive = updatedProduct.IsActive;

            }
        }

        public static void DeleteProduct(Product deletedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == deletedProduct.ProductId);
            if (entity != null)
            {
                _products.Remove(entity);
            }

        }


    }
}