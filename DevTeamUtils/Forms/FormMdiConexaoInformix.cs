using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormMdiConexaoInformix : Form
    {
        private static FormMdiConexaoInformix _Instance = new FormMdiConexaoInformix();
        public static FormMdiConexaoInformix Instance { get { return _Instance; } }
        private FormMdiConexaoInformix()
        {
            InitializeComponent();
        }
    }
}
