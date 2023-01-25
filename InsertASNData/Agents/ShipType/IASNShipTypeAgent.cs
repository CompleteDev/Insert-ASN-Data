using InsertASNData.Models;
using System.Threading.Tasks;

namespace InsertASNData.Agents.ShipType
{
    public interface IASNShipTypeAgent
    {
        Task InsertASNTrackingNumber(ASNShipType shipType, long headerId);
    }
}