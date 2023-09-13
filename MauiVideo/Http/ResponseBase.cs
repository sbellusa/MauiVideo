using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiVideo.Http
{
    public class ResponseBase
    {
        public bool IsSuccessStatusCode { get; set; }

        public int StatusCode { get; set; }



        public bool IsForbidden => StatusCode == 403 || StatusCode == 401;
        public bool DoesNotExist => StatusCode == 400 || StatusCode == 404;
        public bool NotModified => StatusCode == 304;
    }
}
