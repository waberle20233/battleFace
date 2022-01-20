using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleFaceDataAccess.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public double Amount { get; set; }
    }
}
