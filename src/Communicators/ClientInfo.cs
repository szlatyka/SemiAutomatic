using AutomationIDGeneratorWCFServerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicators
{
    public class ClientInfo
    {
        public ClientType ClientType { get; }
        public IAutomationIDGeneratorDuplexCallback Callback { get; }

        public ClientInfo(ClientType clientType, IAutomationIDGeneratorDuplexCallback callback)
        {
            ClientType = clientType;
            Callback = callback;
        }
    }
}
