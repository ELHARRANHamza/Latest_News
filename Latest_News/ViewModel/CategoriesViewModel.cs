using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.ViewModel
{
    public class CategoriesViewModel
    {
        public int id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string cat_Nom { get; set; }
        public string img { get; set; }
        public IFormFile file { get; set; }
    }
}
