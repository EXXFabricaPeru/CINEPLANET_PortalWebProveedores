using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebProov_API.Models
{
    public class User
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Ruc { get; set; }
        public string CreditLine { get; set; }
    }

}
