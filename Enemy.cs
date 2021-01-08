using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpg
{
    public class Enemy
    {
        public Vector2 pos;
        private AnimatedSprite[] enemySprite;
        private SpriteSheet[] sheet;
        private float movespeed = 1.5f;
        public Rectangle enemyBounds;
        public bool isIdle = false;
        public Enemy()
        {
            enemySprite = new AnimatedSprite[10];
            sheet = new SpriteSheet[10];
            pos = new Vector2(100, 50);
            enemyBounds = new Rectangle((int)pos.X - 8, (int)pos.Y - 8, 16, 17);
        }
        public void load(SpriteSheet[] spritesheet)

        {
            for (int i = 0; i < spritesheet.Length; i++)
            {
                sheet[i] = spritesheet[i];
                //for(int i=0;i<spritesheet.Length;i++)
                enemySprite[i] = new AnimatedSprite(sheet[i]);
            }
        }
        public void Update(GameTime gameTime)
        {
            isIdle = true;

            enemySprite[0].Play("idleDown");

            string animation = " ";
            var keyboardstate = Keyboard.GetState();
            var initpos = pos;
            if (keyboardstate.IsKeyDown(Keys.D))
            {
                animation = "walkRight";
                pos.X += movespeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.A))
            {
                animation = "walkLeft";
                pos.X -= movespeed;
                isIdle = false;

            }
            if (keyboardstate.IsKeyDown(Keys.W))
            {
                animation = "walkUp";
                pos.Y -= movespeed;
                isIdle = false;

            }
            if (keyboardstate.IsKeyDown(Keys.S))
            {
                animation = "walkDown";
                pos.Y += movespeed;
                isIdle = false;

            }
            if (!isIdle)
            {
                enemySprite[1].Play(animation);
                enemySprite[1].Update(gameTime);
            }

            enemyBounds.X = (int)pos.X - 8;
            enemyBounds.Y = (int)pos.Y - 8;
            enemySprite[0].Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (isIdle)
                spriteBatch.Draw(enemySprite[0], pos);
            else
                spriteBatch.Draw(enemySprite[1], pos);
            spriteBatch.End();
        }
    }
}
