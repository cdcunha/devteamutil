using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormMdiAcionamentoAgfa : Form
    {
        private static FormMdiAcionamentoAgfa _Instance = new FormMdiAcionamentoAgfa();
        public static FormMdiAcionamentoAgfa Instance { get { return _Instance; } }
        private FormMdiAcionamentoAgfa()
        {
            InitializeComponent();
        }
    }
}
