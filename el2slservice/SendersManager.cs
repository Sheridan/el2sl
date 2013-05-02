using System.Diagnostics;
using Bng.EL2SL.Base;

namespace Bng.EL2SL.Service
{
    class SendersManager
    {
        private El2SlConfig _config;
        private EventLog _localLog;

        public SendersManager()
        {
            new EventLogPermission(EventLogPermissionAccess.Administer, ".").Demand();
            _localLog = new EventLog("Application", ".", "el2slservice");
        }

        public void Start()
        {
            _localLog.WriteEntry("Service el2sl starting", EventLogEntryType.Information);
            _config = new El2SlConfig();
            foreach (EventLog eventLog in EventLog.GetEventLogs())
            {
                new SyslogSender(_config, eventLog, _localLog);
            }
            _localLog.WriteEntry("Service el2sl started", EventLogEntryType.Information);
        }

        public void Stop()
        {
            _localLog.WriteEntry("Service el2sl stopped", EventLogEntryType.Information);
        }
    }
}
