using Live.Coding.Caqui.Consumption.Interface;
using Live.Coding.Caqui.Consumption.Util;
using Live.Coding.Caqui.Model;
using System.Threading.Tasks;

namespace Live.Coding.Caqui.Consumption
{
    public class Login : ILogin
    {
        private readonly ISyncAsync _iSyncAsync;

        public Login(ISyncAsync ISyncAsync)
        {
            _iSyncAsync = ISyncAsync;
        }
        public async Task<string> GetUser(UserModel User)
        {
            var result = "";
            _iSyncAsync.HTTPVerb = HTTPVerb.GET;
            _iSyncAsync.Url = "https://livecaqui.paulomaestro.com.br/Satisfation/GetUser" + "?User=" + User.Login + "&Password=" + User.Password;
            var resultAux = await _iSyncAsync.GoSyncAsync();

            if (!resultAux.Item1)
            {
                result = resultAux.Item2;
            }
            return result;
        }

        public async Task<bool> PostUser(UserModel User)
        {
            var result = false;
            _iSyncAsync.HTTPVerb = HTTPVerb.POST;
            _iSyncAsync.Url = "https://livecaqui.paulomaestro.com.br/Satisfation/PostUser";
            _iSyncAsync.Obj = User;
            var resultAux = await _iSyncAsync.GoSyncAsync();

            if (!resultAux.Item1)
            {
                result = bool.Parse(resultAux.Item2);
            }
            return result;
        }
    }
}
