using System;
using System.Windows.Forms;
using System.Diagnostics;
using Bng.EL2SL.Base;

namespace Bng.EL2SL.Configurator
{
    public partial class FormJT2SL : Form
    {
        private static readonly string[] SFacilityes = new string[]
            {
                "[00] kernel messages",
                "[01] user-level messages",
                "[02] mail system",
                "[03] system daemons",
                "[04] security/authorization messages",
                "[05] messages generated internally by syslogd",
                "[06] line printer subsystem",
                "[07] network news subsystem",
                "[08] UUCP subsystem",
                "[09] clock daemon",
                "[10] security/authorization messages",
                "[11] FTP daemon",
                "[12] NTP subsystem",
                "[13] log audit",
                "[14] log alert",
                "[15] clock daemon",
                "[16] local use 0  (local0)",
                "[17] local use 1  (local1)",
                "[18] local use 2  (local2)",
                "[19] local use 3  (local3)",
                "[20] local use 4  (local4)",
                "[21] local use 5  (local5)",
                "[22] local use 6  (local6)",     
                "[23] local use 7  (local7)"
            };

        private static readonly string[] SPriorityes = new string[]
            {
                "[0] Emergency: system is unusable",
                "[1] Alert: action must be taken immediately",
                "[2] Critical: critical conditions",
                "[3] Error: error conditions",
                "[4] Warning: warning conditions",
                "[5] Notice: normal but significant condition",
                "[6] Informational: informational messages",
                "[7] Debug: debug-level messages"
            };

        private EventLog _eventLog;
        private EventLogEntryType _eventLogEntryType;
        private SyslogFacilityPriorityPair _pair;

        public FormJT2SL(El2SlConfig config, EventLog eventLog, EventLogEntryType eventLogEntryType)
        {
            InitializeComponent();
            _eventLog = eventLog;
            _eventLogEntryType = eventLogEntryType;

            lInfo.Text = string.Format("{0}:{1}", _eventLog.LogDisplayName, eventLogEntryType.ToString());

            cbEnabled.Checked = config.LoggingEnabled(eventLog, eventLogEntryType);
            cbEnabled_CheckedChanged(cbEnabled, new EventArgs());

            #region Grid fill

            #region Preparing Columns headers

            for ( int fh = 0; fh < SFacilityes.Length; fh++ ) // columns
            {
                tlpFacilityPriority.ColumnCount++;
                RotatedLabel lColumn = new RotatedLabel();
                lColumn.NewText = SFacilityes[fh];
                lColumn.RotateAngle = -90;
                tlpFacilityPriority.Controls.Add(lColumn, tlpFacilityPriority.ColumnCount - 1, 0);
            }

            #endregion

            for (int pt = 0; pt < SPriorityes.Length; pt++ ) // rows
            {
                #region Preparing row labels

                Label lRow = new Label();
                lRow.Text = SPriorityes[pt];
                lRow.AutoSize = true;
                tlpFacilityPriority.Controls.Add(lRow, 0, tlpFacilityPriority.RowCount);

                #endregion

                int currentColumn = 1;
                tlpFacilityPriority.RowCount++;
                for (int ft = 0; ft < SFacilityes.Length; ft++) // columns
                {
                    int fpNum = SyslogFacilityPriorityPair.ConvertPairToInt(ft, pt);
                    RadioButton bSelection = new RadioButton();
                    bSelection.Width = 16;
                    bSelection.Tag = fpNum;
                    bSelection.Checked = config.LoggingFacilityPriority(eventLog, eventLogEntryType).AsInt() ==
                                         fpNum;
                    bSelection.CheckedChanged += BSelectionOnCheckedChanged;
                    tlpFacilityPriority.Controls.Add(bSelection, currentColumn, tlpFacilityPriority.RowCount - 1);
                    currentColumn++;
                }

            }

            #endregion

        }

        private void BSelectionOnCheckedChanged(object o, EventArgs eventArgs)
        {
            _pair = new SyslogFacilityPriorityPair((int)(((RadioButton) o).Tag));
            DialogResult = DialogResult.OK;
        }

        public SyslogFacilityPriorityPair FPPair()
        {
            return _pair;
        }

        public bool LoggingEnabled()
        {
            return cbEnabled.Checked;
        }

        private void cbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            tlpFacilityPriority.Enabled = cbEnabled.Checked;
            if (!cbEnabled.Checked) { DialogResult = DialogResult.OK; }
        }
    }
}
