using System.Threading.Tasks;

namespace InsertASNData.Agents.History
{
    public interface IASNHistory
    {
        Task InsertASNHistory(string ASNMessage, long HeaderId, int ASNStatusId);
    }
}