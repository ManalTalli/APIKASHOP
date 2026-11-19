using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL
{
    public interface ITokenService
    {
        Task<string> GenerateJwt(ApplicationUser user);
    }
}
