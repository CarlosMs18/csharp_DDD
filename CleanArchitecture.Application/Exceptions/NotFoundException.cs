using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    { 
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) no fue encontrado")//primer parametro representa el nombre de la clase sobre cual se disparara la exepcion
            //un key que represta el id del record sobre el cual se esta trabajando
        {
            
        }
    }
}
