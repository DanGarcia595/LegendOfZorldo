using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project
{
    static class Interface 
    {
        public static Queue<string> OutputQueue = new Queue<string>();
        public static int oldX = 14;
        public static int oldY = 3;
       // public static int oldXchar = 25;
       // public static int oldYchar = 12;
        public static int Health = 300;
        public static bool north = true;
        public static bool south = true;
        public static bool east = true;
        public static bool west = true;
        public static List<Item> I = new List<Item>();
        public static int roomHeight = 20;
        public static int roomWidth = 30;
        public static void StartDraw() 
        {
            Console.Title = "Legend of Zorldo!";
            Console.SetWindowSize(75, 40);
            Console.SetBufferSize(75, 40);
            Console.SetCursorPosition(4, 15);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(@"
       __                      __       ___  ____          __   __   
      / / ___ ___  ___ ___ ___/ / ___  / _/ /_  /___  ____/ /__/ /__ 
     / /_/ -_/ _ `/ -_/ _ / _  / / _ \/ _/   / // _ \/ __/ / _  / _ \
    /____\__/\_, /\__/_//_\_,_/  \___/_/    /___\___/_/ /_/\_,_/\___/
            /___/                                                    ");
            Console.SetCursorPosition(20, 39);
            Console.Write("Press any key to continue...");

            //Console.ResetColor();
            Console.ReadKey();
        }
        public static void DrawGameWindow(int RoomWidth, int RoomHeight, bool North, bool South, bool East, bool West)//, Character Lank)
        {
            roomHeight = RoomHeight;
            roomWidth = RoomWidth;
            Console.Clear();          
            //Console.Write(" "+ Lank.Rupee);
            Console.WriteLine("");
            Console.SetCursorPosition(1, 1);
            Console.Write("╔");
            for (int i = 0; i < RoomWidth - 2; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");
            for (int i = 0; i < RoomHeight - 2; i++)
            {
                Console.SetCursorPosition(1, 2+i);
                Console.Write("║");
            }
            Console.SetCursorPosition(1, RoomHeight);
            Console.Write("╚");
            for (int i = 0; i < RoomWidth - 2; i++)
            {
                Console.Write("═");
            }
            Console.SetCursorPosition(RoomWidth, 2);
            for (int i = 0; i < RoomHeight - 2; i++)
            {
                Console.SetCursorPosition(RoomWidth, 2 + i);
                Console.Write("║");
            }
            Console.SetCursorPosition(RoomWidth, RoomHeight);
            Console.Write("╝");
            if (North)
            {
                Console.SetCursorPosition((RoomWidth/2)-2, 1);
                Console.Write("─────");
            }
            if (South)
            {
                Console.SetCursorPosition((RoomWidth / 2) - 2, (RoomHeight));
                Console.Write("─────");
            }
            if (West)
            {
                Console.SetCursorPosition(1, (RoomHeight/2)-1);
                Console.Write("│");
                Console.SetCursorPosition(1, (RoomHeight) / 2);
                Console.Write("│");
                Console.SetCursorPosition(1, (RoomHeight / 2)+1);
                Console.Write("│");
            }
            if (East)
            {
                Console.SetCursorPosition(RoomWidth, (RoomHeight / 2) - 1);
                Console.Write("│");
                Console.SetCursorPosition(RoomWidth, (RoomHeight) / 2);
                Console.Write("│");
                Console.SetCursorPosition(RoomWidth, (RoomHeight / 2) + 1);
                Console.Write("│");
            }
            North = north; South = south; East = east; West = west;
            foreach (Character Monster in MainGame.CurrentRoom.CHAR)
            {

            }
            Console.SetCursorPosition(4, Console.WindowHeight - 2);
        }
        public static void RefreshHealth(int health)
        {
            for (int i = 0; i < (Health / 100) + 6;i++ )
            {
                Console.SetCursorPosition(1+i, 0);
                Console.Write(" ");
            }
            Health = health;
            health /= 100;
            Console.SetCursorPosition(1, 0);
            for (int i = 0; i < health; i++)
            {
                Console.Write("♥");
            }
            Console.Write(" ♦: " + MainGame.Lank.Rupee);
        }
        public static void DrawInput()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("─");
            }
            Console.Write(">> ");
            for (int i = 0; i < Console.WindowWidth - 2; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(4, Console.WindowHeight - 2);
        }
        public static void DrawLank(int newX, int newY)
        {
            Console.SetCursorPosition(2+oldX, 2+oldY);
            Console.Write(" ");
            Console.SetCursorPosition(2+newX, 2+newY);
            Console.Write("☻");
            oldX = newX;
            oldY = newY;
            Console.SetCursorPosition(4, Console.WindowHeight - 2);
        }
        public static void DrawCharacter(Character Char ,int oldX, int oldY)
        {

            Console.SetCursorPosition(2 + oldX, 2 + oldY);
            Console.Write(" ");
            Console.SetCursorPosition(2 + Char.X, 2 + Char.Y);
            Console.Write(Char.Symbol);
            Console.SetCursorPosition(4, Console.WindowHeight - 2);
        }
        public static void DrawChest(Chest C)
        {

            Console.SetCursorPosition(2 + C.X, 2 + C.Y);
            Console.Write("C");
            Console.SetCursorPosition(4, Console.WindowHeight - 2);
        }
        public static void DrawPocket()
        {
            ClearPocket();
            I = MainGame.Lank.Pocket;
            int j = 0;
            for (int i = 0; i < roomHeight+1; i++)
            {
                Console.SetCursorPosition(roomWidth + 10/*7*/, i);
                Console.Write("│");
            }
            for (int i = 0; i < roomHeight + 1; i++)
            {
                Console.SetCursorPosition(roomWidth + 34/*37*/, i);
                Console.Write("│");
            }
            Console.SetCursorPosition(roomWidth + 19, 1);
            Console.Write("Pocket");
            foreach (Item thing in MainGame.Lank.Pocket)
            {
                try
                {
                    if (thing.Name == MainGame.Lank.EItem.Name)
                    {
                        Console.SetCursorPosition(roomWidth + 13, 3 + j);
                        Console.Write("X {0}: {1}", thing, thing.Power);
                    }
                    else
                    {
                        Console.SetCursorPosition(roomWidth + 15, 3 + j);
                        Console.Write("{0}: {1}", thing, thing.Power);

                    }
                }
                catch (NullReferenceException)
                {
                    Console.SetCursorPosition(roomWidth + 15, 3 + j);
                    Console.Write("{0}: {1}", thing, thing.Power);
                }
                j++;
            }
            Console.SetCursorPosition(4, Console.WindowHeight - 2);
        }
        public static void ClearPocket()
        {
            for (int i = 0; i < roomHeight + 1; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    Console.SetCursorPosition(roomWidth + 8 + j, i);
                    Console.Write(" ");
                }
            }
        }
        public static void Refresh()
        {
            DrawGameWindow(roomWidth, roomHeight, MainGame.CurrentRoom.North, MainGame.CurrentRoom.South, MainGame.CurrentRoom.East, MainGame.CurrentRoom.West);
            RefreshHealth(MainGame.Lank.Health);
            DrawInput();
            CleanOutput();
            DrawLank(oldX, oldY);
            print("");
            DrawPocket();
            foreach (Character Monster in MainGame.CurrentRoom.CHAR)
            {
                try 
                { 
                    DrawCharacter(Monster, Monster.X,Monster.Y);
                }
                catch (NullReferenceException) { }                               
            }
            foreach (Character pot in MainGame.CurrentRoom.POTS)
            {
                try
                {
                    DrawCharacter(pot, pot.X, pot.Y);
                }
                catch (NullReferenceException) { }
            }

        }
        public static void CleanOutput()
        {
            int i;
            Console.SetCursorPosition(0, roomHeight + 1);
            for (i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("─");
            }
            i = 0;
            foreach(string element in OutputQueue){
                for (int j = 0; j < element.Length; j++)
                {
                    Console.SetCursorPosition(j,  roomHeight + 2 + i);
                    Console.Write(" ");
                }
            i++;
            }

        }
        public static void print(string output)
        {
            
            string[] lines = Regex.Split(output, "\n");
           // if (lines.Length>1)
            foreach (string line in lines)
            {
                
                CleanOutput();
                Console.SetCursorPosition(0, roomHeight + 2);
                if (OutputQueue.Count == (Console.WindowHeight - 4) - (roomHeight + 2))
                {
                    OutputQueue.Dequeue();
                    OutputQueue.Enqueue(line);
                }
                else
                {
                    OutputQueue.Enqueue(line);
                }
                foreach(string thing in OutputQueue){
                        Console.WriteLine(thing);
                }
                Console.SetCursorPosition(4, Console.WindowHeight - 2);
            }

        }
        
    }
}
