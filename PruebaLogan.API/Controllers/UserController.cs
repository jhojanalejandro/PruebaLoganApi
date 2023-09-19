using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaLogan.CORE.Helpers.GenericResponse;
using PruebaLogan.CORE.Interface;
using PruebaLogan.MODEL.Dto;

namespace PruebaLogan.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserCore _user;

        public UserController(IUserCore user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var Data = await _user.GetAlllUsers();
                return Data != null ? Ok(Data) : NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var Data = await _user.GetById(id);
                return Data != null ? Ok(Data) : NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserDto model)
        {
            try
            {
                var isSuccess = await _user.SignUp(model);
                if (isSuccess.Success)
                {
                    var response = ApiResponseHelper.CreateResponse(isSuccess);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseHelper.CreateErrorResponse<string>(isSuccess.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                var response = ApiResponseHelper.CreateErrorResponse<string>(ex.Message);
                return BadRequest(response);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    var Data = await _user.Delete(id);
                    return Data != false ? Ok(Data) : NoContent();
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDto model)
        {
            try
            {
                var isSuccess = await _user.SignUp(model);
                if (isSuccess.Success)
                {
                    var response = ApiResponseHelper.CreateResponse(isSuccess);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseHelper.CreateErrorResponse<string>(isSuccess.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                var response = ApiResponseHelper.CreateErrorResponse<string>(ex.Message);
                return BadRequest(response);
            }
        }



    }
}
