using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace GameFramework
{
    public class SettingsManager
    {

        //-------------------------------------------------------------------------------------
        // Class variables

        // A reference to the game host object
        private GameHost _game;

        //-------------------------------------------------------------------------------------
        // Class constructor

        /// <summary>
        /// Class constructor. Scope is internal so external code cannot create instances.
        /// </summary>
        internal SettingsManager(GameHost game)
        {
            // Store the game reference
            _game = game;
        }


        //-------------------------------------------------------------------------------------
        // Class functions

        /// <summary>
        /// Add a new setting or update a setting value
        /// </summary>
        /// <param name="settingName">The name of the setting to add or update</param>
        /// <param name="value">The new value for the setting</param>
        public void SetValue(string settingName, string value)
        {
            // Convert the setting name to lower case so that names are case-insensitive
            settingName = settingName.ToLower();

            // Does a setting with this name already exist?
            if (IsolatedStorageSettings.ApplicationSettings.Contains(settingName))
            {
                // Yes, so update its value
                IsolatedStorageSettings.ApplicationSettings[settingName] = value;
            }
            else
            {
                // No, so add it
                IsolatedStorageSettings.ApplicationSettings.Add(settingName, value);
            }
        }
        /// <summary>
        /// Add or update a setting as an integer value
        /// </summary>
        public void SetValue(string settingName, int value)
        {
            SetValue(settingName, value.ToString());
        }

        /// <summary>
        /// Add or update a setting as a float value
        /// </summary>
        public void SetValue(string settingName, float value)
        {
            SetValue(settingName, value.ToString());
        }

        /// <summary>
        /// Add or update a setting as a bool value
        /// </summary>
        public void SetValue(string settingName, bool value)
        {
            SetValue(settingName, value.ToString());
        }

        /// <summary>
        /// Add or update a setting as a date value
        /// </summary>
        public void SetValue(string settingName, DateTime value)
        {
            SetValue(settingName, value.ToString("yyyy-MM-ddTHH:mm:ss"));
        }


        /// <summary>
        /// Retrieve a setting value from the object
        /// </summary>
        /// <param name="settingName">The name of the setting to be retrieved</param>
        /// <param name="defaultValue">A value to return if the requested setting does not exist</param>
        public string GetValue(string settingName, string defaultValue)
        {
            // Convert the setting name to lower case so that names are case-insensitive
            settingName = settingName.ToLower();

            // Does a setting with this name exist?
            if (IsolatedStorageSettings.ApplicationSettings.Contains(settingName))
            {
                // Yes, so return it
                return IsolatedStorageSettings.ApplicationSettings[settingName].ToString();
            }
            else
            {
                // No, so return the default value
                return defaultValue;
            }
        }

        /// <summary>
        /// Retrieve a setting as an int value
        /// </summary>
        public int GetValue(string settingName, int defaultValue)
        {
            return int.Parse(GetValue(settingName, defaultValue.ToString()));
        }

        /// <summary>
        /// Retrieve a setting as a float value
        /// </summary>
        public float GetValue(string settingName, float defaultValue)
        {
            return float.Parse(GetValue(settingName, defaultValue.ToString()));
        }

        /// <summary>
        /// Retrieve a setting as a bool value
        /// </summary>
        public bool GetValue(string settingName, bool defaultValue)
        {
            return bool.Parse(GetValue(settingName, defaultValue.ToString()));
        }

        /// <summary>
        /// Retrieve a setting as a date value
        /// </summary>
        public DateTime GetValue(string settingName, DateTime defaultValue)
        {
            return DateTime.Parse(GetValue(settingName, defaultValue.ToString("yyyy-MM-ddTHH:mm:ss")));
        }

        /// <summary>
        /// Clear all current setting values
        /// </summary>
        public void ClearValues()
        {
            IsolatedStorageSettings.ApplicationSettings.Clear();
        }

        /// <summary>
        /// Delete a setting value
        /// </summary>
        /// <param name="settingName">The name of the setting to be deleted</param>
        public void DeleteValue(string settingName)
        {
            // Do we have this setting in the dictionary?
            if (IsolatedStorageSettings.ApplicationSettings.Contains(settingName.ToLower()))
            {
                // Yes, so remove it
                IsolatedStorageSettings.ApplicationSettings.Remove(settingName);
            }
        }

        /// <summary>
        /// Read the values from all SettingsItemObjects contained within the
        /// game's GameObjects collection and store them all for later retrieval.
        /// </summary>
        public void RetrieveValues()
        {
            // Clear any existing values in the settings dictionary
            //_settings.Clear();

            // Loop through the GameObjects list looking for settings objects
            foreach (GameObjectBase obj in _game.GameObjects)
            {
                // Is this a setting object?
                if (obj is SettingsItemObject)
                {
                    // It is, so apply its value to the dictionary
                    SetValue(((SettingsItemObject)obj).Name, ((SettingsItemObject)obj).SelectedValue);
                }
            }
        }

    }
}
