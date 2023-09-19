using PruebaLogan.CORE.Core;
using PruebaLogan.CORE.Helpers.GenericResponse.Interface;
using PruebaLogan.MODEL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaLogan.CORE.Interface
{
    public interface IUserTypeCore
    {
        Task<List<UserTypeDto>> GetAllType();
        Task<UserTypeDto> GetById(string id);
        Task<IGenericResponse<string>> SaveTypeUser(UserTypeDto model);
    }
}
