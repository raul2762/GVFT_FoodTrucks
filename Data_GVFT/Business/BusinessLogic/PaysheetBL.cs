using Data_GVFT.Business.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBoxCustomRM;
using System.Data.Entity.Validation;

namespace Data_GVFT.Business.BusinessLogic
{
   public class PaysheetBL
    {
        private static PaysheetBL instance;
        public static PaysheetBL GetInstance()
        {
            if (instance == null)
            {
                instance = new PaysheetBL();
            }
            return instance;
        }

        public void PaymentPayroll(Paysheet paysheet)
        {
            using (var en  = new DB_SystemFoodTrucksEntities())
            {
                en.Paysheet.Add(paysheet);
                en.SaveChanges();
            }
        }

        public void PayrollWithLoan(Paysheet paysheet, Mov_CxC_Employees mov_CxC)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                using (DbContextTransaction transaction = en.Database.BeginTransaction())
                {
                    try
                    {
                        //var payment_bd = new Paysheet()
                        //{
                        //    Amount = paysheet.Amount,
                        //    Payment_date = paysheet.Payment_date,
                        //    Id_employee = paysheet.Id_employee,
                        //    Note = paysheet.Note
                        //};
                        en.Paysheet.Add(paysheet);
                        en.SaveChanges();

                        var updateloan_bd = en.Mov_CxC_Employees.First(m => m.Id_employee == mov_CxC.Id_employee && m.Transaction_type == 1);
                        updateloan_bd.Amount_rest = mov_CxC.Amount_rest;
                        updateloan_bd.FeeRest = mov_CxC.FeeRest;
                        en.SaveChanges();

                        var payloan = new Mov_CxC_Employees()
                        {
                        Id_employee = mov_CxC.Id_employee,
                        Amount_charged = mov_CxC.Amount_charged,
                        Transaction_date = mov_CxC.Transaction_date,
                        Transaction_type = mov_CxC.Transaction_type,
                        Loan_Amount = 0,
                        Fee_Amount = 0,
                        Fee = 0,
                        FeeRest = 0,
                        Amount_rest = 0
                        };
                        en.Mov_CxC_Employees.Add(payloan);
                        en.SaveChanges();
                        

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxRM.Show(ex.Message);
                        transaction.Rollback();
                    }
                }
            }
        }

        public void RegisterLoan(Mov_CxC_Employees mov_CxC)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                en.Mov_CxC_Employees.Add(mov_CxC);
                en.SaveChanges(); 
            }
        }

        public int? GetFee(int idEmployee)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Mov_CxC_Employees.FirstOrDefault(p => p.Id_employee == idEmployee);
                int? fee = 0;
                if (query != null)
                {
                    fee = query.Fee_Amount;
                }
                return fee;
            }
        }

        public Mov_CxC_Employees GetMov(int idEmployee)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Mov_CxC_Employees.FirstOrDefault(m => m.Id_employee == idEmployee && m.Transaction_type == 1);

                return query;
            }
        }
    }
}
