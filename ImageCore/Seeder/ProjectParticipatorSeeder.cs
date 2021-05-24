using System.Collections.Generic;
using ImageCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class ProjectParticipatorSeeder : ISeeder
    {
        public static void Seed(ModelBuilder modelBuilder,List<UserModel> users)
        {
            ProjectParticipatorModel projectparticipator1 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 1,
                ProjectId = 1,
                UserId = users[0].Id,
            };
            
            ProjectParticipatorModel projectparticipator2 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 2,
                ProjectId = 1,
                UserId = users[1].Id,
            };
            
            ProjectParticipatorModel projectparticipator3 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 3,
                ProjectId = 1,
                UserId = users[2].Id,
            };
            
            ProjectParticipatorModel projectparticipator4 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 4,
                ProjectId = 2,
                UserId = users[1].Id,
            };
            
            ProjectParticipatorModel projectparticipator5 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 5,
                ProjectId = 2,
                UserId = users[3].Id,
            };
            
            ProjectParticipatorModel projectparticipator6 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 6,
                ProjectId = 3,
                UserId = users[0].Id,
            };
            
            ProjectParticipatorModel projectparticipator7 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 7,
                ProjectId = 4,
                UserId = users[0].Id,
            };
            
            ProjectParticipatorModel projectparticipator8 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 8,
                ProjectId = 4,
                UserId = users[1].Id,
            };
            
            ProjectParticipatorModel projectparticipator9 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 9,
                ProjectId = 4,
                UserId = users[2].Id,
            };
            
            ProjectParticipatorModel projectparticipator10 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 10,
                ProjectId = 4,
                UserId = users[3].Id,
            };
            
            ProjectParticipatorModel projectparticipator11 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 11,
                ProjectId = 5,
                UserId = users[0].Id,
            };
            
            ProjectParticipatorModel projectparticipator12 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 12,
                ProjectId = 5,
                UserId = users[1].Id,
            };
            
            ProjectParticipatorModel projectparticipator13 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 13,
                ProjectId = 5,
                UserId = users[2].Id,
            };
            
            ProjectParticipatorModel projectparticipator14 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 14,
                ProjectId = 5,
                UserId = users[3].Id,
            };
            
            ProjectParticipatorModel projectparticipator15 = new ProjectParticipatorModel
            {
                ProjectParticipatorId = 15,
                ProjectId = 5,
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