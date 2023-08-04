using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DigitalAssesment_EF.Models;
using DigitalAssesment_EF.Models.ApiResponse;
using DigitalAssesment_EF.Utility;
using DigitalAssesment_EF.Models.Login;
using DigitalAssesment_EF.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalAssesment_EF.Controllers
{

    public class UsersController : Controller
    {
        private readonly DbHelper db;
        private static IConfiguration _config;
        
        public UsersController(UserDbContext context, IConfiguration config)
        {
            db = new DbHelper(context);
            _config = config;
        }

        [HttpGet]
        [Route("api/[controller]/GetUser")]
        public IActionResult Get()
        {
            string userId = Util.getUserByJwtToken(Request.Headers.Authorization);
            if (userId == null || !userId.Any())
            {
                ApiResponse res = new ApiResponse();
                res.Code = "400";
                res.Message = "Jwt not valid";
                return Ok(res);
            }

            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<UserModel> data = db.GetUsers();
                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetApiResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/values
        [HttpPost]
        [Route("api/[controller]/Registration")]
        public IActionResult registration([FromBody] UserModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                db.saveUser(model);
                return Ok(ResponseHandler.GetApiResponse(type, model));

            }
            catch (Exception ex)
            {

                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("api/[controller]/login")]
        public IActionResult login([FromBody] LoginClass model)
        {
            try
            {
                LoginResponse response = new LoginResponse();
                ResponseType type = ResponseType.Success;
                var data = db.checkUser(model.username);
                try
                {
                    var authenticate = db.AuthenticateUSer(model.username, model.password);
                    response.id = data.id;
                    response.username = data.username;
                    response.UserToken = Util.GenerateToken(model,_config);
                    return Ok(ResponseHandler.GetApiResponse(type, response));
                }
                catch (Exception e)
                {
                    ApiResponse res = new ApiResponse();
                    res.Code = "200";
                    res.Message = "Password Incorrect";
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                ApiResponse response = new ApiResponse();
                response.Code = "200";
                response.Message = "User not found";
                return Ok(response);
            }
        }
    }
}

