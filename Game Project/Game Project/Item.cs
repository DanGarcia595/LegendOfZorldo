using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project
{
    class Item:AdvObj
    {
        //fields
        private string type;
        private int power;  //power also doubles as count for utility/di/other items 

        //properties        
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Power
        {
            get { return power; }
            set { power = value; }
        }

        //methods
        public Item(string n="", string d="", string t="", int p=0,int i=0)
        {
            name = n;
            desc = d;
            type = t;
            power = p;
            id = i;
        }

public void Use()
        {
            switch (name)
            {
                case "BOMBS":
                    {
                        Interface.print("You placed a bomb to the "+ Temp.string2+".");
                        break;
                    }
                case "NIKES":
                    {
                        switch (Temp.string2)
                        {
                            case "NORTH":
                                MainGame.CommandL.ElementAt(0).Movement();
                                MainGame.CommandL.ElementAt(0).Movement();
                                MainGame.CommandL.ElementAt(0).Movement();
                                MainGame.CommandL.ElementAt(0).Movement();
                                MainGame.CommandL.ElementAt(0).Movement();
                                MainGame.CommandL.ElementAt(0).Movement();
                                Interface.print("You used your NIKES, nice work!");
                                break;
                            case "SOUTH":
                                MainGame.CommandL.ElementAt(1).Movement();
                                MainGame.CommandL.ElementAt(1).Movement();
                                MainGame.CommandL.ElementAt(1).Movement();
                                MainGame.CommandL.ElementAt(1).Movement();
                                MainGame.CommandL.ElementAt(1).Movement();
                                MainGame.CommandL.ElementAt(1).Movement();
                                Interface.print("You used your NIKES, nice work!");
                                break;
                            case "EAST":
                                MainGame.CommandL.ElementAt(2).Movement();
                                MainGame.CommandL.ElementAt(2).Movement();
                                MainGame.CommandL.ElementAt(2).Movement();
                                MainGame.CommandL.ElementAt(2).Movement();
                                MainGame.CommandL.ElementAt(2).Movement();
                                MainGame.CommandL.ElementAt(2).Movement();
                                Interface.print("You used your NIKES, nice work!");
                                break;
                            case "WEST":
                                MainGame.CommandL.ElementAt(3).Movement();
                                MainGame.CommandL.ElementAt(3).Movement();
                                MainGame.CommandL.ElementAt(3).Movement();
                                MainGame.CommandL.ElementAt(3).Movement();
                                MainGame.CommandL.ElementAt(3).Movement();
                                MainGame.CommandL.ElementAt(3).Movement();
                                Interface.print("You used your NIKES, nice work!");
                                break;
                        }
                        break;
                    }
                case "FAIRY":
                    if (MainGame.Lank.Health == 300)
                    {
                        Interface.print("You already have full health!");
                        return;
                    }
                    MainGame.Lank.Health = 300;
                    Interface.RefreshHealth(MainGame.Lank.Health);
                    MainGame.Lank.Pocket.Find(t => t.Name == "FAIRY").Power -= 1;
                    if (MainGame.Lank.Pocket.Find(t => t.Name == "FAIRY").Power==0)
                    {
                        MainGame.Lank.Pocket.Remove(MainGame.Lank.Pocket.Find(t => t.Name == "FAIRY"));
                        Interface.DrawPocket();
                    }
                    Interface.DrawPocket();
                    break;
                case "HEART":
                    if (MainGame.Lank.Health == 300)
                    {
                        Interface.print("You already have full health!");
                        return;
                    }
                    MainGame.Lank.Health += 100;
                    if (MainGame.Lank.Health > 300)
                        MainGame.Lank.Health = 300;
                    Interface.RefreshHealth(MainGame.Lank.Health);
                    MainGame.Lank.Pocket.Find(t => t.Name == "HEART").Power -= 1;
                    if (MainGame.Lank.Pocket.Find(t => t.Name == "HEART").Power == 0)
                    {
                        MainGame.Lank.Pocket.Remove(MainGame.Lank.Pocket.Find(t => t.Name == "HEART"));
                        Interface.DrawPocket();

                    }
                    Interface.DrawPocket();
                    break;
                case "BOOMERANG":
                    bool OK=false;
                    switch (Temp.string2)
                    {
                        case "NORTH":
                            foreach (Character monster in MainGame.CurrentRoom.CHAR)
                            {
                                if (monster.Stun)
                                {
                                    Interface.print("Your BOOMERANG has not yet returned.");
                                    return;
                                }
                                if (monster.X == MainGame.Lank.X && monster.Y < MainGame.Lank.Y)
                                {
                                    monster.Stun = true;
                                    Interface.print("You have stunned a " + monster.Name);
                                    Temp.int1 = 3;
                                    OK = true;
                                }
                            }
                            break;
                        case "SOUTH":
                            foreach (Character monster in MainGame.CurrentRoom.CHAR)
                            {
                                if (monster.Stun)
                                {
                                    Interface.print("Your BOOMERANG has not yet returned.");
                                    return;
                                }
                                if (monster.X == MainGame.Lank.X && monster.Y > MainGame.Lank.Y)
                                {
                                    monster.Stun = true;
                                    Interface.print("You have stunned a "+monster.Name);
                                    Temp.int1 = 6;
                                    OK = true;
                                }
                            }
                            break;
                        case "EAST":
                            foreach (Character monster in MainGame.CurrentRoom.CHAR)
                            {
                                if (monster.Stun)
                                {
                                    Interface.print("Your BOOMERANG has not yet returned.");
                                    return;
                                }
                                if (monster.X > MainGame.Lank.X && monster.Y == MainGame.Lank.Y)
                                {
                                    monster.Stun = true;
                                    Interface.print("You have stunned a " + monster.Name);
                                    Temp.int1 = 3;
                                    OK = true;
                                }
                            }
                            break;
                        case "WEST":
                            foreach (Character monster in MainGame.CurrentRoom.CHAR)
                            {
                                if (monster.Stun)
                                {
                                    Interface.print("Your BOOMERANG has not yet returned.");
                                    return;
                                }
                                if (monster.X < MainGame.Lank.X && monster.Y == MainGame.Lank.Y)
                                {
                                    monster.Stun = true;
                                    Interface.print("You have stunned a " + monster.Name);
                                    Temp.int1 = 3;
                                    OK = true;
                                }
                            }
                            break;

            }
                    if(!OK)
                        Interface.print("You cannot hit anything from here...");
                    break;
                case "HOOKSHOT":
                    {
                        Interface.print("You used the Hookshot!");
                        break;
                    }
                default:
                    {
                        Interface.print("Oak's words echoed... There's a time and place for everything, but not now.");
                        break;
                    }
            }
        }

        public override string ToString()
        {
            return name;
        }
       
    }
}
