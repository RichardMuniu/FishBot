using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FishBot;

// Global script that holds the user manager
public static class UserManagerScript : object
{
    // Global user manager
    public static UserManager userManager = new UserManager();
    // Global folder path
    public static string folderPath;

    // Saves all users into the save folder (deletes all txt files first then saves the users)
    public static void SaveUsers()
    {
        string path = folderPath;
        if (Directory.Exists(path)) { Directory.Delete(path, true);}
        Directory.CreateDirectory(path);
        userManager.SaveAllUsers(path);
    }

    // Reads users from the saves folder
    public static void ReadUsers()
    {
        userManager.ReadAllUsers(folderPath);
    }
}
