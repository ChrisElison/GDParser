using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDParser {
    public class CharacterReader {
        private CryptoDataBuffer reader;
        public byte charTestVal;
        public string charClass;  // Del
        public uint charMoney;    // Del
        public bool isHardcore;   // Del
        public byte charSex;      // Del
        public byte charDiff;     // Del
        public byte charGreatestDiff; // Del
        public bool charisInMainQuest; //Del
        public bool charhasBeenInGame; //Del
        public string Name { get; private set; }
        public int Level { get; private set; }
        //public GDInventory Inventory { get; private set; }
        //private GDCharSkillList SkillList = new GDCharSkillList();
        public GDCharBio Bio { get; private set; } = new GDCharBio();

        private void ReadHeader() {
            string classID;
            bool hardcore;
            byte sex;
            string name;

            bool testVal;
            bool testVal2;
            byte testVal3;
            byte testVal4;
            uint testVal5;

            byte testVal6;
            uint testVal7;
            byte testVal8;

            reader.ReadCryptoWString(out name);
            Name = name;

            reader.ReadCryptoByte(out sex);
            charSex = sex;
            if (!reader.ReadCryptoString(out classID))
                throw new FormatException("Could not read class id");
            charClass = classID;
            Level = reader.ReadCryptoIntUnchecked();

            reader.ReadCryptoBool(out hardcore);
            isHardcore = hardcore;

            /* Del */
            //reader.ReadBlockStart(Constants.BLOCK);
            //reader.ReadBlockStart(1);

            //reader.ReadCryptoIntUnchecked();

            //Console.WriteLine("Test (Main quest): " + reader.ReadCryptoBool(out testVal));     // Bool - In main quest?
            //Console.WriteLine("Test 2 (Been in game): " + reader.ReadCryptoBool(out testVal2));  // Bool - Has been in game?

            //reader.ReadCryptoByte(out testVal3);  // Byte - Difficulty?
            //Console.WriteLine("Test 3 (Diff): " + testVal3);

            //reader.ReadCryptoByte(out testVal4);  // Byte - Greatest Diff?
            //Console.WriteLine("Test 4 (Grt Diff): " + testVal4);

            //reader.ReadCryptoUInt(out testVal5);  // Byte - Money?
            //Console.WriteLine("Test 5 (Money): " + testVal5);

            //reader.ReadCryptoByte(out testVal6);  // Byte - greatestSurvivalDifficulty
            //Console.WriteLine("Test 6 (Grt crucible): " + testVal6);

            //reader.ReadCryptoUInt(out testVal7);  // Byte - currentTribute
            //Console.WriteLine("Test 7 (Tribute): " + testVal7);

            //reader.ReadCryptoByte(out testVal8);  // Byte - compass state
            //Console.WriteLine("Test 8 (Compass state): " + testVal8);

            reader.ReadBlockEnd();
            //Console.WriteLine("Test val (string): " + charTestVal);
            /* Del */
        }

        private void ReadCharacterInfo() {
            bool isInMainQuest;
            bool hasBeenInGame;
            byte difficulty;
            byte greatestCampaignDifficulty;
            uint money;
            byte greatestCrucibleDifficulty;
            int tributes;

            reader.ReadBlockStart(Constants.BLOCK);

            int version = reader.ReadCryptoIntUnchecked();
            //if ((version != Constants.VERSION_3) && (version != Constants.VERSION_4)) {
            //    throw new FormatException("ERR_UNSUPPORTED_VERSION");
            //}

            reader.ReadCryptoBool(out isInMainQuest);
            charisInMainQuest = isInMainQuest;

            reader.ReadCryptoBool(out hasBeenInGame);
            charhasBeenInGame = hasBeenInGame;

            //reader.ReadCryptoByte(out difficulty);
            //charDiff = difficulty;

            reader.ReadCryptoByte(out greatestCampaignDifficulty);
            charGreatestDiff = greatestCampaignDifficulty;

            reader.ReadCryptoUInt(out money);
            charMoney = money;

            //if (version == Constants.VERSION_4) {
            //    reader.ReadCryptoByte(out greatestCrucibleDifficulty);
            //    reader.ReadCryptoInt(out tributes);
            //}

            //byte compassState = reader.ReadCryptoByteUnchecked();
            //int lootMode = reader.ReadCryptoIntUnchecked();
            //byte skillWindowHelp = reader.ReadCryptoByteUnchecked();
            //byte alternateConfig = reader.ReadCryptoByteUnchecked();
            //byte alternateConfigEnabled = reader.ReadCryptoByteUnchecked();
            //string texture = reader.ReadCryptoStringUnchecked();

            reader.ReadBlockEnd();
        }

        public void ReadSummary(string file) {
            ReadSummary(File.ReadAllBytes(file));
        }

        public void ReadSummary(byte[] data) {
            reader = new CryptoDataBuffer(data);
            int val = 0;

            reader.ReadCryptoKey();

            if (!reader.ReadCryptoInt(out val)) {
                throw new FormatException("ERR_UNSUPPORTED_VERSION");
            }

            if (val != 0x58434447)
                throw new FormatException("ERR_UNSUPPORTED_VERSION");

            reader.ReadCryptoInt(out val);
            //if (val != 1)
            //   throw new FormatException("ERR_UNSUPPORTED_VERSION");

            ReadHeader();

            reader.ReadCryptoInt(out val, false);
            //if (val != 0)
            //    throw new FormatException("ERR_UNSUPPORTED_VERSION");

            int version;
            reader.ReadCryptoInt(out version);
            //if ((version != 6) && (version != 7)) {
            //   throw new FormatException("ERR_UNSUPPORTED_VERSION");
            //}

            reader.ReadAndDiscardUID();
            ReadCharacterInfo();
            Bio.Read(reader);
            this.Print();

            //Inventory.Read(reader);
        }

        public void Print() {
            Console.WriteLine("Character: " + Name);
            Console.WriteLine("Level: " + Level);
            Console.WriteLine("Class: " + charClass);
            Console.WriteLine("Sex: " + charSex);
            //Console.WriteLine("Difficulty: " + charDiff);
            //Console.WriteLine("Money: " + charMoney);
            Console.WriteLine("Hardcore: " + isHardcore);
            //Console.WriteLine("Is in main quest: " + charisInMainQuest); 
            //Console.WriteLine("Has been in game: " + charhasBeenInGame);

            //int str = (int)((Bio.TotalStrength - 50) / 8);
            //int agi = Bio.Agility;
            //int inte = Bio.Intelligence;
            //String attributes = str + "/" + agi + "/" + inte;
            //Console.WriteLine("Attributes: " + attributes);
        }
    }
}
