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
    public partial class NewLevelForm : Form
    {
        public NewLevelForm()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            this.Close();
            new LevelEditorForm((int)numLevel.Value,(int)numWidth.Value, (int)numHeight.Value, insert.Checked).Show();
        }
    }
}
