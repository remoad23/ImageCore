using System;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
#nullable enable
    
    public interface ISeeder
    {
        public static void Seed(ModelBuilder? modelBuilder = null)  =>  throw new NotImplementedException() ;
    }
}