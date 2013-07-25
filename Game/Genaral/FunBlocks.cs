using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunBlocks
{
    public class FunBlocksGame : IFunBlocksGame
    {
        IBoard GameBoard;
        bool hasFrame;
        private State State = State.Starting;
        private int level;


        public static int XOffeset = 0;
        public static int YOffeset = 0;
        
        public FunBlocksGame(int height, int width)
        {
            GameBoard = new Board(height, width);
            level = 1;
        }

        public void Snapshot(System.Drawing.Graphics graphics)
        {
            GameBoard.DrawYourselfOn(graphics);
            if (GameState.Equals(State.GameOver))
                ShowGameOver(graphics);
        }

        private void ShowGameOver(Graphics graphics)
        {
            Rectangle rectangle = new Rectangle(XOffeset + 20, YOffeset + 60, 160, 50);

            using (SolidBrush myBrush = new SolidBrush(Color.Red))
                graphics.FillRectangle(myBrush, rectangle);

            using (Pen myPen = new Pen(Color.Black))
                graphics.DrawRectangle(myPen, rectangle);

            FunBlocks.Board.DrawString(graphics, "Game Over", XOffeset + 40, YOffeset + 70);
        }

        public IBoard Board
        {
            get { return GameBoard; }
        }

        public void Start()
        {
            State = State.Running;
        }

        public void Pause()
        {
            State = State.Paused;
        }

        public void Resume()
        {
            State = State.Running;
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void GoLeft()
        {
            if(State.Equals(State.Running))
                GameBoard.GoLeft();
        }

        public void GoRight()
        {
            if (State.Equals(State.Running))
                GameBoard.GoRight();
        }

        public void Fall()
        {
            if (State.Equals(State.Running))
                GameBoard.Fall();
            
            if (GameBoard.IsFull)
                State = State.GameOver;
        }

        public void Rotate()
        {
            if (State.Equals(State.Running))
                GameBoard.Rotate();
        }

        public bool HasNewFrame()
        {
            if (hasFrame)
            {
                hasFrame = false;
                return true;
            }
            return false;
        }

        public void DropDown()
        {
            if (State.Equals(State.Running))
                Board.DropDown();
        }

        int IFunBlocksGame.Level
        {
            get
            {
                return level;
            }
        }

        public State GameState
        {
            get
            {
                return State;
            }
        }
    }
}
