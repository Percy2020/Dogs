
using MiracleMileAPI.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleMileAPI.Library
{
  public class UploadFiles_Crud
  {
    private string jsonFile = @"C:\angular\The-Thing-master\MiracleMileAPI\MiracleMileAPI\JsonDB\Files.json";

    public UploadFiles_Crud()
    {
    }
    public void DeleteUploadFile(int id)
    {
      try
      {
        var json = File.ReadAllText(jsonFile);
        var jObject = JObject.Parse(json);
        JArray uploadFileList = (JArray)jObject["uploadFiles"];
        List<UploadFile> results = JsonConvert.DeserializeObject<List<UploadFile>>(jObject.GetValue("uploadFiles").ToString());

        if (id > 0)
        {
          var companyToDeleted = uploadFileList.FirstOrDefault(obj => obj["Id"].Value<int>() == id);
          var r = results.Where(x => x.Id == id.ToString()).FirstOrDefault();
          //TODO: Måste göra filerna unika
          DeliteFileFromMap(r.Url);

          uploadFileList.Remove(companyToDeleted);
          string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
          File.WriteAllText(jsonFile, output);
        }
        else
        {
          Console.Write("Invalid ID, Try Again!");
        }
      }
      catch (Exception)
      {

        throw;
      }
    }
    public List<UploadFile> GetUploadFiles(int id)
    {
      var json = File.ReadAllText(jsonFile);
      var jsonObj = JObject.Parse(json);
      var heroList = jsonObj.GetValue("uploadFiles");
      List<UploadFile> uploadFilesResult = new List<UploadFile>();


      foreach (var h in heroList.Where(obj => obj["AnimalId"].Value<int>() == id))
      {
        var up = JsonConvert.DeserializeObject<UploadFile>(h.ToString());
        var folderName = Path.Combine("Resources", up.Url);
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        byte[] imageArray = System.IO.File.ReadAllBytes(pathToSave);
        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
        up.Url = "data:image/jpeg;base64," + base64ImageRepresentation;

        uploadFilesResult.Add(up);
      }

      return uploadFilesResult;
    }
    public List<UploadFile> Getheros()
    {
      var json = File.ReadAllText(jsonFile);
      var jsonObj = JObject.Parse(json);
      return JsonConvert.DeserializeObject<List<UploadFile>>(jsonObj.GetValue("uploadFiles").ToString());
    }

    public UploadFile AddUploadFiles(UploadFile uploadFile)
    {

      try
      {
        var json = File.ReadAllText(jsonFile);
        var jsonObj = JObject.Parse(json);
        var uploadFileUploadList = jsonObj.GetValue("uploadFiles") as JArray;
        List<UploadFile> results = JsonConvert.DeserializeObject<List<UploadFile>>(jsonObj.GetValue("uploadFiles").ToString());

        if (results.Where(x => x.Id == uploadFile.Id).Count() == 0)
        {
          var newCompany = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(uploadFile));
          uploadFileUploadList.Add(newCompany);

          jsonObj["uploadFiles"] = uploadFileUploadList;
          string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
          File.WriteAllText(jsonFile, newJsonResult);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Add Error : " + ex.Message.ToString());
      }
      return uploadFile;
    }
    public UploadFile UpdateUploadFiles(UploadFile uploadFile)
    {

      try
      {
        var json = File.ReadAllText(jsonFile);
        var jsonObj = JObject.Parse(json);
        var uploadFileUploadList = jsonObj.GetValue("uploadFiles") as JArray;
        List<UploadFile> results = JsonConvert.DeserializeObject<List<UploadFile>>(jsonObj.GetValue("uploadFiles").ToString());
        var r = results.Where(x => x.Id == uploadFile.Id).FirstOrDefault();
        if (r != null && r.AnimalId == "0")
        {
          var r_d = uploadFileUploadList.FirstOrDefault(obj => obj["Id"].Value<int>() == Convert.ToInt32(r.Id));
          uploadFileUploadList.Remove(r_d);
          r.AnimalId = uploadFile.AnimalId;
          var newCompany = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(r));
          uploadFileUploadList.Add(newCompany);

          jsonObj["uploadFiles"] = uploadFileUploadList;
          string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
          File.WriteAllText(jsonFile, newJsonResult);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Add Error : " + ex.Message.ToString());
      }
      return uploadFile;
    }
    public void DeliteFileFromMap(string url)
    {
      File.Delete(url);
    }
  }
}
   