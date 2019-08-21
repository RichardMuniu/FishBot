using System;
using System.IO;
using System.Collections.Generic;

namespace FishBot
{
    /// <summary>
    /// This mutable datatype represents a user with a username and a list of options.
    /// </summary>
    public class User
    {
        private static char[] _allowable = new char[]
        {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
        '1','2','3','4','5','6','7','8','9','0','-','_', '@', '&', '%', '$', '#', '!','*','(',')','=', '+','~','`','<',
        '>','?',':',';','{','}','[',']','|','"','\'','\\','.'};
        private string _username;
        private List<Option> _userOptions = new List<Option>();

        // Rep invariants: 
        //    a name of an option in userOptions would occur at most once
        //    all characters in name are drawn from allowable
        // Be aware that value could be a reference type and is returned as is.
        // Option and List of Option objects are always copied when returned however.
        // Although their values would refer to the member value if the type is a reference type.
        
        /// <summary>
        ///    Gets or sets the name of the user.
        /// </summary>   
        /// <exception cref="System.Exception"> 
        /// Throws an exception if the name provided for the setter is invalid. The name
        /// is invalid if it is not drawn purely from characters of a QWERTY keyboard.
        /// </exception> 
        ///
        public string UserName
        {
            set
            {
                if (!NameIsValid(value))
                {
                    throw new Exception("Name given is invalid.");
                }
                _username = value;
            }
            get
            {
                return _username;
            }
        }

        /// <returns>
        /// Returns the list of copies of all of the user's options.
        /// </returns>
        /// 
        public List<Option> OptionsListCopy
        {
            get
            {
                List<Option> result = new List<Option>();
                for(int i = 0; i < _userOptions.Count; i++)
                {
                    result.Add(new Option(_userOptions[i].Name,_userOptions[i].Value));
                }
                return result;
            }
        }

        /// <returns>
        /// Returns the number of options the user has.
        /// </returns>
        /// 
        public int OptionCount
        {
            get
            {
                return _userOptions.Count;
            }
        }

        /// <summary> Makes a user with a username, and adds the default options to it. </summary>
        ///
        /// <param name="username">
        /// Name of the user.
        /// </param>
        ///
        /// <param name="addDefaultOption">
        /// Default value is true. Adds the default options if true and does not otherwise.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if the name given is invalid. The name
        /// is invalid if it is not drawn purely from characters of a QWERTY keyboard.
        /// </exception>
        ///
        public User(string username, bool addDefaultOptions = true)
        {
            if (!NameIsValid(username))
            {
                throw new Exception("Name given is invalid.");
            }
            _username = username;

            if(addDefaultOptions == false)
                return;

            foreach(var option in DefaultOptions.DefualtOptionsList)
            {
                _userOptions.Add(new Option(option.Name,option.Value));
            }
            
        }

        /// <summary> Checks if the string serves as a valid name. </summary>
        ///
        /// <param name="name">
        /// Name to be checked.
        /// </param>
        ///
        /// <returns> Returns true if and only if the name given is valid. </returns>
        ///        
        private bool NameIsValid(string name)
        {
            if(String.IsNullOrEmpty(name))
                return false;

            foreach(char i in name)
            {
                if(!Array.Exists(_allowable, element => element == i))
                    return false;
            }

            return true;
        }

        /// <summary> Adds an option to the user with the name and value. </summary>
        ///
        /// <param name="name">
        /// Name of the option.
        /// </param>
        ///
        /// <param name="value">
        /// Value of the option.
        /// </param>        
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if the option already exists or if the addition or
        /// creation of the option does not succeed.
        /// </exception>
        ///     
        public void AddOption(string name, object value)
        {
            if (this.HasOption(name))
                throw new Exception("User already has object.");
            _userOptions.Add(new Option(name, value));
        }

        /// <summary> Checks if the user has an option with a certain name. </summary>
        ///
        /// <param name="name">
        /// Name of the option.
        /// </param>
        ///
        /// <returns> 
        /// Returns true if the the user has an object with that name and false otherwise.
        /// </returns>
        ///   
        public bool HasOption(string name)
        {
            for(int i = 0; i < _userOptions.Count; i++)
            {
                if (_userOptions[i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary> Gets an option with a certain name from the user. </summary>
        ///
        /// <param name="name">
        /// Name of the option.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if option is not found.
        /// </exception>
        ///
        /// <returns>
        /// Returns the option with the given name.
        /// </returns>
        ///   
        public Option GetOption(string name)
        {
            for(int i = 0; i < _userOptions.Count; i++)
            {
                if (_userOptions[i].Name == name)
                {
                    return new Option(_userOptions[i].Name, _userOptions[i].Value);
                }
            }
            throw new Exception("Option not found.");
        }

        /// <summary> Gets the value of an option with a certain name from the user. </summary>
        ///
        /// <param name="name">
        /// Name of the option.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if option is not found.
        /// </exception>
        ///
        /// <returns>
        /// Returns the value of the option with the given name.
        /// </returns>
        ///   
        public object GetOptionValue(string name)
        {
            for(int i = 0; i < _userOptions.Count; i++)
            {
                if (_userOptions[i].Name == name)
                {
                    return _userOptions[i].Value;
                }
            }
            throw new Exception("Option not found.");
        }

        /// <summary> Updates an option with a certain name from the user. </summary>
        ///
        /// <param name="name">
        /// Name of the option.
        /// </param>
        ///
        /// <param name="value">
        /// New value of the option.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if option is not found or if the value is null.
        /// </exception>
        ///   
        public void UpdateOption(string name, object value)
        {
            if(value == null)
            {
                throw new ArgumentNullException("Value provided is null.");
            }
            else if(!HasOption(name))
            {
                throw new Exception("The user does not have the option. Name = " + name + ".");
            }
            else
            {
                for(int i = 0; i < _userOptions.Count; i++)
                {
                    if(_userOptions[i].Name == name)
                        _userOptions[i].Value = value;
                }
            }
        }

        /// <summary> Removes an option with a certain name from the user. </summary>
        ///
        /// <param name="name">
        /// Name of the option.
        /// </param>
        ///
        /// <returns> 
        /// Returns true if the option is successfully removed; otherwise false. Returns false if
        /// option is not found.
        /// </returns>
        ///   
        public bool RemoveOption(string name)
        {
            if(!HasOption(name))
                return false;
            for(int i = 0; i < _userOptions.Count; i++)
            {
                if(_userOptions[i].Name == name)
                {
                    _userOptions.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary> Removes all options from the user. </summary>
        ///   
        public void RemoveAllOptions()
        {
            _userOptions = new List<Option>();
        }

        /// <summary> Returns a copy of the user with copies of the options. </summary>
        ///   
        public User Copy()
        {
            User result = new User(_username);
            result.RemoveAllOptions();
            for(int i = 0; i < _userOptions.Count; i++)
            {
                result.AddOption(_userOptions[i].Name, _userOptions[i].Value);
            }
            return result;
        }

        /// <summary>
        /// Defines observational equality to another user. Two users are observationally
        /// equal if and only if they have the same user name and if one of the users has some
        /// option then the other must have one that shares the same name and equal value.
        /// </summary>
        ///
        /// <param name="otherUser">
        /// User we're comparing this user to.
        /// </param>
        /// 
        public bool Similar(User otherUser)
        {
            if(otherUser == null)
                return false;

            if(otherUser.UserName != this.UserName)
                return false;
            
            if(this.OptionCount != otherUser.OptionCount)
                return false;

            for(int i = 0; i < this.OptionCount; i++)
            {
                Option currentOption = _userOptions[i];
                bool pass = false;
                for(int j = 0; j < otherUser.OptionCount; j++)
                {
                    if(currentOption.Similar(otherUser.OptionsListCopy[j]))
                    {
                        pass = true;
                    }
                }
                if(pass == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}