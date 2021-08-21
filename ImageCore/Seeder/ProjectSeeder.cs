using System;
using System.Collections.Generic;
using ImageCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class ProjectSeeder : ISeeder
    {
        public static List<ProjectModel> Seed(ModelBuilder modelBuilder,List<UserModel> users)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            
            ProjectModel project1 = new ProjectModel
            {
                ProjectId = Guid.NewGuid().ToString(),
                UserId =  users[0].Id,
                Name = "Projekt 1",
                Views = 4355,
            };
            
            ProjectModel project2 = new ProjectModel
            {
                ProjectId = Guid.NewGuid().ToString(),
                UserId =  users[3].Id,
                Name = "Projekt 2",
                Views = 4,
            };
            
            ProjectModel project3 = new ProjectModel
            {
                ProjectId = Guid.NewGuid().ToString(),
                UserId =  users[0].Id,
                Name = "Projekt 3",
                Views = 345,
            };
            
            ProjectModel project4 = new ProjectModel
            {
                ProjectId = Guid.NewGuid().ToString(),
                UserId =  users[1].Id,
                Name = "Projekt 4",
                Views = 2,
            };
            
            ProjectModel project5 = new ProjectModel
            {
                ProjectId = Guid.NewGuid().ToString(),
                UserId =  users[2].Id,
                Name = "Projekt 5",
                Views = 25,
            };

            modelBuilder.Entity<ProjectModel>().HasData(
                project1,
                project2,
                project3,
                project4,
                project5
            );
            
            projects.Add(project1);
            projects.Add(project2);
            projects.Add(project3);
            projects.Add(project4);
            projects.Add(project5);
            

            return projects;
        }
    }
}