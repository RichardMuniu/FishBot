using System;
using System.IO;
using System.Collections.Generic;

namespace FishBot
{
    /// <summary>
    /// This mutable datatype represents an option for users.
    /// </summary>
    public class Option
    {
        private static char[] _allowable = new char[]
        {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
        '1','2','3','4','5','6','7','8','9','0','-','_', '@', '&', '%', '$', '#', '!','*','(',')','=', '+','~','`','<',
        '>','?',':',';','{','}','[',']','|','"','\'','\\','.'};
        private string _name;
        private object _value;

        // Rep invariant: 
        //    name.length > 0
        //    all characters in name are drawn from allowable
        //    value is never null.

        /// <summary>
        ///    Gets or sets the name of the option.
        /// </summary>   
        /// <exception cref="System.Exception"> 
        /// Throws an exception if the name provided for the setter is invalid.  The name
        /// is invalid if it is not drawn purely from characters of a QWERTY keyboard.
        /// </exception> 
        ///
        public string Name
        {
            set
            {
                if (!NameIsValid(value))
                {
                    throw new Exception("Name given is invalid.");
                }
                _name = value;
            }
            get
            {
                return _name;
            }
        }

        /// <summary>
        ///    Gets or sets the value of the option: a non-null object.
        /// </summary >
        /// <exception cref="System.Exception"> 
        ///Throws an exception if the value is null.
        /// </exception>
        ///
        public object Value
        {
            set
            {
                if(value == null)
                {
                    throw new NullReferenceException("Value given is null.");
                }
                _value = value;
            }
            get
            {
                return _value;
            }
        }

        /// <summary> Makes an option with a name and a value. </summary>
        ///
        /// <param name="name">
        /// Name of the object.
        /// </param>
        ///
        /// <param name="value">
        /// Value of the object.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if the name of the option is invalid or the value is null. The name
        /// is invalid if it is not drawn purely from characters of a QWERTY keyboard.
        /// </exception>
        ///
        public Option(string name, object value)
        {
            if(!NameIsValid(name))
            {
                throw new Exception("Option name given is invalid.");
            }
            else if(value == null)
            {
                throw new NullReferenceException("Value given is null.");
            }
            else
            {
                _name = name;
                _value = value;
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

        /// <summary>
        /// Defines observational equality to another option. Two options are observationally
        /// equal if and only if they have the same name and equal value.
        /// </summary>
        ///
        /// <param name="otherOption">
        /// Option we're comparing this option to.
        /// </param>
        /// 
        public bool Similar(Option otherOption)
        {
            if(otherOption == null)
            {
                return false;
            }
            if(otherOption.Name == this.Name && otherOption.Value.Equals(this.Value))
                return true;
            else
                return false;
        }
    }
}