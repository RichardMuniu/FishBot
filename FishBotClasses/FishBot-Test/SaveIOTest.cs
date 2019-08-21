using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;

namespace FishBot
{
    /*
    * Testing strategy
    *
    * Partition the inputs as follows:
    * A) For ReadFromString:
    *   1) Name is ill-formatted.
    *   2) Some option name is ill-formatted.
    *   3) Some option type is incompatible.
    *   4) Some option value cannot be parsed.
    *   5) An option line does not have exactly three parts.
    *   6) An option is repeated or is default.
    *   7) String is well-formatted and new options are added.
    * B) For SaveToString:
    *   1) User with no options
    *   2) User with options but some type that is incompatible.
    *   3) User with all options compatible.
    * C) For SaveToFile:
    *   1) File does not exist.
    *   2) File is read-only.
    *   3) File exists and is empty.
    *   4) File exists and is not empty.
    * D) For ReadFromFile:
    *   1) File does not exist.
    *   2) File is ill-formatted.
    *   3) Some option line is ill-formatted (see A2,A3,A4,A5)
    *   4) File is well-formatted and new options are added.
    *
    *   Cover each part testing
    */     
    [TestClass]
    public class SaveIOTest
    {
        // covers A1
        [TestMethod]
        public void ReadStringNameInvalid()
        {
            string text = "username invalid";
            bool pass = true;
            try
            {
                User user = SaveIO.ReadFromString(text);
                pass = false;
            }
            catch{}
            Assert.IsTrue(pass);
        }
        
        // covers A2
        [TestMethod]
        public void ReadStringOptionNameInvalid()
        {
            string text = "usernameValid\noptionبشنبمثnameInvalid int 9";
            bool pass = true;
            try
            {
                User user = SaveIO.ReadFromString(text);
                pass = false;
            }
            catch{}
            Assert.IsTrue(pass);
        }

        // covers A3
        [TestMethod]
        public void ReadStringOptionTypeInvalid()
        {
            string text = "usernameValid\noptionNameValid var 9";
            bool pass = true;
            try
            {
                User user = SaveIO.ReadFromString(text);
                pass = false;
            }
            catch{}
            Assert.IsTrue(pass);
        }

        // covers A4
        [TestMethod]
        public void ReadStringOptionValueCannotParse()
        {
            string text = "usernameValid\noptionNameValid int parseMe";
            bool pass = true;
            try
            {
                User user = SaveIO.ReadFromString(text);
                pass = false;
            }
            catch{}
            Assert.IsTrue(pass);
        }

        // covers A5
        [TestMethod]
        public void ReadStringOptionLineNotThree()
        {
            string text = "usernameValid\noptionNameValid int 9 extra";
            bool pass = true;
            try
            {
                User user = SaveIO.ReadFromString(text);
                pass = false;
            }
            catch{}
            Assert.IsTrue(pass);

            text = "usernameValid\noptionNameValid int";
            try
            {
                User user = SaveIO.ReadFromString(text);
                pass = false;
            }
            catch{}
            Assert.IsTrue(pass);
        }

        // covers A6 and A7
        [TestMethod]
        public void ReadStringOptionRepeatedOrDefault()
        {
            string text = "usernameValid\nVolume int 9\nHi string hello\nHi string test";
            User user = SaveIO.ReadFromString(text);
            Assert.AreEqual(user.GetOptionValue("Volume"), 9);
            Assert.AreEqual(user.GetOptionValue("Hi").Equals("test"), true);
        }

        // covers B1
        [TestMethod]
        public void SaveStringNoOptions()
        {
            User user = new User("name");
            user.RemoveAllOptions();
            string result = SaveIO.SaveToText(user);
            Assert.IsTrue(result == "name");
        }
    
        // covers B2
        [TestMethod]
        public void SaveStringSomeOptionIncompatible()
        {
            User user = new User("name");
            user.AddOption("name", new List<int>());
            bool pass = true;
            try{SaveIO.SaveToText(user); pass = false;}
            catch{}
            Assert.IsTrue(pass);
        }

        // covers B3
        [TestMethod]
        public void SaveStringOptionsAreGood()
        {
            User user = new User("name");
            user.RemoveAllOptions();
            user.AddOption("name1", 0);
            user.AddOption("name2", "hello");
            string result = SaveIO.SaveToText(user);
            Assert.IsTrue(result == "name\nname1 int 0\nname2 string hello" ||
            result == "name\nname2 string hello\nname1 int 0");
        }

        // covers C1 (machine specific)
        [Ignore]
        [TestMethod]
        public void SaveFileDoesNotExist()
        {
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\Test\save1.txt";
            User user = new User("username1", false);
            user.AddOption("option", 3);
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            SaveIO.SaveToFile(user, path);
        }

        // covers C2 (machine specific)
        [Ignore]
        [TestMethod]
        public void SaveFileReadOnly()
        {
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\Test\readonly.txt";
            User user = new User("username1", false);
            user.AddOption("option", 3);
            bool pass = true;
            try
            {
                SaveIO.SaveToFile(user, path);
                pass = false;
            }
            catch{}
            if(pass == false) Assert.Fail();
        }

        // covers C3 (machine specific)
        [Ignore]
        [TestMethod]
        public void SaveFileEmpty()
        {
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\Test\save2.txt";
            User user = new User("username1", false);
            user.AddOption("option", "hello");
            if(File.Exists(path))
            {
                File.Delete(path);
                File.WriteAllText(path, "");
            }
            else
            {
                File.WriteAllText(path, "");
            }

            SaveIO.SaveToFile(user, path);
        }

        // covers C4 (machine specific)
        [Ignore]
        [TestMethod]
        public void SaveFileNonEmpty()
        {
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\Test\save3.txt";
            User user = new User("username1", false);
            user.AddOption("option", 0.3);
            if(File.Exists(path))
            {
                File.Delete(path);
                File.WriteAllText(path, "Scrap.");
            }
            else
            {
                File.WriteAllText(path, "Scrap.");
            }

            SaveIO.SaveToFile(user, path);
        }

        // covers D1 (machine specific)
        [Ignore]
        [TestMethod]
        public void ReadFileDoesNotExist()
        {
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\Test\null.txt";
            bool pass = true;
            if(File.Exists(path))
            {
                File.Delete(path);
            }

            try
            {
                User user = SaveIO.ReadFromFile(path);
                pass = false;
            }catch{}
            Assert.IsTrue(pass);
        }

        // covers D2 (machine specific)
        [Ignore]
        [TestMethod]
        public void ReadFileUserNameIllFormatted()
        {
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\Test\user1.txt";
            bool pass = true;
            try
            {
                User user = SaveIO.ReadFromFile(path);
                pass = false;
            }catch{}
            Assert.IsTrue(pass);
        }

        // covers D3 (machine specific)
        [Ignore]
        [TestMethod]
        public void ReadFileSomeOptionIsIllFormatted()
        {
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\Test\user2.txt";
            bool pass = true;
            try
            {
                User user = SaveIO.ReadFromFile(path);
                pass = false;
            }catch{}
            Assert.IsTrue(pass);
        }

        // covers D4 (machine specific)
        [Ignore]
        [TestMethod]
        public void ReadFileWellFormatted()
        {
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\Test\user3.txt";
            User user = SaveIO.ReadFromFile(path);
            Assert.IsTrue(user.UserName == "username");
            Assert.IsTrue(user.HasOption("op1"));
            Assert.IsTrue(user.HasOption("op2"));
            Assert.IsTrue(user.HasOption("op3"));
            Assert.IsTrue(user.HasOption("op4"));
            Assert.IsTrue(user.HasOption("op5"));
            Assert.IsTrue(user.GetOptionValue("op1").Equals(3));
            Assert.IsTrue(user.GetOptionValue("op2").Equals("hey"));
            Assert.IsTrue(user.GetOptionValue("op3").Equals(0.3f));
            Assert.IsTrue(user.GetOptionValue("op4").Equals(true));
            Assert.IsTrue(user.GetOptionValue("op5").Equals(0.4));
        }
    }
}
