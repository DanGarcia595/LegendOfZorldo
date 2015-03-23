using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project
{
    class AdvObj
    {
        protected string name;
        protected string desc;
        protected int id;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
    }
}
