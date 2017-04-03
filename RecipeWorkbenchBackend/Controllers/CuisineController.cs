using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RnD.Workbench.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeWorkbenchBackend.Controllers
{
    [Route("api/[controller]")]
    public class CuisineController : Controller
    {
        public ICuisineServices CuisineServices
        {
            get; set;
        }

        // GET api/cuisine/{id}
        [HttpGet("{id}")]
        public JsonResult GetCuisine(int id)
        {
            return Json(CuisineServices.GetCuisine(id));
        }

        // GET api/cuisine/
        [HttpGet()]
        public JsonResult GetCuisines()
        {
            return Json(CuisineServices.GetCuisines());
        }
    }
}
