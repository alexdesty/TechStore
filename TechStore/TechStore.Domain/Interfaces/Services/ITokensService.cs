using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Interfaces.Services;

public interface ITokensService
{
    (string token, DateTime expiration) GenerateAccessToken();
}
