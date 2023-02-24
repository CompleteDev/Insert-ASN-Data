using DataAccess.DbAccess;
using InsertASNData.Models;
using System;
using System.Threading.Tasks;

namespace InsertASNData.Agents.Details
{
    public class ASNDetailsAgent : IASNDetailsAgent
    {
        private readonly ISqlDataAccess _db;
        public ASNDetailsAgent(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task InsertASNDetails(ASNDetailsMDL detailsMDL, long HeaderId)
        {
            await _db.SaveData("INSERT INTO ASNDetails(ASNHeaderId,SKU,Quantity,Price) VALUES(@HeaderId,@SKU,@Quantity,@Price)",
                  new { HeaderId, detailsMDL.SKU, detailsMDL.Quantity, detailsMDL.Price });
        }
    }
}
