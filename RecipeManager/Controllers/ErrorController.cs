﻿using System.Net;

using Microsoft.AspNetCore.Mvc;

using RecipeManager.Models;

namespace RecipeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("{statusCode}", Name = nameof(HandleStatusCode))]
        [HttpPut("{statusCode}", Name = nameof(HandleStatusCode))]
        public ActionResult<ApiError> HandleStatusCode(int statusCode)
        {
            var parsedCode = (HttpStatusCode)statusCode;
            var error = new ApiError(statusCode, parsedCode.ToString());
            return error;
        }
    }
}