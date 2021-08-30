using System.Threading.Tasks;
using ImageCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Twilio.Rest.Api.V2010.Account;

namespace ImageCore.Services.Interfaces
{
    public interface IProjectAuth
    {
        public abstract string CreateToken(UserModel user,string projectId,ContextDb context);
        public abstract bool VerifyToken(HttpContext context, ContextDb Context);
    }
}