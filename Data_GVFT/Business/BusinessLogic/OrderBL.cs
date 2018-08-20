using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_GVFT.Business.BusinessEntities;

namespace Data_GVFT.Business.BusinessLogic
{
   public class OrderBL
    {
        private static OrderBL instance;
        public static OrderBL GetInstance()
        {
            if (instance == null)
            {
                instance = new OrderBL();
            }
            return instance;
        }

        public void RegisterPendingOrdr(Busy_tables _Tables, List<Pending_Orders> _Orders)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                using (DbContextTransaction transaction = en.Database.BeginTransaction())
                {
                    try
                    {
                        en.Busy_tables.Add(_Tables);
                        en.SaveChanges();
                        foreach (Pending_Orders itemOrder in _Orders)
                        {
                            en.Pending_Orders.Add(itemOrder);
                        }
                        en.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public void RegisterPendingOrdr2(List<Pending_Orders> _Orders)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                foreach (Pending_Orders itemOrder in _Orders)
                {
                    en.Pending_Orders.Add(itemOrder);
                }
                en.SaveChanges();
            }
        }

        public bool VerifyTable(Busy_tables tables)
        {
            bool isBusy = false;
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Busy_tables.FirstOrDefault(t => t.Table_busy == tables.Table_busy);
                if (query != null)
                {
                    isBusy = true;
                }
                return isBusy;
            }
        }

       public class DetailOrdrClass
        {
            public string NameProd { get; set; }
            public int? Qty { get; set; }
            public decimal? Price { get; set; }
        }
        public List<DetailOrdrClass> GetDetailOrder(int idTable)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from o in en.Pending_Orders
                            where idTable == o.Id_table
                            select new DetailOrdrClass() { NameProd = o.NameProduct, Qty = o.Qty, Price = o.unitPrice };

                return query.ToList();
            }
        }
        public int NofPOrder()
        {
            int cantOrder = 0;
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                cantOrder = en.Busy_tables.Count();
            }
            return cantOrder;
        }
    }
}
