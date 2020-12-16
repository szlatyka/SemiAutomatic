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

    public enum ClientType
    {
        Server = 1,
        TestSystem = 1,
        SAM = 2,
    }

    public enum MessageType
    {
        GenerateAutomationID = 0,
        SystemInitCompleted = 1,
        DisconnectRequest = 2,
        TextMessage = 3
    }
}
