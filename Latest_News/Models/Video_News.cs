using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models
{
    public class Video_News
    {
        public int id { get; set; }
        public DateTime date_Pub { get; set; }
        public string Vid { get; set; }
        [Required]
        [StringLength(150,MinimumLength =3)]
        public string titre { get; set; }
    }
}
