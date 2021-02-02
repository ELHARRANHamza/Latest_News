using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models
{
    public class AppUsers : IdentityUser
    {
        public string cin { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string nom { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string prenom { get; set; }
        public string image { get; set; }
        public string UserType { get; set; }

        public IList<Commentaire> commentaires { get; set; }


    }
}