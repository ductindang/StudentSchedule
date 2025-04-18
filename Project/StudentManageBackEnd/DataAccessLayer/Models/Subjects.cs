﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Subjects
    {
        [Key]
        public int Id {  get; set; }
        
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
