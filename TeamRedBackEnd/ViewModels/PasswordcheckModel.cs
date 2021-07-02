using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd.ViewModels
{
    public class PasswordcheckModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

