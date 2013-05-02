using System;
using System.Diagnostics;
using Bng.EL2SL.Base;
using System.Collections.Generic;

namespace Bng.EL2SL.Service
{
    class SendersManager
    {
        private El2SlConfig _config;
        private EventLog _localLog;
        private List<SyslogSender> _senders;

        public SendersManager()
        {
            new EventLogPermission(EventLogPermissionAccess.Administer, ".").Demand();
            _localLog = new EventLog("Application", ".", "el2slservice");
            _senders = new List<SyslogSender>();
            _config = new El2SlConfig();
        }

        public void Start()
        {
            _localLog.WriteEntry("el2sl starting", EventLogEntryType.Information);
            try
            {
                foreach (EventLog eventLog in EventLog.GetEventLogs())
                {
                    _senders.Add(new SyslogSender(_config, eventLog, _localLog));
                }
            }
            catch (Exception e)
            {
                _localLog.WriteEntry(e.ToString(), EventLogEntryType.Error);
            }
            _localLog.WriteEntry("el2sl started", EventLogEntryType.Information);
        }

        public void Stop()
        {
            _localLog.WriteEntry("el2sl stopping", EventLogEntryType.Information);
            try
            {
                foreach (var syslogSender in _senders)
                {
                    syslogSender.Dispose();
                }
            }
            catch (Exception e)
            {
                _localLog.WriteEntry(e.ToString(), EventLogEntryType.Error);
            }
            
            _localLog.WriteEntry("el2sl stopped", EventLogEntryType.Information);
        }
    }
}
