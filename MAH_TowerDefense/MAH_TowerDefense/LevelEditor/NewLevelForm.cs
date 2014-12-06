using MAH_TowerDefense.Views;
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

        private void NewLevelForm_Load(object sender, EventArgs e)
        {
            int levels = LevelIO.LevelCount();

            numLevel.Maximum = levels + 1;
            numLevel.Value = numLevel.Maximum;

            numWidth.Minimum = WorldRenderer.WIDTH / 64;
            numHeight.Minimum = WorldRenderer.HEIGHT / 64;
        }
    }
}
