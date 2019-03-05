using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Business
{
    public class Base
    {

        public static string GetPersianDate(DateTime InputDateTime)
        {
            PersianCalendar Pcal = new PersianCalendar();

            string PDay = (Pcal.GetDayOfMonth(InputDateTime).ToString());
            if (PDay.Length == 1)
                PDay = "0" + PDay;
            string PMonth = (Pcal.GetMonth(InputDateTime).ToString());
            if (PMonth.Length == 1)
                PMonth = "0" + PMonth;
            string PYear = (Pcal.GetYear(InputDateTime).ToString());
            return (PYear + "/" + PMonth + "/" + PDay);
        }


        public static DateTime GetMiladiDate(string InputDateTime)
        {
            DateTime dt = DateTime.Now;
            try
            {
                PersianCalendar pc = new PersianCalendar();
                string[] date = InputDateTime.Split("/");
                
                dt = pc.ToDateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2]), 0, 0, 0, 0);
                
            }
            catch (Exception)
            {

              //  throw;
            }

            return dt;

        }
    }
}
