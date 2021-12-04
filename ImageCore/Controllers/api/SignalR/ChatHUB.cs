using System;
using System.IO;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel;
using ImageCore.Services;
using ImageCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ImageCore.Controllers.api.SignalR
{
    public class ChatHUB : Hub
    {
        private IProjectAuth ProjectAuth;
        private ContextDb ContextDb;

        public ChatHUB(IProjectAuth projectAuth, ContextDb contextDb)
        {
            ContextDb = contextDb;
            ProjectAuth = projectAuth;
        }

        public async Task RegisterSession(string projectId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, projectId);
            await Clients.Group("test").SendAsync("RegisterSession", "registered");
        }

        public async Task Send(string message,string projectId)
        {
            await Clients.Group(projectId).SendAsync("message", message);
        }

        public async Task NotifyNewImageUploaded(string imageId,string projectId)
        {
            await Clients.Group(projectId).SendAsync("NewImageUploaded", imageId);
        }

        /*public Task SaveProject(string data, string projectId)
        {

            var projectData = JsonSerializer.Serialize(data);
            Console.WriteLine(projectData);

            ImageModel model = new ImageModel()
            {
                FileName = "",
                Path = "",
            };

            //ContextDb.Add(model);
            //ContextDb.SaveChanges();

            return null;

        }*/

    }
}