using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MAH_TowerDefense.LevelEditor
{
    public partial class OpenLevelForm : Form
    {
        public OpenLevelForm()
        {
            InitializeComponent();
        }

        private void NewLevelForm_Load(object sender, EventArgs e)
        {
            int levels = LevelIO.LevelCount();

            numLevel.Maximum = levels;
            numLevel.Value = numLevel.Maximum;
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            this.Close();
            new LevelEditorForm((int)numLevel.Value).Show();
        }
    }
}
