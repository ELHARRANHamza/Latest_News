using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.ViewModel
{
    public class Publiciter_ViewModel
    {
        public int id { get; set; }
        [Required]
        [StringLength(250,MinimumLength =4)]
        public string Titre { get; set; }
        public DateTime date_Publiciter { get; set; }
        public string img { get; set; }
        public IFormFile file { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name_Company { get; set; }
    }
}
