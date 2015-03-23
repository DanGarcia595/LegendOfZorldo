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
                    Interface.print(MainGame.Lank.Name + " has " + MainGame.Lank.Health + " Health");                    
                    Interface.RefreshHealth(MainGame.Lank.Health);
                    Interface.DrawInput();
                    Temp.int2 = 1;

                }
            }
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
                    if (MainGame.Lank.Health <= 0)
                        MainGame.Lank.Die();
                    else
                    {
                        Interface.print(MainGame.Lank.Name + " has " + MainGame.Lank.Health + " Health");
                        Interface.RefreshHealth(MainGame.Lank.Health);
                        Interface.DrawInput();
                        Temp.int2 = 1;
                    }
        
                }


        }
        public void Die()
        {
            if (Name == "Ganondalf")
            {
                Interface.print("You have slain Ganondalf!");
                Console.SetCursorPosition(this.X + 2, this.Y + 2); //removes monster from screen
                Console.Write(" ");
                MainGame.CurrentRoom.CHAR.Remove(this); //hopefully removes monster from monster list
                Interface.print("It looked as if he was guarding something in the northern region\nof the room, but it's too dark to tell.");
                MainGame.CurrentRoom.CHEST.ElementAt(0).Open = false;
                Interface.DrawChest(MainGame.CurrentRoom.CHEST.ElementAt(0));
                return;
            }
            Random rand = new Random();
            int i = rand.Next(1, 4);
            if (i > 1)
            {
                Interface.print(Name + " has died and dropped a " + EItem);
                Console.SetCursorPosition(this.X + 2, this.Y + 2); //removes monster from screen
                Console.Write(" ");
                MainGame.CurrentRoom.CHAR.Remove(this); //hopefully removes monster from monster list
                MainGame.CurrentRoom.POTS.Remove(this); // and pots
                if (EItem.Name == "RUPEE")
                {
                    MainGame.Lank.Rupee += EItem.Power;
                    MainGame.Lank.Rupee += EItem.Power;
                    MainGame.Lank.Rupee += EItem.Power;
                    MainGame.Lank.Rupee += EItem.Power;
                    MainGame.Lank.Rupee += EItem.Power;
                }
                else if (EItem.Name == "ARROWS")
                {
                    MainGame.Lank.ChangePocket(MainGame.ItemL.ElementAt(9));
                }
                else
                {
                    MainGame.Lank.ChangePocket(EItem, MainGame.ADD);//adds item to pocket
                    Interface.DrawPocket();
                }
            }
            else
            {
                Interface.print(Name + " has died.");
                Console.SetCursorPosition(this.X + 2, this.Y + 2); //removes monster from screen
                Console.Write(" ");
                MainGame.CurrentRoom.CHAR.Remove(this); //hopefully removes monster from monster list
                MainGame.CurrentRoom.POTS.Remove(this); // and pots
            }
            
            
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


  //        
            if (X + 1 == MainGame.Lank.X && Y == MainGame.Lank.Y)
            {
                if (Temp.int2 == 1)
                {
                    Temp.int2--;
                    return;
                }
                Fight();
                MainGame.Lank.Fight(this);
                if (MainGame.Lank.EItem.Name == "BOW" && (MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(9)) || MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(20))))
                    if (MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(9)) || MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(20)))
                    {
                        MainGame.Lank.Pocket.Find(tmp => tmp.Name == "ARROWS").Power--;
                        Interface.DrawPocket();
                    }


                East = false;
            }
            if (X - 1 == MainGame.Lank.X && Y == MainGame.Lank.Y)
            {
                if (Temp.int2 == 1)
                {
                    Temp.int2--;
                    return;
                }
                Fight();
                MainGame.Lank.Fight(this);
                if (MainGame.Lank.EItem.Name == "BOW" && (MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(9)) || MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(20))))
                    if (MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(9)) || MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(20)))
                    {
                        MainGame.Lank.Pocket.Find(tmp => tmp.Name == "ARROWS").Power--;
                        Interface.DrawPocket();
                    }
                West = false;
            }
            if (Y - 1 == MainGame.Lank.Y && X == MainGame.Lank.X)
            {
                if (Temp.int2 == 1)
                {
                    Temp.int2--;
                    return;
                }
                Fight();
                MainGame.Lank.Fight(this);
                if (MainGame.Lank.EItem.Name == "BOW" && (MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(9)) || MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(20))))
                    if (MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(9)) || MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(20)))
                    {
                        MainGame.Lank.Pocket.Find(tmp => tmp.Name == "ARROWS").Power--;
                        Interface.DrawPocket();
                    }
                North = false;
            }
            if (Y + 1 == MainGame.Lank.Y && X == MainGame.Lank.X)
            {
                if (Temp.int2 == 1)
                {
                    Temp.int2--;
                    return;
                }
                Fight();
                MainGame.Lank.Fight(this);
                if (MainGame.Lank.EItem.Name == "BOW" && (MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(9)) || MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(20))))
                    if (MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(9)) || MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(20)))
                    {
                        MainGame.Lank.Pocket.Find(tmp => tmp.Name == "ARROWS").Power--;
                        Interface.DrawPocket();
                    }
                South = false;
            }
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