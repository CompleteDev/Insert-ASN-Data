using InsertASNData.Models;
using System.Threading.Tasks;

namespace InsertASNData.Agents.Header
{
    public interface IInsertASNHeaderAgent
    {
        Task<long> InsertASNHeader(ASNHeaderMDL headerMDL);
    }
}