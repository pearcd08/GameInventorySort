using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Assignment
{
    class SFML
    {
        private RenderWindow window;
        private VideoMode mode = new VideoMode(840, 480);
        Hero hero;
        GameManager game;

        public SFML()
        {
            this.window = new RenderWindow(this.mode, "Game Inventory System");
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
            this.window.MouseButtonPressed += (sender, e) =>
            {
                Vector2f mousePos = this.window.MapPixelToCoords(new Vector2i(e.X, e.Y));
                if (game.sortButton.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
                {
                    game.bubbleSortByName(game.inventoryList);
                }
                else if (game.sortButton2.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
                {
                    game.bubbleSortByPrice(game.inventoryList);
                }
            };

            //inventory events
            bool keyI = Keyboard.IsKeyPressed(Keyboard.Key.I);
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




