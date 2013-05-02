using System;
using System.ServiceProcess;

namespace Bng.EL2SL.Service
{
    public class Service : ServiceBase
    {
        private SendersManager _sManager;
        public Service()
        {
            AutoLog = true;
            CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            _sManager = new SendersManager();
            _sManager.Start();
        }

        protected override void OnStop()
        {
            try
            {
                _sManager.Stop();
                _sManager = null;
                GC.Collect();
            }
            catch (SystemException e)
            {
            }
        }
    }
}
