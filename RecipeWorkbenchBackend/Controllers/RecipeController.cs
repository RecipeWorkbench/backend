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

        // GET api/recipe/startswith/{name}
        [HttpGet("startswith/{name}")]
        public JsonResult GetRecipesStartingWith(string name, [FromQuery] int skip, [FromQuery] int take)
        {
            return Json(RecipeServices.GetRecipesStartingWith(name, skip, take));
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
