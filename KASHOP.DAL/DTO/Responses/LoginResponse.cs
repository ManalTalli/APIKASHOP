using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExipesAt { get; set; }
    }
}
