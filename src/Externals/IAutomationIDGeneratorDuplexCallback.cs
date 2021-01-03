using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace AutomationIDGeneratorWCFServerInterfaces
{
    public interface IAutomationIDGeneratorDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageFromService(string message);

        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(ClientType sender, MessageType message, string comment);
    }
}
