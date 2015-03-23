using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project
{
     class XY
    {
        private int x;
        private int y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
      public  XY(int ex=0, int why=0)
        {
            X = ex;
            Y = why;
        }
        public  List<XY> Coordinates()
        {
            List<XY> coordinates = new List<XY>();
            //XY xy = new XY();
            for(int i=2; i<Interface.roomHeight-2;i++)
                for (int k = 2; k < Interface.roomWidth - 2; k++)
                {
                    if ((i != Interface.roomHeight - 2 && k != Interface.roomWidth / 2) || (i!=2 && k!=Interface.roomWidth))
                    {
                        coordinates.Add(new XY(k,i));
                    }
                }
            //return null;
            return coordinates;
        }
    }
    
}
