using Live.Coding.Caqui.Consumption.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Live.Coding.Caqui.Consumption.Util
{
    public class SyncAsync : IDisposable, ISyncAsync
    {
        public object Obj { get; set; }
        public HTTPVerb HTTPVerb { get; set; }
        public string Url { get; set; }

        private readonly HttpClient _httpclient;
        private HttpClient _httpclientToken;
        private Task<HttpResponseMessage> _taskhttpmessage;
        private HttpResponseMessage _httpmessage;

        public SyncAsync()
        {
            _httpclient = new HttpClient();
        }
        private HttpResponseMessage Post()
        {
            _taskhttpmessage = _httpclient.PostAsync(Url, new JsonContent(Obj));
            return Wait();
        }
        private HttpResponseMessage Get()
        {
            _taskhttpmessage = _httpclient.GetAsync(Url);
            return Wait();
        }
        private HttpResponseMessage Put()
        {
            _taskhttpmessage = _httpclient.PutAsync(Url, new JsonContent(Obj));
            return Wait();
        }
        private HttpResponseMessage Delete()
        {
            _taskhttpmessage = _httpclient.DeleteAsync(Url);
            return Wait();
        }
        private HttpResponseMessage Wait()
        {
            _taskhttpmessage.Wait();
            return _taskhttpmessage.Result;
        }
        public async Task<(bool, string)> GoSyncAsync()
        {
            var result = (false, "");
            try
            {
                switch (HTTPVerb)
                {
                    case HTTPVerb.GET:
                        _httpmessage = Get();
                        break;
                    case HTTPVerb.POST:
                        _httpmessage = Post();
                        break;
                    case HTTPVerb.PUT:
                        _httpmessage = Put();
                        break;
                    case HTTPVerb.DELETE:
                        _httpmessage = Delete();
                        break;
                    default:
                        break;
                }
                Task<string> retorno = _httpmessage.Content.ReadAsStringAsync();
                retorno.Wait();

                if (_httpmessage.StatusCode == HttpStatusCode.OK)
                {
                    result = (false, retorno.Result);
                }
                else
                {
                    result = (true, "Algo deu errado");
                }
            }
            catch (InvalidOperationException ex)
            {
                result = (true, ex.Message);
            }
            return result;
        }
        public void Dispose()
        {
            _httpclient.Dispose();
            _httpmessage.Dispose();
        }
    }
    public enum HTTPVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
