using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiracleMileAPI.Library;
using MiracleMileAPI.Model;

namespace MiracleMileAPI.Controllers
{
  //[ApiController]
  //[Route("[controller]")]
  [Route("miraclemile")]
  [ApiController]
  public class AnimalController : ControllerBase
    {
    private readonly IWebHostEnvironment _hostingEnvironment;


    public AnimalController(IWebHostEnvironment hostingEnvironment)
    {
      _hostingEnvironment = hostingEnvironment;

    }
    // GET: api/Animal
    [HttpGet("getAnimalTypes")]
    public IEnumerable<string> GetAnimalTypes()
    {
         
        List<AnimalType> animalList = new List<AnimalType>();

        animalList.Add(new AnimalType() { Type = "Hund"});

        animalList.Add(new AnimalType() { Type = "Katt" });
        animalList.Add(new AnimalType() { Type = "Kanin" });

        List<string> animalTypeList = new List<string>();

        foreach (AnimalType animal in animalList)
        {
            animalTypeList.Add(animal.Type);
        }

        return animalTypeList;
    }

    // GET: api/Animal
    [HttpGet("getAnimalBreeds")]
    public IEnumerable<string> GetAnimalBreeds()
    {
      List<AnimalType> animalList = new List<AnimalType>();

      animalList.Add(new AnimalType() { Type = "Tax" });

      animalList.Add(new AnimalType() { Type = "Pudel" });
      animalList.Add(new AnimalType() { Type = "Beagle" });
      animalList.Add(new AnimalType() { Type = "Akita" });

      List<string> animalTypeList = new List<string>();

      foreach (AnimalType animal in animalList)
      {
        animalTypeList.Add(animal.Type);
      }

      return animalTypeList;
    }

    // GET: api/Animal
    [HttpGet("getAnimalGenders")]
    public IEnumerable<string> GetAnimalGenders()
    {

      List<AnimalType> animalList = new List<AnimalType>();

      animalList.Add(new AnimalType() { Type = "Hane" });

      animalList.Add(new AnimalType() { Type = "Hona" });

      List<string> animalTypeList = new List<string>();

      foreach (AnimalType animal in animalList)
      {
        animalTypeList.Add(animal.Type);
      }

      return animalTypeList;
    }
   
    // GET: api/Animal/5
    [HttpGet("GetAnimales/{ownerId}")]
        public IEnumerable<Animal> GetAnimales(int ownerId = 0)
        {
            Animal_Crud animalCrud = new Animal_Crud(_hostingEnvironment);
          return animalCrud.GetAnimalsByOwnerId(ownerId);
        }
    // GET: api/Animal/5
    [HttpGet("GetAllAnimales")]
    public IEnumerable<Animal> GetAllAnimales()
    {
      Animal_Crud animalCrud = new Animal_Crud(_hostingEnvironment);
      return animalCrud.GetAnimals();
    }
    // POST: api/Animal
    [HttpPost("SaveAnimal")]
        public Animal SaveAnimal([FromBody] Animal animal)
        {
          Animal_Crud animalCrud = new Animal_Crud(_hostingEnvironment);
          if(animal.Id == 0)
          {
            return animalCrud.CreateAnimal(animal);
          }
          else
          {
            return animalCrud.UpdateAnimal(animal);
          }
        }

        // PUT: api/Animal/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        // DELETE: api/ApiWithActions/5
        [HttpGet("DeleteAnimal/{AnimalId}")]
        public IEnumerable<Animal> DeleteAnimal(int AnimalId = 0)
        {

          Animal_Crud animalCrud = new Animal_Crud(_hostingEnvironment);
          var deleteAnimal = animalCrud.GetAnimalById(AnimalId);
          animalCrud.DeleteAnimal(deleteAnimal.Id);
          return animalCrud.GetAnimalsByOwnerId(deleteAnimal.OwnerId);
        }
  }
}
