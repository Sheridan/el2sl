using Bng.Configs.Registry;
using System.Diagnostics;

namespace Bng.EL2SL.Base
{
    public class El2SlConfig
    {
        private RegistryConfig _registry;
        public El2SlConfig()
        {
            _registry = new RegistryConfig(@"software\el2sl\");
        }

        public int Port
        {
            get { return _registry.Read ("connection", "port", 514  ); }
            set {        _registry.Write("connection", "port", value); }
        }

        public string Host
        {
            get { return _registry.Read ("connection", "host", "logger"); }
            set {        _registry.Write("connection", "host", value   ); }
        }

        public bool LoggingEnabled(EventLog eventLog, EventLogEntryType eventLogEntryType)
        {
            return _registry.Read(eventLog.Log, eventLogEntryType.ToString() + "Enabled", false);
        }

        public void SetLoggingEnabled(EventLog eventLog, EventLogEntryType eventLogEntryType, bool value)
        {
            _registry.Write(eventLog.Log, eventLogEntryType.ToString() + "Enabled", value);
        }

        public SyslogFacilityPriorityPair LoggingFacilityPriority(EventLog eventLog, EventLogEntryType eventLogEntryType)
        {
            return SyslogFacilityPriorityPair.ConvertIntToPair(_registry.Read(eventLog.Log, eventLogEntryType.ToString() + "FP", SyslogFacilityPriorityPair.ConvertPairToInt(16,6)));
        }

        public void SetLoggingFacilityPriority(EventLog eventLog, EventLogEntryType eventLogEntryType, SyslogFacilityPriorityPair sfpp)
        {
            _registry.Write(eventLog.Log, eventLogEntryType.ToString() + "FP", sfpp.AsInt());
        }
    }
}
