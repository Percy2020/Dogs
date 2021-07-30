using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiracleMileAPI.Library;
using MiracleMileAPI.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MiracleMileAPI.Controllers
{
  //[Route("api/[controller]")]
  [Route("miraclemile")]
  [ApiController]
  public class UploadFilesController : ControllerBase
  {

    private readonly IWebHostEnvironment _hostingEnvironment;


    public UploadFilesController(IWebHostEnvironment hostingEnvironment)
    {
      _hostingEnvironment = hostingEnvironment;

    }

    //// GET: api/UploadFiles
    [HttpGet("files/{id}")]
    public IEnumerable<UploadFile> GetFiles(int id)
    {
      //return new string[] { "value1", "value2" };
      
      UploadFiles_Crud uploadFiles = new UploadFiles_Crud();
      return uploadFiles.GetUploadFiles(id);
    }

    //// GET: api/UploadFiles/5
    //[HttpGet("{id}", Name = "Get")]
    //public string Get(int id)
    //{
    //    return "value";
    //}

    //// POST: api/UploadFiles
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}
    // POST: api/Animal
    [HttpPost("SaveUpload")]
    public UploadFile SaveUpload([FromBody] UploadFile uploadFile)
    {
      //Animal_Crud animalCrud = new Animal_Crud(_hostingEnvironment);
      UploadFiles_Crud uploadFiles = new UploadFiles_Crud();
      //return uploadFiles.CreateAnimal(animal)
      return uploadFiles.UpdateUploadFiles(uploadFile);
    }
    [HttpPost("Upload/{id}"), DisableRequestSizeLimit]
    public IActionResult Upload(int id)
    {
      try
      {
        var file = Request.Form.Files[0];
        var folderName = Path.Combine("Resources", "Images");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (file.Length > 0)
        {
          var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
          var fullPath = Path.Combine(pathToSave, fileName);
          var dbPath = Path.Combine(folderName, fileName);

          UploadFiles_Crud uploadFiles = new UploadFiles_Crud();
          uploadFiles.AddUploadFiles(new UploadFile() { Id = id.ToString(), Name = fileName, Url = fullPath, AnimalId ="0" });


          using (var stream = new FileStream(fullPath, FileMode.Create))
          {
            file.CopyTo(stream);
          }
          return Ok(new { dbPath });
        }
        else
        {
          return BadRequest();
        }
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex}");
      }
    }

    // DELETE: api/ApiWithActions/5
    [HttpGet("DeleteUploadFile/{fileId}")]
    public void DeleteUploadFile(int fileId = 0)
    {
      UploadFiles_Crud uploadFiles = new UploadFiles_Crud();
      uploadFiles.DeleteUploadFile(fileId);
    }
    //// PUT: api/UploadFiles/5
    //[HttpPut("{id}")]
    //    public void Put(int id, [FromBody] string value)
    //    {
    //    }

    //    // DELETE: api/ApiWithActions/5
    //    [HttpDelete("{id}")]
    //    public void Delete(int id)
    //    {
    //    }
  }
}
