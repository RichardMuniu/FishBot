using System;
using System.Collections.Generic;

namespace FishBot
{
    class Fish
    {
        List<PowerUp> _powerUps;
        int health; // to be removed***
        List<MechanicalAbility> abilities;
        /// Default constructor for a fish object. Takes in no arguments.
        public Fish() { }
        
    }
    /// Class that manages a list of users who play the game
    class UserManager
    {
        List<User> _users;
        /// Constructor for a UserManager object. Upon game loading, this method 
        /// reads user data from a passed in location and creates and manages a list
        /// of User objects, which can be only be retieved and modified in the original 
        /// location by this class 
        /// requires: a string representing the location of the file containing the user
        ///     data
        /// effects: returns nothing, but creates a list of users using the user data provided
        public UserManager(string location)
        {
            /// TODO: implement this function
        }
        /// effects: returns a list containing all loaded users as User objects
        public List<User> AllUsers
        {
            get {return _users; } //// *** might  need a protective safe copy: list references are mutable
        }
        /*
         * TODO: Devise some composite to save updated user info back to the original file 
         */
    }

    /// Class that defines a game user. A user is identified by their unique 
    /// username, and the user object stores information about each user's 
    /// custom settings and level progress
    class User
    {
        string _userName;
        int _progress;
        int _score;

        /// accessors/mutators

        /// effects: returns a string representing the user's in-game name
        /// effects: given a string value, sets the user's in-game name to that value 
        public string UserName
        {
            get {return _userName; }
            set {_userName = value; }
        }
        /// effects: returns an integer representing a user's progress
        /// effects: given an integer value, sets the user's progress to that value 
        public int Progress
        {
            get {return _progress; }
            set {_progress = value; }
        }
        /// effects: returns an integer representing a user's last saved game score
        /// effects: given an integer value, sets the user's score to that value 
        public int Score
        {
            get {return _score; }
            set {_score = value; }
        }
        /// Default constructor for a User object
        public User() {}
        /// Constructor for a User object. 
        public User(string userName)
        {
            _userName = userName;
            _progress = 0;
            _score = 0;
        }
    }
    
    /// Base class for PowerUp types
    abstract class PowerUp
    {
        ///TODO: pure virtual methods?
    }
    class ProjectileUpgrader : PowerUp
    {

    }
    class ShellArmor : PowerUp
    {

    }
    class SpeedBoost : PowerUp
    {

    }

    /// Virtual base class for obstacle objects
    abstract class Obstacle
    {

    }

    class TNTBox : Obstacle
    {

    }

    class Tar : Obstacle
    {

    }

    class SeaMine :Obstacle
    {

    }
    class Turbines : Obstacle
    {

    }

    abstract class MechanicalAbility
    {

    }
    /// A mechanical ability that adds a fish's movement speed. This enables increased 
    /// versatility and reaction time in the face of obstacles 
    class SpeedChanger : MechanicalAbility
    {
        int _speedChangerLevel;
        float _speedValue;
        /// Default constructor for a SpeedChanger object.

        /// effects: returns the level of the SpeedChanger object; not settable
        public int Level
        {
            get {return _speedChangerLevel; }
        }
        /// effects: returns the speed boost value of the SpeedChanger object; not settable
        public float Speed
        {
            get {return _speedValue; }
        }
        public SpeedChanger() {}
        /// Constructor for a SpeedChanger object.
        /// requires: an int specifying the speed level of the ability. Higher levels allow
        ///     for faster user(fish) movement.
        /// effects: returns a SpeedChanger object of the specified level. 
        public SpeedChanger(int speedLevel)
        {
            _speedChangerLevel = speedLevel;
            /// TODO: implement this method, determine _speedValue setting criteria depending on 
            /// the speed level passed in
            /// Eg:
            /*
             *  Dict speedDict = new Dictionary<int, float>  /// Dictionary of levels and their 
                                        {                        associated speeds
                                            { 1, 10.0 },
                                            { 2, 25.0 },
                                            { 3, 40.0 }
                                        };
             *  _speedValue = speedDict[_speedLevel];
             */
        }

        ///TODO: specify other methods

    }
    class ProjectileShooter : MechanicalAbility
    {
        int _shooterLevel;
        float _damage;
        /// effects: returns the level of the projectile shooter object
        public int Level
        {
            get {return _shooterLevel; }
        }
        /// effects: returns the damage per shot of the Projectile Shooter object; not settable
        public float Damage
        {
            get {return _damage; }
        }
        public ProjectileShooter() {}
        /// Constructor for a ProjectileShooter object.
        /// requires: an int specifying the speed level of the ability. Higher levels allow
        ///     for higher damage shots - possible improvements would include better shooter 
        ///     animations
        /// effects: returns a ProjectileShooter object of the specified level. 
        public ProjectileShooter(int shooterLevel)
        {
            _shooterLevel = shooterLevel;
            /// TODO: implement this method, determine _speedValue setting criteria depending on 
            /// the shooter level passed in
            /// Eg:
            /*
             *  Dict damageDict = new Dictionary<int, float>  /// Dictionary of levels and their 
                                        {                        associated damages.
                                            { 1, 0.25 },
                                            { 2, 0.50 },
                                            { 3, 0.70 }
                                        };
             *  _speedValue = damageDict[_speedLevel];
             */
        }

        ///TODO: specify other methods

    }
}
