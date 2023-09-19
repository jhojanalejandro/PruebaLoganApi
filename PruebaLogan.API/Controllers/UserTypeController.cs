using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaLogan.CORE.Helpers.GenericResponse;
using PruebaLogan.CORE.Interface;
using PruebaLogan.MODEL.Dto;

namespace PruebaLogan.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeCore _userType;

        public UserTypeController(IUserTypeCore userType)
        {
            _userType = userType;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllType()
        {
            try
            {
                var Data = await _userType.GetAllType();
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
                var Data = await _userType.GetById(id);
                return Data != null ? Ok(Data) : NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> SaveTypeUser(UserTypeDto model)
        {
            try
            {
                var isSuccess = await _userType.SaveTypeUser(model);
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
