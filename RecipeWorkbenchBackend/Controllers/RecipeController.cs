using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RnD.Workbench.DataTransferObjects;
using RnD.Workbench.Services.Interfaces;

namespace RecipeWorkbenchBackend.Controllers
{
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        private readonly IRecipeServices recipeServices;

        public IRecipeServices RecipeServices
        {
            get => recipeServices;
        }

        public RecipeController(IRecipeServices service)
        {
            recipeServices = service;
        }

        // GET api/recipe/{id}
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return Json(RecipeServices.GetRecipe(id));
        }

        // GET api/recipe/filter
        [HttpGet("filter")]
        public JsonResult GetRecipes([FromQuery] string name, [FromQuery] int ingredient, [FromQuery] int cuisine, [FromQuery] int skip, [FromQuery] int take)
        {
            return Json(RecipeServices.GetRecipes(name, ingredient, cuisine, skip, take));
        }

        // GET api/recipe/count
        [HttpGet("count")]
        public int GetRecipesCount([FromQuery] string name, [FromQuery] int ingredient, [FromQuery] int cuisine, [FromQuery] int skip, [FromQuery] int take)
        {
            return RecipeServices.GetRecipesCount(name, ingredient, cuisine, skip, take);
        }

        // POST api/recipe
        [HttpPost]
        public JsonResult Create([FromBody]NewRecipeDto recipe)
        {
            return Json(RecipeServices.Create(recipe));
        }

        // POST api/recipe/transform
        [HttpPost("transform")]
        public JsonResult Transform([FromBody]TransformRecipeDto task)
        {
            return Json(RecipeServices.Transform(task));
        }
    }
}
