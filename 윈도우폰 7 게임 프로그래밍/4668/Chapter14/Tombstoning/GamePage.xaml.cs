﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using SLGameFramework;

namespace Tombstoning
{
    public partial class GamePage : PhoneApplicationPage
    {

        //-------------------------------------------------------------------------------------
        // Class constructor

        public GamePage()
        {
            InitializeComponent();
        }


        //-------------------------------------------------------------------------------------
        // Static properties

        private static GameState _gameState;
        /// <summary>
        /// Returns a reference to the game's GameState object
        /// </summary>
        internal static GameState GameState
        {
            get
            {
                if (_gameState == null) _gameState = new GameState();
                return _gameState;
            }
            set
            {
                _gameState = value;
            }
        }


        //-------------------------------------------------------------------------------------
        // Navigation

        /// <summary>
        /// Process and display the GameState as appropriate, based on whether we are
        /// starting a new game or resuming an existing game.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // See if we can find a resume mode specified in the querystring
            string resumeMode;
            NavigationContext.QueryString.TryGetValue("GameState", out resumeMode);
            if (resumeMode == "Resume")
            {
                gameStateText.Text = "Game state: resuming an existing game.";
            }
            else
            {
                gameStateText.Text = "Game state: starting a new game.";
            }
            
            // Display the player's score
            scoreText.Text = "Score: " + GameState.Score.ToString();
        }


        //-------------------------------------------------------------------------------------
        // Game functions

        private void endGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Indicate that the game has finished
            GameState.IsGameActive = false;

            // Was the score good enough for the high score table?
            if (HighScorePage.HighScores.GetTable("Default").ScoreQualifies(GameState.Score))
            {
                // Yes, so go to the "enter your name" page
                MainPage.TargetGamePage = MainPage.GamePages.HighScoreNamePage;
            }
            else
            {
                // No, so go to the high score page
                MainPage.TargetGamePage = MainPage.GamePages.HighScorePage;
            }
            // Either way, navigate back to the menu so that it can redirect for us
            NavigationService.GoBack();
        }
    }
}