using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtFactory.Pod.Controllers
{
  using ArtFactory.Pod.Models;

  public class ArtifactController : ApiController
    {
        [HttpPost]
        [Route("api/Artifact")]
        public IHttpActionResult Post([FromBody] ArtifactRequest artifactRequest)
        {

          return Ok(artifactRequest.DocumentId);
        }
    }
}
