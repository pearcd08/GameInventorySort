using SFML.Graphics;
using SFML.System;
using System;

namespace Assignment
{
    class Chest
    {
        private RectangleShape chest;
        private Random rand = new Random();
        public RectangleShape chest2 { get { return chest; } } //gives a get return for the collisionPlayerChest function in the gameManager class
        public Vector2f position { get { return chestPos; } }
        Vector2f chestPos;
        Vector2f chestSize = new Vector2f(50, 30);
        Color[] chestColour = { Color.Red, Color.Yellow, Color.Green };

        public Chest() //constructer for the chest 
        {
            this.chest = new RectangleShape();
            this.chest.Size = chestSize;
            this.chest.FillColor = chestColour[rand.Next(0, chestColour.Length)];
            this.chestPos.X = getChestPosX();
            this.chestPos.Y = getChestPosY();
            this.chest.Position = this.chestPos;
        }

        public float getChestPosX()  // returns a random X position for the chest within the game map
        {
            float chestPosX = rand.Next(640 - 50);
            return chestPosX;
        }

        public float getChestPosY() //returns a random Y position for the chest within the game map
        {
            float chestPosY = rand.Next(480 - 30);
            return chestPosY;
        }

        public void update()  //updates the chest postion on the map, 
        {
            this.chest.Position = this.chestPos;
        }

        public void draw(RenderTarget window) //draws the chests on the map
        {
            window.Draw(this.chest);
        }
    }
}
