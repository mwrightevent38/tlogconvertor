using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Event38.ImageUtility
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] filepath)
        {
       
            
            IEnumerable<string> tlog;
            string csv;
            tlog = Helpers.ImageUtilityHelper.GetTLog(filepath[0]);
            var enumerator = tlog.GetEnumerator();
            enumerator.MoveNext();
            csv = Helpers.ImageUtilityHelper.tlogToCSV(enumerator.Current);
         }
    }
}
