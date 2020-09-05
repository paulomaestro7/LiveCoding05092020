using Live.Coding.Caqui.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Live.Coding.Caqui.Consumption.Interface
{
    public interface ILogin
    {
        Task<string> GetUser(UserModel User);
        Task<bool> PostUser(UserModel User);
    }
}
