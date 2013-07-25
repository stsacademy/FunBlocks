using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FunBlocks
{
    public interface IFunBlocksGame
    {
        void Start();
        void Pause();
        void Resume();
        void Exit();
        State GameState { get; }

        int Level { get; }

        void GoLeft();
        void GoRight();
        void Fall();
        void DropDown();
        void Rotate();

        void Snapshot(Graphics graphics);
        bool HasNewFrame();
        IBoard Board { get; }
    }
}
