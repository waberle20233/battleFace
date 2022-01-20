using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleFaceDataAccess.Models
{
    public class User_Key
    {
        [Key]
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
