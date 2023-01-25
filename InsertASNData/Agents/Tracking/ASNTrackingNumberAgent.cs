using DataAccess.DbAccess;
using InsertASNData.Models;
using System;
using System.Threading.Tasks;

namespace InsertASNData.Agents.Tracking
{
    public class ASNTrackingNumberAgent : IASNTrackingNumberAgent
    {
        private readonly ISqlDataAccess _db;
        public ASNTrackingNumberAgent(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task InsertASNTrackingNumber(ASNTrackingMDL trackingMDL, long headerId)
        {
            await _db.SaveData("INSERT INTO ASNTracking(ASNHeaderId,TrackingNumber) VALUES(@headerId,@TrackingNumber)", new { headerId, trackingMDL.TrackingNumber });
        }
    }
}
