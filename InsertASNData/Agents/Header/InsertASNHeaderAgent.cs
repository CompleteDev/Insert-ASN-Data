using DataAccess.DbAccess;
using InsertASNData.Models;
using System;
using System.Threading.Tasks;

namespace InsertASNData.Agents.Header
{
    public class InsertASNHeaderAgent : IInsertASNHeaderAgent
    {
        private readonly ISqlDataAccess _db;

        public InsertASNHeaderAgent(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<long> InsertASNHeader(ASNHeaderMDL headerMDL)
        {
            try
            {
                var results = await _db.ExecuteScalar<long, dynamic>(
                "INSERT INTO ASNHeader(AccountNumber,VendorReference,SentDate,Cartons,Pallets,StatusId) " +
                "VALUES(@AccountNumber,@VendorRefernce,@SentDate,@Cartons,@Pallets,@StatusId); SELECT SCOPE_IDENTITY()", new { headerMDL.AccountNumber, headerMDL.VendorRefernce, headerMDL.SentDate, headerMDL.Cartons, headerMDL.Pallets, headerMDL.StatusId });

                return results;
            }
            catch (Exception ecx)
            {
                return 0;

            }

        }

    }
}
