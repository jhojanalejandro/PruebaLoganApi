using PruebaLogan.CORE.Helpers.GenericResponse.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaLogan.CORE.Helpers.GenericResponse
{
    public class GenericResponse<T>: IGenericResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
