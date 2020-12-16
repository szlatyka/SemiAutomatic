using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationIDGeneratorWCFServerInterfaces;
using System.ServiceModel;
using System.Diagnostics;

namespace Externals
{
    public class SamWcfTestClient : IAutomationIDGeneratorDuplexCallback
    {
        private IAutomationIDGeneratorServiceContract m_Service;

        public void Init()
        {
            InstanceContext context = new InstanceContext(this);
            DuplexChannelFactory<IAutomationIDGeneratorServiceContract> factory = new DuplexChannelFactory<IAutomationIDGeneratorServiceContract>(context, new WSDualHttpBinding(), new EndpointAddress("http://localhost:8888/AutomationIDGenerator"));

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
