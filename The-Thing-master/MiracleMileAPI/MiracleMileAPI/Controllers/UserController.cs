using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiracleMileAPI.Model;
using MiracleMileAPI.Sessions;
using Newtonsoft.Json;

namespace MiracleMileAPI.Controllers
{
    [Route("miraclemile")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IWebHostEnvironment _hostingEnvironment;

 
        public UserController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
           
        }

        //// GET: api/User
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/User/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/User
        [HttpPost("registerUser")]
        public async Task<IActionResult> Post([FromBody] Register register)
        {

            string pattern = @"^(19|20)?(\d{6}([-+]|\s)\d{4}|(?!19|20)\d{10})$";
           
            Match m = Regex.Match(register.SocialSecurityNumber, pattern, RegexOptions.IgnoreCase);
            

                if (register.Email != null && register.Password != null && m.Success)
            {
                var newId = Guid.NewGuid().GetHashCode();
                var user = new User
                {
                    Id = newId,
                    Email = register.Email,
                    Password = register.Password,
                    Name = register.Name + " " + register.Surname,
                    SocialSecurityNumber = register.SocialSecurityNumber,
                    GivenName = register.Name,
                    Surname = register.Surname,
                    AgreeMarketing = true,
                    SubscribeToEmailNotification = true
                };

                List<User> jsonList = GetUsers();

                jsonList.Add(user);
                //write string to file

                var jsonData = JsonConvert.SerializeObject(jsonList);
                var contentRootPath = _hostingEnvironment.ContentRootPath;
                var file = $@"{contentRootPath}/JsonDB/User.json";
                System.IO.File.WriteAllText(file, jsonData);
                return Ok(register);

            }
            else
            {

                return BadRequest(new ResponseMessage{Error = true, Code = "1" , Message = "objektet som tagits emot är tom" });
            }

            
        }

       
        private List<User> GetUsers()
        {
            var contentRootPath = _hostingEnvironment.ContentRootPath;
            var file = $@"{contentRootPath}/JsonDB/User.json";
            var userList = System.IO.File.ReadAllText(file);
            List<User> jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userList);
            return jsonObject;
        }

        [HttpPost("getUser")]
        public async Task<IActionResult> GetUser()
        {
            
            string authHeaderToken = Request.Headers["bearer"];
            if (authHeaderToken != null)
            {
                var givenName = TokenData.GetClaimByKey(authHeaderToken, "givenname");
                var socialSecurityNumber = TokenData.GetClaimByKey(authHeaderToken, "sub");
                var users = GetUsers();
                var user = users.FirstOrDefault(u => u.SocialSecurityNumber.ToLower() == socialSecurityNumber.ToLower() && u.GivenName.ToLower() == givenName.ToLower());
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }

        }


        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User postData)
        {

            string authHeaderToken = Request.Headers["bearer"];
            if (authHeaderToken != null)
            {
                var givenName = TokenData.GetClaimByKey(authHeaderToken, "givenname");
                var socialSecurityNumber = TokenData.GetClaimByKey(authHeaderToken, "sub");
                var users = GetUsers();
                var user = users.FirstOrDefault(u => u.SocialSecurityNumber.ToLower() == socialSecurityNumber.ToLower() && u.GivenName.ToLower() == givenName.ToLower());
                if (user != null)
                {

                    for (int i = 0; i < users.Count; i++) // Loop through List with for
                    {
                       
                        if(users[i].SocialSecurityNumber.ToLower() == socialSecurityNumber.ToLower() && users[i].GivenName.ToLower() == givenName.ToLower())
                        {
                            users[i].Email = postData.Email;
                            users[i].SubscribeToEmailNotification = postData.SubscribeToEmailNotification;
                            users[i].AgreeMarketing = postData.AgreeMarketing;
                            users[i].MobilePhoneNumber = postData.MobilePhoneNumber;
                            users[i].PhoneNumber = postData.PhoneNumber;
                        }

                    }

                    var jsonData = JsonConvert.SerializeObject(users);
                    var contentRootPath = _hostingEnvironment.ContentRootPath;
                    var file = $@"{contentRootPath}/JsonDB/User.json";
                    System.IO.File.WriteAllText(file, jsonData);

                    return Ok(user);
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }

        }
        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
