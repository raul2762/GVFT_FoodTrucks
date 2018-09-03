using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_GVFT.Business.BusinessEntities;
using MessageBoxCustomRM;

namespace Data_GVFT.Business.BusinessLogic
{
   public class CreditBL
    {
        private static CreditBL instance;
        public static CreditBL GetInstance()
        {
            if (instance == null)
            {
                instance = new CreditBL();
            }
            return instance;
        }

        public List<Expiry_of_mode> GetTypeCredit()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from t in en.Expiry_of_mode
                            select t;
                return query.ToList();
            }
        }

        public void RegisterCreditCode(Credits credits)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                try
                {
                    en.Credits.Add(credits);
                    en.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessage = "";
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            errorMessage += "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage;
                        }
                    }
                    MessageBoxRM.Show(errorMessage);
                }
                
            }
        }

        public int GetIdTypeCredit(Expiry_of_mode  expiry)
        {
            int id = 0;
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Expiry_of_mode.FirstOrDefault(t => t.ModeName == expiry.ModeName);
                if (query.Id != 0)
                {
                    id = query.Id;
                }
                return id;
            }
        }
    }
}
