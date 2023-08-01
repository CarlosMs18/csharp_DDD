using CleanArchitecture.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;


namespace CleanArchitecture.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        //En este caso, el this se utiliza para indicar que el método AddApplicationService es una extensión del tipo IServiceCollection
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());//registrar el assembly de la libreria que nos permoite hacer el mapping de una clase origen a una destino
                                                                    //automaticamente leera todas las clases que esten heredando las clases del automapper y las inyectara

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); 
            //TODAS LAS CLASSES DE MI APPLICATION QUE ESTEN REFERENCIANDO AL ABSTRACT VALIDATION Y LOS PAQUETES DE FLUE VALIDATION
            //AUTOMATICAMENTE INYECTARA E INSTANCIAR LOS OBJETOS PARA QUE ESA VALIDACION SEA POSIBLE DENTRO DE MI PROYECTO

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnhandledExceptionBehaviour<,>));//para los behvarious pipeline de VALIDATION Y TMB DE LAS EXCEPCIONES
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
