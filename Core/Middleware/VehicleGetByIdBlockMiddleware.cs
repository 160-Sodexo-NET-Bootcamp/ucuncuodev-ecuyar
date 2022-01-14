using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Middleware
{
    public class VehicleGetByIdBlockMiddleware
    {
        private readonly RequestDelegate next;

        public VehicleGetByIdBlockMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = Convert.ToString(context.Request.Path);

            if (path.Contains("/getVehicleById"))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("You can not use this method!!!");

                return;
            }

            await next.Invoke(context);
        }


    }
}
