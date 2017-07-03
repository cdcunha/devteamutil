using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormMdiAcionamentoAgfa : Form
    {
        private static FormMdiAcionamentoAgfa _Instance = new FormMdiAcionamentoAgfa();
        public static FormMdiAcionamentoAgfa Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new FormMdiAcionamentoAgfa();
                return _Instance;
            }
        }
        private FormMdiAcionamentoAgfa()
        {
            InitializeComponent();
        }

        private void FormMdiAcionamentoAgfa_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_Instance != null)
            {
                _Instance.Dispose();
                _Instance = null;
            }
        }
    }
}
