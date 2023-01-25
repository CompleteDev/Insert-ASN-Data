using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using InsertASNData.Agents.Details;
using InsertASNData.Agents.Header;
using InsertASNData.Agents.ShipType;
using InsertASNData.Agents.Tracking;
using InsertASNData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;


namespace InsertASNData
{
    public class InsertASN
    {
        private readonly ILogger<InsertASN> _logger;
        private readonly ASNHeaderMDL _headerMDL;
        private readonly ASNDetailsMDL _detailsMDL;
        private readonly ASNTrackingMDL _trackingMDL;
        private readonly ASNShipType _shipTypeMDL;

        private readonly IInsertASNHeaderAgent _headerAgent;
        private readonly IASNDetailsAgent _detailsAgent;
        private readonly IASNTrackingNumberAgent _trackingAgent;
        private readonly IASNShipTypeAgent _shiptype;

        public InsertASN(ILogger<InsertASN> log, ASNHeaderMDL headerMDL, IInsertASNHeaderAgent headerAgent, ASNDetailsMDL detailsMDL,IASNDetailsAgent detailsAgent,IASNTrackingNumberAgent trackingAgent,
                         ASNTrackingMDL trackingMDL, IASNShipTypeAgent shiptype, ASNShipType shipTypeMDL)
        {
            _logger = log;
            _headerMDL = headerMDL;

            _headerAgent = headerAgent;
            _detailsAgent = detailsAgent;
            _detailsMDL = detailsMDL; 
            _trackingAgent = trackingAgent;
            _trackingMDL = trackingMDL;
            _shiptype = shiptype;
            _shipTypeMDL = shipTypeMDL;
        }

        [FunctionName("InsertASN")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            //ADD Try catch and logging here

            _headerMDL.AccountNumber =  data?.AccountNumber;
            _headerMDL.VendorRefernce = data?.ShipmentNumber;
            _headerMDL.SentDate = DateTime.Now;
            _headerMDL.Cartons = Convert.ToInt32(data?.Cartons);
            _headerMDL.Pallets = Convert.ToInt32(data?.Pallets);
            _headerMDL.StatusId = 1;

            var results = await _headerAgent.InsertASNHeader(_headerMDL);

            _headerMDL.ASNHeaderId = Convert.ToInt64(results);

            foreach(var product in data?.InboundProducts)
            {
                _detailsMDL.SKU = product.SKU;
                _detailsMDL.Quantity = product.Quantity;
                _detailsMDL.Price = product.Price;

                await _detailsAgent.InsertASNDetails(_detailsMDL, _headerMDL.ASNHeaderId);
            }

            foreach(var tracking in data?.ListOfTrackingNumbers)
            {
                _trackingMDL.TrackingNumber = tracking;

                await _trackingAgent.InsertASNTrackingNumber(_trackingMDL,_headerMDL.ASNHeaderId);
            }

            _shipTypeMDL.ShipTypeId = data?.ShipmentType;

            await _shiptype.InsertASNTrackingNumber(_shipTypeMDL,_headerMDL.ASNHeaderId);


            string headerIdString = Convert.ToString(_headerMDL.ASNHeaderId);

            string responseMessage = string.IsNullOrEmpty(headerIdString)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : headerIdString;

            return new OkObjectResult(responseMessage);
        }
    }
}

