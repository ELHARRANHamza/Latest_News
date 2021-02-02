using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models
{
    public class Categories
    {
        public int id { get; set; }
        [Required]
        [StringLength(150,MinimumLength =3)]
        public string cat_Nom { get; set; }
        public string img { get; set; }
        public IList<Latest_News> News { get; set; }
    }
}
