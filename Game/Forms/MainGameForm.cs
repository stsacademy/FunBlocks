using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using FunBlocks;

namespace FunBlocks
{
    public partial class MainGameForm : Form
    {
        FunBlocksGame FunBlocks;
        int StartInterval = 1000;
        int CurrentInerval = 1000;

        public MainGameForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            
            FunBlocks = new FunBlocksGame(400, 200);
            FunBlocks.Start();
        }

        private void NewGame(object sender, EventArgs e)
        {
            FunBlocks = new FunBlocksGame(400, 200);
            FunBlocks.Start();
            timer1.Interval = StartInterval;
            CurrentInerval = StartInterval;

            timer1.Start();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 37: 
                    FunBlocks.GoLeft();
                    break;
                case 38: 
                    FunBlocks.Rotate();
                    break;
                case 39: 
                    FunBlocks.GoRight();
                    break;
                case 40: 
                    FunBlocks.Fall();
                    break;
                case 32: 
                    FunBlocks.DropDown();
                    break;
                case 27:
                    if (FunBlocks.GameState.Equals(State.Running))
                        FunBlocks.Pause();
                    else
                        FunBlocks.Resume();
                break;
            }
            this.Refresh();
            ReactIfGameOver();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            FunBlocks.Snapshot(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer1.Interval = StartInterval;
            this.timer1.Enabled = true;
            this.ClientSize = new Size(FunBlocks.Board.Width + 120, FunBlocks.Board.Height + menuStrip1.Height);
            FunBlocksGame.YOffeset = menuStrip1.Height;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(FunBlocks.GameState.Equals(State.Running))
            {
                if (DateTime.Now.Ticks - FunBlocks.LastModified.Ticks <= 200)
                {
                    timer1.Interval = 200;
                    return;
                }
                else
                {
                    if (CurrentInerval > 400)
                        timer1.Interval = CurrentInerval -= 10;
                }
            }

            if(FunBlocks != null)
                FunBlocks.Fall();

            this.Refresh();
            ReactIfGameOver();
        }

        private void Pause(object sender, EventArgs e)
        {
            FunBlocks.Pause();
        }

        private void Resume(object sender, EventArgs e)
        {
            FunBlocks.Resume();
        }

        private void ScoreBoardHadler(object sender, EventArgs e)
        {

        }

        public void ReactIfGameOver()
        {
            if (FunBlocks.GameState.Equals(State.GameOver))
                timer1.Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
