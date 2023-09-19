using Microsoft.EntityFrameworkCore;
using PruebaLogan.CORE.Helpers.GenericResponse.Interface;
using PruebaLogan.CORE.Helpers.GenericResponse;
using PruebaLogan.CORE.Resources;
using PruebaLogan.MODEL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PruebaLogan.CONTEXT.Context;
using PruebaLogan.CORE.Interface;
using PruebaLogan.MODEL.Entities;

namespace PruebaLogan.CORE.Core
{
    public class UserTypeCore: IUserTypeCore
    {
        #region VARIABLE
        private readonly TestLoganContext _context;
        private readonly IMapper _mapper;
        #endregion

        public UserTypeCore(TestLoganContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<UserTypeDto>> GetAllType()
        {

            return await _context.UserType.Select(ct => new UserTypeDto()
            {
                Id = ct.Id,
                Descripcion = ct.Descripcion,
                Codigo = ct.Codigo,
            })
            .AsNoTracking()
            .ToListAsync();

        }


        public async Task<IGenericResponse<string>> SaveTypeUser(UserTypeDto model)
        {

            var userTypeupdate = _context.UserType.FirstOrDefault(x => x.Id.Equals(model.Id));
            if (userTypeupdate != null)
            {
                var map = _mapper.Map(model, userTypeupdate);
                map.Id = Guid.NewGuid();
                _context.UserType.Add(map);
                await _context.SaveChangesAsync();
            }
            else
            {
                var map = _mapper.Map<UserType>(model);
                map.Id = Guid.NewGuid();
                _context.UserType.Add(map);
                await _context.SaveChangesAsync();
            }
            return ApiResponseHelper.CreateResponse<string>(null, true, Resource.REGISTERSUCCESSFULL);


        }
        public async Task<UserTypeDto?> GetById(string id)
        {
            var result = _context.UserType.Where(x => x.Id.Equals(Guid.Parse(id)));
            return await result.Select(us => new UserTypeDto
            {
                Id = us.Id,
                Descripcion = us.Descripcion,
                Codigo = us.Codigo,
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        }


    }
}
