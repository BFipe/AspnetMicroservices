using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Ordering.Application.Behaviours;

namespace Ordering.Application.Extentions
{
    public static class OrderingApplicationExtention
    {
        public static IServiceCollection AddApplicationExtentions (this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapProfile));

            services.AddFluentValidationAutoValidation();

            services.AddMediatR(q =>
            {
                q.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
