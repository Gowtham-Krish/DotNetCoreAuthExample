using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp
{
    public interface ITokenManager
    {
        string GenerateJWTToken(IdentityUser userInfo, string roles);
    }
}
