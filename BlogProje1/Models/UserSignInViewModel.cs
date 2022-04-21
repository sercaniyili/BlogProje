﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Models
{
    public class UserSignInViewModel
    {

        [Required(ErrorMessage ="Lütfen kullanıcı adını giriniz")]
        public string username { get; set; }
        
        [Required(ErrorMessage = "Lütfen şifrenizi adını giriniz")]
        public string paswword { get; set; }
    }
}
