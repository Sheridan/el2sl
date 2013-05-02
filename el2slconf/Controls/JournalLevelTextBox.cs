using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Bng.EL2SL.Base;

namespace Bng.EL2SL.Configurator.Controls
{
    public partial class JournalLevelTextBox : UserControl
    {
        private EventLogEntryType _eventLogEntryType;
        private EventLog _eventLog;
        private El2SlConfig _config;

        public JournalLevelTextBox(El2SlConfig config, EventLog eventLog, EventLogEntryType eventLogEntryType)
        {
            _eventLog = eventLog;
            _eventLogEntryType = eventLogEntryType;
            _config = config;

            InitializeComponent();

            UpdateControls();
        }

        private void OnClick(object sender, EventArgs eventArgs)
        {
            FormJT2SL formJt2Sl = new FormJT2SL(_config, _eventLog, _eventLogEntryType);
            if (formJt2Sl.ShowDialog() == DialogResult.OK)
            {
                _config.SetLoggingEnabled(_eventLog, _eventLogEntryType, formJt2Sl.LoggingEnabled());
                if (formJt2Sl.LoggingEnabled())
                {
                    _config.SetLoggingFacilityPriority(_eventLog, _eventLogEntryType, formJt2Sl.FPPair());
                }
                UpdateControls();
            }
            formJt2Sl.Dispose();
        }

        public void UpdateControls()
        {
            checkBox.Checked = _config.LoggingEnabled(_eventLog, _eventLogEntryType);
            textBox.Enabled = checkBox.Checked;
            textBox.Text = _config.LoggingFacilityPriority(_eventLog, _eventLogEntryType).AsString();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            string[] splitted = textBox.Text.Trim().Split(':');
            int facility = System.Convert.ToInt32(splitted[0]);
            int priority = System.Convert.ToInt32(splitted[1]);
            if (facility > 23) { facility = 23; }
            if (facility < 0)  { facility = 0 ; }
            if (priority > 7)  { priority = 7 ; }
            if (priority < 0)  { priority = 0 ; }
            SyslogFacilityPriorityPair pair = new SyslogFacilityPriorityPair(facility, priority);
            _config.SetLoggingFacilityPriority(_eventLog, _eventLogEntryType, pair);
            UpdateControls();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            _config.SetLoggingEnabled(_eventLog, _eventLogEntryType, checkBox.Checked);
            UpdateControls();
        }
    }
}
