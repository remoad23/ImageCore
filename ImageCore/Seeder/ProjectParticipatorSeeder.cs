using System;
using System.Collections.Generic;
using ImageCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class ProjectParticipatorSeeder : ISeeder
    {
        public static void Seed(ModelBuilder modelBuilder,List<UserModel> users,List<ProjectModel> projects)
        {
            ProjectParticipatorModel projectparticipator1 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[0].ProjectId,
                UserId = users[0].Id,
            };
            
            ProjectParticipatorModel projectparticipator2 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[0].ProjectId,
                UserId = users[1].Id,
            };
            
            ProjectParticipatorModel projectparticipator3 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[0].ProjectId,
                UserId = users[2].Id,
            };
            
            ProjectParticipatorModel projectparticipator4 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[1].ProjectId,
                UserId = users[1].Id,
            };
            
            ProjectParticipatorModel projectparticipator5 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[1].ProjectId,
                UserId = users[3].Id,
            };
            
            ProjectParticipatorModel projectparticipator6 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[2].ProjectId,
                UserId = users[0].Id,
            };
            
            ProjectParticipatorModel projectparticipator7 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[3].ProjectId,
                UserId = users[0].Id,
            };
            
            ProjectParticipatorModel projectparticipator8 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[3].ProjectId,
                UserId = users[1].Id,
            };
            
            ProjectParticipatorModel projectparticipator9 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[3].ProjectId,
                UserId = users[2].Id,
            };
            
            ProjectParticipatorModel projectparticipator10 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[3].ProjectId,
                UserId = users[3].Id,
            };
            
            ProjectParticipatorModel projectparticipator11 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[4].ProjectId,
                UserId = users[0].Id,
            };
            
            ProjectParticipatorModel projectparticipator12 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[4].ProjectId,
                UserId = users[1].Id,
            };
            
            ProjectParticipatorModel projectparticipator13 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[4].ProjectId,
                UserId = users[2].Id,
            };
            
            ProjectParticipatorModel projectparticipator14 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[4].ProjectId,
                UserId = users[3].Id,
            };
            
            ProjectParticipatorModel projectparticipator15 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projects[4].ProjectId,
                UserId = users[4].Id,
            };

            modelBuilder.Entity<ProjectParticipatorModel>().HasData(
                projectparticipator1,
                projectparticipator2,
                projectparticipator3,
                projectparticipator4,
                projectparticipator5,
                projectparticipator6,
                projectparticipator7,
                projectparticipator8,
                projectparticipator9,
                projectparticipator10,
                projectparticipator11,
                projectparticipator12,
                projectparticipator13,
                projectparticipator14,
                projectparticipator15
            );

        }
    }
}