using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpg
{
    public class Player
    {
        public Vector2 pos;
        private AnimatedSprite[] playerSprite;
        private SpriteSheet[] sheet;
        private float movespeed = 1.5f;
        public Rectangle playerBounds;
        public bool isIdle = false;
        public Player()
        {
            playerSprite = new AnimatedSprite[10];
            sheet = new SpriteSheet[10];
            pos = new Vector2(100, 50);
            playerBounds = new Rectangle((int)pos.X-8,(int)pos.Y-8,16,17);
        }
        public void load(SpriteSheet[] spritesheet)

        {
            for (int i = 0; i < spritesheet.Length; i++)
            {
                sheet[i] = spritesheet[i];
                //for(int i=0;i<spritesheet.Length;i++)
                playerSprite[i] = new AnimatedSprite(sheet[i]);
            }
        }
        public void Update( GameTime gameTime)
        {
            isIdle = true;
            
            playerSprite[0].Play("idleDown");
            
            string animation=" ";
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
                playerSprite[1].Play(animation);
                playerSprite[1].Update(gameTime);
            }
            
            playerBounds.X=(int)pos.X-8;
            playerBounds.Y = (int)pos.Y-8;
            playerSprite[0].Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch,Matrix matrix)
        {
            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                samplerState: SamplerState.PointClamp,
                effect: null,
                blendState: null,
                rasterizerState: null,
                depthStencilState: null,
                transformMatrix: matrix);
            if (isIdle)
            spriteBatch.Draw(playerSprite[0], pos);
            else
                spriteBatch.Draw(playerSprite[1], pos);
            spriteBatch.End();
        }
    }
}
