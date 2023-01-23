using DataAccess.DbAccess;
using InsertASNData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertASNData.Agents.ShipType
{
    public class ASNShipTypeAgent : IASNShipTypeAgent
    {

        private readonly ISqlDataAccess _db;

        public ASNShipTypeAgent(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task InsertASNTrackingNumber(ASNShipType shipType, long headerId)
        {
            await _db.SaveData("INSERT INTO ASNShipType(ASNHeaderId,ShipTypeId) VALUES(@headerId,@ShipTypeId)", new { headerId, shipType.ShipTypeId });
        }

    }
}
