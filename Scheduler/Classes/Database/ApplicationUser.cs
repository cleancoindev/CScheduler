﻿using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CScheduler.Classes.Database
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Activated")]
        public bool IsActivated { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Public key")]
        public string PublicKey { get; set; }

        [Display(Name = "Private key")]
        public string PrivateKey { get; set; }

        [Display(Name = "API key")]
        public string ApiKey { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}