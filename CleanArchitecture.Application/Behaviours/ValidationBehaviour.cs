using CleanArchitecture.Application.Exceptions;
using FluentValidation;
using MediatR;
using ValidationException = CleanArchitecture.Application.Exceptions.ValidationException;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {  //Trquest es de tipo IRequest casteando un TResponse
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if (_validators.Any())//valiudar primero si tenemos ualguna validacio logica en mi proyexto
            {
                var context = new ValidationContext<TRequest>(request);

           

                var validatorResults = await Task.WhenAll
                    (_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                //tenemos que capturar todas las validaciones que hemos escrito en la aplicacion
                //esta linea tambien ejecuta , pero no las ejecuta dentro del metodo final sino en el tubo de peticiones
                //en el medio antes de que llegue a la app

               var failures = validatorResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if(failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }
            return await next(); //que continue el flujo de la app si no hay errores
        }
    }
}
