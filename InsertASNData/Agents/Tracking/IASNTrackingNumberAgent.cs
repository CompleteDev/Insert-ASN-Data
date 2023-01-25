using InsertASNData.Models;
using System.Threading.Tasks;

namespace InsertASNData.Agents.Tracking
{
    public interface IASNTrackingNumberAgent
    {
        Task InsertASNTrackingNumber(ASNTrackingMDL trackingMDL, long headerId);
    }
}