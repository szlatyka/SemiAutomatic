using AutomationIDGeneratorWCFServerInterfaces;
using SemiAuto.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SemiAuto.Communicators
{
    public class MasterServiceHandler
    {
        private ServiceHost m_Host = null;

        public void Start()
        {
            try
            {
                this.m_Host = new ServiceHost(typeof(MasterService), new Uri(Constants.TEST_AUTOMATION_SERIVICE_ENDPOINT));

                WSDualHttpBinding binding = new WSDualHttpBinding();
                binding.OpenTimeout = TimeSpan.MaxValue;
                binding.CloseTimeout = TimeSpan.MaxValue;
                binding.SendTimeout = TimeSpan.MaxValue;
                binding.ReceiveTimeout = TimeSpan.MaxValue;

                this.m_Host.AddServiceEndpoint(typeof(IAutomationIDGeneratorServiceContract), binding, "");

                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
                this.m_Host.Description.Behaviors.Add(serviceBehavior);

                this.m_Host.Open();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                this.m_Host?.Abort();
            }
            finally
            {
            }
        }

        public void Stop()
        {
            this.m_Host?.Close();
        }

    }
}
