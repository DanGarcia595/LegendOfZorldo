using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Project
{
    public class Save
    {
        
        public class SaveGameData
        {
            public lank lank1;
            public List<monster> monsters;

            public class lank 
            { 
                public int health;
                public string name;
                public string desc;
                public int id;

                public int speed;
                public int strength;
                public int defense;
                public item eitem;
                public List<item> pocket;
                public bool stun;
                public int x;
                public int y;
                public int room;
            }

            public class item
            {
                public string name;
                public string desc;
                public int id;
                public string type;
                public int power; 

            }
            public class monster
            {
                public int health;
                public string name;
                public string desc;
                public int id;

                public int speed;
                public int strength;
                public int defense;
                public item eitem;
                public char symbol;
                public bool stun;
                public int x;
                public int y;
                public int room;
            }
   
        }

        public static void SaveGame()
        {
            // Create the data to save.
            
            SaveGameData data = new SaveGameData();
            data.lank1 = new SaveGameData.lank();
            data.lank1.eitem = new SaveGameData.item();
            data.lank1.pocket = new List<SaveGameData.item>();
            data.monsters = new List<SaveGameData.monster>();

            data.lank1.name = MainGame.Lank.Name;
            data.lank1.desc = MainGame.Lank.Desc;
            data.lank1.id = MainGame.Lank.ID;
            data.lank1.health = MainGame.Lank.Health;
            data.lank1.speed = MainGame.Lank.Speed;
            data.lank1.strength = MainGame.Lank.Strength;
            data.lank1.defense = MainGame.Lank.Defense;
            
            try
            {
                foreach (Item i in MainGame.Lank.Pocket)
                {
                    SaveGameData.item j = new SaveGameData.item();
                    j.name = i.Name;
                    j.desc = i.Desc;
                    j.id = i.ID;
                    j.power = i.Power;
                    j.type = i.Type;
                    data.lank1.pocket.Add(j);
                }
                data.lank1.eitem.name = MainGame.Lank.EItem.Name;
                data.lank1.eitem.desc = MainGame.Lank.EItem.Desc;
                data.lank1.eitem.id = MainGame.Lank.EItem.ID;
                data.lank1.eitem.power = MainGame.Lank.EItem.Power;
                data.lank1.eitem.type = MainGame.Lank.EItem.Type;
            }
            catch (NullReferenceException) { data.lank1.eitem = new SaveGameData.item(); }

            foreach (Character m in MainGame.CurrentRoom.CHAR)
            {
                SaveGameData.monster n = new SaveGameData.monster();
                n.name = m.Name;
                n.desc = m.Desc;
                n.health = m.Health;
                n.speed = m.Speed;
                n.strength = m.Strength;
                n.defense = m.Defense;
                n.id = m.ID;
                n.x = m.X;
                n.y = m.Y;
                n.room = m.Room;
                n.symbol = m.Symbol;
                try
                {
                    n.eitem.name = m.EItem.Name;
                    n.eitem.desc = m.EItem.Desc;
                    n.eitem.id = m.EItem.ID;
                    n.eitem.power = m.EItem.Power;
                    n.eitem.type = m.EItem.Type;
                }
                catch (NullReferenceException) { n.eitem = null;/*new SaveGameData.item();*/ }
                data.monsters.Add(n);
            }

            data.lank1.stun = MainGame.Lank.Stun;
            data.lank1.x = MainGame.Lank.X;
            data.lank1.y = MainGame.Lank.Y;
            data.lank1.room = MainGame.Lank.Room;
            



            string filename = "savegame.xml";

            Stream stream = new FileStream(filename, FileMode.Create);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            serializer.Serialize(stream, data);

            // Close the file.
            stream.Close();

        }

       public static void LoadGame()
        {


            string filename = "savegame.xml";

            // Check to see whether the save exists.
            if (!File.Exists(filename))
            {
                // If not, dispose of the container and return.
                Interface.print("Error, no file exists");
                return;
            }

            // Open the file.
            Stream stream = File.Open(filename, FileMode.Open);

            // Read the data from the file.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            SaveGameData data = (SaveGameData)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();

            // Dispose the container.
            //MainGame.Lank = data.Lank;
            MainGame.Lank.Name = data.lank1.name;

            MainGame.Lank.Desc = data.lank1.desc;
            MainGame.Lank.ID = data.lank1.id;
            MainGame.Lank.Health = data.lank1.health;
            MainGame.Lank.Speed = data.lank1.speed;
            MainGame.Lank.Strength = data.lank1.strength;
            MainGame.Lank.Defense = data.lank1.defense;
            try
            {
                MainGame.Lank.Pocket = new List<Item>();
                foreach (SaveGameData.item i in data.lank1.pocket)
                {
                    Item j = new Item();//ew Item();
                    j.Name = i.name;
                    j.Desc = i.desc;
                    j.ID = i.id;
                    j.Power = i.power;
                    j.Type = i.type;
                    MainGame.Lank.Pocket.Add(j);
                }
                MainGame.Lank.EItem.Name = data.lank1.eitem.name;
                MainGame.Lank.EItem.Desc = data.lank1.eitem.desc;
                MainGame.Lank.EItem.ID = data.lank1.eitem.id;
                MainGame.Lank.EItem.Power = data.lank1.eitem.power;
                MainGame.Lank.EItem.Type = data.lank1.eitem.type;
            }
            catch (NullReferenceException)
            {
                MainGame.Lank.EItem = new Item();
                Interface.print("No held item");
            }
            MainGame.CurrentRoom.CHAR = new List<Character>();
            foreach (SaveGameData.monster m in data.monsters)
            {
                Character n = new Character();
                n.Name = m.name;
                n.Desc = m.desc;
                n.Health = m.health;
                n.Speed = m.speed;
                n.Strength = m.strength;
                n.Defense = m.defense;
                n.ID = m.id;
                n.X = m.x;
                n.Y = m.y;
                n.Room = m.room;
                n.Symbol = m.symbol;
                try
                {
                    n.EItem.Name = m.eitem.name;
                    n.EItem.Desc = m.eitem.desc;
                    n.EItem.ID = m.eitem.id;
                    n.EItem.Power = m.eitem.power;
                    n.EItem.Type = m.eitem.type;
                }
                catch (NullReferenceException) { n.EItem = null;/* new Item();*/ }
                MainGame.CurrentRoom.CHAR.Add(n);
            }


            // data.eitem = MainGame.Lank.EItem;
            MainGame.Lank.Stun = data.lank1.stun;
            MainGame.Lank.X = data.lank1.x;
            MainGame.Lank.Y = data.lank1.y;
            MainGame.Lank.Room = data.lank1.room;

            // Report the data to the console.
            Interface.print("Health:     " + MainGame.Lank.Health);
            Interface.print("Room:       " + MainGame.Lank.Room);
            Interface.print("Rupees:     " + MainGame.Lank.Rupee);
            Interface.print("Position:   " + MainGame.Lank.X + "," + MainGame.Lank.Y);
            Interface.print("Held Item:     " + MainGame.Lank.EItem.Name);
            Interface.print("Characters in room:     "); foreach (Character m in MainGame.CurrentRoom.CHAR) { Interface.print("    "+m.Name); }

            Interface.Refresh();
            Interface.DrawLank(MainGame.Lank.X, MainGame.Lank.Y);
        }
    }
}
