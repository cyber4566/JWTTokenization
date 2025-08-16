using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Login.Orchestration.Interface
{
    public interface ILogin
    {
        public bool UserValid(string username, string password);




    }
}
