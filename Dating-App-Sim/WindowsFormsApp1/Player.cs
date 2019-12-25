using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp1
{
    public class Player
    {
        //Propertyt
        public string Name { get; set; }
        public int Score { get; set; }
        
        // Override joka muuttaa miten objekti muutetaan stringiksi
        public override string ToString()
        {
            return $"{Name}: {Score}";
        }

        //Tallentaa tuloksen uudelle riville tekstitiedostoon käyttäjän tiedostot kansioon
        public void saveScore()
        {
            string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\dating-app-sim_Files";
            string filePath = dirPath + @"/Top_Scores.txt";
            if (!Directory.Exists(dirPath)) 
            { 
                Directory.CreateDirectory(dirPath); 
            }
            File.AppendAllText(filePath, ToString() + "\n");
        }
        public Player()
        {
            Name = "";
            Score = 0;
        }

        public Player(string name, int score)
        {
            Name = name;
            Score = score;
        }
        
    }
}
