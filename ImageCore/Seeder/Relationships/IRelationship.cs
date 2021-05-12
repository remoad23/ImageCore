using System;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder.Relationships
{
    public interface IRelationship
    {
        #nullable enable
        public static void BuildRelationships(ModelBuilder? modelBuilder = null)  =>  throw new NotImplementedException() ;
    }
}