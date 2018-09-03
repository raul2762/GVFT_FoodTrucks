using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_GVFT.Business.BusinessEntities;
using System.Data.Entity;
using MessageBoxCustomRM;

namespace Data_GVFT.Business.BusinessLogic
{
   public class MerLogic
    {
        private static MerLogic instance;
        public static MerLogic GetInstance()
        {
            if (instance == null)
            {
                instance = new MerLogic();
            }
            return instance;
        }

        public void RegisterMerch(Merchandise merchandise)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                //en.SP_AddMerchandise(nameMerch, stock);
                en.Merchandise.Add(merchandise);
                en.SaveChanges();
            }
        }

        public void RegisterPofMerch(purchase_of_merchandise purchase, Merchandise merchandise)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                using (DbContextTransaction transaction = en.Database.BeginTransaction())
                {
                    try
                    {
                        var merch_bd = en.Merchandise.First(x => x.Id == merchandise.Id);
                        merch_bd.Stock = merchandise.Stock;
                        en.SaveChanges();

                        en.purchase_of_merchandise.Add(purchase);
                        en.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
                //en.SP_PurchaseMerchandise(detail, amount, datePurchase, qty, idMerch, idSuppl,idUser);

            }
        }
        public void RegisterSupplier(String nameSuppl, String addrs, String city,String phone)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                en.SP_AddSupplier(nameSuppl, addrs, city, phone);
            }
        }
        public void UpdateMerch(Merchandise merchandise_)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var merch_bd = en.Merchandise.FirstOrDefault(x => x.Id == merchandise_.Id);
                en.Entry(merch_bd).State = EntityState.Modified;
                en.Entry(merch_bd).CurrentValues.SetValues(merchandise_);
                en.SaveChanges();
            }
        }

        public class GetMerchandise
        {
            public string Name { get; set; }
        }
        public List<GetMerchandise> GetMerchandiseList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Merchandise
                            select new GetMerchandise() { Name = p.Name };
                return query.ToList();
            }
        }

        public class GetSupplier
        {
            public string Name { get; set; }
        }
        public List<GetSupplier> GetSupplierList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Supplier
                            select new GetSupplier() { Name = p.Supplier1 };
                return query.ToList();
            }
        }
        public int GetIdSuppl(Supplier supplName)
        {
            int id = 0;
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Supplier.FirstOrDefault(s => s.Supplier1 == supplName.Supplier1);
                if (query != null)
                {
                    id = query.Id;
                }
                return id;
            }
        }

        public class GetLocation
        {
            public string Name { get; set; }
        }
        public List<GetLocation> GetLocationsList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Location
                            select new GetLocation() { Name = p.nameLocation };
                return query.ToList();
            }
        }

        public class GetMerch
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Stock { get; set; }
        }
        public List<GetMerch> GerMerchList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Merchandise
                            select new GetMerch() { Id = p.Id, Name = p.Name, Stock = p.Stock };
                return query.ToList();
            }
        }
        public int GetMerchId(Merchandise nameMerch)
        {
            int id = 0;
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Merchandise.FirstOrDefault(m => m.Name == nameMerch.Name);
                if (query != null)
                {
                    id = query.Id;
                }
                return id;
            }
        }
    }
}
