using MAH_TowerDefense.Entity.Bullets;
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
    public partial class LevelEditorForm : Form
    {
        private int width;
        private int height;
        private int level;
        private bool insert;

        public LevelEditorForm(int level, int width, int height, bool insert)
        {
            InitializeComponent();

            this.width = width;
            this.height = height;
            this.level = level;
            this.insert = insert;
        }

        private void LevelEditorForm_Load(object sender, EventArgs e)
        {
            this.levelEditor1.SetSize(width, height);
        }

        private void AddWave_Click(object sender, EventArgs e)
        {
            WaveModel model = new WaveModel();
            model.Name = "Wave #" + (waveList.Items.Count + 1);
            waveList.Items.Add(model);
        }

        private void waveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (waveList.SelectedItem != null)
                enemyInput.Text = ((WaveModel)waveList.SelectedItem).Enemies;
        }

        private void enemyInput_TextChanged(object sender, EventArgs e)
        {
            ((WaveModel)waveList.SelectedItem).Enemies = enemyInput.Text;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelIO.SaveLevel(new LevelModel.SingleLevel() { 
                Width = width,
                Height = height, 
                LevelIndex = level,
                PathPoints = levelEditor1.GetPoints(),
                Waves = waveList.Items.Cast<WaveModel>().ToList()
            }, true);
        }

        private void clearPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            levelEditor1.ClearPath();
        }

        private void newStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            new NewLevelForm().Show();
        }


    }
}
