using AutomationIDGeneratorWCFServerInterfaces;
using SemiAuto.Externals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SemiAuto.Communicators
{
    public class TestClient : IAutomationIDGeneratorDuplexCallback
    {
        private IAutomationIDGeneratorServiceContract m_Service;

        public void Init()
        {
            InstanceContext context = new InstanceContext(this);
            DuplexChannelFactory<IAutomationIDGeneratorServiceContract> factory = new DuplexChannelFactory<IAutomationIDGeneratorServiceContract>(context, new WSDualHttpBinding(), new EndpointAddress(Constants.TEST_AUTOMATION_SERIVICE_ENDPOINT));

            this.m_Service = factory.CreateChannel();
            this.m_Service.Init(ClientType.TestSystem);
        }

        public void RequestGenerateIds()
        {
            this.m_Service.Message(ClientType.TestSystem, MessageType.GenerateAutomationID, "");
        }

        public void MessageFromService(string message)
        {
            Debug.WriteLine(message);
        }

        public void ReceiveMessage(ClientType sender, MessageType message, string comment)
        {
            Debug.WriteLine(comment);
        }
    }
}
