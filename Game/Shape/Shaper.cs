using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FunBlocks
{
    class Shaper
    {
        public readonly static Color OColor = Color.Yellow;
        public readonly static Color IColor = Color.SkyBlue;
        public readonly static Color TColor = Color.Purple;
        public readonly static Color LColor = Color.Orange;
        public readonly static Color JColor = Color.DarkBlue;
        public readonly static Color SColor = Color.Green;
        public readonly static Color ZColor = Color.Red;

        private static Random random = new Random();

        public static IShape O_Shape(int x, int y)
        {
            ICube root = new Cube(x, y, OColor);

            ICube[] cubes = { root,
                              new Cube(root.CoordinateX + 1, root.CoordinateY + 1, OColor),
                              new Cube(root.CoordinateX+1,root.CoordinateY, OColor),
                              new Cube(root.CoordinateX,root.CoordinateY+1, OColor)};
            return new Shape(root, cubes, OColor);
        }

        public static IShape I_Shape(int x, int y)
        {
            ICube root = new Cube(x, y, IColor);

            ICube[] cubes = { new Cube(root.CoordinateX, root.CoordinateY - 1, IColor),
                              root,
                              new Cube(root.CoordinateX, root.CoordinateY + 1, IColor),
                              new Cube(root.CoordinateX, root.CoordinateY + 2, IColor)};
            return new Shape(root, cubes, IColor);
        }

        public static IShape T_Shape(int x, int y)
        {
            ICube root = new Cube(x, y, TColor);

            ICube[] cubes = { root,
                              new Cube(root.CoordinateX - 1, root.CoordinateY, TColor),
                              new Cube(root.CoordinateX, root.CoordinateY + 1, TColor),
                              new Cube(root.CoordinateX + 1, root.CoordinateY, TColor)};
            return new Shape(root, cubes, TColor);
        }

        public static IShape L_Shape(int x, int y)
        {
            ICube root = new Cube(x, y, LColor);

            ICube[] cubes = { root,
                              new Cube(root.CoordinateX - 1, root.CoordinateY, LColor),
                              new Cube(root.CoordinateX + 1, root.CoordinateY - 1, LColor),
                              new Cube(root.CoordinateX + 1, root.CoordinateY, LColor)};
            return new Shape(root, cubes, LColor);
        }

        public static IShape J_Shape(int x, int y)
        {
            ICube root = new Cube(x, y, JColor);

            ICube[] cubes = { root,
                              new Cube(root.CoordinateX - 1, root.CoordinateY, JColor),
                              new Cube(root.CoordinateX + 1, root.CoordinateY + 1, JColor),
                              new Cube(root.CoordinateX + 1, root.CoordinateY, JColor)};
            return new Shape(root, cubes, JColor);
        }

        public static IShape S_Shape(int x, int y)
        {
            ICube root = new Cube(x, y, SColor);

            ICube[] cubes = { root,
                              new Cube(root.CoordinateX - 1, root.CoordinateY, SColor),
                              new Cube(root.CoordinateX, root.CoordinateY - 1, SColor),
                              new Cube(root.CoordinateX + 1, root.CoordinateY - 1, SColor)};
            return new Shape(root, cubes, SColor);
        }

        public static IShape Z_Shape(int x, int y)
        {
            ICube root = new Cube(x, y, ZColor);

            ICube[] cubes = { root,
                              new Cube(root.CoordinateX - 1, root.CoordinateY, ZColor),
                              new Cube(root.CoordinateX, root.CoordinateY + 1, ZColor),
                              new Cube(root.CoordinateX + 1, root.CoordinateY + 1, ZColor)};
            return new Shape(root, cubes, ZColor);
        }
        public static IShape MakeRandom(int x, int y)
        {
            IShape result ;
            switch (random.Next(0, 7))
            {
                case 0: result = I_Shape(x, y); break;
                case 1: result = O_Shape(x, y); break;
                case 2: result = T_Shape(x, y); break;
                case 3: result = L_Shape(x, y); break;
                case 4: result = J_Shape(x, y); break;
                case 5: result = S_Shape(x, y); break;
                case 6: result = Z_Shape(x, y); break;
                default: result = null; break;
            }
            return result;
        }
    }
}
