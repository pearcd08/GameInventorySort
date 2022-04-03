using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Assignment
{
    class SFML
    {
        private RenderWindow window;
        private VideoMode mode = new VideoMode(840, 480); //set the window size
        Hero hero; //connect to the hero class
        GameManager game; // connect to the game manager class

        public SFML()
        {
            this.window = new RenderWindow(this.mode, "Game Inventory System"); //title of the window
            this.window.SetVerticalSyncEnabled(true);
            this.window.Closed += (sender, args) =>
            {
                this.window.Close();
            };

            hero = new Hero();
            game = new GameManager();
        }

        public void Run()
        {
            // add the items into the game memory
            game.addItem();

            // mouse events
            this.window.MouseButtonPressed += (sender, e) =>  //if mouse button is pressed
            {
                Vector2f mousePos = this.window.MapPixelToCoords(new Vector2i(e.X, e.Y)); //sets the postion of the mouse cursor to a Vector2f
                if (game.sortButton.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))   // if the sort Button intersects with mouse cursor
                {
                    game.bubbleSortByName(game.inventoryList); //sort the names
                }
                else if (game.sortButton2.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))  // if mouse position intersects with button 2
                {
                    game.bubbleSortByPrice(game.inventoryList); // sort the price
                }
            };

            //inventory events
            bool keyI = Keyboard.IsKeyPressed(Keyboard.Key.I); //key shortcuts for sorting, was implemented before mouse click button
            bool keyO = Keyboard.IsKeyPressed(Keyboard.Key.O);

            bool keyPressed = keyI || keyO;

            if (keyPressed)
            {
                if (keyI)
                {
                    game.bubbleSortByName(game.inventoryList);
                }
                else if (keyO)
                {

                    game.bubbleSortByPrice(game.inventoryList);
                }
            }


            while (this.window.IsOpen)
            {
                this.Update();
                this.Draw();
                this.HandleEvents();
            }
        }

        public void HandleEvents()
        {
            this.window.DispatchEvents();
        }

        public void Update()
        {
            this.game.addChest();
            this.game.update(hero);
            this.hero.update();
        }

        public void Draw()
        {
            this.window.Clear(Color.Black);
            this.game.draw(this.window);
            this.hero.draw(this.window);
            this.window.Display();
        }
    }
}




