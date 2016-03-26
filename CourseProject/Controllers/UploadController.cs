using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Interfaces;
using CourseProject.Providers;

namespace CourseProject.Controllers
{
    public class UploadController : ApiController
    {
        private readonly IAccountService service;

        public UploadController(IAccountService serv)
        {
            service = serv;
        }
        
 
        [HttpDelete]
        public IHttpActionResult Delete(string fileName)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("api/upload")]
        public async Task<IHttpActionResult> Add()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            try
            {
                var provider = new CustomMultipartFormDataStreamProvider(service.WorkingFolder);

                await Request.Content.ReadAsMultipartAsync(provider);

                return Ok(await service.UploadFile(provider));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

       
    }
}
