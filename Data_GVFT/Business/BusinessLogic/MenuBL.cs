using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_GVFT.Business.BusinessEntities;
using ItemControl;

namespace Data_GVFT.Business.BusinessLogic
{
   public class MenuBL
    {
        private static MenuBL instance;
        public static MenuBL GetInstance()
        {
            if (instance == null)
            {
                instance = new MenuBL();
            }
            return instance;
        }

        public void RegisterProduct(String nameProduct, int price, int idCategory)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                en.SP_AddProduct(nameProduct, price, idCategory);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var product_bd = en.Product.FirstOrDefault(p => p.Id == product.Id);
                en.Entry(product_bd).State = EntityState.Modified;
                en.Entry(product_bd).CurrentValues.SetValues(product);
                en.SaveChanges();
            }
        }

        public class GetCategory
        {
            public string Name { get; set; }
        }
        public List<GetCategory> GetCategoryList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Category
                            select new GetCategory() { Name = p.Name };
                return query.ToList();
            }
        }

        public class GetProduct
        {
            public string Name { get; set; }
        }
        public List<GetProduct> GetProductList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Product
                            select new GetProduct() { Name = p.Name_product };
                return query.ToList();
            }
        }

        public Product GetProductDetail(Product product)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Product.FirstOrDefault(p => p.Id == product.Id);

                return query;
            }
        }

        public class Categorias
        {
            public string Nombre { get; set; }
        }
        public List<Categorias> GetCategorias()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Category
                            select new Categorias() { Nombre = p.Name };
               return query.ToList();
            }
        }

        public List<MenuItem_UC> GetMenuItems()
        {
            using (var en  = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Product
                            join c in en.Category on p.Id_category equals c.Id
                            select new MenuItem_UC() { Id = p.Id, Nombre = p.Name_product, Precio = (int)p.unitPrice, Categoria = c.Name };

                return query.ToList();
            }
        }
    }
}
