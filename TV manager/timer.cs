using System;

namespace TV_manager
{
    class timer
    {
        public static string Main
        {
            get
            {
                DateTime date1 = new DateTime();
                date1 = DateTime.Now;
                string date2 = date1.ToString();
                return date2;
            }
        }
    }
}
