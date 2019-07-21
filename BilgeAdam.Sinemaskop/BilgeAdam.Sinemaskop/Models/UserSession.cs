using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdam.Sinemaskop.Models
{
    public class UserSession
    {
        public string UserName { get; set; }
        public bool IsLoggedIn { get { return !string.IsNullOrEmpty(UserName); } }
    }
}
