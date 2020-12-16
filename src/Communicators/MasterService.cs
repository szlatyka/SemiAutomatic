using AutomationIDGeneratorWCFServerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Communicators
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class MasterService : IAutomationIDGeneratorServiceContract
    {
        private List<ClientInfo> m_Clients = new List<ClientInfo>();

        public void Init(ClientType clientType)
        {
            IAutomationIDGeneratorDuplexCallback callback = OperationContext.Current.GetCallbackChannel<IAutomationIDGeneratorDuplexCallback>();

            if (this.m_Clients.FirstOrDefault(q => q.Callback == callback) == null)
            {
                this.m_Clients.Add(new ClientInfo(clientType, callback));
                this.SendMessageToClients($"Client connected: {clientType}");
            }
        }

        public void Close(ClientType clientType)
        {
            IAutomationIDGeneratorDuplexCallback callback = OperationContext.Current.GetCallbackChannel<IAutomationIDGeneratorDuplexCallback>();

            ClientInfo client = m_Clients.FirstOrDefault(q => q.Callback == callback);
            if (client != null)
            {
                this.m_Clients.Remove(client);
                this.SendMessageToClients($"Client diconnected: {clientType}");
            }
        }

        public void Message(ClientType client, MessageType message, string comment)
        {
            foreach (var clientInfo in this.m_Clients)
            {
                try
                {
                    clientInfo.Callback.ReceiveMessage(client, message, comment);
                }
                catch
                {
                    //
                }
            }
        }

        private void SendMessageToClients(string message)
        {
            foreach (var clientInfo in this.m_Clients)
            {
                try
                {
                    clientInfo.Callback.MessageFromService(message);
                }
                catch
                {
                    //
                }
            }
        }
    }
}
