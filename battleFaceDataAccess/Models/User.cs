﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleFaceDataAccess.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        
    }
}
