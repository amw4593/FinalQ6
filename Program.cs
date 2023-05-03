using System;
using System.IO;
using Newtonsoft.Json;

namespace FinalQ6
{
    class PlayerSettings
    {
        public string PlayerName { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public string[] Inventory { get; set; }
        public string LicenseKey { get; set; }

        private static PlayerSettings instance = null;

        private PlayerSettings() { }

        public static PlayerSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerSettings();
                }
                return instance;
            }
        }

        public void Save(string filePath)
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, json);
        }

        public void Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                PlayerSettings loadedSettings = JsonConvert.DeserializeObject<PlayerSettings>(json);
                this.PlayerName = loadedSettings.PlayerName;
                this.Level = loadedSettings.Level;
                this.Hp = loadedSettings.Hp;
                this.Inventory = loadedSettings.Inventory;
                this.LicenseKey = loadedSettings.LicenseKey;
            }
            else
            {
                Console.WriteLine("Error: File not found");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "player_settings.json";

            PlayerSettings settings1 = PlayerSettings.Instance;
            settings1.PlayerName = "dschuh";
            settings1.Level = 4;
            settings1.Hp = 99;
            settings1.Inventory = new string[] { "spear", "water bottle", "hammer", "sonic screwdriver", "cannonball", "wood", "Scooby snack", "Hydra", "poisonous potato", "dead bush", "repair powder" };
            settings1.LicenseKey = "DFGU99-1454";

            settings1.Save(filePath);

            PlayerSettings settings2 = PlayerSettings.Instance;
            settings2.Load(filePath);

            Console.WriteLine(settings2.PlayerName);
            Console.WriteLine(settings2.Level);
            Console.WriteLine(settings2.Hp);
            Console.WriteLine(string.Join(",", settings2.Inventory));
            Console.WriteLine(settings2.LicenseKey);

            Console.ReadLine();
        }
    }
}
