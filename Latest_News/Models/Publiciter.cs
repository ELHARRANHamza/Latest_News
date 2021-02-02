using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models
{
    public class Publiciter
    {
        public int id { get; set; }
        public string Titre { get; set; }
        public DateTime date_Publiciter { get; set; }
        public string img { get; set; }
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string Name_Company { get; set; }
    }
}
