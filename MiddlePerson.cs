using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace lab01
{
    public class MiddlePerson
    {
        private readonly RequestDelegate _next;
        public MiddlePerson(RequestDelegate next, IOptions<person> options)
        {
            _next = next;
            p = options.Value;
        }

        public person p { get; }

        public async Task InvokeAsync(HttpContext context)
        {
            StringBuilder str = new StringBuilder();

            str.Append($"<p>Name: {p?.Name}");
            str.Append($"<p>Age: {p?.Age}");
            
            
            await context.Response.WriteAsync(str.ToString());
            await _next(context); 
        }
    }
}