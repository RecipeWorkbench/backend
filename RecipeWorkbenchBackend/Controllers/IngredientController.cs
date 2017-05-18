using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RnD.Workbench.Services.Interfaces;
using RnD.Workbench.DataTransferObjects;

namespace RecipeWorkbenchBackend.Controllers
{
    [Route("api/[controller]")]
    public class IngredientController : Controller
    {
        public IIngredientServices IngredientServices
        {
            get; set;
        }

        public IngredientController(IIngredientServices ingredientServices)
        {
            IngredientServices = ingredientServices;
        }

        // GET api/ingredient/{id}
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return Json(IngredientServices.GetIngredient(id));
        }

        // GET api/ingredient
        [HttpGet()]
        public JsonResult GetIngredientHeaders()
        {
            return Json(IngredientServices.GetIngredientHeaders());
        }

        // GET api/ingredient/startswith/{name}
        [HttpGet("startswith/{name}")]
        public JsonResult GetIngredientsStartingWith(string name)
        {
            return Json(IngredientServices.GetIngredientsStartingWith(name));
        }

        // GET api/ingredient/filter?name={name}&compound={compound}&skip={skip}&take={take}
        [HttpGet("filter")]
        public JsonResult GetIngredients([FromQuery] string name, [FromQuery] int compound, [FromQuery] int skip, [FromQuery] int take)
        {
            return Json(IngredientServices.GetIngredients(name, compound, skip, take));
        }

        // GET api/ingredient/count
        [HttpGet("count")]
        public int GetIngredientsCount([FromQuery] string name, [FromQuery] int compound, [FromQuery] int skip, [FromQuery] int take)
        {
            return IngredientServices.GetIngredientsCount(name, compound, skip, take);
        }
    }
}
