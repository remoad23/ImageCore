using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ImageCore.Controllers
{
    public class ContactController : Controller
    {
        private ContextDb Context;
        private UserManager<UserModel> UserManager;
        
        public ContactController(ContextDb context,UserManager<UserModel> userManager)
        {
            Context = context;
            UserManager = userManager;
        }
        
        [Authorize(Roles="Admin,User")]
        public async Task<IActionResult> Index()
        {
            string id = UserManager.GetUserId(User);
            string currentUserName = UserManager.GetUserName(User);
            
            var contacts = Context.Contact.
                Where(u => u.ContactUserId.Equals(id) && u.RequestValidated )
                .Join(
                    Context.Users,
                    model => model.UserId,
                    users => users.Id,
                    (contact, users) => new
                    {
                        Username = users.UserName,
                        UserId = users.Id,
                        ContactUserId = contact.ContactUserId,
                        ContactId = contact.ContactId
                    }
                )
                .ToList();
            
            var contacts2 = Context.Contact.
                Where(u => u.UserId.Equals(id) && u.RequestValidated )
                .Join(
                    Context.Users,
                    model => model.ContactUserId,
                    users => users.Id,
                    (contact, users) => new
                    {
                        Username = users.UserName,
                        UserId = users.Id,
                        ContactUserId = contact.ContactUserId,
                        ContactId = contact.ContactId
                    }
                )
                .ToList();

            List<string> contactuserids = new List<string>();
            List<string> usernames = new List<string>();
            List<int> contactIds = new List<int>();

            foreach (var contact in contacts)
            {
                if (id.Equals(contact.ContactUserId)) 
                    contactuserids.Add(contact.UserId);
                else 
                    contactuserids.Add(contact.ContactUserId);
                
                usernames.Add(contact.Username);
                contactIds.Add(contact.ContactId);
            }
            
            foreach (var contact in contacts2)
            {
                if (id.Equals(contact.ContactUserId))
                    contactuserids.Add(contact.UserId);
                else
                    contactuserids.Add(contact.ContactUserId);
                
                usernames.Add(contact.Username);
                contactIds.Add(contact.ContactId);
            }
            
            ViewData["RequestScheme"] = Request.Scheme;

            ContactViewModel contactvm = new ContactViewModel
            {
                Usernames = usernames,
                UserIds =  contactuserids,
                ContactIds =  contactIds
            };
            
            return View(contactvm);
        }

        [Authorize(Roles="Admin,User")]
        public IActionResult ContactRequests()
        {
            string id = UserManager.GetUserId(User);
            var contacts = Context.Contact
                .Where(e => !e.RequestValidated && e.ContactUserId.Equals(id))
                .Join(
                    Context.Users,
                    model => model.UserId,
                    users => users.Id,
                    (contact, users) => new
                    {
                        Username = users.UserName,
                        ContactId = contact.ContactId,
                        UserId = contact.UserId
                    }
                )
                .ToList();

            List<int> contactids = new List<int>();
            List<string> usernames = new List<string>();
            List<string> userids = new List<string>();

            foreach (var contact in contacts)
            {
                contactids.Add(contact.ContactId);
                usernames.Add(contact.Username);
                userids.Add(contact.UserId);
            }
            
            Console.WriteLine("contact:" +contactids.Count() + "  usernames:" + usernames.Count());


            ViewData["RequestScheme"] = Request.Scheme;


                ContactRequestViewModel contactRequest = new ContactRequestViewModel
            {
                Usernames = usernames,
                ContactIds =  contactids,
                UserIds = userids
            };
           
            return View(contactRequest);
        }


        [Authorize(Roles="Admin,User")]
        public async Task<IActionResult> ContactFind([FromQuery]string query)
        {
            string id = UserManager.GetUserId(User);
            
            List<UserModel> users = null;

            users = Context.Users
                .Where(e => !e.Id.Equals(id) && e.UserName.ToLower().Equals(query.ToLower()) )
                .Take(10)
                .ToList();

            if (!users.Any())
            {
                // user die nicht miteinander befreundet sind
                users = Context.Users
                    .Where(e => !e.Id.Equals(id) && e.UserName.ToLower().Contains(query.ToLower()))
                    .Take(10)
                    .ToList();
            } 
            

            List<ContactFindViewModel> queriedUser = new List<ContactFindViewModel>();
            
            foreach(var user in users)
            {
                var possibleContact = Context.Contact
                    .Where(c => c.UserId.Equals(user.Id) && c.ContactUserId.Equals(id) 
                                || c.UserId.Equals(id) && c.ContactUserId.Equals(user.Id))
                    .SingleOrDefault();

                if (possibleContact is not null)
                {
                    queriedUser.Add(new ContactFindViewModel
                    {
                        UserId = user.Id,
                        Username = user.UserName,
                        IsContact = true,
                        ContactRequestValidated = possibleContact.RequestValidated ? true : false,
                    });
                }
                else
                {
                    queriedUser.Add(new ContactFindViewModel
                    {
                        UserId = user.Id,
                        Username = user.UserName,
                        IsContact = false,
                        ContactRequestValidated = false,
                    });
                }
            }

            ViewData["RequestScheme"] = Request.Scheme;

            return View(queriedUser);
        }

        [HttpGet]
        [Authorize(Roles="Admin,User")]
        public IActionResult QueryContacts([FromQuery] string query)
        {
            string id = UserManager.GetUserId(User);

            QueryUserViewModel queriedUsers = new QueryUserViewModel();

            var possibleContact = Context.Contact
                .Where(c => c.UserId.Equals(id))
                .Join(
                    Context.Users,
                    model => model.ContactUserId,
                    users => users.Id,
                    (contact, users) => new
                    {
                        Username = users.UserName,
                        UserId = contact.ContactUserId,
                        RequestValidated = contact.RequestValidated
                    }
                );
            
            var possibleContact2 = Context.Contact
                .Where(c => c.ContactUserId.Equals(id))
                .Join(
                    Context.Users,
                    model => model.UserId,
                    users => users.Id,
                    (contact, users) => new
                    {
                        Username = users.UserName,
                        UserId = contact.UserId,
                        RequestValidated = contact.RequestValidated
                    }
                );


            if (possibleContact.Any())
            {
                foreach (var contact in possibleContact)
                {
                    if (!contact.RequestValidated) continue;
                    if (contact.Username.Contains(query))
                    {
                        Console.WriteLine(contact.Username);
                        queriedUsers.UserIds.Add(contact.UserId);
                        queriedUsers.Usernames.Add(contact.Username);
                    }
                }
            }
            
            if (possibleContact2.Any())
            {
                foreach (var contact in possibleContact2)
                {
                    if (!contact.RequestValidated) continue;
                    if (contact.Username.Contains(query))
                    {
                        Console.WriteLine(contact.Username);
                        queriedUsers.UserIds.Add(contact.UserId);
                        queriedUsers.Usernames.Add(contact.Username);
                    }
                    
                }
            }


            ViewData["RequestScheme"] = Request.Scheme;

            return Ok(JsonConvert.SerializeObject(queriedUsers));
        }

        [Authorize(Roles="Admin,User")]
        [HttpPut]
        [Route("Contact/Store")]
        public IActionResult AcceptRequest([FromQuery]int contactId)
        {
            var contact = Context.Contact.Find(contactId);
            contact.RequestValidated = true;
            Context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeclineRequest([FromQuery] int contactId)
        {
            ContactModel user = Context.Contact.Find(contactId);
            Context.Contact.Remove(user);
            Context.SaveChanges();
            return Ok();
        }



        [Authorize(Roles="Admin,User")]
        [HttpPost]
        [Route("Contact/Store")]
        public IActionResult Store([FromQuery]string contactId)
        {
            string id = UserManager.GetUserId(User);

            ContactModel contact = new ContactModel
            {
                UserId = id,
                ContactUserId = contactId,
                RequestValidated = false
            };
            Context.Contact.Add(contact);
            Context.SaveChanges();
       
            return Json( Url.Action("Destroy", "Contact", new {contactId = contact.ContactId}, Request.Scheme));
        }

        [Authorize(Roles="Admin,User")]
        [HttpDelete]
        public IActionResult Destroy([FromQuery] int contactId)
        {
            string id = UserManager.GetUserId(User);
            ContactModel user = Context.Contact.Find(contactId);
            string contactToAddId = user.UserId.Equals(id) ? user.ContactUserId : user.UserId;
            Context.Contact.Remove(user);
            Context.SaveChanges();
            return Json( Url.Action("Store", "Contact", new {contactId = contactToAddId}, Request.Scheme),
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                });
        }
    }
}
