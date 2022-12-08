using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIPReference
{
    public class AppInfo
    {
        public static string OpeDate()
        {
            try
            {
                if (Properties.Settings.Default.opedate.Length == 0)
                {
                    return DateTime.Today.ToString("yyyyMMdd");
                }
                else
                {
                    return Properties.Settings.Default.opedate;
                }
            }
            catch
            {
                return "";
            }
        }

        public static Dictionary<string,string> Goki()
        {
            var terms = Properties.Settings.Default.OcrFoundationTerm.Split('|');
            var gokis = Properties.Settings.Default.OcrFoundationGoki.Split('|');

            if(terms.Length == 0 || terms.Length != gokis.Length)
            {
                return new Dictionary<string, string>();
            }

            Dictionary<string, string> ret = new Dictionary<string, string>();
            for(int i = 0; i < terms.Length; ++i)
            {
                ret.Add(terms[i], gokis[i]);
            }
            return ret;
        }
    }
}
