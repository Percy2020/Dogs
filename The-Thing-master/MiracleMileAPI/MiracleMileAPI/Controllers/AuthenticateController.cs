using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace MiracleMileAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("miraclemile")]
    [ApiController]
    //[ValidateToken]
    public class AuthenticateController : ControllerBase
    {
        MakeHttpRequest MakeHttpRequest;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AuthenticateController(IHttpClientFactory clientFactory, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            MakeHttpRequest = new MakeHttpRequest(clientFactory);
        }

        // GET: api/Authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Login login)
        {
            //var tokenTest = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxOTUwMDExODIwNDYiLCJqdGkiOiI4ZjcxMTQ3NC1jMTdjLTQ0NTgtODVmYy1lZmZhNzVmYTBlZDMiLCJpYXQiOjE1NzQyNDc5MTMsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiIxOTUwMDExODIwNDYiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE5NTAwMTE4MjA0NiIsImh0dHA6Ly9zY2hlbWFzLmRhbmljYS5zZS9pZGVudGl0eS9jbGFpbXMvZGlzcGxheW5hbWUiOiJBcm5lIEFybmVzc29uIiwiaHR0cDovL3NjaGVtYXMuZGFuaWNhLnNlL2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiQXJuZXNzb24iLCJodHRwOi8vc2NoZW1hcy5kYW5pY2Euc2UvaWRlbnRpdHkvY2xhaW1zL2dpdmVubmFtZSI6IkFybmUiLCJodHRwOi8vc2NoZW1hcy5kYW5pY2Euc2UvaWRlbnRpdHkvY2xhaW1zL2lzc3VlciI6IkRhbmljYUJhbmtJZEFQSS1UZXN0IiwiaHR0cDovL3NjaGVtYXMuZGFuaWNhLnNlL2lkZW50aXR5L2NsYWltcy9hdWRpZW5jZSI6IkRhbmljYS1UZXN0IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoicHJpdmF0cGVyc29uIiwiaHR0cDovL3NjaGVtYXMuZGFuaWNhLnNlL2lkZW50aXR5L2NsYWltcy9hY3RvbmJlaGFsZm9mIjoiMTk1MDAxMTgyMDQ2IiwibmJmIjoxNTc0MjQ3OTEzLCJleHAiOjE1NzQyNDk3MTMsImlzcyI6IkRhbmljYUJhbmtJZEFQSS1UZXN0IiwiYXVkIjoiRGFuaWNhLVRlc3QifQ.LQjhzjQgdLBhXfarRe5IifnkE_g5QZfreJezWEkUln8";
            //var validMinutes = 30;
            //var timeLeftReminderMinutes = 5;
            //MakeHttpRequest m = new MakeHttpRequest();

 
            var users = getUsers();
            var user =   users.FirstOrDefault(u => u.Email.ToLower() == login.Email.ToLower());
            if (user != null) 
            { 

                if (user.Email.ToLower() == login.Email.ToLower() && user.Password == login.Password)
                {

                var token = TokenData.CreateJwtToken(user);

                return Ok(new Token() { AuthToken = token, });

                }
                else
                {
                    return Ok(new Token() { AuthToken = "", });
                }
            }
            else
            {
                return Ok(new Token() { AuthToken = "", });
            }

   
            /*
            var list = new AccountList();
            list.Accounts = new List<Account>();
            var account = new Account
            {
             Id = 4,
             ChangeType = "dELETE",
              EligibleForDeposit = true,
              Number = "88888",
              Name = "89999"
            };

            list.Accounts.Add(account);


            var user = new User
            {
                Email = "test@live.se",
                MobilePhoneNumber = "0706880589",
                PhoneNumber = "0706880589",
                AgreeMarketing = true, 
            };*/
            //Post
            //var test88 = await MakeHttpRequest.SendRequest<User>(APIendpoints.PostProfile, tokenTest, user);

            /*
            User user;

            user = new User
            {
                PersonalNumber = "198805199299",
                Name = "Marco Villegas",
                GivenName = "Marco",
                Surname = "Villegas",
            };

            var token = TokenData.CreateJwtToken(user);

            var test = TokenData.ValidateToken(token);

            return new string[] { "Token", token };*/


            //Python Test

            //instance of python engine
            /* var engine = Python.CreateEngine();
             //reading code from file
             var source = engine.CreateScriptSourceFromFile(Path.Combine(Environment.CurrentDirectory, "python\\test.py"));
             var scope = engine.CreateScope();
             //executing script in scope
             source.Execute(scope);
             var classCalculator = scope.GetVariable("Test_Clause_1");
             //initializing class
             var calculatorInstance = engine.Operations.CreateInstance(classCalculator);
             var text = "From Iron Python";
             var plus = calculatorInstance.assign_variables(4,4);*/
            //var svar1 = "5 + 10 = "+ plus;
            //var plus2 = calculatorInstance.increment(5);
            //var svar2 = "5++ =" + plus2;


            // return new string[] { "Token", "test : " + test88 };

        }

           // GET: api/Authenticate
        [HttpPost("newToken")]
        public async Task<IActionResult> NewToken()
        {

            string authHeaderToken = Request.Headers["bearer"];
            
            var socialSecurityNumber = TokenData.GetClaimByKey(authHeaderToken, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            var surname = TokenData.GetClaimByKey(authHeaderToken, "surname");
            var givenName = TokenData.GetClaimByKey(authHeaderToken, "givenname");
            var id = Convert.ToInt32(TokenData.GetClaimByKey(authHeaderToken, "id"));
            var name = givenName + " " + surname;

            var user = new User() {
                 Id = id,
                 Name = name,
                 Surname = surname,
                GivenName = givenName,
                SocialSecurityNumber = socialSecurityNumber
                };

            if (user != null)
            {
                var token = TokenData.CreateJwtToken(user);

                return Ok(new Token() { AuthToken = token, });
            }
            else
            {
                return Ok(new Token() { AuthToken = "", });
            }

        }





        [ValidateToken]
        //GET: api/Authenticate/5
        [HttpGet("list")]
        public IEnumerable<string> List()
        {
            return new string[] { "Token", "23", "test" };
        }

        private List<User> getUsers()
        {
            var contentRootPath = _hostingEnvironment.ContentRootPath;
            var file = $@"{contentRootPath}/JsonDB/User.json";
            var userList = System.IO.File.ReadAllText(file);
            List<User> jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userList);
            return jsonObject;
        }


        /* // GET: api/Authenticate/5
         [HttpGet("{id}", Name = "Get")]
         public string Get(int id)
         {
             return "value";
         }

         // POST: api/Authenticate
         [HttpPost]
         public void Post([FromBody] string value)
         {
         }

         // PUT: api/Authenticate/5
         [HttpPut("{id}")]
         public void Put(int id, [FromBody] string value)
         {
         }

         // DELETE: api/ApiWithActions/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}
