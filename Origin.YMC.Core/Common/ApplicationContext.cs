using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Origin.YMC.Core.Common
{
    public class ApplicationContext
    {
        public static bool IsArabic => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() == "AR";
        public static string CurrentLanguage => Thread.CurrentThread.CurrentCulture.Name;
        public static bool IsStarted { get; set; }

        public static string AdminHostUrl => System.Configuration.ConfigurationManager.AppSettings["adminHostUrl"];
    }
}
