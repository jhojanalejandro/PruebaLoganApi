using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaLogan.MODEL.Dto
{
    public class UserDto
    {
        public string? Id { get; set; }

        public string Clave { get; set; }

        public string Cuenta { get; set; }

        public Guid TipoUsuario { get; set; }

        public bool? Estado { get; set; }

        public string Nombre { get; set; }

        public string? TipoUsuarios { get; set; }

        public string? Estados { get; set; }

    }
}
