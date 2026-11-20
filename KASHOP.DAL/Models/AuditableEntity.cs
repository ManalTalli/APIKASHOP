using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Models
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreateById { get; set; }
        public ApplicationUser? CreateBy { get; set; }       
        public DateTime UpdatedAt { get; set; }
        public string? UpdateById { get; set; }
        public ApplicationUser? UpdateBy { get; set; }

    }
}
