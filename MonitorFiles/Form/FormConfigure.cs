using MonitorFiles.Class;

namespace MonitorFiles
{
    public partial class FormConfigure : Form
    {
        public FormConfigure()
        {
            InitializeComponent();
        }

        public dynamic? JsonObjSettings { get; set; }

        private void ButtonCompress_Click(object sender, EventArgs e)
        {
            using MfApplicationDatabaseMaintain cp = new();
            cp.Compress();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
