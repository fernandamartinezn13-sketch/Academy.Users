using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Users.Application.Users.Commands.UserManagement;

public class GetManagementUserCommandResponse
{

    public string? email { get; set; }
    public string status { get; set; }
    public string message { get; set; }
    public DateTime timestamp { get; set; }
}
