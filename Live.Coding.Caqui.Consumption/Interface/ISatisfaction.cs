using Live.Coding.Caqui.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Live.Coding.Caqui.Consumption.Interface
{
    public interface ISatisfaction
    {
        Task<List<SatisfactionModel>> GetSatisfaction(string Hash);
        Task<List<SatisfactionModel>> PostSatisfaction(SatisfactionModel Satisfaction);
    }
}
