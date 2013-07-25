using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace FunBlocks
{
    public class Board : IBoard
    {
        private CubeCollection FallenCubes = new CubeCollection();
        private Shape FallingShape;
        private Shape NextShape;

        private int MyHeight;
        private int MyWidth;

        private SolidBrush BackgroungBrush;
        private Pen BackgroundPen;

        private int Points = 0;
        private static int highScore;
        private int PlayerScore = 0;

        private bool full = false;

        public Board(int heihgt, int width)
        {
            MyHeight = heihgt;
            MyWidth = width;
            
            BackgroungBrush = new SolidBrush(Color.DimGray);
            BackgroundPen = new Pen(Color.DimGray);

            FallingShape = (Shape) Shaper.MakeRandom(4, -2);

            GenerateNextShape();
        }

        int HighScore
        {
            get { return highScore; }
            set
            {
                if (value > highScore)
                    highScore = value;
            }
        }

        public static void DrawString(Graphics graphics, string stringLine, float x, float y)
        {
            Font drawFont = new Font("Arial", 14);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.White);
            StringFormat drawFormat = new StringFormat();
            graphics.DrawString(stringLine, drawFont, drawBrush, x, y, drawFormat);
            drawFormat.Dispose();
            drawFont.Dispose();
            drawBrush.Dispose();
        }

        private void GenerateNextShape()
        {
            NextShape = (Shape) Shaper.MakeRandom(12, 1);
        }

        public int Height
        {
            get { return MyHeight; }
        }

        public int Width
        {
            get { return MyWidth; }
        }

        public ICube[] Cubes
        {
            get { return FallenCubes.ToArray(); }
        }

        public bool IsUnder(ITwoDimensional shape)
        {
            throw new NotImplementedException();
        }

        public bool isOverlapingWith(ITwoDimensional shape)
        {
            return FallenCubes.Any(x => x.isOverlapingWith(shape));
        }

        public int Fall()
        {
            if (HasFallen(FallingShape))
            {
                FallenCubes.AddRange(FallingShape.Cubes);

                FallingShape = NextShape;

                FallingShape.MoveTo(4, -4);

                GenerateNextShape();

                for (int i = 0; i < 20; i++)
                {
                    if (FallenCubes.Count(x => x.CoordinateY == i) == 10)
                    {
                        Points *= 2;
                        if(Points == 0)
                            Points = 1;

                        FallenCubes.RemoveAll(x => x.CoordinateY == i);
                        FallenCubes.FallAll(x => x.CoordinateY < i);
                    }
                }
            }
            else
                Points = 0;
            
            FallingShape.Fall();

            if (FallingShape.isOverlapingWith(FallenCubes))
                full = true;

            PlayerScore += Points;
            HighScore = PlayerScore;
            if(FallingShape.FindHighestCube() < 0 && full == false)
                return Points + Fall();
            return Points;
        }

        System.Drawing.Color ITwoDimensional.Color
        {
            get { return Color.Black; }
        }

        public void DrawYourselfOn(System.Drawing.Graphics graphics)
        {
            for(int i = 0 ; i < 10;i++)
                graphics.DrawRectangle(BackgroundPen, i*Cube.CubeSize + FunBlocksGame.XOffeset, FunBlocksGame.YOffeset,Cube.CubeSize,MyHeight);

            FallingShape.DrawYourselfOn(graphics);

            FallenCubes.DrawYourselfOn(graphics);

            NextShape.DrawYourselfOn(graphics);
            DrawString(graphics,String.Format("High Score \n {0}\nYour Score \n {1}", highScore, PlayerScore), 200, 120);
        }

        public void GoLeft()
        {
            if ((!this.IsRightOf(FallingShape) && !FallingShape.IsRightOf(FallenCubes)))
                FallingShape.GoLeft();
        }

        public void GoRight()
        {
            if ((!this.IsLeftOf(FallingShape) && !FallingShape.IsLeftOf(FallenCubes)))
                FallingShape.GoRight();
        }

        public void Rotate()
        {
            if (FallingShape.Color.Equals(Shaper.OColor))
                return;

            Shape temp = (Shape)((Shape)FallingShape).Clone();
            
            temp.Rotate();

            if (FallingShape.Color.Equals(Shaper.LColor))
            {
                temp.Rotate();
                temp.Rotate();
            }

            if (!(isOverlapingWith(temp) || IsOutside(temp)))
                FallingShape = temp;
        }

        public bool IsOutside(ITwoDimensional shape)
        {
            return shape.Cubes.Count(x=>(x.CoordinateX < 0 || x.CoordinateX >= 10)) != 0;
        }

        public bool HasFallen(ITwoDimensional shape)
        {
            if(shape.Cubes.Any(x => x.CoordinateY >= 19))
                return true;

            if (FallenCubes.Any(x => x.IsUnder(FallingShape)))
                return true;

            return false;
        }

        public bool IsLeftOf(ITwoDimensional shape)
        {
            return shape.Cubes.Any(x => x.CoordinateX >= (MyWidth / Cube.CubeSize) - 1);
        }

        public bool IsRightOf(ITwoDimensional shape)
        {
            return shape.Cubes.Any(x => x.CoordinateX <= 0);
        }

        public void DropDown()
        {
            while (!HasFallen(FallingShape))
                Fall();
        }

        public bool IsFull
        {
            get { return full; }
        }
    }
}
