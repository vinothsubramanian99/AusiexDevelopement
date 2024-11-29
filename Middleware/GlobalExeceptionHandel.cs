
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject3.Middleware
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            try{
                await next(context);

            }
            catch(Exception ex){
                 var pb=new ProblemDetails();
                 pb.Type="";
                 pb.Title="Internal server error";
                 pb.Detail=ex.Message;
                 pb.Status=(int)HttpStatusCode.InternalServerError;
                 pb.Instance=context.Request.Path;


                await context.Response.WriteAsJsonAsync(pb);
            }
           
        }
    }
}