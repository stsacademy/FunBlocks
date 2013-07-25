using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBlocks
{
    public interface ICube : ITwoDimensional, IEquatable<ICube>, ICloneable
    {
        int CoordinateX { get; }
        int CoordinateY { get; }
        void MoveTo(int x, int y);
    }
}
