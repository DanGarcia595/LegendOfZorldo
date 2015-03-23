using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Game_Project
{
    class Command:AdvObj
    {
        public Command(string n="", string d="", int i=0)
        {
            name = n;
            desc = d;
            id = i;
        }

        public void Movement()
        {
            switch (name)
            {
                  
                case "UPARROW":
                    foreach (Character monster in MainGame.CurrentRoom.CHAR)
                    {
                        if (monster.X == MainGame.Lank.X && monster.Y == MainGame.Lank.Y - 1)
                        {
                            Interface.print("A "+monster.Name+" is blocking your path. Defeat it to move forward!");
                            return;
                        }
                    }
                    foreach (Character monster in MainGame.CurrentRoom.POTS)
                    {
                        if (monster.X == MainGame.Lank.X && monster.Y == MainGame.Lank.Y - 1)
                        {
                            Interface.print("A pot is blocking your path. Try destroying it using the FIGHT command.");
                            return;
                        }
                    }
                    foreach (Chest c in MainGame.CurrentRoom.CHEST)
                    {
                        if(c.X == MainGame.Lank.X && c.Y == MainGame.Lank.Y - 1 && !c.Open)
                        {
                            Interface.print("A chest blocks your path, perhaps open it?");
                            return;
                        }
                    }
                    if (MainGame.Lank.Y != 0)
                        Interface.DrawLank(MainGame.Lank.X, --MainGame.Lank.Y);
                    else
                        Interface.print("That would be a wall... Or maybe a door?");
                    break;
                case "DOWNARROW":
                    foreach (Character monster in MainGame.CurrentRoom.CHAR)
                    {
                        if (monster.X == MainGame.Lank.X && monster.Y == MainGame.Lank.Y + 1)
                        {
                            Interface.print("A "+monster.Name+" is blocking your path. Defeat it to move forward!");
                            return;
                        }
                    }
                    foreach (Character monster in MainGame.CurrentRoom.POTS)
                    {
                        if (monster.X == MainGame.Lank.X && monster.Y == MainGame.Lank.Y + 1)
                        {
                            Interface.print("A pot is blocking your path. Try destroying it using the FIGHT command.");
                            return;
                        }
                    }
                    foreach (Chest c in MainGame.CurrentRoom.CHEST)
                    {
                        if (c.X == MainGame.Lank.X && c.Y == MainGame.Lank.Y + 1 && !c.Open)
                        {
                            Interface.print("A chest blocks your path, perhaps open it?");
                            return;
                        }
                    }
                    if (MainGame.Lank.Y != Interface.roomHeight - 3)
                        Interface.DrawLank(MainGame.Lank.X, ++MainGame.Lank.Y);
                    else
                        Interface.print("That would be a wall... Or maybe a door?");
                    break;
                case "RIGHTARROW":
                    foreach (Character monster in MainGame.CurrentRoom.CHAR)
                    {
                        if (monster.X == MainGame.Lank.X+1 && monster.Y == MainGame.Lank.Y)
                        {
                            Interface.print("A "+monster.Name+" is blocking your path. Defeat it to move forward!");
                            return;
                        }
                    }
                    foreach (Character monster in MainGame.CurrentRoom.POTS)
                    {
                        if (monster.X == MainGame.Lank.X + 1 && monster.Y == MainGame.Lank.Y)
                        {
                            Interface.print("A pot is blocking your path. Try destroying it using the FIGHT command.");
                            return;
                        }
                    }
                    foreach (Chest c in MainGame.CurrentRoom.CHEST)
                    {
                        if (c.X == MainGame.Lank.X + 1 && c.Y == MainGame.Lank.Y && !c.Open)
                        {
                            Interface.print("A chest blocks your path, perhaps open it?");
                            return;
                        }
                    }
                    if (MainGame.Lank.X != Interface.roomWidth - 3)
                        Interface.DrawLank(++MainGame.Lank.X, MainGame.Lank.Y);
                    else
                        Interface.print("That would be a wall... Or maybe a door?");
                    break;
                case "LEFTARROW":
                    foreach (Character monster in MainGame.CurrentRoom.CHAR)
                    {
                        if (monster.X == MainGame.Lank.X - 1 && monster.Y == MainGame.Lank.Y)
                        {
                            Interface.print("A "+monster.Name+" is blocking your path. Defeat it to move forward!");
                            return;
                        }
                    }
                    foreach (Character monster in MainGame.CurrentRoom.POTS)
                    {
                        if (monster.X == MainGame.Lank.X - 1 && monster.Y == MainGame.Lank.Y)
                        {
                            Interface.print("A pot is blocking your path. Try destroying it using the FIGHT command.");
                            return;
                        }
                    }
                    foreach (Chest c in MainGame.CurrentRoom.CHEST)
                    {
                        if (c.X == MainGame.Lank.X - 1 && c.Y == MainGame.Lank.Y && !c.Open)
                        {
                            Interface.print("A chest blocks your path, perhaps open it?");
                            return;
                        }
                    }
                    if (MainGame.Lank.X != 0)
                        Interface.DrawLank(--MainGame.Lank.X, MainGame.Lank.Y);
                    else
                        Interface.print("That would be a wall... Or maybe a door?");
                    break;
            }
        }
        public void Use()
        {
            foreach (Item i in MainGame.Lank.Pocket)
                if (Temp.string1 == i.Name)
                    Temp.item1 = i;
            Temp.item1.Use();
            Interface.DrawInput(); 
            return;
        }
        public void Examine()   //still need to write this function (needs Richards room desc)
        {
            string s;               //examine items
            if (Temp.two)
            {
                s = Temp.string1 + " " + Temp.string2;
            }
            else
            {
                 s = Temp.string1;
            }
            
                bool OK = false;
                foreach (Item i in MainGame.Lank.Pocket)
                {
                    if (s == i.Name)
                    {
                        string[] lines = Regex.Split(i.Desc, "\n");
                        foreach (string line in lines){
                            Interface.print(line);
                        }
                        
                        Interface.DrawInput();
                        OK = true;
                    }
                }
            if(s=="ROOM")
            {
                Interface.print(MainGame.CurrentRoom.Desc);
                Interface.DrawInput();
                OK = true;
            }
                if (!OK)
                {
                    Interface.print("What is that?");
                    Interface.DrawInput();
                }
        }
        public void Help()      //manpages?
        {
            Console.Clear();
            Console.WriteLine();
            Console.SetBufferSize(75, 45);
            Console.WriteLine("                           HELP PAGES\n");
            foreach (Command c in MainGame.CommandL)
            {
                Console.WriteLine("     {0}: {1}", c.Name, c.Desc);
            }
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible=false;
            Console.ReadKey();
            Console.Clear();
            Console.CursorVisible=true;
            Console.SetBufferSize(75, 40);
            Interface.Refresh();
            Interface.DrawInput(); 
        }
        public void Open()      //requires Richards Room layout and chests
        {
            switch(Temp.string1)
            { 
                case "CHEST":
                    bool OK = false;
                    foreach (Chest c in MainGame.CurrentRoom.CHEST)
                    {
                        if ((MainGame.Lank.X + 1 == c.X && MainGame.Lank.Y == c.Y) | (MainGame.Lank.X - 1 == c.X && MainGame.Lank.Y == c.Y) | (MainGame.Lank.X == c.X && MainGame.Lank.Y + 1 == c.Y) | (MainGame.Lank.X == c.X && MainGame.Lank.Y - 1 == c.Y))
                        {
                            //may want to add a switch case here to buff health when he gets a new shield
                            switch (c.Contents.Name)
                            {
                                case "HEART CONTAINER":
                                    MainGame.Lank.Health = 400;
                                    c.Open = true;
                                    Interface.print("You got a " + c.Contents.Name + "!");
                                    Interface.Refresh();
                                    OK = true;
                                    Console.ReadKey();
                                    break;
                                case "RUPEE":
                                    MainGame.Lank.Rupee += c.Contents.Power;
                                    c.Open = true;
                                    Interface.print("You found " + c.Contents.Power + " " + c.Contents.Name + "S!");
                                    Interface.Refresh();
                                    OK = true;
                                    break;
                                case "KOKIRI SHIELD":
                                    MainGame.Lank.Pocket.Add(c.Contents);
                                    MainGame.Lank.Defense += 5;
                                    c.Open = true;
                                    Interface.print("You got the " + c.Contents.Name + "!");
                                    Interface.Refresh();
                                    OK = true;
                                    break;
                                case "NAYRUS TEARS":
                                    MainGame.Lank.Pocket.Add(c.Contents);
                                    MainGame.Lank.Defense += 10;
                                    c.Open = true;
                                    Interface.print("You got the " + c.Contents.Name + "!");
                                    Interface.Refresh();
                                    OK = true;
                                    break;
                                case "BOW":
                                    MainGame.Lank.Pocket.Add(c.Contents);
                                    MainGame.Lank.ChangePocket(MainGame.ItemL.ElementAt(9));
                                    MainGame.Lank.ChangePocket(MainGame.ItemL.ElementAt(9));
                                    c.Open = true;
                                    Interface.print("You got the " + c.Contents.Name + "!");
                                    Interface.Refresh();
                                    OK = true;
                                    break;
                                default:
                                    MainGame.Lank.Pocket.Add(c.Contents);
                                    c.Open = true;
                                    Interface.print("You got the " + c.Contents.Name + "!");
                                    Interface.Refresh();
                                    OK = true;
                                    break;
                            }
                        }
                    }
                    if (!OK)
                    {
                        Interface.print("There are no chests nearby...");
                        Interface.DrawInput();
                    }
                    break;
                case "DOOR":
                 //ROOMS
                    if (MainGame.Lank.X == 0 && MainGame.Lank.Y == (Interface.roomHeight-3) / 2)    //west
                    {
                        if (MainGame.CurrentRoom.ID == 303)
                        {
                            if (MainGame.Lank.Pocket.Contains(MainGame.ChestL.ElementAt(10).Contents)||MainGame.Lank.Pocket.Contains(MainGame.ItemL.ElementAt(13)))
                            {
                                Interface.print("You used the BOSS KEY.");
                                MainGame.CurrentRoom.LANK = true;
                                MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC - 4 && tmp.YLOC == MainGame.CurrentRoom.YLOC).CHAR=MainGame.Ganon();
                                MainGame.CurrentRoom = MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC - 4 && tmp.YLOC == MainGame.CurrentRoom.YLOC);
                                MainGame.CurrentRoom.PrintToRoom("lank");
                                MainGame.Lank.Room = MainGame.CurrentRoom.ID;
                                Interface.print(MainGame.CurrentRoom.Desc);
                                //Interface.DrawGameWindow(Interface.roomWidth, Interface.roomHeight, MainGame.CurrentRoom.North, MainGame.CurrentRoom.South, MainGame.CurrentRoom.East, MainGame.CurrentRoom.West);
                                Interface.Refresh();
                                Interface.DrawInput();
                                MainGame.sp.Stop();
                                MainGame.sp = new System.Media.SoundPlayer(@"End.wav");
                                MainGame.sp.PlayLooping();
                                return;
                            }
                            else
                            {
                                Interface.print("The door is locked...");
                                return;
                            }
                        }

                     MainGame.CurrentRoom.LANK = true;
                     MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC - 4 && tmp.YLOC == MainGame.CurrentRoom.YLOC).FillRoom(1, 5, 1, 5);
                     MainGame.CurrentRoom = MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC - 4 && tmp.YLOC == MainGame.CurrentRoom.YLOC);
                     MainGame.Lank.Room = MainGame.CurrentRoom.ID;
                     Interface.print(MainGame.CurrentRoom.Desc);
                     //Interface.DrawGameWindow(Interface.roomWidth, Interface.roomHeight, MainGame.CurrentRoom.North, MainGame.CurrentRoom.South, MainGame.CurrentRoom.East, MainGame.CurrentRoom.West);
                     Interface.Refresh();
                     Interface.DrawInput();
                 }
                 else if (MainGame.Lank.X == Interface.roomWidth-3 && MainGame.Lank.Y == (Interface.roomHeight-3) / 2)  //east
                 {
                     if (MainGame.CurrentRoom.ID == 301)
                     {
                         MainGame.CurrentRoom.LANK = true;
                         MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC + 4 && tmp.YLOC == MainGame.CurrentRoom.YLOC).FillRoom(15, 15, 1, 5);
                         MainGame.CurrentRoom = MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC + 4 && tmp.YLOC == MainGame.CurrentRoom.YLOC);
                         MainGame.Lank.Room = MainGame.CurrentRoom.ID;
                         Interface.print(MainGame.CurrentRoom.Desc);
                         //Interface.DrawGameWindow(Interface.roomWidth, Interface.roomHeight, MainGame.CurrentRoom.North, MainGame.CurrentRoom.South, MainGame.CurrentRoom.East, MainGame.CurrentRoom.West);
                         Interface.Refresh();
                         Interface.DrawInput();
                     }
                     else if (MainGame.CurrentRoom.ID == 316)
                     {
                         Interface.print("The door is locked...");
                         return;
                     }
                     else
                     {
                         MainGame.CurrentRoom.LANK = true;
                         MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC + 4 && tmp.YLOC == MainGame.CurrentRoom.YLOC).FillRoom(1, 5, 1, 5);
                         MainGame.CurrentRoom = MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC + 4 && tmp.YLOC == MainGame.CurrentRoom.YLOC);
                         MainGame.Lank.Room = MainGame.CurrentRoom.ID;
                         Interface.print(MainGame.CurrentRoom.Desc);
                         //Interface.DrawGameWindow(Interface.roomWidth, Interface.roomHeight, MainGame.CurrentRoom.North, MainGame.CurrentRoom.South, MainGame.CurrentRoom.East, MainGame.CurrentRoom.West);
                         Interface.Refresh();
                         Interface.DrawInput();
                     }
                 }
                 else if (MainGame.Lank.X == (Interface.roomWidth - 3)/2 && MainGame.Lank.Y == 0)   //north
                 {
                     MainGame.CurrentRoom.LANK = true;
                     MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC  && tmp.YLOC == MainGame.CurrentRoom.YLOC -3).FillRoom(1, 5, 1, 5);
                     MainGame.CurrentRoom = MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC  && tmp.YLOC == MainGame.CurrentRoom.YLOC - 3);
                     MainGame.Lank.Room = MainGame.CurrentRoom.ID;
                     Interface.print(MainGame.CurrentRoom.Desc);
                        //Interface.DrawGameWindow(Interface.roomWidth, Interface.roomHeight, MainGame.CurrentRoom.North, MainGame.CurrentRoom.South, MainGame.CurrentRoom.East, MainGame.CurrentRoom.West);
                     Interface.Refresh();
                     Interface.DrawInput();
                 }
                 else if (MainGame.Lank.X == (Interface.roomWidth - 3)/2 && MainGame.Lank.Y == Interface.roomHeight - 3)    //south
                 {
                     MainGame.CurrentRoom.LANK = true;
                     MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC && tmp.YLOC == MainGame.CurrentRoom.YLOC+3).FillRoom(1, 5, 1, 5);
                     MainGame.CurrentRoom = MainGame.RoomL.Find(tmp => tmp.XLOC == MainGame.CurrentRoom.XLOC && tmp.YLOC == MainGame.CurrentRoom.YLOC+3);
                     MainGame.Lank.Room = MainGame.CurrentRoom.ID;
                     Interface.print(MainGame.CurrentRoom.Desc);
                     //Interface.DrawGameWindow(Interface.roomWidth, Interface.roomHeight, MainGame.CurrentRoom.North, MainGame.CurrentRoom.South, MainGame.CurrentRoom.East, MainGame.CurrentRoom.West);
                     Interface.Refresh();
                     Interface.DrawInput();
                  }
                 else Interface.print("Cant open door");
                 break;
        }
        }
        public void Fight()     //parse input once more then pass to Bobby's fight function
        {
            Character monsterFight = new Character();
            //bool North = true, South = true, East = true, West = true;
            if (MainGame.Lank.EItem.Name == "BOW")
            {
                try
                {
                    MainGame.Lank.Pocket.Find(tmp => tmp.Name == "ARROWS").Power--;
                    if (MainGame.Lank.Pocket.Find(tmp => tmp.Name == "ARROWS").Power == 0)
                    {
                        MainGame.Lank.Pocket.Remove(MainGame.Lank.Pocket.Find(tmp => tmp.Name == "ARROWS"));
                    }
                    Interface.DrawPocket();
                    switch (Temp.string1)
                    {
                        case "NORTH":
                            foreach (Character monster in MainGame.CurrentRoom.CHAR)
                            {
                                if (monster.X == MainGame.Lank.X && monster.Y < MainGame.Lank.Y)
                                {
                                    monsterFight = monster;
                                    monsterFight.Y--;
                                    goto MORTAL;
                                }
                            }
                            break;
                        case "SOUTH":
                            foreach (Character monster in MainGame.CurrentRoom.CHAR)
                            {
                                if (monster.X == MainGame.Lank.X && monster.Y > MainGame.Lank.Y)
                                {
                                    monsterFight = monster;
                                    monsterFight.Y++;
                                    goto MORTAL;
                                }
                            }
                            break;
                        case "EAST":
                            foreach (Character monster in MainGame.CurrentRoom.CHAR)
                            {
                                if (monster.X > MainGame.Lank.X && monster.Y == MainGame.Lank.Y)
                                {
                                    monsterFight = monster;
                                    monsterFight.X++;
                                    goto MORTAL;
                                }
                            }
                            break;
                        case "WEST":
                            foreach (Character monster in MainGame.CurrentRoom.CHAR)
                            {
                                if (monster.X < MainGame.Lank.X && monster.Y == MainGame.Lank.Y)
                                {
                                    monsterFight = monster;
                                    monsterFight.X--;
                                    goto MORTAL;
                                }
                            }
                            break;
                        default:
                            Interface.print("You cannot hit anything from here...");
                            return;
                    }
                }
                catch (NullReferenceException)
                {
                        Interface.print("You don't have any arrows!");
                        Interface.DrawInput();
                        return;
                }

            }
                foreach (Character monster in MainGame.CurrentRoom.CHAR)
                {
                    if (monster.X + 1 == MainGame.Lank.X && monster.Y == MainGame.Lank.Y && Temp.string1 == "WEST")
                        monsterFight = monster;
                    else if (monster.X - 1 == MainGame.Lank.X && monster.Y == MainGame.Lank.Y && Temp.string1 == "EAST")
                        monsterFight = monster;
                    else if (monster.Y - 1 == MainGame.Lank.Y && monster.X == MainGame.Lank.X && Temp.string1 == "SOUTH")
                        monsterFight = monster;
                    else if (monster.Y + 1 == MainGame.Lank.Y && monster.X == MainGame.Lank.X && Temp.string1 == "NORTH")
                        monsterFight = monster;
                }
                foreach (Character monster in MainGame.CurrentRoom.POTS)
                {
                    if (monster.X + 1 == MainGame.Lank.X && monster.Y == MainGame.Lank.Y && Temp.string1 == "WEST")
                        monsterFight = monster;
                    else if (monster.X - 1 == MainGame.Lank.X && monster.Y == MainGame.Lank.Y && Temp.string1 == "EAST")
                        monsterFight = monster;
                    else if (monster.Y - 1 == MainGame.Lank.Y && monster.X == MainGame.Lank.X && Temp.string1 == "SOUTH")
                        monsterFight = monster;
                    else if (monster.Y + 1 == MainGame.Lank.Y && monster.X == MainGame.Lank.X && Temp.string1 == "NORTH")
                        monsterFight = monster;
                }
            MORTAL: try
                {

                    MainGame.Lank.Fight(monsterFight);



                    // MainGame.Lank.Fight(MainGame.Navi);
               //     if (MainGame.CurrentRoom.CHAR.Find(en => en.Name == monsterFight.Name).Health <= 0)
                 //       MainGame.CurrentRoom.CHAR.Find(en => en.Name == monsterFight.Name).Die();
                //    else
                 //   {
                     //   Interface.print(MainGame.CurrentRoom.CHAR.Find(en => en.Name == monsterFight.Name).Name + " has " + MainGame.CurrentRoom.CHAR.Find(en => en.Name == monsterFight.Name).Health + " Health");
                        MainGame.CurrentRoom.CHAR.Find(en => en.Name == monsterFight.Name).Fight();

                        if (MainGame.Lank.Health <= 0)
                            MainGame.Lank.Die();

                       // Interface.print(MainGame.Lank.Name + " has " + MainGame.Lank.Health + " Health");
                 //   }
                    Interface.RefreshHealth(MainGame.Lank.Health);
                }
                catch (NullReferenceException)
                {
                    try
                    {
                        MainGame.CurrentRoom.POTS.Find(en => en.Name == monsterFight.Name).Die();
                        Interface.RefreshHealth(MainGame.Lank.Health);
                    }
                    catch (NullReferenceException)
                    {
                        Interface.print("Cannot fight in that direction");
                        Interface.DrawInput();
                    }
                }
                Interface.DrawInput();
            
        }
        public void Equip()     //Can write this later, not important atm
        {
            string s;
            Item x = new Item();  
            if (Temp.two)
            {
                s = Temp.string1 + " " + Temp.string2;
            }
            else
            {
                s = Temp.string1;
            }
            foreach (Item i in MainGame.Lank.Pocket)
                if (s == i.Name)
                    x = i;
            if (x.Name == "")
            {
                Interface.print("You don't have that...");
                Interface.DrawInput();
                return;
            }
            switch (x.Name)
            {
                case "BOW":
                case "WOODEN SWORD":
                case "KOKIRI SWORD":
                case "DINS EDGE":
                case "BOOMERANG":
                        if (MainGame.Lank.Pocket.Contains(x))
                        {
                            MainGame.Lank.EItem = x;
                            Interface.DrawInput();
                            Interface.DrawPocket();
                            return;
                        }
                        else
                        {
                            Interface.print("You don't have that...");
                            Interface.DrawInput();
                            return;
                        }
                default:
                            Interface.print("Oak's words echoed... There's a time and place for everything, but not now.");
                            Interface.DrawInput();
                            return;
            }
        }
        public void SaveGame()   //saves the current game, mostly done 
        { 
            Save.SaveGame(); 
            Interface.DrawInput();
        }
        public void LoadGame()     //loads the current game, same as save
        { 
            Save.LoadGame();
            Interface.Refresh();
            Interface.DrawInput();
        }
        public void Map()       //RICHARD!
        { 
		Room.Map(MainGame.Lank);
		Interface.DrawInput();
		}
        public void Execute()
        {
            switch (name)
            {
                case "UPARROW":
                case "DOWNARROW":
                case "RIGHTARROW":
                case "LEFTARROW":
                    this.Movement();
                    break;
                case "USE":
                    this.Use();
                    break;
                case "EXAMINE":
                    this.Examine();
                    break;
                case "HELP":
                    this.Help();
                    break;
                case "OPEN":
                    this.Open();
                    break;
                case "FIGHT":
                    this.Fight();
                    break;
                case "EQUIP":
                    this.Equip();
                    break;
                case "MAP":
                    this.Map();
                    break;
                case "EXIT":
                    Environment.Exit(0);
                    break;
                case "SAVE":
                    this.SaveGame();
                    break;
                case "LOAD":
                    this.LoadGame();
                    break;
                default:
                    Interface.print("How did you do that???");
                    break;
            }
        }

        /*
           bool OK = true;
                do{
            Command c = GetCommand();
            Console.WriteLine("{0} {1}", c.Name,Temp.string1);      //TEST COMMANDS
                }while(OK);  */
    }
}
