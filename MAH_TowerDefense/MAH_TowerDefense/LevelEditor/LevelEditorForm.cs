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

        public LevelEditorForm(int level = 0)
        {
            InitializeComponent();

            this.level = level;
        }

        private void LevelEditorForm_Load(object sender, EventArgs e)
        {
            if ((width == 0 || height == 0) && level != 0)
            {
                // Load
                LevelModel.SingleLevel model = LevelIO.LoadLevel(level);
                this.width = model.Width;
                this.height = model.Height;
                this.levelEditor1.InitPath(model.PathPoints);
                this.levelEditor1.SetSize(width, height, false);
                foreach (var wave in model.Waves)
                    waveList.Items.Add(wave);
            }
            else
            {
                // New map
                this.levelEditor1.SetSize(width, height);
            }

            if (level == 0)
                levelEditor1.ClearPath();

            this.Activate();
            this.levelEditor1.Focus();

            this.Text += " Level: " + level;
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
            if (level == 0)
            {
                MessageBox.Show("Please create a new Level first!");
                return;
            }

            levelEditor1.FinishDrawing();
            LevelIO.SaveLevel(new LevelModel.SingleLevel() { 
                Width = width,
                Height = height, 
                LevelIndex = level,
                PathPoints = levelEditor1.GetPoints(),
                Waves = waveList.Items.Cast<WaveModel>().ToList()
            }, insert);

            insert = false;
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

        private void starter_Tick(object sender, EventArgs e)
        {
            starter.Stop();

            this.levelEditor1.PreviewKeyDown += new PreviewKeyDownEventHandler(levelEditor1.PreviewKey);
            this.levelEditor1.MouseWheel += new System.Windows.Forms.MouseEventHandler(levelEditor1.ScrollEvent);
            this.levelEditor1.MouseClick += new System.Windows.Forms.MouseEventHandler(levelEditor1.MousePressedDown);
            //levelEditor1.ClearPath();

            levelEditor1.Select();
        }

        private void levelEditor1_Click(object sender, EventArgs e)
        {
            levelEditor1.Focus();
            levelEditor1.Select();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LevelIO.LevelCount() == 0)
            {
                MessageBox.Show("But whyy :( You can't open levels you haven't created yet!");
                return;
            }

            this.Close();
            new OpenLevelForm().Show();
        }

        private void deleteLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelIO.DeleteLevel(level);
            this.Close();

            new LevelEditorForm().Show();
        }
    }
}
