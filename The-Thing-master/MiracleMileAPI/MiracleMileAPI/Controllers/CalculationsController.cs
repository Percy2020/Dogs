using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiracleMileAPI.Filters;
using MiracleMileAPI.Http;
using MiracleMileAPI.Model;
using MiracleMileAPI.Sessions;
using Newtonsoft.Json;

namespace MiracleMileAPI.Controllers
{
    [Route("miraclemile")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {

        [HttpPost("aktiebolag")]
        public string Aktiebolag([FromBody] CalculationInput calculationInput)
        {
            var test = calculationInput;
            var text = "Token";
            return JsonConvert.SerializeObject(text);
        }
    }
}
