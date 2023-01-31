using DataAccess.DbAccess;
using InsertASNData.Models;
using System;
using System.Threading.Tasks;

namespace InsertASNData.Agents.History
{
    public class ASNHistory : IASNHistory
    {
        private readonly ISqlDataAccess _db;
        public ASNHistory(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task InsertASNHistory(string ASNMessage, long HeaderId, int ASNStatusId)
        {
            await _db.SaveData("INSERT INTO ASNHistory(ASNHeaderId,ASNMessage,ASNStatusId) VALUES(@HeaderId,@ASNMessage,@ASNStatusId)",
                  new { HeaderId, ASNMessage, ASNStatusId });
        }
    }
}
