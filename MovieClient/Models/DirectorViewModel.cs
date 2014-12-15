﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace MovieClient.Models
{
    public class DirectorViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
