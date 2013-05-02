using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Bng.EL2SL.Base;
using Bng.EL2SL.Configurator.Controls;

namespace Bng.EL2SL.Configurator
{
    public partial class FormMain : Form
    {
        private El2SlConfig _config;
        public FormMain()
        {
            _config = new El2SlConfig();
            InitializeComponent();
            
            #region Fill Grid
            
            #region Preparing rows labels

            foreach (EventLogEntryType et in Enum.GetValues(typeof (EventLogEntryType))) // columns
            {
                tlpJournals.ColumnCount++;
                VerticalLabel jLevels = new VerticalLabel();
                jLevels.Text = et.ToString();
                tlpJournals.Controls.Add(jLevels, tlpJournals.ColumnCount - 1, 0);
            }

            #endregion

            foreach (EventLog eventLog in EventLog.GetEventLogs()) // rows
            {
                #region Preparing row labels

                Label lJournals = new Label();
                lJournals.TextAlign = ContentAlignment.MiddleRight;
                lJournals.Text = eventLog.LogDisplayName;
                lJournals.AutoSize = true;
                tlpJournals.Controls.Add(lJournals, 0, tlpJournals.RowCount);

                #endregion

                int currentColumn = 1;
                tlpJournals.RowCount++;
                foreach (EventLogEntryType eventLogEntryType in Enum.GetValues(typeof (EventLogEntryType))) // columns
                {
                    tlpJournals.Controls.Add(new JournalLevelTextBox(_config, eventLog, eventLogEntryType), currentColumn,
                                             tlpJournals.RowCount - 1);
                    currentColumn++;
                }
                
            }

            //tlpJournals.RowCount--;
            //tlpJournals.ColumnCount--;

            #endregion

            tbHost.Text = _config.Host;
            nudPort.Value = _config.Port;

        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
            formAbout.Dispose();
        }

        private void tbHost_TextChanged(object sender, EventArgs e)
        {
            _config.Host = tbHost.Text;
        }

        private void nudPort_ValueChanged(object sender, EventArgs e)
        {
            _config.Port = (int)nudPort.Value;
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = @"reg files (*.reg)|*.reg|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _config.Export(dlg.FileName);
            }
        }

    }
}