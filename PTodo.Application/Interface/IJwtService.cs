using ETodo.Domain.Dto;
using MTodo.Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTodo.Application.Interface
{
    public interface IJwtService
    {
        Tokens Authenticate(AppTodoItem user);
    }
}
