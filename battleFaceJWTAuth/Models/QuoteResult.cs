using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace battleFaceJWTAuth.Models
{
    public class QuoteResult
    {
        [DataType(DataType.Currency)]
        public double total { get; set; }
        public string currency_id { get; set; }
        public int quotation_id { get; set; }
    }
}
