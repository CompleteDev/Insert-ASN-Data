using DataAccess.DbAccess;
using InsertASNData.Agents.Details;
using InsertASNData.Agents.Header;
using InsertASNData.Agents.ShipType;
using InsertASNData.Agents.Tracking;
using InsertASNData.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(InsertASNData.Startup))]


namespace InsertASNData
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IInsertASNHeaderAgent, InsertASNHeaderAgent>();
            builder.Services.AddScoped<IASNDetailsAgent, ASNDetailsAgent>();
            builder.Services.AddScoped<IASNTrackingNumberAgent, ASNTrackingNumberAgent>();
            builder.Services.AddScoped<IASNShipTypeAgent, ASNShipTypeAgent>();
            builder.Services.AddScoped<ASNHeaderMDL, ASNHeaderMDL>();
            builder.Services.AddScoped<ASNDetailsMDL, ASNDetailsMDL>();
            builder.Services.AddScoped<ASNTrackingMDL, ASNTrackingMDL>();
            builder.Services.AddScoped<ASNShipType, ASNShipType>();
            builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();

        }


    }
}
