using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;
using TiledSharp;

namespace rpg
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TmxMap map;
        private TIlemapManager mapRenderer;
        private Player player;
        public List<Rectangle> collisionObjects;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 256;
            _graphics.PreferredBackBufferHeight = 256;
            _graphics.ApplyChanges();
            player = new Player();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new TmxMap("Content/map.tmx");
            SpriteSheet[] sheets = { Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Idle/playersheetIdle.sf", new JsonContentLoader()),
            Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Walk/playersheetWalk.sf", new JsonContentLoader())};
            player.load(sheets);
            var tileset = Content.Load<Texture2D>("Tiny Adventure Pack/" + map.Tilesets[0].Name.ToString());
            var tileWidth = map.Tilesets[0].TileWidth;
            var tileHeight = map.Tilesets[0].TileHeight;
            var tilesetTilesWide = tileset.Width / tileWidth;
             mapRenderer = new TIlemapManager(_spriteBatch, map, tileset, tilesetTilesWide, tileWidth, tileHeight);
            collisionObjects = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Collisions"].Objects)
            {
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var initialpos = player.pos;
            player.Update(gameTime);
            
            foreach(var rect in collisionObjects)
            {
                
                if(rect.Intersects(player.playerBounds))
                {
                    player.pos = initialpos;
                    player.isIdle = true;
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            mapRenderer.Draw();
            player.Draw(_spriteBatch);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
