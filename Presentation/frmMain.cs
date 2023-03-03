using System;
using System.Linq;
using System.Windows.Forms;
using wxDCS_Injector.Service;

namespace wxDCS_Injector.Presentation
{
    sealed partial class frmMain : Form
    {
        readonly IMetarService _metarService;
        readonly IInjectService _injectService;
        readonly ILog _log;

        OpenFileDialog _file;
        Schema.METAR _metar;

        public frmMain(IMetarService metarService, IInjectService injectService, ILog log)
        {
            InitializeComponent();

            Text = $"wxDCS-Injector v{Application.ProductVersion}";

            _metarService = metarService;

            _injectService = injectService;
            _injectService.UseCurrentDate = miCurrentDate.Checked;
            _injectService.UseCurrentTime = miCurrentTime.Checked;

            _log = log;
        }

        void btnFile_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = openFileDialog.SafeFileName;
                    _file = openFileDialog;
                    Log($"Selected mission file: {openFileDialog.FileName}");
                }
            }

            CheckButton();
        }

        static void Exit()
        {
            Application.Exit();
        }

        void btnExit_Click(object sender, EventArgs e) => Exit();

        void GetMETAR()
        {
            if (string.IsNullOrEmpty(txtICAO.Text))
                txtMETAR.Text = "No ICAO entered";
            else if (txtICAO.Text.Length < 4 || !txtICAO.Text.All(char.IsLetterOrDigit))
                txtMETAR.Text = "Invalid ICAO";
            else
            {

                Log("Fetching METAR...");
                var metarDto = _metarService.GetMETAR(txtICAO.Text);

                if (metarDto.Error != null)
                {
                    var error = metarDto.Error;
                    txtMETAR.Text = error.Message;
                    Log(error.ToString());
                }
                else
                {
                    _metar = metarDto.METAR;
                    if (_metar == null)
                    {
                        const string Error = "METAR data not found";
                        txtMETAR.Text = Error;
                        Log(Error);
                        return;
                    }

                    var strMETAR = _metar.raw_text;
                    txtMETAR.Text = strMETAR;
                    Log(strMETAR);
                }
            }

            CheckButton();
        }

        void btnICAO_Click(object sender, EventArgs e) => GetMETAR();

        void txtICAO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                GetMETAR();
            }
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            _file = null;
            txtFile.Text = "No file selected";
            dlgFile.Reset();
            btnFile.Focus();
            txtICAO.Text = string.Empty;
            txtMETAR.Text = string.Empty;
            _metar = null;

            CheckButton();
        }

        void btnLog_Click(object sender, EventArgs e)
        {
            _log.Form.Show();
            _log.Form.BringToFront();
        }

        void btnInject_Click(object sender, EventArgs e) => _injectService.InjectMETAR(_file.FileName, _metar);

        void Log(string txt) => _log.Log(txt);

        void CheckButton() => btnInject.Enabled = _file != null
            && !string.IsNullOrEmpty(txtICAO.Text)
            && !string.IsNullOrEmpty(txtMETAR.Text)
            && _metar != null;

        void miExit_Click(object sender, EventArgs e) => Exit();

        void miSave_Click(object sender, EventArgs e)
        {
            miSave.Checked = !miSave.Checked;
            _log.SaveLog = miSave.Checked;
        }

        void miCurrentDate_Click(object sender, EventArgs e)
        {
            miCurrentDate.Checked = !miCurrentDate.Checked;
            _injectService.UseCurrentDate = miCurrentDate.Checked;
        }

        void miCurrentTime_Click(object sender, EventArgs e)
        {
            miCurrentTime.Checked = !miCurrentTime.Checked;
            _injectService.UseCurrentTime = miCurrentTime.Checked;

        }
    }
}
