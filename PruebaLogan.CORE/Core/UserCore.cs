using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PruebaLogan.CONTEXT.Context;
using PruebaLogan.CORE.Helpers.GenericResponse;
using PruebaLogan.CORE.Helpers.GenericResponse.Interface;
using PruebaLogan.CORE.Interface;
using PruebaLogan.CORE.Resources;
using PruebaLogan.MODEL.Dto;
using PruebaLogan.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PruebaLogan.CORE.Core
{
    public class UserCore : IUserCore
    {
        #region VARIABLE
        private readonly TestLoganContext _context;
        private readonly IMapper _mapper;
        #endregion
        #region CONTRUCTOR
        public UserCore(TestLoganContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region PUBLIC METODS

        public async Task<List<UserDto>> GetAlllUsers()
        {
            return await _context.UserT.Select(ct => new UserDto()
            {
                Id = ct.Id.ToString(),
                Cuenta = ct.Cuenta,
                Clave = ct.Clave,
                Nombre = ct.Nombre,
                TipoUsuario = ct.TipoUsuario,
                Estado = ct.Estado,
                TipoUsuarios = ct.TipoUsuarioNavigation.Descripcion,
                Estados = ct.Estado == true ? "ACTIVO" : "INACTIVO"
            })
            .AsNoTracking()
            .ToListAsync();

        }


        public async Task<UserDto?> GetById(string id)
        {
            var result = _context.UserT.Where(x => x.Id.Equals(Guid.Parse(id)));
            return await result.Select(us => new UserDto
            {
                Id = us.Id.ToString(),
                Cuenta = us.Cuenta,
                Clave = us.Clave,
                TipoUsuario = us.TipoUsuario,
                Estado = us.Estado,
                Nombre = us.Nombre,
                TipoUsuarios = us.TipoUsuarioNavigation.Descripcion,
                Estados = us.Estado == true ? "ACTIVO" : "INACTIVO"
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        }



        public async Task<bool> Delete(string id)
        {
            var user = _context.UserT.Where(x => x.Id.Equals(Guid.Parse(id))).FirstOrDefault();
            if (user != null)
            {

                var result = _context.UserT.Remove(user);
                var res = await _context.SaveChangesAsync();
                return res != 0 ? true : false;
            }
            return false;
        }

        public async Task<IGenericResponse<string>> SignUp(UserDto model)
        {

            var userupdate = _context.UserT.FirstOrDefault(x => x.Cuenta.Equals(model.Cuenta));
            if (userupdate != null && string.IsNullOrEmpty(model.Id))
            {
                return ApiResponseHelper.CreateErrorResponse<string>(Resource.USEREXIST);
            }
            else
            {
                if (userupdate != null)
                {
                    var map = _mapper.Map(model, userupdate);
                    _context.UserT.Update(map);
                }
                else
                {
                    var map = _mapper.Map<UserT>(model);
                    map.Id = Guid.NewGuid();
                    _context.UserT.Add(map);
                }

                await _context.SaveChangesAsync();
            }
            return ApiResponseHelper.CreateResponse<string>(null, true, Resource.REGISTERSUCCESSFULL);


        }

        public async Task<IGenericResponse<string>> UpdateUser(UserDto model)
        {

            var userupdate = _context.UserT.FirstOrDefault(x => x.Cuenta.Equals(model.Cuenta));
            if (userupdate == null)
            {
                return ApiResponseHelper.CreateErrorResponse<string>(Resource.NOFUND);
            }
            else
            {
                var map = _mapper.Map(model, userupdate);
                _context.UserT.Add(map);
                await _context.SaveChangesAsync();
            }
            return ApiResponseHelper.CreateResponse<string>(null, true, Resource.UPDATESUCCESSFULL);


        }



        #endregion

        #region PRIVATE METODS
        #endregion
    }
}
