using Origin.YMC.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Core.Content
{

    public class KeyHashMap
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }

    public static class StaticContent
    {
        public static List<KeyHashMap> GetAllSpecialties()
        {
            return new List<KeyHashMap>() {
                new KeyHashMap{ Name="Allergy and Immunology",Value=1},
                new KeyHashMap{ Name="Cardiology",Value=2},
                new KeyHashMap{ Name="Adult Congenital Heart disease",Value=3},
                new KeyHashMap{ Name="Interventional Cardiology",Value=4},
                new KeyHashMap{ Name="Electrophysiology",Value=5},
                new KeyHashMap{ Name="Dermatology",Value=6},
                new KeyHashMap{ Name="Endocrinology",Value=7},
                new KeyHashMap{ Name="Hematology / Oncology",Value=8},
                new KeyHashMap{ Name="Infectious Disease",Value=9},
                new KeyHashMap{ Name="Nephrology",Value=10},
                new KeyHashMap{ Name="Neurosurgery",Value=11},
                new KeyHashMap{ Name="Neurology",Value=12},
                new KeyHashMap{ Name="Ophthalmology: Eye Center",Value=13},
                new KeyHashMap{ Name="Orthopaedic Surgery &Sports Medicine",Value=14},
                new KeyHashMap{ Name="Pulmonology & Critical Care",Value=15},
                new KeyHashMap{ Name="Rheumatology",Value=16},
                new KeyHashMap{ Name="Urology",Value=17},
            };
        }
        public static List<KeyHashMap> GetAllGenders()
        {
            if (!ApplicationContext.IsArabic)
            {
                return new List<KeyHashMap>(){
                      new KeyHashMap(){Name="Male",Value=1 },
                      new KeyHashMap(){Name="Female",Value=2 },
                 };
            }
            else
            {
                return new List<KeyHashMap>(){
                      new KeyHashMap(){Name="ذكر",Value=1 },
                      new KeyHashMap(){Name="أنثى",Value=2 },
                 };
            }
        }
        public static List<KeyHashMap> GetAllHeardAboutUs()
        {
            if (!ApplicationContext.IsArabic)
            {
                return new List<KeyHashMap>{
                     new KeyHashMap(){Name="Social media",Value=1 },
                     new KeyHashMap(){Name="Media",Value=2 },
                     new KeyHashMap(){Name="Google Ads",Value=3 },
                     new KeyHashMap(){Name="Friends",Value=4 },

                };
            }
            else
            {
                return new List<KeyHashMap>{
                     new KeyHashMap(){Name="وسائل التواصل الاجتماعي",Value=1 },
                     new KeyHashMap(){Name="وسائل الإعلام",Value=2 },
                     new KeyHashMap(){Name="اعلانات جوجل",Value=3 },
                     new KeyHashMap(){Name="اصدقاء",Value=4 },
                };
            }
        }
        public static List<KeyHashMap> GetAllSecurityQuestions()
        {
            if (!ApplicationContext.IsArabic)
            {
                return new List<KeyHashMap>{
                      new KeyHashMap(){Name="What is the first name of your best friend in high school?",Value=1 },
                      new KeyHashMap(){Name="What was the name of your first pet?",Value=2 },
                      new KeyHashMap(){Name="What was the first thing you learned to cook?",Value=3 },
                      new KeyHashMap(){Name="What was the first film you saw in the theater?",Value=4 },
                      new KeyHashMap(){Name="Where did you go the first time you flew on a plane?",Value=5 },
                      new KeyHashMap(){Name="What is the last name of your favorite elementary school teacher?",Value=6 },
               };
            }
            else
            {
                return new List<KeyHashMap>{
                      new KeyHashMap(){Name="ما هو الاسم الأول لأفضل صديق لك في المدرسة الثانوية؟",Value=1 },
                      new KeyHashMap(){Name="ماذا كان اسم أول حيوان أليف لديك؟",Value=2 },
                      new KeyHashMap(){Name="ما هو أول شيء تعلمته للطهي؟",Value=3 },
                      new KeyHashMap(){Name="ما هو أول فيلم شاهدته في المسرح؟",Value=4 },
                      new KeyHashMap(){Name="أين ذهبت في المرة الأولى التي سافرت فيها على متن طائرة؟",Value=5 },
                      new KeyHashMap(){Name="ما هو الاسم الأخير لمعلم المدرسة الابتدائية المفضل لديك؟",Value=6 },
               };
            }
        }

        public static List<KeyHashMap> GetAllCreditCardType()
        {

            return new List<KeyHashMap>{
                new KeyHashMap(){Name="Mastercard",Value=1 },
                new KeyHashMap(){Name="Ebay",Value=2 },
                new KeyHashMap(){Name="Visa",Value=3 },
                new KeyHashMap(){Name="Hsbc",Value=4 },
                new KeyHashMap(){Name="Paypal",Value=5 },
                new KeyHashMap(){Name="Amircan Express",Value=6 },
            };
        }
    }
}












































































































































































































































































































