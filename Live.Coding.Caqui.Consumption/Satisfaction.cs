using Live.Coding.Caqui.Consumption.Interface;
using Live.Coding.Caqui.Consumption.Util;
using Live.Coding.Caqui.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Live.Coding.Caqui.Consumption
{
    public class Satisfaction : ISatisfaction
    {
        private readonly ISyncAsync _iSyncAsync;
        public Satisfaction(ISyncAsync ISyncAsync)
        {
            _iSyncAsync = ISyncAsync;
        }
        public async Task<List<SatisfactionModel>> GetSatisfaction(string Hash)
        {
            var result = new List<SatisfactionModel>();
            _iSyncAsync.HTTPVerb = HTTPVerb.GET;
            _iSyncAsync.Url = "https://livecaqui.paulomaestro.com.br/Satisfation/GetSatisfaction" + "?Hash=" + Hash;
            var resultAux = await _iSyncAsync.GoSyncAsync();

            if (!resultAux.Item1)
            {
                result = JsonConvert.DeserializeObject<List<SatisfactionModel>>(resultAux.Item2);
            }
            return result;
        }

        public async Task<List<SatisfactionModel>> PostSatisfaction(SatisfactionModel Satisfaction)
        {
            var result = new List<SatisfactionModel>();
            _iSyncAsync.HTTPVerb = HTTPVerb.POST;
            _iSyncAsync.Url = "https://livecaqui.paulomaestro.com.br/Satisfation/PostSatisfaction";
            _iSyncAsync.Obj = Satisfaction;
            var resultAux = await _iSyncAsync.GoSyncAsync();

            if (!resultAux.Item1)
            {
                result = JsonConvert.DeserializeObject<List<SatisfactionModel>>(resultAux.Item2);
            }
            return result;
        }
    }
}
