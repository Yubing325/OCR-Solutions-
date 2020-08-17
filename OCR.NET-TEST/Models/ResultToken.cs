using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCR.NET_TEST.Models
{
    public class ResultToken
    {
        public string Refresh_Token { get; set; }

        public long Expires_In  { get; set; }

        public string Scope{ get; set; }

        public string Access_Token { get; set; }

        public string session_secret{ get; set; }
    }
}
