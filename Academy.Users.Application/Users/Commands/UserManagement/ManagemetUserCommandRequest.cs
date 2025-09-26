using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Users.Application.Users.Commands.UserManagement;

public class ManagemetUserCommandRequest
{
    public string email { get; set; }
    public int status { get; set; }
    public string reason { get; set; }
    
}
