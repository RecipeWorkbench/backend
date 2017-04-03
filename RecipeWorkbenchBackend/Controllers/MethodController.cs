using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RnD.Workbench.Services.Interfaces;

namespace RecipeWorkbenchBackend.Controllers
{
    [Route("api/[controller]")]
    public class MethodController : Controller
    {
        public IMethodServices MethodServices
        {
            get; set;
        }

        // GET api/method/{id}
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return Json(MethodServices.GetMethod(id));
        }
    }
}
