using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project
{
    class Character : AdvObj
    {
        //fields                    /**/ means this line was edited 

        private int health;
        private int speed;
        private int strength;
        private int defense;
        private Item eitem; 
        private bool stun; 
        private int x;
        private int y;
        private int room;
        private char symbol;

        //properties
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }
        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }
        public Item EItem   //equipped item 
        {
            get { return eitem; }
            set { eitem = value; }
        }
        public bool Stun   //is the target stunned? 
        {
            get { return stun; }
            set { stun = value; }
        }
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
        public int Room
        {
            get { return room; }
            set { room = value; }
        }
        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }
        //constructor
        public Character(string n = "", string ds = "", int id = 0, int h = 0, int sp = 0, 
            int st = 0, int d = 0, Item ei = null, int xt = 0, int yt = 0,int r = 0, bool s = false, char sy = 'x') /**/
        {
            Name = n; Desc = ds; ID = id; //AdvObj fields
            health = h; speed = sp; strength = st; defense = d; //stats
            eitem = ei; //held item
            stun = s;   //stunned
            x = xt; y = yt; room = r;//coordinates
            symbol = sy; //symbol
        }
        //methods

        public void Equip(Item a) //used to equip items that are held   
        {
            eitem = a;  /**/
            return;
        }

        public void Fight() //a.Fight(Lank)
        {
            if (Stun)
                return;
            if (MainGame.Lank.eitem.Name == "BOW")
            {
                if ((X == MainGame.Lank.X && Y == (MainGame.Lank.Y + 2)) || (X == MainGame.Lank.X && Y == (MainGame.Lank.Y - 2)) || (X == (MainGame.Lank.X - 2) && Y == MainGame.Lank.Y) || (X == (MainGame.Lank.X + 2) && Y == MainGame.Lank.Y))
                {
                    Random rand = new Random();
                    int damage;
                    int dodge = rand.Next(1, 100); //dodge chance
                    if (dodge < MainGame.Lank.Speed)
                    {
                        Interface.print(MainGame.Lank.Name + " dodged " + Name + "\'s move.");
                        return;
                    }
                    damage = (EItem == null) ? Strength - MainGame.Lank.Defense : Strength * EItem.Power - MainGame.Lank.Defense; //if no item held
                    damage = (damage > 0) ? damage : 1; //if damage is negative set to 1
                    Interface.print(Name + " did " + damage + " Damage to " + MainGame.Lank.Name);
                    MainGame.Lank.Health -= damage;

                }
            }
            else
            {
                if ((X == MainGame.Lank.X && Y == (MainGame.Lank.Y + 1)) || (X == MainGame.Lank.X && Y == (MainGame.Lank.Y - 1)) || (X == (MainGame.Lank.X - 1) && Y == MainGame.Lank.Y) || (X == (MainGame.Lank.X + 1) && Y == MainGame.Lank.Y))
                {
                    Random rand = new Random();
                    int damage;
                    int dodge = rand.Next(1, 100); //dodge chance
                    if (dodge < MainGame.Lank.Speed)
                    {
                        Interface.print(MainGame.Lank.Name + " dodged " + Name + "\'s move.");
                        return;
                    }
                    damage = (EItem == null) ? Strength - MainGame.Lank.Defense : Strength * EItem.Power - MainGame.Lank.Defense; //if no item held
                    damage = (damage > 0) ? damage : 1; //if damage is negative set to 1
                    Interface.print(Name + " did " + damage + " Damage to " + MainGame.Lank.Name);
                    MainGame.Lank.Health -= damage;

                }
            }


        }
        public void Die()
        {
            Interface.print(Name + " has died and dropped a " + EItem);
            Console.SetCursorPosition(this.X+2, this.Y+2); //removes monster from screen
            Console.Write(" ");
            MainGame.CurrentRoom.CHAR.Remove(this); //hopefully removes monster from monster list
            MainGame.CurrentRoom.POTS.Remove(this); // and pots
            MainGame.Lank.ChangePocket(EItem,MainGame.ADD);//adds item to pocket
            Interface.DrawPocket();
            
            
        }
        public void Stalk()//makes character follow lank and avoids other enemies
        {
            //bool cantMove = false;
            if (Stun)
            {
                if (Temp.int1 == 0)
                    this.Stun = false;
                else
                {
                    Temp.int1--;
                    Interface.print(Name + " is stunned");
                }
                return;
            }
            bool North = true, South = true, East = true, West = true;
            string direction = "";
            int tempx = X; int tempy = Y;
            int xDif = X - MainGame.Lank.X;
            int yDif = Y - MainGame.Lank.Y;


            if (Math.Abs(xDif) >= Math.Abs(yDif))
                direction = (xDif > 0) ? "West" : "East";
            else if (Math.Abs(yDif) > Math.Abs(xDif))
                direction = (yDif > 0) ? "North" : "South";


          
            if (X + 1 == MainGame.Lank.X && Y == MainGame.Lank.Y)
                East = false;
            if (X - 1 == MainGame.Lank.X && Y == MainGame.Lank.Y)
                West = false;
            if (Y - 1 == MainGame.Lank.Y && X == MainGame.Lank.X)
                North = false;
            if (Y + 1 == MainGame.Lank.Y && X == MainGame.Lank.X)
                South = false;
            foreach (Character otherMonster in MainGame.CurrentRoom.CHAR)
            {   
                
                if (X + 1 == otherMonster.X && Y == otherMonster.Y)
                    East = false;
                if (X - 1 == otherMonster.X && Y == otherMonster.Y)
                    West = false;
                if (Y - 1 == otherMonster.Y && X == otherMonster.X)
                    North = false;
                if (Y + 1 == otherMonster.Y && X == otherMonster.X)
                    South = false;
            }
            foreach (Character otherMonster in MainGame.CurrentRoom.POTS)
            {

                if (X + 1 == otherMonster.X && Y == otherMonster.Y)
                    East = false;
                if (X - 1 == otherMonster.X && Y == otherMonster.Y)
                    West = false;
                if (Y - 1 == otherMonster.Y && X == otherMonster.X)
                    North = false;
                if (Y + 1 == otherMonster.Y && X == otherMonster.X)
                    South = false;
            }
            /*
            if (!East && direction == "East") direction = (yDif > 0) ? "North" : "South";
            if (!West && direction == "West") direction = (yDif > 0) ? "North" : "South";
            if (!North && direction == "North") direction = (xDif > 0) ? "West" : "East";
            if (!South && direction == "South") direction = (xDif > 0) ? "West" : "East";
            */

            switch (direction)
            {
                case "East":
                    if(East)X++;
                    break;
                case "West":
                    if (West) X--;
                    break;
                case "North":
                    if (North) Y--;
                    break;
                case "South":
                    if (South) Y++;
                    break;
            }
            Interface.DrawCharacter(this, tempx, tempy);

            
        }
        

    }
}