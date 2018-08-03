using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data_GVFT.Business.BusinessLogic;

namespace GVFT_FoodTrucksTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var CountEF = LoginBL.GetInstance().GetDepartmentsList().Count;
            Assert.IsNotNull(CountEF);

            var CountEF2 = LoginBL.GetInstance().GetRoleList().Count;
            Assert.IsNotNull(CountEF2);

            var CountEF3 = LoginBL.GetInstance().GetPaysheetModeList().Count;
            Assert.IsNotNull(CountEF3);

            var CountEF4 = LoginBL.GetInstance().GetStatusAccList().Count;
            Assert.IsNotNull(CountEF4);
        }
    }
}
