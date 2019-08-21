using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace FishBot
{
    /// <summary>
    /// This class manages saving and loading multiple users, and selecting a current user.
    /// </summary>
    public class UserManager
    {

        // Rep Invariant
        //  names of users in user list are distinct
        //  strings in pathlist are distinct
        //  string in usernamelist are distinct
        //  the three private lists have the same size
        private List<User> _userList = new List<User>();
        private List<string> _userNameList = new List<string>();
        private int _selectedIndex = -1;

        ///
        /// <summary>
        /// Returns the number of users in the manager.
        /// </summary>
        ///
        public int UserCount
        {
            get
            {
                return _userList.Count;
            }
        }
        
        /// <summary>
        ///    Returns a copy of the list of users (each user is copied as well).
        /// </summary>   
        ///
        public List<User> UserListCopy
        {
            get
            {
                List<User> result = new List<User>();
                for(int i = 0; i < _userList.Count; i++)
                {
                    result.Add(_userList[i].Copy());
                }
                return result;
            }
        }

        /// <summary>
        ///    Returns a copy of the selected user.
        /// </summary>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if no user is selected.
        /// </exception>   
        ///
        public User SelectedUserCopy
        {
            get
            {
                if( _selectedIndex == -1)
                    throw new Exception("No user is selected.");
                else
                    return _userList[_selectedIndex].Copy();

            }
        }

        /// <summary>
        ///    Returns the index of the selected user (-1 if none is selected).
        /// </summary>   
        ///
        public int SelectedUserIndex
        {
            get
            {
                return _selectedIndex;
            }
        }

        /// <summary>
        ///    Returns a copy of the list of usernames.
        /// </summary>   
        ///
        public List<string> UserNameListCopy
        {
            get
            {
                List<string> result = new List<string>();
                for(int i = 0; i < _userNameList.Count; i++)
                {
                    result.Add(_userNameList[i]);
                }
                return result;
            }
        }
        
        /// <summary> Empty constructor. </summary>
        ///
        public UserManager(){}

        /// <summary> Saves the users into .txt files in folder path. The file-name are integers </summary>
        ///
        /// <param name="folderPath">
        /// Path to the folder to which files are to be saved.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if saving does not succeed.
        /// </exception>
        ///
        public void SaveAllUsers(string folderPath)
        {
            if(folderPath.Contains("/"))
            {
                if(folderPath[folderPath.Length - 1] != '/')
                    folderPath = folderPath + "/";
            }
            else if(folderPath.Contains("\\"))
            {
                if(folderPath[folderPath.Length - 1] != '\\')
                    folderPath = folderPath + "\\";
            }
            else
            {
                throw new IOException("The folder path given is invalid.");
            }

            for(int i = 0; i < _userList.Count; i++)
            {
                try
                {
                    SaveIO.SaveToFile(_userList[i], folderPath + i.ToString() + ".txt");
                }
                catch
                {
                    throw new IOException("Could not save file.");
                }
            }
        }

        /// <summary> 
        /// Reads text files that hold users in the folder path. Old list data is removed. Duplicate name reads
        /// are dealt with by overwriting old data. Files to be read must end with .txt extension.
        /// </summary>
        ///
        /// <param name="folderPath">
        /// Path to the folder from which files are to be read.
        /// </param>
        ///
        public void ReadAllUsers(string folderPath)
        {
            _userList = new List<User>();
            _userNameList = new List<string>();
            User currentUser;
            foreach (string file in Directory.GetFiles(folderPath, "*.txt"))
            {
                try
                {
                    currentUser = SaveIO.ReadFromFile(file);
                    if(HasUser(currentUser.UserName))
                    {
                        int i = GetUserIndex(currentUser.UserName);
                        _userList[i] = currentUser;
                        _userNameList[i] = currentUser.UserName;
                    }
                    else
                    {
                        _userList.Add(currentUser);
                        _userNameList.Add(currentUser.UserName);
                    }
                }
                catch{}
            }
        }

        /// <summary> Returns true is the user list has the name given and false otherwise. </summary>
        ///
        /// <param name = "name">
        /// Name of the the user.
        /// </param>
        ///
        public bool HasUser(string name)
        {
            for(int i = 0; i < _userList.Count; i++)
            {
                if(_userList[i].UserName == name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary> Returns a copy of the user of a certain name. </summary>
        ///
        /// <param name = "name">
        /// Name of the the user.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if the user is not found.
        /// </exception>
        ///
        public User GetUserCopy(string name)
        {
            if(HasUser(name))
            {
                int index = GetUserIndex(name);
                return _userList[index].Copy();
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        /// <summary> Returns the index of the user of a certain name. </summary>
        ///
        /// <param name = "name">
        /// Name of the the user.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if the user is not found.
        /// </exception>
        ///
        public int GetUserIndex(string name)
        {
            if(!HasUser(name))
                throw new Exception("User not found.");
            for(int i = 0; i < _userList.Count; i++)
            {
                if(_userList[i].UserName == name)
                    return i;
            }

            throw new Exception("This line should not have been reached.");
        }

        /// <summary> Selects the user of a certain name. </summary>
        ///
        /// <param name="name">
        /// Name of the the user.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if the user is not found.
        /// </exception>
        ///
        public void SelectUser(string name)
        {
            if(HasUser(name))
            {
                _selectedIndex = GetUserIndex(name);
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        /// <summary> Sets the selected index to -1. </summary>
        ///
        public void Deselect()
        {
            _selectedIndex = -1;
        }

        /// <summary>
        /// Selects the user of a certain index. Deselects if the index is -1.
        /// </summary>
        ///
        /// <param name="index">
        /// Index of the the user or -1.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if the index is invalid.
        /// </exception>
        ///
        public void SelectUser(int index)
        {
            if(index == -1)
            {
                _selectedIndex = -1;
                return;
            }
            else if(!(0 <= index && index <= (_userList.Count - 1)))
            {
                throw new Exception("Index is invalid.");
            }
            else
            {
                _selectedIndex = index;
            }
        }

        /// <summary> Changes the selected user to the new given user </summary>
        ///
        /// <param name = "user">
        /// The user that replaces the selected one.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if no user is selected or if a non-selected user holds
        /// the same name as the user given. Also, throws an exception if the user given is null.
        /// </exception>
        ///
        public void ReconfigureSelectedUser(User user)
        {
            if(user == null)
            {
                throw new NullReferenceException("User is null.");
            }

            if(_selectedIndex == -1)
            {
                throw new Exception("No user is selected.");
            }

            if(HasUser(user.UserName))
            {
                if(GetUserIndex(user.UserName) != _selectedIndex)
                {
                    throw new Exception("Cannot duplicate users.");
                }
            }
            _userList[_selectedIndex] = user.Copy();
            _userNameList[_selectedIndex] = user.UserName;
        }

        /// <summary> Removes the user of a certain name. </summary>
        ///
        /// <param name = "name">
        /// Name of the the user.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if the user is not found.
        /// </exception>
        ///
        public void RemoveUser(string name)
        {
            if(!HasUser(name))
                throw new Exception("User not found.");
            else
            {
                int index = GetUserIndex(name);
                string selectedUserName;
                if (index == _selectedIndex)
                {
                    _selectedIndex = -1;
                    _userList.RemoveAt(index);
                    _userNameList.RemoveAt(index);
                }
                else
                {
                    selectedUserName = _userNameList[_selectedIndex];
                    _userList.RemoveAt(index);
                    _userNameList.RemoveAt(index);
                    _selectedIndex = GetUserIndex(selectedUserName);
                }
            }
        }

        /// <summary> Adds the user to the list of users. </summary>
        ///
        /// <param name = "user">
        /// The user to be added.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if the username is already in the list or if the user is null.
        /// </exception>
        ///
        public void AddUser(User user)
        {
            if(user == null)
            {
                throw new NullReferenceException("User is null.");
            }
            if(HasUser(user.UserName))
            {
                throw new Exception("User already in list.");
            }
            else
            {
                _userList.Add(user.Copy());
                _userNameList.Add(user.UserName);
            }
        }

        /// <summary> Updates a certain option of the selected user. </summary>
        ///
        /// <param name= "optionName">
        /// Name of the the option.
        /// </param>
        ///
        /// <param name= "value">
        /// New value to be given.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if no user is selected or if the option could not be found or updated.
        /// </exception>
        ///
        public void UpdateOptionSelectedUser(string optionName, object value)
        {
            if(SelectedUserIndex == -1)
                throw new Exception("No user is selected.");
            User selectedUser = SelectedUserCopy;
            selectedUser.UpdateOption(optionName, value);
            ReconfigureSelectedUser(selectedUser);
        }

        /// <summary> Checks if the selected has an option with a cartain name.
        /// Returns true if it is the case and false otherwise.
        /// </summary>
        ///
        /// <param name = "optionName">
        /// Name of the the option.
        /// </param>
        ///
        public bool HasOptionSelectedUser(string optionName)
        {
            User selectedUser = SelectedUserCopy;
            if(selectedUser.HasOption(optionName))
                return true;
            else
                return false;
        }

        /// <summary> Removes a certain option of the selected user. </summary>
        ///
        /// <param name = "optionName">
        /// Name of the the option.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if no user is selected or if the option could not be found or updated.
        /// </exception>
        ///
        public void RemoveOptionSelectedUser(string optionName)
        {
            if(SelectedUserIndex == -1)
                throw new Exception("No user is selected.");
            User selectedUser = SelectedUserCopy;
            if(selectedUser.RemoveOption(optionName) == true)
            {}
            else
            {
                throw new Exception("Option could not be removed.");
            }
            ReconfigureSelectedUser(selectedUser);
        }

        /// <summary> Removes a certain option of the selected user. </summary>
        ///
        /// <param name = "optionName">
        /// Name of the the option.
        /// </param>
        ///
        /// <param name = "value">
        /// Value of the the option.
        /// </param>
        ///
        /// <exception cref="System.Exception">
        /// Throws an exception if no user is selected or if the option could not be added
        /// which happens if the option already exists or the value is null.
        /// </exception>
        ///
        public void AddOptionSelectedUser(string optionName, object value)
        {
            if(SelectedUserIndex == -1)
                throw new Exception("No user is selected.");
            User selectedUser = SelectedUserCopy;
            selectedUser.AddOption(optionName, value);
            ReconfigureSelectedUser(selectedUser);
        }    
    }
}