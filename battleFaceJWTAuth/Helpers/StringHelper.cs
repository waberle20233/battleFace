using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace battleFaceJWTAuth.Helpers
{
    public class StringHelper
    {
        public const int FIXED_RATE = 3;
        public static Dictionary<string, double> AgeLoadTable = new Dictionary<string, double> {
            { "18-30", .6 },
            { "31-40", .7 },
            { "41-50", .8 },
            { "51-60", .9 },
            { "61-70", 1 },
        };

        public static List<int> ValidateAges(string agesStr, List<string> errors)
        {
            
            string[] ages = agesStr.Split(",");
            if(ages.Length == 0)
            {
                errors.Add("You must enter an age.");
            }
            List<int> result = new List<int>();

            foreach (string age in ages)
            {
                int tmpAge;
                if (int.TryParse(age.Trim(), out tmpAge))
                {
                    if (tmpAge != 0)
                    {
                        result.Add(tmpAge);
                    }
                    else
                    {
                        errors.Add("0 is not a Valid Age.");
                    }
                    
                }
            }

            return result;
        }
        

        public static double ValidateDays(string startDate, string endDate)
        {
            
            DateTime start;
            DateTime end;
            DateTime.TryParse(startDate, out start);
            DateTime.TryParse(endDate, out end);

            return end.Subtract(start).TotalDays + 1;
        }

        public static double GetAgeLoad(int age)
        {
            if(age > 17 && age < 31)
            {
                return AgeLoadTable["18-30"];
            }
            else if (age > 30 && age < 41)
            {
                return AgeLoadTable["31-40"];
            }
            else if (age > 40 && age < 51)
            {
                return AgeLoadTable["41-50"];
            }
            else if (age > 50 && age < 61)
            {
                return AgeLoadTable["51-60"];
            }
            else if (age > 60 && age < 71)
            {
                return AgeLoadTable["61-70"];
            }
            else
            {
                return 0;
            }
        }

        public static List<string> GetCurrencyCodes()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures) 
                .Select(x => (new RegionInfo(x.LCID)).ISOCurrencySymbol)
                .Distinct()
                .OrderBy(x => x).ToList();
        }
    }
}
