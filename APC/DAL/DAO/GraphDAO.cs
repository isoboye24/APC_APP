using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class GraphDAO:APCContexts
    {
        //public string SelectAmountRaised(int year)
        //{           
        //    string monthlyDues = "SELECT PERSONAL_ATTENDANCE.monthID, SUM(PERSONAL_ATTENDANCE.monthlyDues)\r\n" +
        //    "FROM PERSONAL_ATTENDANCE\r\n" +
        //    "WHERE PERSONAL_ATTENDANCE.year = @year AND PERSONAL_ATTENDANCE.isDeleted = 0\r\n" +
        //    "GROUP BY PERSONAL_ATTENDANCE.monthID\r\n" +
        //    "ORDER BY PERSONAL_ATTENDANCE.monthID ASC";
        //    return monthlyDues.Replace("@year", year.ToString());
        //}        
    }
}
