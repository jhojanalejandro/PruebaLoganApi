using PruebaLogan.MODEL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaLogan.CORE.Helpers.GenericResponse.Interface;

namespace PruebaLogan.CORE.Interface
{
    public interface IUserCore
    {
        Task<List<UserDto>> GetAlllUsers();
        Task<UserDto> GetById(string id);
        Task<bool> Delete(string id);
        Task<IGenericResponse<string>> SignUp(UserDto model);
        Task<IGenericResponse<string>> UpdateUser(UserDto model);
    }
}
