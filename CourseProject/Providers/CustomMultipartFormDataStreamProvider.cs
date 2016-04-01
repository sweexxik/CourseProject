using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CourseProject.Providers
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string rootPath) : base(rootPath)
        {
        }

        //public override string GetLocalFileName(HttpContentHeaders headers)
        //{
        //    //Make the file name URL safe and then use it & is the only disallowed url character allowed in a windows filename
        //    var fileName = headers.ContentDisposition.FileName;

        //    var lastDot = headers.ContentDisposition.FileName.LastIndexOf('.');

        //    var extension = fileName.Substring(lastDot);

        //    var name = Guid.NewGuid() + extension;

        //    return name.Trim('"')
        //                .Replace("&", "and");
        //}
    }
}
