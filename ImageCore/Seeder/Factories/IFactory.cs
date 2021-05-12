using System;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder.Factories
{
    public interface IFactory
    {
        #nullable enable
        public static void BuildFactory(ModelBuilder? modelBuilder = null)  =>  throw new NotImplementedException() ;
    }
}