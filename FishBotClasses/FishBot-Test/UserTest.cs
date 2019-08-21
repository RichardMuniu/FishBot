using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FishBot
{
    /*
    * Testing strategy
    *
    * Partition the inputs as follows:
    * A) For Constructor:
    *   1) Name is empty, null, or invalid.
    *   2) Name is valid.
    * B) For UserName propery:
    *   1) Name is empty, null, or invalid.
    *   2) Name is valid and getting works.
    * C) For OptionsList:
    *   1) Empty List.
    *   2) Non-Empty List.
    *   3) Newly created user with addDefaultOptions true has the default options.
    *   4) Newly created user with addDefaultOptions false has no default options.
    * D) For AddOption:
    *   1) Name is empty, null or invalid.
    *   2) Name already exists.
    *   3) Value is null.
    *   4) Name and value are valid.
    * E) For HasOption:
    *   1) Empty list.
    *   2) Case-sensitivity.
    *   3) The list does not have the option.
    *   4) The list has the option.
    * F) For GetOption:
    *   1) The list does not have the option.
    *   2) The list has the option.
    * G) For GetOptionValue:
    *   1) The list does not have the option.
    *   2) The list has the option.
    * H) For UpdateOption:
    *   1) The list does not have the option.
    *   2) Value is null.
    *   3) The list has the option.
    * I) For RemoveOption:
    *   1) The list does not have the option.
    *   2) The list has the option.
    * J) For RemoveAllOptions:
    *   1) The user does not have any option.
    *   2) The user has options.
    * K) For Copy:
    *   1) The result is an actual copy with the same username and copies of the options.
    * L) For Similar:
    *   1) Usernames are different.
    *   2) Usernames are same; some option name is different.
    *   3) Usernames are same; option names are same; some option value is different.
    *   4) Usernames are same; options are similar.
    *   5) Other user is null.
    *
    * Cover each part testing.
    */ 
    [TestClass]
    public class UserTest
    {
        // covers A1
        [TestMethod]
        public void ConstructorNameEmptyNullInvalid()
        {
            string name1 = "";
            string name2 = null;
            string name3 = "hel lo";
            bool pass = true;
            try{ User user = new User(name1); pass = false;}
            catch{}
            try{ User user = new User(name2); pass = false;}
            catch{}
            try{ User user = new User(name3); pass = false;}
            catch{}
            if(pass == false) {Assert.Fail();}
        }

        // covers A2
        [TestMethod]
        public void ConstructorNameValid()
        {
            string name = "hello";
            bool pass = true;
            try{ User user = new User(name);}
            catch{pass = false;}
            if(pass == false) {Assert.Fail();}
        }

        // covers B1
        [TestMethod]
        public void UserNameEmptyNullInvalid()
        {
            User user = new User("valid");
            string name1 = "";
            string name2 = null;
            string name3 = "hel lo";
            bool pass = true;
            try{ user.UserName = name1; pass = false;}
            catch{}
            try{ user.UserName = name2; pass = false;}
            catch{}
            try{ user.UserName = name3; pass = false;}
            catch{}
            if(pass == false) {Assert.Fail();}
        }

        // covers B2
        [TestMethod]
        public void UserNameValid()
        {
            string name = "hello";
            User user = new User(name);
            Assert.AreEqual(user.UserName, "hello");
            user.UserName = "hey";
            Assert.AreEqual(user.UserName, "hey");
        }

        // covers C1
        [TestMethod]
        public void OptionsListEmpty()
        {
            string name = "hello";
            User user = new User(name);
            List<Option> opList = user.OptionsListCopy;
            for(int i = 0; i < opList.Count; i++)
            {
               user.RemoveOption(opList[i].Name);
            }
            Assert.AreEqual(user.OptionsListCopy.Count, 0);
        }

        // covers C2
        [TestMethod]
        public void OptionsListNonEmpty()
        {
            string name = "hello";
            User user = new User(name);
            List<Option> opList = user.OptionsListCopy;
            for(int i = 0; i < opList.Count; i++)
            {
               user.RemoveOption(opList[i].Name);
            }
            Assert.AreEqual(user.OptionsListCopy.Count, 0);
        }

        // covers C3
        [TestMethod]
        public void OptionsListDefaultTrue()
        {
            string name = "hello";
            User user = new User(name);
            List<Option> opList = user.OptionsListCopy;
            List<Option> defList = DefaultOptions.DefualtOptionsList;
            bool pass = false;
            Option currentOption;
            Option currentDefault;
            for(int j = 0; j < opList.Count; j++)
            {
                currentOption = opList[j];
                for(int i = 0; i < defList.Count; i++)
                {
                    currentDefault = defList[i];
                    if(currentOption.Name == currentDefault.Name && currentOption.Value.Equals(currentDefault.Value))
                    {
                        pass = true;
                        break;
                    }                         
                }
                if(pass == false)
                    Assert.Fail();
                pass = false;
            }
        }

        // covers C4
        [TestMethod]
        public void OptionsListDefaultFalse()
        {
            string name = "hello";
            User user = new User(name, false);
            List<Option> opList = user.OptionsListCopy;
            Assert.IsTrue(opList.Count == 0);
        }
    
        // covers D1
        [TestMethod]
        public void AddOptionInvalidName()
        {
            User user = new User("hello");
            string name1 = "";
            string name2 = null;
            string name3 = "hel lo";
            bool pass = true;
            try{ user.AddOption(name1, 0); pass = false;}
            catch{}
            try{ user.AddOption(name2, 0); pass = false;}
            catch{}
            try{ user.AddOption(name3, 0); pass = false;}
            catch{}
            if(pass == false) {Assert.Fail();}
        }

        // covers D2
        [TestMethod]
        public void AddOptionNameExists()
        {
            User user = new User("hello");
            bool pass = true;
            try{ user.AddOption("Volume", 0); pass = false;}
            catch{}
            Assert.IsTrue(pass);
        }

        // covers D3
        [TestMethod]
        public void AddOptionValueNull()
        {
            User user = new User("hello");
            bool pass = true;
            try{ user.AddOption("name", null); pass = false;}
            catch{}
            Assert.IsTrue(pass);
        }

        // covers D4
        [TestMethod]
        public void AddOptionNameValid()
        {
            User user = new User("hello");
            try{ user.AddOption("name", "value");}
            catch{Assert.Fail();}
        }

        // covers E1
        [TestMethod]
        public void HasOptionEmptyList()
        {
            string name = "hello";
            User user = new User(name);
            List<Option> opList = user.OptionsListCopy;
            for(int i = 0; i < opList.Count; i++)
            {
               user.RemoveOption(opList[i].Name);
            }
            for(int i = 0; i < opList.Count; i++)
            {
               if(user.HasOption(opList[i].Name))
               {Assert.Fail();}
            }

        }

        // covers E2, E3 and E4
        [TestMethod]
        public void HasOptionCaseSensitive()
        {
            User user = new User("user");
            if(!user.HasOption("Volume"))
                Assert.Fail();
            if(user.HasOption("volume"))
                Assert.Fail();
        }
    
        // covers F1
        [TestMethod]
        public void GetOptionDoesNotHaveOption()
        {
            User user = new User("user");
            bool pass = true;
            try{user.GetOption("invalid"); pass = false;}
            catch{}
            Assert.IsTrue(pass);
        }

        // covers F2
        [TestMethod]
        public void GetOptionHasOption()
        {
            User user = new User("user");
            user.AddOption("newOp", 9);
            try{user.GetOption("newOp");}
            catch{Assert.Fail();}
        }

        // covers G1
        [TestMethod]
        public void GetOptionValueDoesNotHaveOption()
        {
            User user = new User("user");
            bool pass = true;
            try{user.GetOptionValue("invalid"); pass = false;}
            catch{}
            Assert.IsTrue(pass);
        }

        // covers G2
        [TestMethod]
        public void GetOptionValueHasOption()
        {
            User user = new User("user");
            user.AddOption("newOp",9);
            Assert.AreEqual(user.GetOptionValue("newOp"),9);
        }

        // covers H1
        [TestMethod]
        public void UpdateOptionDoesNotHaveOption()
        {
            User user = new User("user");
            bool pass = true;
            try{user.UpdateOption("invalid",9); pass = false;}
            catch{}
            Assert.IsTrue(pass);
        }

        // covers H2
        [TestMethod]
        public void UpdateOptionValueIsNull()
        {
            User user = new User("user");
            user.AddOption("newOp",9);
            bool pass = true;
            try{user.UpdateOption("newOp",null); pass = false;}
            catch{}
            Assert.IsTrue(pass);
        }

        // covers H3
        [TestMethod]
        public void UpdateOptionHasOption()
        {
            User user = new User("user");
            user.AddOption("newOp",0);
            user.UpdateOption("newOp",9);
            Assert.AreEqual(user.GetOptionValue("newOp"),9);
        }

        // covers I1
        [TestMethod]
        public void RemoveOptionDoesNotHaveOption()
        {
            User user = new User("user");
            Assert.IsFalse(user.RemoveOption("invalid"));
        }

        // covers I2
        [TestMethod]
        public void RemoveOptionHasOption()
        {
            User user = new User("user");
            user.AddOption("newOp", 0);
            user.RemoveOption("newOp");    
            Assert.IsFalse(user.HasOption("newOp"));
        }

        // covers J1
        [TestMethod]
        public void RemoveAllOptionsDoesNotHaveAnyOption()
        {
            User user = new User("user");
            List<Option> listOp = user.OptionsListCopy;
            for(int i = 0; i < listOp.Count; i++)
            {
                user.RemoveOption(listOp[i].Name);
            }
            Assert.IsTrue(user.OptionCount == 0);    
            user.RemoveAllOptions();
            Assert.IsTrue(user.OptionCount == 0);    
        }

        // covers J2
        [TestMethod]
        public void RemoveAllOptionsHasSomeOptions()
        {
            User user = new User("user");
            user.AddOption("newOp", 0);
            Assert.IsTrue(user.OptionCount > 0);    
            user.RemoveAllOptions();
            Assert.IsTrue(user.OptionCount == 0);    
        }
    
        // covers K1
        [TestMethod]
        public void CopyUserTest()
        {
            User user = new User("originalName");
            user.AddOption("integer", 9);
            user.AddOption("string", "text");
            
            User userCopy = user.Copy();
            Assert.IsTrue(userCopy.UserName == user.UserName);
            Assert.IsTrue(userCopy.HasOption("integer"));
            Assert.IsTrue(userCopy.HasOption("string"));
            Assert.IsTrue(userCopy.GetOptionValue("integer").Equals(9));
            Assert.IsTrue(userCopy.GetOptionValue("string").Equals("text"));
            
            user.RemoveOption("integer");
            Assert.IsTrue(userCopy.HasOption("integer"));
            Assert.IsTrue(userCopy.HasOption("string"));
            Assert.IsTrue(userCopy.GetOptionValue("integer").Equals(9));
            Assert.IsTrue(userCopy.GetOptionValue("string").Equals("text"));

            userCopy.AddOption("newOption", 0.5);
            Assert.IsFalse(user.HasOption("new option"));
        }
    
        // covers L1
        [TestMethod]
        public void SimilarUserNamesDifferent()
        {
            User user1 = new User("name1");
            User user2 = new User("name2");
            Assert.IsFalse(user1.Similar(user2) || user2.Similar(user1));
        }

        // covers L2
        [TestMethod]
        public void SimilarOptionNameDifferent()
        {
            User user1 = new User("name1", false);
            User user2 = new User("name2", false);
            user1.AddOption("name1", 3);
            user2.AddOption("name2", 3);
            Assert.IsFalse(user1.Similar(user2) || user2.Similar(user1));
        }

        // covers L3
        [TestMethod]
        public void SimilarOptionValueDifferent()
        {
            User user1 = new User("name1", false);
            User user2 = new User("name2", false);
            user1.AddOption("name", 3);
            user2.AddOption("name", 4);
            Assert.IsFalse(user1.Similar(user2) || user2.Similar(user1));
        }

        // covers L4
        [TestMethod]
        public void SimilarUsersAreSimilar()
        {
            User user1 = new User("name", false);
            User user2 = new User("name", false);
            Assert.IsTrue(user1.Similar(user2) && user2.Similar(user1));
            
            user1.AddOption("name", 3);
            user2.AddOption("name", 3);
            Assert.IsTrue(user1.Similar(user2) && user2.Similar(user1));

            user1 = new User("name", true);
            user2 = new User("name", true);
            Assert.IsTrue(user1.Similar(user2) && user2.Similar(user1));
        }

        // covers L5
        [TestMethod]
        public void SimilarUsersNull()
        {
            User user1 = new User("name", false);
            User user2 = null;
            Assert.IsTrue(!user1.Similar(user2));
        }
    }
}