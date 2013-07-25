using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace FunBlocks
{
    public class Cube : ICube
    {
        public int x;
        public int y;

        private Color MyColor = Color.Blue;
        public static readonly int CubeSize = 20;

        public Cube(int x, int y, Color color)
        {
            CoordinateX = x;
            CoordinateY = y;
            MyColor = color;
        }

        public int CoordinateX
        {
            get
            {
                return x;
            }
            private set
            {
                x = value;
            }
        }

        public int CoordinateY
        {
            get
            {
                return y;
            }
            private set
            {
                y = value;
            }
        }

        public bool IsUnder(ITwoDimensional shape)
        {
            return shape.Cubes.Any(cube => (cube.CoordinateX == x && cube.CoordinateY == y - 1));
        }

        public bool isOverlapingWith(ITwoDimensional shape)
        {
            return shape.Cubes.Any(x => x.Equals(this));
        }

        ICube[] ITwoDimensional.Cubes
        {
            get
            {
                return new ICube[] { this };
            }
        }

        public int Fall()
        {
            y++;

            return 0;
        }

        public object Clone()
        {
            return new Cube(x, y, MyColor);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public Color Color
        {
            get { return MyColor; }
        }

        System.Drawing.Color ITwoDimensional.Color
        {
            get { return Color; }
        }

        public void DrawYourselfOn(Graphics graphics)
        {
            Rectangle rectangle = new Rectangle(x * CubeSize + FunBlocksGame.XOffeset, y * CubeSize + FunBlocksGame.YOffeset, CubeSize, CubeSize);

            using (SolidBrush myBrush = new SolidBrush(MyColor))
                graphics.FillRectangle(myBrush, rectangle);
            
            using (Pen myPen = new Pen(Color.Black))
                graphics.DrawRectangle(myPen, rectangle);
        }

        public void GoLeft()
        {
            x--;
        }

        public void GoRight()
        {
            x++;
        }

        public void Rotate()
        {
        }

        public bool IsLeftOf(ITwoDimensional shape)
        {
            return shape.Cubes.Any(cube => cube.CoordinateX == x + 1 && cube.CoordinateY == y);
        }

        public bool IsRightOf(ITwoDimensional shape)
        {
            return shape.Cubes.Any(cube => cube.CoordinateX == x - 1 && cube.CoordinateY == y);
        }

        public void MoveTo(int x, int y)
        {
            CoordinateX = x;
            CoordinateY = y;
        }

        public bool Equals(ICube other)
        {
            return x == other.CoordinateX && y == other.CoordinateY;
        }
    }
}
