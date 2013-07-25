using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBlocks
{
    public interface IBoard : ITwoDimensional
    {
        int Height { get; }
        int Width { get; }

        bool HasFallen(ITwoDimensional shape);

        bool IsFull { get; }

        void DropDown();
    }
}
