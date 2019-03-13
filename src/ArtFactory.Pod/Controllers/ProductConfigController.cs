using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtFactory.Pod.Controllers
{
  using ArtFactory.Pod.Models;

  public class ProductConfigController : ApiController
    {
        [Route("api/ProductConfig/{AccountId}/{CampaignId}/{DocumentId}")]
        public IHttpActionResult Get(int accountId, int campaignId, int documentId)
        {
          var config = new ProductConfig
          {
            AccountId = accountId,
            CampaignId = campaignId,
            DocumentId = documentId,
            Dials = new List<Dial>
            {
              new Dial {DialName = "RetailPrice", ControlType = ControlType.Text},
              new Dial {DialName = "GrowerAddress", ControlType = ControlType.Text}
            }
          };

          return Json(config);
        }
    }
}
