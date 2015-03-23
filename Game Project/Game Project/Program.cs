using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
namespace Game_Project
{

    class MainGame
    {
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0x0F020;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        //---------INITIALIZERS---------//
        public const bool ADD = true;
        public const bool REMOVE = false;
        public const int CHESTOFFSET = 50;
        public const int ITEMOFFSET = 1;
        public const int CHAROFFSET = 100;
        public const int ROOMOFFSET = 200;
        public const int CMDOFFSET = 500;
        static public List<Item> ItemL = new List<Item>();
        //static public List<Character> PotsList = Pots();
        static public List<Chest> ChestL = Chests();
        static public List<Command> CommandL = Commands();
		static public List<Room> RoomL = Room.CreateRooms();
        
        
        static public Room CurrentRoom = null;
        static public Lank Lank = new Lank();
        //static public List<Character> Monsters = new List<Character>(); 
        //static public Character Navi = Characters().ElementAt(6);
        static public Regex ucommands = new Regex(@"((?:^USE\s+FAIRY\s*$)|(?:^USE\s+HEART\s*$)|(?:^USE\s+[a-zA-Z]+\s+NORTH\s*$)|(?:^USE\s+[a-zA-Z]+\s+SOUTH\s*$)|(?:^USE\s+[a-zA-Z]+\s+EAST\s*$)|(?:^USE\s+[a-zA-Z]+\s+WEST\s*$))|((?:^EXAMINE\s+[a-zA-Z]+\s*[a-zA-Z]*$)|(?:^EQUIP\s+[a-zA-Z]+\s?[a-zA-Z]*$))|((?:^OPEN\s+DOOR\s*$)|(?:^OPEN\s+CHEST\s*$)|(?:^FIGHT\s+NORTH\s*$)|(?:^FIGHT\s+SOUTH\s*$)|(?:^FIGHT\s+EAST\s*$)|(?:^FIGHT\s+WEST\s*$))");
        //------------------------------/

  
        static void Main(string[] args)
        {
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@"LoZsong.wav");
            sp.PlayLooping();
            //sp.Play();
            Interface.StartDraw();
            
            MainGame.RoomL.ElementAt(14).FillRoom(1,4,7,10);
            MainGame.CurrentRoom = RoomL.ElementAt(14);
            MainGame.ItemL = Items();
            //Console.ReadLine();
            Console.SetWindowSize(75, 40);
            Console.SetBufferSize(75, 40);
            Lank.Room = 314;
            Lank.EItem = ItemL.ElementAt(4);
            Lank.X = 13; Lank.Y = 8;
            Interface.oldX = Lank.X; Interface.oldY = Lank.Y;
            Item thing = new Item();
            //Lank.Pocket = ItemL;
            //Lank.Pocket.Remove(Lank.Pocket.ElementAt(0));
            //Lank.Pocket.Remove(Lank.Pocket.Last());\
            Lank.ChangePocket(ItemL.ElementAt(0));
            Lank.ChangePocket(ItemL.ElementAt(1));
            Lank.ChangePocket(ItemL.ElementAt(6));
            Lank.ChangePocket(ItemL.ElementAt(9));
            Lank.ChangePocket(ItemL.ElementAt(7));
            Lank.ChangePocket(ItemL.ElementAt(10));
            Lank.ChangePocket(ItemL.ElementAt(17));   //Can now use Fairys and Hearts, they will give you correct HP and dynamically change
            Lank.ChangePocket(ItemL.ElementAt(18));
            //Lank.ChangePocket(ItemL.ElementAt(19));
           // Lank.Pocket.Add(ItemL.ElementAt(6));
            //Lank.EItem = ItemL.ElementAt(2);
            Lank.Rupee=0;


            Interface.DrawPocket();
            Interface.Refresh();
            Interface.RefreshHealth(Lank.Health);
          
            foreach (Character p in CurrentRoom.POTS)
            {
                Interface.DrawCharacter(p, p.X, p.Y);
            }

            //List<Item> Pocket = Items();



            while (true)
            {
                foreach (Chest c in CurrentRoom.CHEST)
                    if(!c.Open)
                        Interface.DrawChest(c);
           //     if(ChestL.ElementAt(0).Open)
           //            Interface.print("Chest L is open!");   I LOVE LISTS!
                Command C = GetCommand();
                C.Execute();

                Console.SetCursorPosition(20, 0);
                Console.Write(Lank.X + "," + Lank.Y);

                foreach (Character monster in CurrentRoom.CHAR)
                {
                    monster.Stalk();
                }
                foreach (Character p in CurrentRoom.POTS)
                {
                    Interface.DrawCharacter(p, p.X, p.Y);
                }
                List<ConsoleColor> L = new List<ConsoleColor>();
                //foreach (ConsoleColor color in ConsoleColor.)
                Console.ForegroundColor = ConsoleColor.Green;
            }     
        }

        public static List<Item> Items()        //CREATE THE ITEM LIST ,  IDs 001-020     WILL ADD CONTROLS TO DESCRIPTIONS AFTER WE HAVE DECIDED WHAT CONTROLS ARE
        {                               //when referring to an item use its id-1
            List<Item> I = new List<Item>();
            //WEAPONS
            Item WoodenSword = new Item("WOODEN SWORD", "A standard wooden sword used by many aspiring knights during training.\n", "Weapon", 1, 001);
            Item BrokenShield = new Item("BROKEN SHIELD", "A crude shield with splintered edges.\n", "Utility", 1, 002);
            Item KokiriSword = new Item("KOKIRI SWORD", "A forgotten blade with a well fashioned hilt and a strange yet alluring\nluster.\n", "Weapon", 2, 003);
            Item KokiriShield = new Item("KOKIRI SHIELD", "A well made shield with strange ancient symbols etched on the face.\n", "Utility", 2, 004);
            Item DinsEdge = new Item("DINS EDGE", "A finely crafted blade with a jewl embedded on the hilt, glowing as if it\nwas ablaze.\n", "Weapon", 9000, 005);
            Item NayrusTears = new Item("NAYRUS TEARS", "A mysterious shield with eyes painted on the front.\nThe eyes seem sad and lifeless, yet sometimes one can hear the muted sound\nof someone stuggling to breathe...\n", "Utility", 3, 006);
            Item Bow = new Item("BOW", "An aged bow carved from a rustic, yet surprisingly sturdy wood.\n", "Weapon", 2, 007);

            //UTILITIES     power = max number of items or special... 0 = special 1 = 10max 2 = 20max 3 = 30max ... 10 = 100max
            Item Boomerang = new Item("BOOMERANG", "A coveted item revered by many as 'The Hand of the Ancient'.\n", "Utility", 0, 008);
            Item Bombs = new Item("BOMBS", "When you're stuck between a rock and a hard place... JUST BLOW IT UP!\n", "Utility", 1, 009);
            Item Arrows = new Item("ARROWS", "These finely cut arrows will surely pierce anything stupid enough to get\nin your way.\n", "Utility", 5, 010);
            Item PegasusBoots = new Item("NIKES", "One of the seven gifts left to humanity, these shoes will allow you to\nmove at amazing speeds!\n", "Utility", 0, 011);
            Item Hookshot = new Item("HOOKSHOT", "One of the greatest modern inventions ever to be conceived.\nThis pre-loaded grappling chain will allow you to reach new areas!\n", "Utility", 0, 012);

            //DUNGEON ITEMS
            Item SmallKey = new Item("SMALL KEY", "It's just a small key... Perhaps you can use this to unlock something?\n", "DI", 1, 013);
            Item BossKey = new Item("BOSS KEY", "This looks important, you should probably hold onto it.\n", "DI", 0, 014);
            Item DungeonMap = new Item("DUNGEON MAP", "Thank god you know how to read!\n", "DI", 0, 015);
            Item Compass = new Item("COMPASS", "You're not really sure how this works...\nYou just feel inclined to follow the pointy thing.\n", "DI", 0, 016);
            Item HeartContainer = new Item("HEART CONTAINER", "The eviscerated remains of your enemy is a treasure in itself...\nYou sick cannibal.\n", "DI", 0, 017);

            //OTHER
            Item Heart = new Item("HEART", "Your slain foe's still beating heart. YUM!", "Other", 1, 018);
            Item Fairy = new Item("FAIRY", "This little creature will heal your wounds! Freak...", "Other", 1, 019);
            Item rupee = new Item("RUPEE", "SHINY!\n", "Other", 10, 020);

            Item[] Itemz = { WoodenSword, BrokenShield, KokiriSword, KokiriShield, DinsEdge, NayrusTears, Bow, Boomerang, Bombs, Arrows, PegasusBoots, Hookshot, SmallKey, BossKey, DungeonMap, Compass, HeartContainer, Heart, Fairy, rupee };
            //I = Itemz.ToList();
            I.AddRange(Itemz);
            return I.ToList();
        }
        public static List<Chest> Chests()      //CREATE THE CHEST LIST , IDs 050-068
        {                                //when referring to chest refer to it by its id-50
            List<Chest> C = new List<Chest>();
            List<Item> I = Items();
            Chest kokirisword = new Chest(I.ElementAt(2), 0, 0, 050);
            Chest kokirishield = new Chest(I.ElementAt(3), 0, 0, 051);
            Chest dinsedge = new Chest(I.ElementAt(4), 0, 0, 052);
            Chest nayrustears = new Chest(I.ElementAt(5), 0, 0, 053);
            Chest bow = new Chest(I.ElementAt(6), 0, 0, 054);
            Chest boomerang = new Chest(I.ElementAt(7), 0, 0, 055);
            Chest bombs = new Chest(I.ElementAt(8), 0, 0, 056);
            Chest pegasusboots = new Chest(I.ElementAt(10), 0, 0, 057);
            Chest hookshot = new Chest(I.ElementAt(11), 0, 0, 058);
            Chest smallkey = new Chest(I.ElementAt(12), 0, 0, 059);
            Chest bosskey = new Chest(I.ElementAt(13), 0, 0, 060);
            Chest dungeonmap = new Chest(I.ElementAt(14), 5, 16, 061);
            Chest compass = new Chest(I.ElementAt(15), 0, 0, 062);
            Chest heartcontainer = new Chest(I.ElementAt(16), 0, 0, 063);
            Item r5 = new Item("RUPEE","hjjhghjg","hhjgjh",5,676776);
            Chest rupee5 = new Chest(r5, 17, 5, 064);
            Item r10 = new Item("RUPEE","hjjhghjg","hhjgjh",10,676776);
            Chest rupee10 = new Chest(r10, 0, 0, 065);
            Item r20 = new Item("RUPEE","hjjhghjg","hhjgjh",20,676776);
            Chest rupee20 = new Chest(r20, 4, 19, 066);
            Item r50 = new Item("RUPEE", "hjjhghjg", "hhjgjh", 50, 676776);
            Chest rupee50 = new Chest(r50, 0, 0, 067);
            Item r100 = new Item("RUPEE", "hjjhghjg", "hhjgjh", 100, 676776);
            Chest rupee100 = new Chest(r100, 0, 0, 068);
           // rupee5.Contents.Power = 5;
           // rupee20.Contents.Power = 20;    //these powers overwrite eachother
           // rupee50.Contents.Power = 50;
           // rupee100.Contents.Power = 100;
            Chest[] Chestz = { kokirisword, kokirishield, dinsedge, nayrustears, bow, boomerang, bombs, pegasusboots, hookshot, smallkey, bosskey, dungeonmap, compass, heartcontainer, rupee5, rupee10, rupee20, rupee50, rupee100 };
           // C.AddRange(Chestz);
            C = Chestz.ToList();
            return C;
        }
        public static List<Command> Commands()  //CREATE THE COMMAND LIST , IDs 500-510
        {
            List<Command> L = new List<Command>();
            Command North = new Command("UPARROW", "This command moves Lank north one block.\n", 500);
            Command South = new Command("DOWNARROW", "This command moves Lank south one block.\n", 501);
            Command East = new Command("RIGHTARROW", "This command moves Lank east one block.\n", 502);
            Command West = new Command("LEFTARROW", "This command moves Lank west one block.\n", 503);
            Command Use = new Command("USE", "This command allows Lank to use his items.\n      Ex) 'Use Bow West', this will shoot and arrow to the west.\n", 504);
            Command Examine = new Command("EXAMINE", "This command allows Lank to examine his surroundings such as\n              a room or item.\n      Ex) 'Examine Room', will give you the description of the room.\n          'Examine Bombs', will give you the description of the item Bombs.", 505);
            Command Help = new Command("HELP", "This command will allow the user to open the help pages to tell\n           you how to play the game... Hey you're here now! Hi!!!\n", 506);
            Command Open = new Command("OPEN", "This command will allow Lank to open chests and doors.\n          Ex) 'Open West', will open a chest or door that is one block west               of Lank.\n", 507);
            Command Fight = new Command("FIGHT", "This command will allow Lank to engage in glorious combat with             his enemies!\n          Ex) 'Fight North', will have Lank fight an enemy one block north                of Lank.\n", 508);
            Command Equip = new Command("EQUIP", "This command will equip Lank with the specified weapon.\n          Ex) 'Equip Din's Edge', this will equip Din's Edge to Lank.\n", 509);
            Command Map = new Command("MAP", "This command will open the dungeon map for Lank's use.\n", 510);
            Command Exit = new Command("EXIT", "This command will close the game. Save First. Use with caution.\n", 511);
            Command SaveGame= new Command("SAVE", "This command will save the game.\n", 512);            
            Command LoadGame= new Command("LOAD", "This command will load a previous game.\n", 513);
            Command[] Commandz = { North, South, East, West, Use, Examine, Help, Open, Fight, Equip, Map, Exit,SaveGame,LoadGame };
            L.AddRange(Commandz);
            return L;
        }
        public static List<Character> Characters()
        {
            List<Item> I = Items();
            Random rand = new Random();
            Item[] enemyItem = new Item[100];
            for (int i = 0; i < 20; i++)
            {

                enemyItem[i] = I.ElementAt(rand.Next(18, 20) - ITEMOFFSET);
            }

            List<Character> E = new List<Character>();
            Character Skulltula = new Character("Skulltula", "These guys are giant spiders and fall on you at the most inopportune times", 102, 100, 10, 10, 10, enemyItem[1], 0, 0, 0, false, 'a');
            Character Lizalfos = new Character("Lizalfos", "Dude....Giant Lizard People!", 103, 200, 40, 10, 30, enemyItem[2], 0, 2, 0, false,'φ');
            Character Keese = new Character("Keese", "These bat-like attrocities will surely get on your nerves", 104, 100, 20, 20, 10, enemyItem[3], 0, 0, 0, false, 'ß');
            Character Stalfos = new Character("Stalfos", "Some living skeleton that like to poke you with their sword", 105, 200, 10, 30, 30, enemyItem[4], 0, 0, 0, false, '♣');
            Character Likelike = new Character("Like-Like", "Amorphous blobs that surround and digest you", 106, 200, 10, 10, 50, enemyItem[5], 0, 0, 0, false, '☼');            
            Character Navi = new Character("Navi", "Hey! Listen!", 104, 100, 20, 20, 10, enemyItem[3], 10, 10, 0, false, '*');
 	        Character Ganondalf = new Character("Ganondalf", "A once nice old wizard turned lord of evil who stole the Princess Zorldo", 107, 300, 50, 50, 50, enemyItem[6], 0, 0, 0, false, 'δ');
            Character[] enemies = { Skulltula, Lizalfos, Keese, Stalfos, Likelike, Ganondalf,Navi };
            E = enemies.ToList();
            //E.AddRange(enemies);
            return E;
        }
        public static List<Character> Pots()
        {
            List<Item> I = Items();
            Random rand = new Random();
            Item[] enemyItem = new Item[100];
            for (int i = 0; i < 20; i++)
            {

                enemyItem[i] = I.ElementAt(rand.Next(18, 21) - ITEMOFFSET);
            }

            List<Character> E = new List<Character>();

            Character pot1 = new Character("pot 1", "It's a pot", 120, 1, 0, 0, 0, enemyItem[1], 11, 11, 0, false, '₧');
            Character pot2 = new Character("pot 2", "It's a pot", 120, 1, 0, 0, 0, enemyItem[2], 12, 13, 0, false, '₧');
            Character pot3 = new Character("pot 3", "It's a pot", 120, 1, 0, 0, 0, enemyItem[3], 9, 4, 0, false, '₧');
            Character pot4 = new Character("pot 4", "It's a pot", 120, 1, 0, 0, 0, enemyItem[4], 3, 5, 0, false, '₧');
            Character pot5 = new Character("pot 5", "It's a pot", 120, 1, 0, 0, 0, enemyItem[1], 12, 12, 0, false, '₧');
            Character pot6 = new Character("pot 6", "It's a pot", 120, 1, 0, 0, 0, enemyItem[2], 13, 14, 0, false, '₧');
            Character pot7 = new Character("pot 7", "It's a pot", 120, 1, 0, 0, 0, enemyItem[3], 10, 3, 0, false, '₧');
            Character pot8 = new Character("pot 8", "It's a pot", 120, 1, 0, 0, 0, enemyItem[4], 4, 6, 0, false, '₧');
            Character pot9 = new Character("pot 9", "It's a pot", 120, 1, 0, 0, 0, enemyItem[1], 1, 12, 0, false, '₧');
            Character pot10 = new Character("pot 10", "It's a pot", 120, 1, 0, 0, 0, enemyItem[2], 6, 10, 0, false, '₧');
            Character pot11 = new Character("pot 11", "It's a pot", 120, 1, 0, 0, 0, enemyItem[3], 1, 3, 0, false, '₧');
            Character pot12 = new Character("pot 12", "It's a pot", 120, 1, 0, 0, 0, enemyItem[4], 8, 5, 0, false, '₧');
            Character[] pots = { pot1, pot2, pot3, pot4, pot5, pot6, pot7, pot8, pot9, pot10, pot11, pot12 };
            E = pots.ToList();
            //E.AddRange(enemies);
            return E;
        }
        public static Command[] UCommands(ref List<Command> CMD) //USED TO CREATE USER ONLY COMMANDS... don't worry about it
        {
            Command[] cmd = { CMD.ElementAt(4), CMD.ElementAt(5), CMD.ElementAt(7), CMD.ElementAt(8), CMD.ElementAt(9) };
            CMD.RemoveAt(4);
            CMD.RemoveAt(4);
            CMD.RemoveAt(5);
            CMD.RemoveAt(5);
            CMD.RemoveAt(5);
            return cmd;
        }
        public static Command GetCommand() //GET ANY USER COMMAND, ALWAYS FOLLOW THIS STATEMENT WITH (COMMAND).EXECUTE  AKA THE REASON REGEX IS AMAZING
        {
            List<Command> CMD = Commands();
            Command[] cmd = UCommands(ref CMD);
            ConsoleKeyInfo cki;
            string error = "Invalid command... DekuScrub...";
            string input;
            Command response = new Command();
            bool OK = false;
            do
            {
               
                cki = Console.ReadKey();    //CANNOT DELETE FIRST KEY PRESSED can fix this later
                switch (cki.Key.ToString().ToUpper())
                {
                    case "UPARROW":
                        Temp.string1 = cki.Key.ToString().ToUpper();
                        return CMD.ElementAt(0);
                    case "DOWNARROW":
                        Temp.string1 = cki.Key.ToString().ToUpper();
                        return CMD.ElementAt(1);
                    case "RIGHTARROW":
                        Temp.string1 = cki.Key.ToString().ToUpper();
                        return CMD.ElementAt(2);
                    case "LEFTARROW":
                        Temp.string1 = cki.Key.ToString().ToUpper();
                        return CMD.ElementAt(3);
                    default:
                        break;
                }
                input = cki.Key.ToString().ToUpper() + Console.ReadLine().ToUpper();
                Match commands = ucommands.Match(input);
                foreach (Command c in CMD)
                    if (input == c.Name.ToUpper())
                        return c;
                if (commands.Success)
                {
                    if (commands.Groups[1].Value != "")  //use item
                    {
                        string[] parts = Regex.Split(input, " ");
                        Temp.string1 = parts[1]; //item
                        if(parts.Length==3)
                        Temp.string2 = parts[2]; //direction
                        return cmd[0];
                    }
                    else if (commands.Groups[2].Value != "") //examine or equip
                    {
                        string[] parts = Regex.Split(input, " ");
                        Temp.string1 = parts[1]; //what you are examining or equiping?

                        if (parts[0] == "EXAMINE")
                            response = cmd[1];
                        else
                            response = cmd[4];
                        if (parts.Length == 3)
                        {
                            Temp.two = true;
                            Temp.string2 = parts[2];    //is item two words long?
                            return response;
                        }
                        else
                        {
                            Temp.two = false;
                            return response;
                        }
                    }
                    else                                    //open or fight
                    {
                        string[] parts = Regex.Split(input, " ");
                        Temp.string1 = parts[1];
                        if (parts[0] == "OPEN")
                            return cmd[2];
                        return cmd[3];
                    }
                }
                else if (!OK)
                {
                    Interface.print(error);
                    Interface.DrawInput();
                }

            } while (!OK);
            return response;
        } 

    }
}
