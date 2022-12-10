using System;
using c_sharp_angular.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_angular.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase

    {

    }
}

