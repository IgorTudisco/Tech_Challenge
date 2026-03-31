using CloudGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudGame.Application.Interfaces.Security
{
    public interface ITokenService
    {
        string GetToken(User user);
    }
}
