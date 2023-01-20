using System;
using System.IO;
using System.Windows.Forms;

namespace wxDCS_Injector.Presentation
{
    interface ILog
    {
        frmLog Form { get; }

        void Log(string txt);

        void Success(string txt);

        void Error(string txt, Exception ex);

        bool SaveLog { get; set; }
    }

    partial class frmLog : Form, ILog
    {
        public frmLog() => InitializeComponent();

        public frmLog Form => this;

        void btnClose_Click(object sender, EventArgs e) => Hide();

        void btnClear_Click(object sender, EventArgs e) => txtLog.Text = string.Empty;

        public void Log(string txt)
        {
            txtLog.AppendText(Environment.NewLine + txt);

            if (SaveLog)
            {
                try
                {
                    File.WriteAllText("wxDCS_Injector.log", txtLog.Text);
                }
                catch (Exception ex)
                {
                    Error("Error writing to log file", ex);
                }
            }
        }

        public void Success(string txt)
        {
            MessageBox.Show($"{txt}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Log(txt);
        }

        public void Error(string txt, Exception ex)
        {
            MessageBox.Show(txt, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Log($"Error: {txt}{Environment.NewLine}Exception: {ex}");
        }

        public bool SaveLog { get; set; }
    }
}
