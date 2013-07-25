using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBlocks
{
    // comment
    public interface ITwoDimensional
    {
        ICube[] Cubes { get; }
        
        bool IsUnder(ITwoDimensional shape);
        bool isOverlapingWith(ITwoDimensional shape);
        bool IsLeftOf(ITwoDimensional shape);
        bool IsRightOf(ITwoDimensional shape);

        int Fall();
        void GoLeft();
        void GoRight();
        void Rotate();
        
        Color Color { get; }
        void DrawYourselfOn(Graphics graphics);
    }
}