using Microsoft.AspNetCore.Identity;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueueForChildren.Data.Identity
{
    public class User : IdentityUser
    { 
        public long? ParentId { get; set; }
        public virtual Parent? Parent { get; set; }

        public string Name { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; }
    }
}
