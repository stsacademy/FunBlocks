using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBlocks
{
    public class CubeCollection : List<ICube>, ITwoDimensional
    {
        public CubeCollection(ICube[] cubes)
        {
            AddRange(cubes);
        }

        public CubeCollection()
        {
        }

        public ICube[] Cubes
        {
            get { return ToArray(); }
        }

        public bool IsUnder(ITwoDimensional shape)
        {
            Shape NextLocation = (Shape)((Shape)shape).Clone();
            NextLocation.Fall();
            return NextLocation.isOverlapingWith(this);
        }

        public bool isOverlapingWith(ITwoDimensional shape)
        {
            return this.Any(x => x.isOverlapingWith(shape));
        }

        public bool IsLeftOf(ITwoDimensional shape)
        {
            return this.Any(x => x.IsLeftOf(shape));
        }

        public bool IsRightOf(ITwoDimensional shape)
        {
            return this.Any(x => x.IsRightOf(shape));
        }

        public int Fall()
        {
            foreach (var cube in this)
                cube.Fall();

            return 0;
        }

        public void GoLeft()
        {
            foreach (var cube in this)
                cube.GoLeft();
        }

        public void GoRight()
        {
            foreach (var cube in this)
                cube.GoRight();
        }

        public virtual void Rotate()
        {
            throw new Exception("Can't perform Rotate on a CubeCollection");
        }

        public System.Drawing.Color Color
        {
            get 
            {
                if (this.Count == 0) 
                    return default(Color);
          
                return this[0].Color;
            }
        }

        public void DrawYourselfOn(System.Drawing.Graphics graphics)
        {
            this.ForEach(x => x.DrawYourselfOn(graphics));
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void FallAll(Predicate<ICube> predicate)
        {
            base.FindAll(predicate).ConvertAll(x=>x.Fall());
        }
    }
}
