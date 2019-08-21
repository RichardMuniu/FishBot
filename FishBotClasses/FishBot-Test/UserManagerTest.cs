using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace FishBot
{
    /*
    * Testing strategy
    *
    * Partition the inputs as follows:
    * A) For UserListCopy & UserNameListCopy & UserCount properties:
    *   1) UserList empty and count is zero.
    *   2) Actual copy is returned with actual count.
    * B) For SelectedUserCopy & SelectedUserIndex properties:
    *   1) No user is selected.
    *   2) Some user is selected and a copy is returned
    * C) For HasUser:
    *   1) The list has the user and the list does not.
    * D) For GetUserCopy & GetUserIndex:
    *   1) The list does not have the user and the list has the user.
    * E) For SelectUser and DeselectUser:
    *   1) The name or index are not present and the name and index are present.
    * F) For ReconfigureSelectedUser:
    *   1) No user selected.
    *   2) Duplicate names.
    *   3) User given null.
    *   4) Reconfiguration succeeds.
    * G) For RemoveUser:
    *   1) The list does not have the user.
    *   2) Selected user is removed.
    *   3) Non-selected user is removed and selected index is updated.
    * H) For AddUser:
    *   1) The manager has the user.
    *   2) The manager does not have the user.
    * I) For Remove, Add, Update, Has Option for Selected User:
    *   1) No user is selected.
    *   2) Some user is selected.
    * J) For ReadAllUsers:
    *   1) Nothing to read. All non-readable files.
    *   2) Read TestManagerRead folder.
    * K) For SaveAllUsers:
    *   1) Nothing to save (target is TestManagerSaveEmpty)
    *   2) Save a list of users (target is TestManagerSaveNonEmpty)
    *
    * Cover each part testing.
    */ 
    [TestClass]
    public class UserManagerTest
    {
        // covers A1
        [TestMethod]
        public void UserListUserNameListCopyEmptyLists()
        {
            UserManager manager = new UserManager();
            List<User> userlist = manager.UserListCopy;
            List<string> namelist = manager.UserNameListCopy;
            Assert.IsTrue(userlist.Count == 0 && namelist.Count == 0);
            Assert.IsTrue(manager.UserCount == 0);
        }

        // covers A2
        [TestMethod]
        public void UserListUserNameListCopyNonEmptyLists()
        {
            UserManager manager = new UserManager();
            User user = new User("name");
            manager.AddUser(user);
            List<User> userlist = manager.UserListCopy;
            List<string> namelist = manager.UserNameListCopy;
            Assert.IsTrue(userlist.Count == 1 && namelist.Count == 1);
            Assert.IsTrue(namelist[0] == "name" && userlist[0].UserName == "name");
            Assert.IsTrue(userlist[0].OptionCount == userlist[0].OptionCount);
            Assert.IsTrue(manager.UserCount == 1);

        }

        // covers B1
        [TestMethod]
        public void SelectedUserCopyAndIndexNoUserSelected()
        {
            UserManager manager = new UserManager();
            Assert.IsTrue(manager.SelectedUserIndex == -1);
            try{User testUser = manager.SelectedUserCopy;}
            catch{return;}
            Assert.Fail();
        }

        // covers B2
        [TestMethod]
        public void SelectedUserCopyAndIndexSomeUserSelected()
        {
            UserManager manager = new UserManager();
            Assert.IsTrue(manager.SelectedUserIndex == -1);
            bool pass = true;
            try{User testUser = manager.SelectedUserCopy; pass = false;}
            catch{return;}
            Assert.IsTrue(pass);
            
            User user = new User("name");
            manager.AddUser(user);
            Assert.IsTrue(manager.SelectedUserIndex == -1);
            try{User testUser = manager.SelectedUserCopy; pass = false;}
            catch{}
            Assert.IsTrue(pass);

            manager.SelectUser(0);
            Assert.IsTrue(manager.SelectedUserIndex == 0);
            Assert.IsTrue(manager.SelectedUserCopy.Similar(user));
        }

        // covers C1
        [TestMethod]
        public void HasUserTest()
        {
            UserManager manager = new UserManager();
            Assert.IsTrue(!manager.HasUser("name"));
            User user = new User("name");
            manager.AddUser(user);
            Assert.IsTrue(manager.SelectedUserIndex == -1);
            bool pass = true;
            try{User testUser = manager.SelectedUserCopy; pass = false;}
            catch{}
            Assert.IsTrue(pass);

            manager.SelectUser(0);
            Assert.IsTrue(manager.SelectedUserIndex == 0);
            Assert.IsTrue(manager.SelectedUserCopy.Similar(user));
        }

        // covers D1
        [TestMethod]
        public void GetUserCopyAndIndex()
        {
            UserManager manager = new UserManager();
            bool pass = true;
            try
            {
                manager.GetUserCopy("name");
                pass = false;
            } catch{} Assert.IsTrue(pass);
            try
            {
                manager.GetUserIndex("name");
                pass = false;
            } catch{} Assert.IsTrue(pass);

            manager.AddUser(new User("name"));
            Assert.IsTrue(manager.GetUserCopy("name").Similar(new User("name")));
            Assert.IsTrue(manager.GetUserIndex("name") == 0);
        }

        // covers E1
        [TestMethod]
        public void SelectDeselectUser()
        {
            UserManager manager = new UserManager();
            bool pass = true;
            try
            {
                manager.SelectUser("name");
                pass = false;
            } catch{} Assert.IsTrue(pass);
            try
            {
                manager.SelectUser(0);
                pass = false;
            } catch{} Assert.IsTrue(pass);

            manager.AddUser(new User("name"));
            manager.SelectUser("name");
            Assert.IsTrue(manager.SelectedUserCopy.Similar(new User("name")));
            Assert.IsTrue(manager.SelectedUserIndex == 0);

            manager.Deselect();
            Assert.IsTrue(manager.SelectedUserIndex == -1);
        }

        // covers F1
        [TestMethod]
        public void ReconfigureNoUserSelected()
        {
            UserManager manager = new UserManager();
            try
            {
                manager.ReconfigureSelectedUser(new User("name"));
            }
            catch{return;}
            Assert.Fail();
        }

        // covers F2
        [TestMethod]
        public void ReconfigureDuplicateUser()
        {
            UserManager manager = new UserManager();
            User user1 = new User("name1");
            User user2 = new User("name2");
            manager.AddUser(user1);
            manager.AddUser(user2);
            manager.SelectUser("name1");
            try
            {
                manager.ReconfigureSelectedUser(new User("name2", false));
            }
            catch{return;}
            Assert.Fail();
        }

        // covers F3
        [TestMethod]
        public void ReconfigureUserNull()
        {
            UserManager manager = new UserManager();
            User user1 = new User("name1");
            manager.AddUser(user1);
            manager.SelectUser("name1");
            try
            {
                manager.ReconfigureSelectedUser(null);
            }
            catch{return;}
            Assert.Fail();
        }

        // covers F4
        [TestMethod]
        public void ReconfigureUserSucceeds()
        {
            UserManager manager = new UserManager();
            User user1 = new User("name1");
            User user2 = new User("name2");
            manager.AddUser(user1);
            manager.SelectUser("name1");
            manager.ReconfigureSelectedUser(user2);
            Assert.IsTrue(manager.SelectedUserCopy.Similar(user2));
            manager.ReconfigureSelectedUser(new User("name2",false));
            user2.RemoveAllOptions();
            Assert.IsTrue(manager.SelectedUserCopy.Similar(user2));
        }

        // covers G1
        [TestMethod]
        public void RemoveUserListDoesNotHaveUser()
        {
            UserManager manager = new UserManager();
            try
            {
                manager.RemoveUser("name");    
            }
            catch{return;}
            Assert.Fail();
        }

        // covers G2
        [TestMethod]
        public void RemoveUserSelectedUserRemoved()
        {
            UserManager manager = new UserManager();
            manager.AddUser(new User("name"));
            manager.SelectUser("name");
            manager.RemoveUser("name");
            Assert.IsTrue(manager.SelectedUserIndex == -1);
        }

        // covers G3
        [TestMethod]
        public void RemoveUserNonSelectedRemoved()
        {
            UserManager manager = new UserManager();
            manager.AddUser(new User("name1"));
            manager.AddUser(new User("name2"));
            manager.SelectUser("name2");
            Assert.IsTrue(manager.SelectedUserIndex == 1);
            manager.RemoveUser("name1");
            Assert.IsTrue(manager.SelectedUserIndex == 0);
            Assert.IsTrue(manager.SelectedUserCopy.Similar(new User("name2")));
            Assert.IsTrue(manager.UserListCopy.Count == 1);
        }

        // covers H1
        [TestMethod]
        public void AddUserAlreadyInList()
        {
            UserManager manager = new UserManager();
            manager.AddUser(new User("name1"));
            try
            {
                manager.AddUser(new User("name1"));
            } catch{return;}
            Assert.Fail();
        }

        // covers H2
        [TestMethod]
        public void AddUserNotInList()
        {
            UserManager manager = new UserManager();
            manager.AddUser(new User("name1"));
            Assert.IsTrue(manager.HasUser("name1"));
        }

        // covers I1
        [TestMethod]
        public void SelectedUserOptionsTestNoSelectedUser()
        {
            UserManager manager = new UserManager();
            manager.AddUser(new User("name1"));
            bool pass = true;
            try
            {
                manager.AddOptionSelectedUser("name",3);
                pass = false;
            }catch{} Assert.IsTrue(pass);
            try
            {
                manager.RemoveOptionSelectedUser("Volume");
                pass = false;
            }catch{} Assert.IsTrue(pass);
            try
            {
                manager.UpdateOptionSelectedUser("Volume", 3);
                pass = false;
            }catch{} Assert.IsTrue(pass);
            try
            {
                manager.HasOptionSelectedUser("Volume");
                pass = false;
            }catch{} Assert.IsTrue(pass);
        }

        // covers I2
        [TestMethod]
        public void SelectedUserOptionsTestSomeSelectedUser()
        {
            UserManager manager = new UserManager();
            manager.AddUser(new User("name1"));
            manager.SelectUser("name1");
            manager.AddOptionSelectedUser("name",3);
            manager.RemoveOptionSelectedUser("Brightness");
            manager.UpdateOptionSelectedUser("Volume", 3);
            manager.HasOptionSelectedUser("Volume");
        }

        // covers J1 (machine specific)
        [Ignore]
        [TestMethod]
        public void ReadAllUsersNothingToRead()
        {
            UserManager manager = new UserManager();
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\";
            manager.ReadAllUsers(path);
            Assert.IsTrue(manager.UserCount == 0);
            path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test";
            manager.ReadAllUsers(path);
            Assert.IsTrue(manager.UserCount == 0);
        }

        // covers J2 (machine specific)
        [Ignore]
        [TestMethod]
        public void ReadAllUsersTestManagerReadFolder()
        {
            UserManager manager = new UserManager();
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\TestManagerRead";
            manager.ReadAllUsers(path);
            Assert.IsTrue(manager.UserCount == 2);
            Assert.IsTrue(manager.HasUser("user1"));
            Assert.IsTrue(manager.HasUser("user2"));
            User user1 = manager.GetUserCopy("user1");
            User user2 = manager.GetUserCopy("user2");
            Assert.IsTrue(user1.HasOption("op1"));
            Assert.IsTrue(user1.GetOptionValue("op1").Equals(3) || user1.GetOptionValue("op1").Equals(6));
        }

        // covers K1 (machine specific)
        [Ignore]
        [TestMethod]
        public void SaveAllUsersNothingToSave()
        {
            UserManager manager = new UserManager();
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\TestManagerSaveEmpty";
            manager.SaveAllUsers(path);
        }

        // covers K2 (machine specific)
        [Ignore]
        [TestMethod]
        public void SaveAllUsersSaveToTestManagerNonEmpty()
        {
            UserManager manager = new UserManager();
            string path = @"C:\Users\ymqad\Desktop\Git Folder\project\FishBotClasses\FishBot-Test\TestManagerSaveNonEmpty";
            manager.AddUser(new User("name1"));
            manager.SelectUser("name1");
            manager.AddOptionSelectedUser("name",3);
            manager.SelectUser(-1);
            manager.AddUser(new User("name2"));
            manager.SaveAllUsers(path);
        }
    }
}