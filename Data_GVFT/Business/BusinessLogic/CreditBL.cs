using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_GVFT.Business.BusinessEntities;

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

        public List<Trans_type> GetTrans_Types()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from t in en.Trans_type
                            select t;
                return query.ToList();
            }
        }

        public void RegisterCreditCode(Credits credits)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                en.Credits.Add(credits);
                en.SaveChanges();
            }
        }

        public int GetIdTransType(Trans_type trans_Type)
        {
            int id = 0;
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Trans_type.FirstOrDefault(t => t.Type_trans == trans_Type.Type_trans);
                if (query.Id != 0)
                {
                    id = query.Id;
                }
                return id;
            }
        }
    }
}
