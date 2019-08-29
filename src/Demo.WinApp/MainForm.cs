using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Demo.WinApp.UI;

namespace Demo.WinApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }
        public AsyncUiHelperForMessageEventBus AsyncMessageHelper { get; set; }

        private void MyInitializeComponent()
        {
            SetupUi();

            AsyncMessageHelper = this.txtMessage.CreateAsyncUiHelperForMessageEventBus(message => { this.txtMessage.AppendText(message); });
        }
        
        private void MainForm_Load(object sender, System.EventArgs e)
        {

        }

        private void btnSend_Click(object sender, System.EventArgs e)
        {
            var args = GetCallArgs();
            AsyncMessageHelper.AutoAppendLine = args.AutoLine;
            AsyncMessageHelper.WithDatePrefix = args.AutoDate;
            AsyncMessageHelper.SafeUpdateUi("CallTraceApi()");

            StartAsyncMessageDemo(args.Count, args.IntervalMs);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            StopAsyncMessageDemo();
            Task.Delay(100).Wait();
            this.txtMessage.Clear();
        }

        private void SetupUi()
        {
            this.checkAutoLine.Checked = true;
            this.checkAutoDate.Checked = true;

            this.cbxCount.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxCount.Items.Add(1);
            this.cbxCount.Items.Add(2);
            this.cbxCount.Items.Add(5);
            for (int i = 1; i <= 10; i++)
            {
                this.cbxCount.Items.Add(i * 10);
            }
            this.cbxCount.SelectedIndex = 0;

            this.cbxInterval.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxInterval.Items.Add(10);
            this.cbxInterval.Items.Add(20);
            this.cbxInterval.Items.Add(50);
            this.cbxInterval.Items.Add(10 * 10);
            this.cbxInterval.Items.Add(20 * 10);
            this.cbxInterval.Items.Add(50 * 10);
            this.cbxInterval.Items.Add(10 * 100);
            this.cbxInterval.Items.Add(20 * 100);
            this.cbxInterval.Items.Add(50 * 100);

            this.cbxInterval.SelectedIndex = 0;
        }

        private CallArgs GetCallArgs()
        {
            var args = new CallArgs();
            args.AutoLine = this.checkAutoLine.Checked;
            args.AutoDate = this.checkAutoDate.Checked;
            args.Count = (int)this.cbxCount.SelectedItem;
            args.IntervalMs = (int)this.cbxInterval.SelectedItem;
            return args;
        }

        #region demo for async

        private bool _processing = false;
        private int _messageIndex = 0;

        private void StartAsyncMessageDemo(int count, int interval)
        {
            _messageIndex = 0;
            _processing = true;
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(interval)).Wait();
                    if (!_processing)
                    {
                        break;
                    }
                    _messageIndex++;

                    AsyncMessageHelper.SafeUpdateUi("message " + _messageIndex);
                }
                _processing = false;
            });
        }

        private void StopAsyncMessageDemo()
        {
            _processing = false;
            this.txtMessage.Text = @"-- stopped and cleared! --";
            Task.Delay(300).Wait();
            this.txtMessage.Clear();
        }

        #endregion
    }
}
