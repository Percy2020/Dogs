using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleMileAPI.Model
{
    public class ResponseMessage
    {
        public Boolean Error { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
