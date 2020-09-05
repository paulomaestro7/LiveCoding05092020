using Live.Coding.Caqui.Consumption.Util;
using System.Threading.Tasks;

namespace Live.Coding.Caqui.Consumption.Interface
{
    public interface ISyncAsync
    {
        object Obj { get; set; }
        HTTPVerb HTTPVerb { get; set; }
        string Url { get; set; }
        Task<(bool, string)> GoSyncAsync();
    }
}
