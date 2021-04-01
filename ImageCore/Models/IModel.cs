using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace ImageCore.Models
{
    public interface IModel
    {
        /*
         * Used to Seed entites in DBContext
         */
         void Seed();
    }
}