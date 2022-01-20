using battleFaceJWTAuth.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace battleFaceJWTAuth.Models
{
    public class QuotationViewModel
    {
        [Display(Name = "Ages")]
        [Required(ErrorMessage ="At least 1 age is required.")]
        public string age { get; set; }
        [Display(Name = "Currency")]
        [Required(ErrorMessage = "Currency code is Required.")]
        public string currency_id { get; set; }
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date is Required.")]
        public string start_date { get; set; }
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End date is Required.")]
        public string end_date { get; set; }
        [Display(Name = "Currency Codes")]
        public List<string> CurrencyCodes { get; set; }


        private List<int> _ages;
        private double _duration;

        public List<string> Validate()
        {
            List<string> errors = new List<string>();

            _ages = StringHelper.ValidateAges(age, errors);
           
            List<string> currencySymbols = StringHelper.GetCurrencyCodes();

            if(!currencySymbols.Any(oo => oo == currency_id))
            {
                errors.Add("Please use a valid currency Id in ISO 4217 format.");
            }

            _duration = StringHelper.ValidateDays(start_date, end_date);
            if(_duration < 1)
            {
                errors.Add("Please ensure you have proper start and end dates.");
            }

            return errors;
        }

        public QuoteResult GetResult()
        {
            QuoteResult result = new QuoteResult();
            foreach(int age in _ages)
            {
                double load = StringHelper.GetAgeLoad(age);
                result.total += StringHelper.FIXED_RATE * _duration * load;
            }

            result.total = Math.Round(result.total, 2);

            return result;
        }
    }
}
