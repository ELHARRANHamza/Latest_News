using Latest_News.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.ViewModel
{
    public class NewsViewModels
    {
        public int id { get; set; }
        [DataType(DataType.Url)]
        public string url { get; set; }
        [Required]
        [StringLength(250,MinimumLength =4)]
        public string Titre { get; set; }
        [Required]
        [MinLength(8)]
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime date_Publiciter { get; set; }
        public IFormFile file { get; set; }
        public int id_Cat { get; set; }
        public IList<Categories> GetCategories { get; set; }
    }
}
