using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models
{
    public class Latest_News
    {
        public int id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 4)]
        public string Titre { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        public string Image { get; set; }
        [DataType(DataType.Url)]
        public string lien_Ytb { get; set; }
        public DateTime date_Publiciter { get; set; }
        public IList<Commentaire> commentaires { get; set; }
        public Categories GetCategories { get; set; }
    }
}
