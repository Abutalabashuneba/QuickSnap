using System;
using SwinGameSDK;
using CardGames.GameLogic;

namespace CardGames
{
    public class SnapGame
    {
        public static void LoadResources()
        {
            SwinGame.PlaySoundEffect("SwinGameStart.wav");
            Bitmap cards;
            cards = SwinGame.LoadBitmapNamed ("Cards", "Cards.png");
            SwinGame.BitmapSetCellDetails (cards, 82, 110, 13, 5, 53);      // set the cells in the bitmap to match the cards
        }

		/// <summary>
		/// Respond to the user input -- with requests affecting myGame
		/// </summary>
		/// <param name="myGame">The game object to update in response to events.</param>
		private static void HandleUserInput(Snap myGame)
		{
			//Fetch the next batch of UI interaction
			SwinGame.ProcessEvents();

			if (SwinGame.KeyTyped (KeyCode.vk_SPACE))
			{
				myGame.Start ();
			}

			if (myGame.IsStarted) {
				if (SwinGame.KeyTyped (KeyCode.vk_LSHIFT) && SwinGame.KeyTyped (KeyCode.vk_RSHIFT)) {
                    //TODO: add sound effects
                    SwinGame.PlaySoundEffect("snap.wav");
                } else if (SwinGame.KeyTyped (KeyCode.vk_LSHIFT)) {
					myGame.PlayerHit (0);
                    SwinGame.PlaySoundEffect("snap0.wav");
				} else if (SwinGame.KeyTyped (KeyCode.vk_RSHIFT)) {
					myGame.PlayerHit (1);
                    SwinGame.PlaySoundEffect("snap1.wav");
                }
			}

		}

		/// <summary>
		/// Draws the game to the Window.
		/// </summary>
		/// <param name="myGame">The details of the game -- mostly top card and scores.</param>
		private static void DrawGame(Snap myGame)
		{
			SwinGame.DrawBitmap ("cardsBoard.png",0,0);

			SwinGame.LoadFontNamed ("GameFont", "Chunk.otf", 24);
			SwinGame.LoadFontNamed ("TopFont","Chunk.otf", 32);
			SwinGame.LoadFontNamed ("StartFont", "Chunk.otf", 24);

			// Draw the top card
			Card top = myGame.TopCard;
			if (top != null)
			{
				SwinGame.DrawText ("Top Card is " + top.ToString (), Color.Red,"TopFont", 310, 105);
				SwinGame.DrawText ("Player 1 score: " + myGame.Score(0), Color.White,"GameFont", 45, 40);
				SwinGame.DrawText ("Player 2 score: " + myGame.Score(1), Color.White,"GameFont", 625, 40);
				SwinGame.DrawCell (SwinGame.BitmapNamed ("Cards"), top.CardIndex, 565 , 225);
			}
			else
			{
				SwinGame.DrawText ("No card played yet...", Color.Red,"StartFont", 10, 10);
			}


			// Draw the back of the cards... to represent the deck
			SwinGame.DrawCell (SwinGame.BitmapNamed ("Cards"), 52, 200, 230);

			//Draw onto the screen
			SwinGame.RefreshScreen(60);
		}

		/// <summary>
		/// Updates the game -- it should flip the cards itself once started!
		/// </summary>
		/// <param name="myGame">The game to be updated...</param>
		private static void UpdateGame(Snap myGame)
		{
			myGame.Update(); // just ask the game to do this...
		}

        public static void Main()
        {
            //Open the game window
            SwinGame.OpenGraphicsWindow("Snap!", 860, 500);

			//Load the card images and set their cell details
            LoadResources();
            
			// Create the game!
			Snap myGame = new Snap ();

            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
				HandleUserInput (myGame);
				DrawGame (myGame);
				UpdateGame (myGame);
            }
        }
    }
}