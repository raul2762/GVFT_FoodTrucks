using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_GVFT.Business.BusinessEntities;
using System.Data.Entity;

namespace Data_GVFT.Business.BusinessLogic
{
   public class LoginBL
    {
        public int IdUser { get; set; }
        private static LoginBL instance;
        public static LoginBL GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginBL();
            }
            return instance;
        }
        public void RegisterEmployee(Employees employees)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                en.Employees.Add(employees);
                en.SaveChanges();
            }
        }

        public void RegisterUserLogin(Login_FT objuser)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                en.Login_FT.Add(objuser);
                en.SaveChanges();
            }
        }

        public void UpdateUser(Login_FT login_)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var userLogin_bd = en.Login_FT.FirstOrDefault(x => x.Id == login_.Id);
                en.Entry(userLogin_bd).State = EntityState.Modified;
                en.Entry(userLogin_bd).CurrentValues.SetValues(login_);
                en.SaveChanges();
            }
        }

        public void UpdateEmployee(Employees employees_)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var employee_bd = en.Employees.FirstOrDefault(x => x.Id == employees_.Id);
                en.Entry(employee_bd).State = EntityState.Modified;
                en.Entry(employee_bd).CurrentValues.SetValues(employees_);
                en.SaveChanges();
            }
        }
        public bool checkUsername(Login_FT userName)
        {
            bool userExist = false;
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Login_FT.FirstOrDefault(x => x.Login_name == userName.Login_name);
                if (query != null)
                {
                    if (userName.Login_name == query.Login_name)
                    {
                        userExist = true;
                    }
                    else
                    {
                        userExist = false;
                    }
                }
                return userExist;
            }
        }
        public class getPass
        {
            public string pass { get; set; }
        }
        public bool checkPassword(Login_FT loginPss)
        {
            bool passCorrect = false;
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var pss = new getPass();
                var query = en.Login_FT.FirstOrDefault(x => x.Login_name == loginPss.Login_name);
                if (loginPss.Login_pass == query.Login_pass)
                {
                    IdUser = query.Id;
                    passCorrect = true;
                }
                return passCorrect;
            }
        }
        public bool chackIsActive(Login_FT login_FT)
        {
            bool isActive = false;
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Login_FT.FirstOrDefault(x => x.Login_name == login_FT.Login_name);
                if (query.IsActive == 1)
                {
                    isActive = true;
                }
                return isActive;
            }
        }
        public class GetUsername
        {
            public string userName { get; set; }
        }
        public List<GetUsername> GetUserList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Login_FT
                            select new GetUsername() { userName = p.Login_name};

                return query.ToList();
            }
        }
        public class GetEmployeeName
        {
            public string Name { get; set; }
            public int IdEmployee { get; set; }
        }
        public List<Employees> GetEmployeeList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Employees
                            select p;
                return query.ToList();
            }
        }

        public class GetRoles
        {
            public string Roles { get; set; }
        }
        public List<GetRoles> GetRoleList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Roles_Account
                            select new GetRoles() { Roles = p.Role_name };
                return query.ToList();
            }
        }

        public class GetDepartments
        {
            public string  DepartName { get; set; }
        }
        public List<GetDepartments> GetDepartmentsList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Departments
                            select new GetDepartments() { DepartName = p.Depart_name };
                return query.ToList();
            }
        }

        public class GetPaysheetMode
        {
            public string PaysheetModeName { get; set; }
        }
        public List<GetPaysheetMode> GetPaysheetModeList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.PaysheetModePay
                            select new GetPaysheetMode() { PaysheetModeName = p.PayMode };
                return query.ToList();
            }
        }

        public class GetStatusAcc
        {
            public string StatusName { get; set; }
        }
        public List<GetStatusAcc> GetStatusAccList()
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = from p in en.Status_Account
                            select new GetStatusAcc() { StatusName = p.Status };
                return query.ToList();
            }
        }

        public Login_FT GetLoginsDetail(Login_FT login_FT)
        {
            using (var en = new DB_SystemFoodTrucksEntities())
            {
                var query = en.Login_FT.FirstOrDefault(x => x.Id == login_FT.Id);

                return query;
            }
        }
    }
}
