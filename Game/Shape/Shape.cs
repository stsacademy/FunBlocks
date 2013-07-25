using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FunBlocks 
{
    public class Shape : CubeCollection, IShape, ICloneable
    {
        private ICube root;
        private Color color;

        public Shape(ICube root, ICube[] cubes, Color color)
            :base(cubes)
        {
            this.root = root;
            this.color = color;
        }
        
        public ICube Root
        {
            get
            {
                return root;
            }
        }

        public object Clone()
        {
            return new Shape(root, Cubes, color);
        }

        public override void Rotate()
        {
            List<ICube> temp = ConvertAll(RotateShape);
            Clear();
            AddRange(temp);
        }

        private ICube RotateShape(ICube cube)
        {
            if (cube.Equals(root))
                return cube;

            Matrix matrix = new Matrix(0, 1, -1, 0, root.CoordinateX, root.CoordinateY);
            PointF[] points = { new PointF(cube.CoordinateX - root.CoordinateX, cube.CoordinateY - root.CoordinateY) };
            matrix.TransformPoints(points);

            return new Cube(((int)points[0].X), ((int)points[0].Y), cube.Color);
        }

        public int FindHighestCube()
        {
            int min = 20;

            foreach (var cube in Cubes)
                if (cube.CoordinateY < min)
                    min = cube.CoordinateY;

            return min;

        }

        public void MoveTo(int x, int y)
        {
            int deltaX = x - root.CoordinateX;
            int deltaY = y - root.CoordinateY;

            base.ForEach(cube => cube.MoveTo(cube.CoordinateX + deltaX,cube.CoordinateY + deltaY));
        }
    }
}
