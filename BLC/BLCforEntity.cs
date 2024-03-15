using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLC
{
    public partial class Params_Get_Todo_By_USER_ID
    {
        public int user_id { get; set; }

    }
    public partial class Params_Get_Person_By_EMAIL_ADDRESS
    {
        public string email { get; set; }
        public string password { get; set; }

    }
}
