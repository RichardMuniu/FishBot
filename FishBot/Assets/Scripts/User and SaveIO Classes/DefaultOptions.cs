using System;
using System.IO;
using System.Collections.Generic;

namespace FishBot
{
    //
    // This static datatype is for holding DefaultOptions that are
    // given to every newly created user. Make sure property DefaultOptionsList
    // is edited whenever a default option is added or removed.
    // It is also good to note that the types used here are compatible witht the
    // in SaveIO and that they are value types and not reference types.
    //
    public static class DefaultOptions
    {
        /// <summary>
        /// Gives a new list of copies of default options.
        /// </summary>
        public static List<Option> DefualtOptionsList
        {
            get
            {
                return new List<Option>
                {
                    DefaultVolume,
                    DefaultBrightness,
                    DefaultLevel,
                    DefaultScore
                };
            }
        }

        /// <summary>
        /// Default option for volume. Name is "Volume" and
        /// value is the integer 100.
        /// </summary>
        public static Option DefaultVolume
        {
            get
            {
                return new Option("Volume", 100);
            }
        }

        /// <summary>
        /// Default option for volume. Name is "Brightness" and
        /// value is the integer 100.
        /// </summary>
        public static Option DefaultBrightness
        {
            get
            {
                return new Option("Brightness", 100);
            }
        }

        /// <summary>
        /// Default option for volume. Name is "Level" and
        /// value is the integer 1.
        /// </summary>
        public static Option DefaultLevel
        {
            get
            {
                return new Option("Level", 1);
            }
        }

        /// <summary>
        /// Default option for volume. Name is "Score" and
        /// value is the integer 0.
        /// </summary>
        public static Option DefaultScore
        {
            get
            {
                return new Option("Score", 0);
            }
        }
    }
}