using InsertASNData.Models;
using System.Threading.Tasks;

namespace InsertASNData.Agents.Details
{
    public interface IASNDetailsAgent
    {
        Task InsertASNDetails(ASNDetailsMDL detailsMDL, long HeaderId);
    }
}