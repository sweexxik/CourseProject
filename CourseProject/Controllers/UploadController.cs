using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseProject.Interfaces;
using CourseProject.Providers;
using CourseProject.Repositories;

namespace CourseProject.Controllers
{
    public class UploadController : ApiController
    {
        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Uploads";

        private IUnitOfWork db;

        public UploadController()
        {
            db = new EfUnitOfWork();
        }

        [HttpGet]
        [Authorize]
        [Route("api/upload/{fileName}")]
        public async Task<IHttpActionResult> Get(string fileName)
        {
            var photoFolder = new DirectoryInfo(workingFolder);

            var result = "";

            foreach (var fileInfo in photoFolder.GetFiles())
            {
                if (fileInfo.Name.Split('.')[0] == fileName)
                {
                    result = fileInfo.FullName;
                }
            }

            return Ok(result);
        }

 
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string fileName)
        {
            if (!FileExists(fileName))
            {
                return NotFound();
            }
            try
            {
                var filePath = Directory.GetFiles(workingFolder, fileName)
                  .FirstOrDefault();

                await Task.Factory.StartNew(() =>
                {
                    if (filePath != null)
                        File.Delete(filePath);
                });

              
                return Ok(new { message = "OK" });
            }
            catch (Exception ex)
            {
               
                return BadRequest("NE ok");
            }
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
                var provider = new CustomMultipartFormDataStreamProvider(workingFolder);

                await Request.Content.ReadAsMultipartAsync(provider);
  
                var user = await db.FindUser(provider.FormData.Get("username"));

                var result = CloudinaryUpload(provider);

                user.AvatarUri = result.Uri.AbsoluteUri;

                await db.UpdateUser(user);

                return Ok(user);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        private ImageUploadResult CloudinaryUpload(CustomMultipartFormDataStreamProvider provider)
        {
            var account = new Account("ddttiy9ko", "799681156658259", "_A8bJk28HFotHtOJCMPFKrb1rII");

            var cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams();

            uploadParams.File = new FileDescription($"{provider.FileData[0].LocalFileName}");

            return cloudinary.Upload(uploadParams);
        }

        public bool FileExists(string fileName)
        {
            var file = Directory.GetFiles(workingFolder, fileName)
              .FirstOrDefault();

            return file != null;
        }
    }
}
