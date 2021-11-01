using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Assignment
{
    class Hero
    {
        public int circleRadius = 20;
        public float heroSpeed = 4f;
        private CircleShape hero;
        public CircleShape hero2 { get { return hero; } }
        Vector2f position = new Vector2f(300, 300);
        public Color heroColor = Color.White;
        public Color heroOutline = Color.White;

        Text heroPos = new Text();


        public Hero()
        {
            hero = new CircleShape(circleRadius);
            hero.FillColor = heroColor;
            hero.OutlineColor = heroOutline;
            hero.Position = position;
        }

        //public void heroLocation() 
        //{
        //    String pos = position.ToString();
        //    heroPos.DisplayedString = pos;
        //    Vector2f txtPos = new Vector2f(50, 50);
        //    hero.Position = txtPos;
        //    heroPos.FillColor = Color.White;
        //    heroPos.CharacterSize = 5;


        //}

        
  
        public void heroMovement()
        {
            // bool collides  hero.getGlobalBounds().intersect(ChestManager getGlobalBounds());`
            bool keyLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool keyRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            bool keyUp = Keyboard.IsKeyPressed(Keyboard.Key.W);
            bool keyDown = Keyboard.IsKeyPressed(Keyboard.Key.S);

            bool keyPressed = keyLeft || keyRight || keyUp || keyDown;

            if (keyPressed)
            {
                if (keyLeft && position.X > 0)
                {
                    position.X -= heroSpeed;
                }
                else if (keyRight && position.X < (640 - circleRadius * 2))
                {
                    position.X += heroSpeed;
                }
                else if (keyUp && position.Y > 0)
                {
                    position.Y -= heroSpeed;
                }
                else if (keyDown && position.Y < (480 - circleRadius * 2))
                {
                    position.Y += heroSpeed;
                }
                
            }
        }

        public void update()
        {
            this.heroMovement();
            this.hero.Position = position;
            this.hero.OutlineColor = heroOutline;
            this.hero.FillColor = heroColor;
           

        }

        public void draw(RenderTarget window)
        {
            window.Draw(this.hero);
            
            
        }
    } 
}
