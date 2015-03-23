using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project
{
    class Chest:AdvObj
    {
        //fields
        private Item contents;  //contents of the chest
        private int x;  //x coordinate
        private int y;  //y coordinate
        private bool open; //is it open?
        private bool locked;//is it locked?

        //properties
        public Item Contents
        {
            get { return contents; }
            set { contents = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get{ return y; }
            set{ y = value;}
        }

        public bool Open
        {
            get { return open; }
            set { open = value; }
        }

        /*public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }
        */
        //methods               IDs should begin at 50
        public Chest( Item c=null, int a=0, int b=0, int i=0, bool o=false)
        {
            contents = c;
            x = a;
            y = b;
            id = i;
            open = o;
           
        }
    }
}
