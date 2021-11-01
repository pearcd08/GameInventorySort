using SFML.Graphics;
using SFML.System;
using System;

namespace Assignment
{
    class Chest
    {
        private RectangleShape chest;
        private Random rand = new Random();
        public RectangleShape chest2 { get { return chest; } }
        public Vector2f position { get { return chestPos; } }
        Vector2f chestPos;
        Vector2f chestSize = new Vector2f(50, 30);
        Color[] chestColour = { Color.Red, Color.Yellow, Color.Green };

        public Chest()
        {
            this.chest = new RectangleShape();
            this.chest.Size = chestSize;
            this.chest.FillColor = chestColour[rand.Next(0, chestColour.Length)];
            this.chestPos.X = getChestPosX();
            this.chestPos.Y = getChestPosY();
            this.chest.Position = this.chestPos;
        }

        public float getChestPosX()
        {
            float chestPosX = rand.Next(640 - 50);
            return chestPosX;
        }

        public float getChestPosY()
        {
            float chestPosY = rand.Next(480 - 30);
            return chestPosY;
        }

        public void update()
        {
            this.chest.Position = this.chestPos;
        }

        public void draw(RenderTarget window)
        {
            window.Draw(this.chest);
        }
    }
}
