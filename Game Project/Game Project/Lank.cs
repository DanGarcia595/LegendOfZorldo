using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project
{
    class Lank:Character
    {

        private List<Item> pocket;
        private int rupee;
 
        public List<Item> Pocket
        {
            get { return pocket; }
            set { pocket = value; }
        }
        public int Rupee
        {
            get { return rupee; }
            set 
            {
                rupee = value;
            }
        }

        public Lank() //constructor
        {
        Name="Lank";Desc="I is Lank"; ID=101;
            Health=300;  Speed=25; Strength=25; Defense=25;
            EItem = new Item(); ; Pocket = new List<Item>();
            X=0; Y=0; Room =314; Stun=false;
        }
        //this may no longer be needed
        public Lank(string n="Lank", string ds="I is Lank", int id=101, int h=300, int sp=25, int st=25, int d=25, Item ei=null, List<Item> pk = null, int xt=0, int yt=0,int r =314,bool s=false) 
        {
            Name = n; Desc = ds; ID = id; //AdvObj fields
            Health = h; Speed = sp; Strength = st; Defense = d; 
            EItem = ei;
            Stun = s;
            X = xt; Y = yt; Room = r;
            pocket = pk;
        }
        public void ChangePocket(Item newItem=null,bool addRemove=true)
        {
            if (addRemove) //if adding to pocket
                //if (MainGame.Lank.Pocket.Find(it => it.Name == newItem.Name).Name == newItem.Name)//if item already in pocket
                try { MainGame.Lank.Pocket.Find(it => it.Name == newItem.Name).Power += newItem.Power; } //combine power together
                catch (NullReferenceException)
                { ////if not in pocket
                    Pocket.Add(newItem);
                }
            else//if removing from pocket
                Pocket.Remove(newItem);
            Interface.DrawPocket();
        }
        public void Fight(Character b) //Lank.Fight(b) must use to decide enemy to fight
        {
            
            if (Stun)
            {
                Interface.print(Name + " is stunned");
                return;
            }
            Random rand = new Random();
            int damage;
            int dodge = rand.Next(1, 100); //dodge chance

            try
            {
                if (dodge < MainGame.CurrentRoom.CHAR.Find(en => en.Name == b.Name).Speed) //if speed is higher than dodge chance, then dodge
                {
                    Interface.print(MainGame.CurrentRoom.CHAR.Find(en => en.Name == b.Name).Name + " dodged " + Name + "\'s move.");
                    return;
                }
            }
            catch (NullReferenceException)
            {
                MainGame.CurrentRoom.POTS.Find(en => en.Name == b.Name).Health -= 1;
                return;
            }
            //checks of item is equiped and calculates damage
            damage = (EItem == null) ? Strength - MainGame.CurrentRoom.CHAR.Find(en => en.Name == b.Name).Defense : Strength * EItem.Power - MainGame.CurrentRoom.CHAR.Find(en => en.Name == b.Name).Defense; //if no item held
            damage = (damage > 0) ? damage : 1;//if damage is negative set to 1
            Interface.print(Name + " did " + damage + " Damage to " + b.Name);
            MainGame.CurrentRoom.CHAR.Find(en => en.Name == b.Name).Health -= damage; //subtracts damage from health
            
        
        }
        public new void Die()
        {
            Interface.print("You have died....Would you like to restart?");
            //restart or whatever
        }
    }
}
