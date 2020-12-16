using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace AutomationIDGeneratorWCFServerInterfaces
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IAutomationIDGeneratorDuplexCallback))]
    public interface IAutomationIDGeneratorServiceContract
    {
        [OperationContract(IsOneWay = true)]
        void Init(ClientType client);

        [OperationContract(IsOneWay = true)]
        void Close(ClientType sender);

        [OperationContract(IsOneWay = true)]
        void Message(ClientType sender, MessageType mesasge, string comment);
    }
}
