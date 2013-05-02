using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Globalization;
using Bng.EL2SL.Base;

namespace Bng.EL2SL.Service
{
    
    class SyslogSender
    {
        private UdpClient _udpClient;
        private EventLog _eventLog;
        private EventLog _localLog;
        private El2SlConfig _config;
        private UTF8Encoding _encoding;
        private CultureInfo _enUS;

        public SyslogSender(El2SlConfig config, EventLog el, EventLog localLog)
        {
            _enUS = new System.Globalization.CultureInfo("en-US");
            _localLog = localLog;
            _config = config;
            _eventLog = el;
            _encoding = new UTF8Encoding();
            _eventLog.EnableRaisingEvents = true;
            _eventLog.EntryWritten += new EntryWrittenEventHandler(eventLog_EntryWritten);
            _udpClient = new UdpClient();
            _udpClient.DontFragment = false;
            _udpClient.Connect(_config.Host, _config.Port);
            try
            {
                if (_udpClient.Client.Connected)
                {
                    _localLog.WriteEntry("el2slservice connected to syslog", EventLogEntryType.Information);
                }
            }
            catch (SocketException se)
            {
                _localLog.WriteEntry("el2slservice can not connect to syslog. Reason: " + se.Message, EventLogEntryType.Error);
            }
        }

        void eventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
            //<165>Aug 24 05:34:00 CST 1987 mymachine myproc[10]: %% It's
            //time to make the do-nuts.  %%  Ingredients: Mix=OK, Jelly=OK #
            //Devices: Mixer=OK, Jelly_Injector=OK, Frier=OK # Transport:
            //Conveyer1=OK, Conveyer2=OK # %%
            if (_config.LoggingEnabled(_eventLog, e.Entry.EntryType))
            {
                string logString = _config.LoggingFacilityPriority(_eventLog, e.Entry.EntryType).AsSyslogHead();
                logString += e.Entry.TimeGenerated.ToString("MMM dd HH:mm:ss ", _enUS);
                logString += e.Entry.MachineName.Trim().Replace(' ', '_');
                logString += " " + e.Entry.Source.Trim().Replace(' ', '_') + ": ";
                logString += e.Entry.Message.Trim();
                byte[] data = _encoding.GetBytes(logString);
                _udpClient.Send(data, data.Length);
            }
        }
    }
}
