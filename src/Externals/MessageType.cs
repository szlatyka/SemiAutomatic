using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationIDGeneratorWCFServerInterfaces
{
    public enum MessageType
    {
        GenerateAutomationID = 0,
        SystemInitCompleted = 1,
        DisconnectRequest = 2,
        TextMessage = 3
    }
}
