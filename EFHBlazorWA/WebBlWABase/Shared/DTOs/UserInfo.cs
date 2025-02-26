using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFHBlazzer.Shared.DTOs
{
    public class UserInfo
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string OldPassword { get; set; }
        public string Code { get; set; }


        public string Mail { get; set; }

    }
}
