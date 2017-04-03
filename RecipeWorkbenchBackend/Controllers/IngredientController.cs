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

        // GET api/ingredient/{id}
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return Json(IngredientServices.GetIngredient(id));
        }

        // GET api/ingredient/startswith/{name}
        [HttpGet("startswith/{name}")]
        public JsonResult GetIngredientsStartingWith(string name)
        {
            return Json(IngredientServices.GetIngredientsStartingWith(name));
        }
    }
}
