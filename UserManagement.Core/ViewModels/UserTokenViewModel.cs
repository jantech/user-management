using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.ViewModels
{
    public class UserTokenViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Token { get; set; }
    }
}
