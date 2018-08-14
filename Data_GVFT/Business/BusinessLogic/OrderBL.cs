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

        public void RegisterPendingOrdr(Busy_tables _Tables, Pending_Orders _Orders)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                using (DbContextTransaction transaction = en.Database.BeginTransaction())
                {
                    try
                    {
                        en.Busy_tables.Add(_Tables);
                        en.SaveChanges();
                        en.Pending_Orders.Add(_Orders);
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
    }
}
