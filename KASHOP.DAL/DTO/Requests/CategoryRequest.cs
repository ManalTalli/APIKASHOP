using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Requests
{
    public class CategoryRequest
    {
        public bool Status { get; set; }
        public List<CategoryTranslation> Translations { get; set; } = new();

    }
}
