using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace FishBot
{   
    /// <summary>
    /// This static datatype is for saving user files and reading them.
    /// The save file/string follows a simple format. The first line is the username (it is not
    /// allowed to be null, empty or invalid). As for the rest of the lines, empty lines are ignored,
    /// and non-empty ones must follow the format "optionName optionType optionValue" where optionName
    /// is the name of the option, optionType is the string representing value type (must be in TypeList), and
    /// optionValue is non-null string that can be parsed into an object represented by optionType.
    /// </summary>
    public static class SaveIO
    {
        // Types of interest for option values (editable)
        // When type list is edited TypeParser and TypeToString should be edited and vice-versa.
        // It is recommended that the type is a value type as well.
        // When adding a new item to this list, it should be taken into consideration how parsing and converting to string work for type
        // and to make sure that consistency and preservation of information is presented.
        private static List<string> _typeList = new List<string>{
            "int",
            "string",
            "float",
            "bool",
            "double",
            "object"
        };
        
        /// <summary> Parses the content provided into an object with type specified by type name </summary>
        ///
        /// <param name="typeName">
        /// Name of the type: required to be in TypeList.
        /// </param>
        ///
        /// <param name="content">
        /// String to be parsed
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if parsing does not succeed.
        /// </exception>
        ///
        /// <returns> Returns the parsed object. </returns>
        ///
        private static object Parser(string typeName, string content)
        {
            string caseSwitch = typeName;
            try
            {
                switch (caseSwitch)
                {
                    case "int":
                        return Int32.Parse(content);
                    case "string":
                        return content;
                    case "float":
                        return float.Parse(content);
                    case "bool":
                        return bool.Parse(content);
                    case "double":
                        return double.Parse(content);
                    case "object":
                        return (object) content;
                    default:
                        throw new Exception("Type " + typeName + " is not supported by TypeParser.");
                }
            }
            catch
            {
                throw new Exception("An error occured in TypeParser. typeName = " + typeName + ", content = " + content);
            }
        }
        
        /// <summary> Gives the type name in TypeList for the Type provided. </summary>
        ///
        /// <param name="type">
        /// The type object.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if the Type's string is not in TypeList.
        /// </exception>
        ///
        /// <returns> Returns the string name of the type. </returns>
        ///
        private static string TypeString(Type type)
        {
            string caseSwitch = type.ToString();
            try
            {
                switch (caseSwitch)
                {
                    case "System.Int32":
                        return "int";
                    case "System.String":
                        return "string";
                    case "System.Single":
                        return "float";
                    case "System.Boolean":
                        return "bool";
                    case "System.Double":
                        return "double";
                    case "System.Object":
                        return "object";
                    default:
                        throw new Exception("Type" + caseSwitch + " is not supported by TypeParser.");
                }
            }
            catch
            {
                throw new Exception("An error occured in TypeString. typeName = " + caseSwitch);
            }    
        }
        
        /// <summary> Reads a user from a readable text file. </summary>
        ///
        /// <param name="path">
        /// Path of the file.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if reading does not succeed.
        /// </exception>
        ///
        /// <returns> Returns the User represented by the file. </returns>
        ///
        public static User ReadFromFile(string path)
        {
            if (!File.Exists(path))
            {
               throw new FileNotFoundException("The file provided was not found. Path: " + path + ".");
            }

            string content;
            try
            {
                // Reads the file into a string
                content = File.ReadAllText(path);
                content.Replace("\r","");
            }
            catch
            {
                throw new FileLoadException("Could not read the file. Path: " + path + ".");
            }

            // Loads the map
            return ReadFromString(content);
        }
        
        /// <summary> Reads a user from a string. Default options are updated if they are mentioned in the file.</summary>
        ///
        /// <param name="text">
        /// String to be read.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if reading does not succeed.
        /// </exception>
        ///
        /// <returns> Returns the User represented by the string. </returns>
        ///
        public static User ReadFromString(string text)
        {
            // Defines the user
            User user;
            
            // Splits the string
            string[] stringSeparator = new string[] {"\r\n","\r","\n"};
            string[] lines = text.Split(stringSeparator, StringSplitOptions.None);
            string[][] _information = new string[lines.Length][];

            for(int i = 1; i < lines.Length; i++)
            {
                _information[i] = lines[i].Split(new char[] {' '});
            }

            // Checks on the first line representing user name
            string usernameLine = lines[0];
            if(string.IsNullOrEmpty(usernameLine))
            { throw new Exception("The first line of the string is ill-formatted.");}
            else
            { user = new User(lines[0]);}

            // Defines some variables
            string optionName;
            string optionType;
            object optionValue;
            
            // Loops through the lines
            for(int i = 1; i < lines.Length; i++)
            {
                // Checks if line arrived to is empty
                if (string.IsNullOrEmpty(lines[i])) {continue;}
                else if(_information[i].Length != 3)
                {throw new Exception("In the file/string, Line " + i.ToString() + " is ill-formatted.");}
                // Record Information
                try
                {
                    optionName = _information[i][0];
                    optionType = _information[i][1];
                    optionValue = Parser(optionType, _information[i][2]);
                    if (user.HasOption(optionName))
                    {
                        user.UpdateOption(optionName, optionValue);
                    }
                    else
                    {
                        user.AddOption(optionName, optionValue);
                    }
                }
                catch
                {
                    throw new Exception("In the file/string, Line " + i.ToString() + " is ill-formatted.");
                }
            }
            
            // Returns            
            return user;
        }

        /// <summary> Saves the user information into the file given by the path. File is created if it does not exist.</summary>
        ///
        /// <param name="user">
        /// User to be saved.
        /// </param>
        ///
        /// <param name="path">
        /// Path to save-file.
        /// </param>
        ///        
        /// <exception cref="System.Exception"> 
        /// Throws an exception if saving does not succeed.
        /// </exception>
        ///        
        public static void SaveToFile(User user, string path)
        {
            string contents = SaveToText(user);
            try
            {
                File.WriteAllText(path, contents);
            }
            catch
            {
                throw new Exception("Could not write into the path provided.\n Path = " + path + "\n");
            }
        }

        /// <summary> Saves the user information into a well-formatted string and returns it. </summary>
        ///
        /// <param name="user">
        /// User to be saved.
        /// </param>
        ///
        /// <exception cref="System.Exception"> 
        /// Throws an exception if saving does not succeed.
        /// </exception>
        ///
        /// <returns> Returns the string representing the content of the save. </returns>
        ///       
        public static string SaveToText(User user)
        {
            // Creates the string
            string contents = "";
            // Adds the username
            contents += user.UserName;
            // Gets the list of options
            List<Option> optionsList = user.OptionsListCopy;
            // Loops throught the options
            for(int i = 0; i < optionsList.Count; i++)
            {
                // Variables
                Option currentOption = optionsList[i];
                string currentName = currentOption.Name;
                object currentValue = currentOption.Value;
                // Formats and adds the object to a new line
                contents += "\n";
                contents += currentName;
                contents += " ";
                try {contents += TypeString(currentValue.GetType());}
                catch { throw new Exception("The type of Option " + i.ToString() + "of user " + user.UserName + " is not valid for parsing.");}
                contents += " ";
                contents += currentValue.ToString();
            }
            // Return the string.
            return contents;
        }
    }
}