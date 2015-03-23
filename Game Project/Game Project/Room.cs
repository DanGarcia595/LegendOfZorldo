using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project
{
    class Room:AdvObj
    {
        public Random rand = new Random();
        //fields
        private int xLocation;
        private int yLocation;
        private List<Character> characters;
        private List<Character> pots;
        private List<Chest> chests;
        private bool lank;
        //private List<string> exit;
        private bool north;
        private bool south;
        private bool east;
        private bool west;

        //properties
        public int XLOC
        {
            get { return xLocation; }
            set { xLocation = value; }
        }//X coordinate of room
        public int YLOC
        {
            get { return yLocation; }
            set { yLocation = value; }
        }//Y coordinate of room
      public List<Character> CHAR
        {
            get { return characters; }
            set { characters = value; }
        }//Characters in the room
      public List<Character> POTS
        {
            get { return pots; }
            set { pots = value; }
        }//Number of pots in the room
        public List<Chest> CHEST
        {
            get { return chests; }
            set { chests = value; }
        }//Chests in the room
        public bool LANK
        {
            get { return lank; }
            set { lank = value; }
        }//Has Lank been in the room before?
        public bool North
        {
            get { return north; }
            set { north = value; }
        }
        public bool South
        {
            get { return south; }
            set { south = value; }
        }
        public bool East
        {
            get { return east; }
            set { east = value; }
        }
        public bool West
        {
            get { return west; }
            set { west = value; }
        }

        //constructor
        Room()
        {
            xLocation = 0;
            yLocation = 0;
            characters = new List<Character>();
            pots = new List<Character>();
            chests = new List<Chest>();
            lank = false;
            north = false;
            south = false;
            east = false;
            west = false;
            Name = "";
            Desc = "";
            ID = 0;
        }
        Room(int xLocatio = 0, int yLocatio = 0, List<Character> character = null, List<Character> pot = null, List<Chest> chest = null, bool lan = false, bool n = false, bool s = false, bool e = false, bool w = false, string name = null, string description = null, int id = 0)
        {
            xLocation = xLocatio;
            yLocation = yLocatio;
            characters = character;
            pots = pot;
            chests = chest;
            lank = lan;
            north = n;
            south = s;
            east = e;
            west = w;
            //exit = exi;
            Name = name;
            Desc = description;
            ID = id;

        }
		//methods
        static public List<Room> CreateRooms()
        {

            Room room1, room2, room3, room4, room5, room6, room7, room8, room9, room10, room11, room12, room13, room14, room15, roomB;
            room1 = new Room(29, 15, new List<Character>(), new List<Character>(), new List<Chest>(), false, false,true,true,false, "Room 1", "You entered a room. It smells...", 301);
            room2 = new Room(33, 15, new List<Character>(), new List<Character>(), new List<Chest>(), false, false, false, false, true, "Room 2", "You entered a room. It's dark...", 302);
            roomB = new Room(37, 15, new List<Character>(), new List<Character>(), new List<Chest>(), false, false, false, true, false, "Room B", "As you step into the room, the door slams shut behind you \nwith a loud clang.Inside is a dimly lit room smelling \nfaintly of potpourri and wizardry.", 316);//  In the middle, tied to pillar is Zorldo", 316);
            room3 = new Room(41, 15, new List<Character>(), new List<Character>(), new List<Chest>(), false, false, false, true, true, "Room 3", "You entered a room.", 303);
            room4 = new Room(45, 15, new List<Character>(), new List<Character>(), new List<Chest>(), false, false,true,false,true,  "Room 4", "You entered a room.", 304);
            room5 = new Room(29, 18, new List<Character>(), new List<Character>(), new List<Chest>(), false, true,true,false,false, "Room 5", "You entered a room.", 305);
            room6 = new Room(37, 18, new List<Character>(), new List<Character>(), new List<Chest>(), false, false,true,false,false, "Room 6", "You entered a room.", 306);
            room7 = new Room(45, 18, new List<Character>(), new List<Character>(), new List<Chest>(), false, true,true,false,false, "Room 7", "You entered a room.", 307);
            room8 = new Room(29, 21, new List<Character>(), new List<Character>(), new List<Chest>(), false, true,false,true,false, "Room 8", "You entered a room.", 308);
            room9 = new Room(33, 21, new List<Character>(), new List<Character>(), new List<Chest>(), false, false,false,true,true, "Room 9", "You entered a room.", 309);
            room10 = new Room(37, 21, new List<Character>(), new List<Character>(), new List<Chest> { MainGame.ChestL.ElementAt(15), MainGame.ChestL.ElementAt(12) }, false, true, true, true, true, "Room 10", "You entered a room. This room makes you feel good... Don't ask why...", 310);
            room11 = new Room(41, 21, new List<Character>(), new List<Character>(), new List<Chest>(), false, false,true,true,true, "Room 11", "You entered a room.", 311);
            room12 = new Room(45, 21, new List<Character>(), new List<Character>(), new List<Chest>(), false, true,false,false,true, "Room 12", "You entered a room.", 312);
            room13 = new Room(33, 24, new List<Character>(), new List<Character>(), new List<Chest>(), false, false,false,true,false, "Room 13", "You entered a room.", 313);
            room14 = new Room(37, 24, new List<Character>(), new List<Character>(), new List<Chest> { MainGame.ChestL.ElementAt(14), MainGame.ChestL.ElementAt(11) }, true, true, false, true, true, "Room 14", "You entered a room. Haven't I been here before?", 314);
            room15 = new Room(41, 24, new List<Character>(), new List<Character>(), new List<Chest>(), false, true,false,false,true, "Room 15", "You entered a room.", 315);
            List<Room> rooms= new List<Room>{room1, room2, roomB, room3, room4, room5, room6, room7, room8, room9, room10, room11, room12, room13, room14, room15};
            return rooms;
            
        }
        static public void Map(Lank LANK)
        {
            if (!LANK.Pocket.Contains(MainGame.ChestL.ElementAt(11).Contents))
            {
                Interface.print("Too bad you don't know how to read!");
            }
            else
            {
                Console.Clear();
                List<Room> temp = MainGame.RoomL;
                foreach (Room i in temp)
                {
                    Console.SetCursorPosition(i.XLOC, i.YLOC);
                    switch (i.LANK)
                    {
                        case (true): Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            break;
                        case (false): Console.BackgroundColor = ConsoleColor.Yellow;
                            break;
                    }
                    if (LANK.Room == i.ID)
                        Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("   ");
                    Console.SetCursorPosition(i.XLOC, i.YLOC + 1);
                    if (LANK.Pocket.Contains(MainGame.ChestL.ElementAt(12).Contents) && i.CHEST.Count != 0)
                    {
                        bool OK = false;
                        foreach (Chest c in i.CHEST)
                            if (!c.Open)
                                OK = true;
                        if(OK)
                            Console.Write("  C");
                        else
                            Console.Write("   ");
                    }
                    else
                        Console.Write("   ");
                }
                Console.CursorVisible = false;
                Console.ReadKey();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.CursorVisible = true;
                Interface.Refresh();
            }

        }//Prints the map, ends on any key press and refreshes the UI
        /*public void ExitRoom()
        {
            if (EXIT.Contains("south"))
            {
                //DrawRoom(North)
            }
            else if (EXIT.Contains("north"))
            {
                //DrawRoom
            }
            else if (EXIT.Contains("east"))
            {
                //DrawRoom(east)
            }
            else if (EXIT.Contains("west"))
            {
                //DrawRoom(west)
            }
        }
        public void DrawRoom()
        {
            if (EXIT.Contains("south"))
            {
               // Console.SetCursorPosition();
                Console.Write("#");
            }
            else if (EXIT.Contains("north"))
            {
               // Console.SetCursorPosition();
                Console.Write("#");
            }
            else if (EXIT.Contains("east"))
            {
                //Console.SetCursorPosition();
                Console.Write("#");
            }
            else if (EXIT.Contains("west"))
            {
               // Console.SetCursorPosition();
                Console.Write("#");
            }
            
        }*/
        public void FillRoom(int lowMonster, int highMonster, int lowPots, int highPots)
        {
            //Random rand = new Random();
            int numMonsters = rand.Next(lowMonster, highMonster);
            List<Character> Pots = MainGame.Pots();
            for (int i = 0; i < numMonsters; i++)
            {
                CHAR.Add(Randomizer("monster", Pots));
            }
            int numPots = rand.Next(lowPots, highPots);

            for (int i = 0; i < numPots; i++)
            {
                POTS.Add(Randomizer("pot", Pots));
            }

            List<XY> coordinates = new List<XY>();
            XY dummy = new XY();
            coordinates = dummy.Coordinates();
            int temp;
            for (int i = 0; i < numMonsters; i++)
            {
                temp = rand.Next(coordinates.Count);
                CHAR[i].X = coordinates.ElementAt(temp).X;
                CHAR[i].Y = coordinates.ElementAt(temp).Y;
                coordinates.RemoveAt(temp);
            }
            for (int i = 0; i < numPots; i++)
            {
                temp = rand.Next(coordinates.Count);
                POTS[i].X = coordinates.ElementAt(temp).X;
                POTS[i].Y = coordinates.ElementAt(temp).Y;
                coordinates.RemoveAt(temp);
            }


            this.PrintToRoom("monster");
            this.PrintToRoom("pots");
            this.PrintToRoom("lank");

        }
        public void PrintToRoom(string type) //Takes pot, monster, or lank  and prints out at random locations (Lank is printed at the start of the room)
        {
            switch (type)
            {
                case ("pots"): for (int i = 0; i < POTS.Count; i++) { Interface.DrawCharacter(POTS[i], POTS[i].X, POTS[i].Y); } break;
                case ("monster"): for (int i = 0; i < CHAR.Count; i++) { Interface.DrawCharacter(CHAR[i], CHAR[i].X, CHAR[i].Y); } break;
                case ("lank"): 
                    Interface.DrawLank(MainGame.Lank.X = Math.Abs(MainGame.Lank.X - Interface.roomWidth+3),MainGame.Lank.Y = Math.Abs(MainGame.Lank.Y - Interface.roomHeight+4));
                    break;

            }
        }
        public Character Randomizer(string monstPot, List<Character> Pots)
        {
            if (monstPot == "monster")
            {
                List<Character> Enemies = MainGame.Characters();
                //Random rand = new Random();
                Character monster = new Character();
                monster = Enemies.ElementAt(rand.Next(0, 6));
                return monster;
            }
            else if (monstPot == "pot")
            {
                //List<Character> Pots = MainGame.Pots();

                Character Pot = new Character();
                int temp = rand.Next(0, Pots.Count);
                Pot = Pots.ElementAt(temp);
                Pots.Remove(Pot);

                return Pot;
            }
            else return null;
        }//Returns a random monster
    }
}