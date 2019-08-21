using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using FishBot;
using TMPro;

// Handles the leaderboard screen
public class LeaderBoard : MonoBehaviour
{
    // Scroll content object of the leaderboard
    public GameObject scrollContent;
    // Private score dictionary
    private Dictionary<string, int> _scoreDict = new Dictionary<string, int>();
    // Global user manager
    private UserManager _userManager = UserManagerScript.userManager;
    // Private list of users
    private List<User> _users;

    private void Start()
    {
        // Get list of users
        _users = _userManager.UserListCopy;
        
        // Loop through the users
        foreach (User user in _users)
        {
            // Get username
            string userName = user.UserName; 
            // Get score
            int userScore = Convert.ToInt32(user.GetOptionValue("Score"));
            // Add to dictionary
            _scoreDict.Add(userName, userScore);
        }
        
        // Order the dictionary
        var sortedDict = from ele in _scoreDict
                orderby ele.Value descending
                select ele;
        // Creates lines for displaying the dictionary
        var lines = sortedDict.Select(kvp => "\t" + kvp.Key + "\t\t\t\t" + kvp.Value.ToString());
        // Joins the lines together
        string ranking = string.Join(Environment.NewLine, lines);
        // View the lines in the scroll content
        scrollContent.GetComponent<TextMeshProUGUI>().text = ranking;
    }
}
