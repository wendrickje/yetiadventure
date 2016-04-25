using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using YetiAdventure.Common;
using YetiAdventure.Components;
using YetiAdventure.Interfaces;
using YetiAdventure.Levels;

namespace YetiAdventure
{
    public class Level
    {

        public const float GravityAcceleration = 7f;
        public const float GroundDragFactor = 0.58f;
        public const float AirDragFactor = 0.3f;
        public const float MaxMoveSpeed = 1500.0f;
        public Level(string name = null)
        {
            Name = name;
            _manager = LevelManager.Create(name);
            _player = new Player();
        }

        LevelManager _manager;

        public LevelManager Manager
        {
            get { return _manager; }
        }


        private int _levelWidth;
        /// <summary>
        /// Width of level measured in tiles.
        /// </summary>
        public int LevelWidth
        {
            get { return _levelWidth; }
        }


        private int _levelHeight;
        /// <summary>
        /// Height of the level measured in tiles.
        /// </summary>
        public int LevelHeight
        {
            get { return _levelHeight; }
        }


        private Vector2 _startPosition;

        public Vector2 StartPosition
        {
            get { return _startPosition; }
        }

        private int _tileSize;

        public int TileSize
        {
            get { return _tileSize; }
        }

        Texture2D _tileset;

        public Texture2D TileSet
        {
            get { return _tileset; }
        }

        private Player _player;

        public Player Player
        {
            get { return _player; }
        }


        #region game functions
        /// <summary>
        /// 
        /// </summary>
        public virtual void Initalize()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="content"></param>
        public virtual void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {

            _tileset = content.Load<Texture2D>(Manager.TileSet.Resource);

            //open level file
            //var stream = TitleContainer.OpenStream(Manager.LevelLayout);

            var assembly = typeof(LevelManager).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(Manager.LevelLayout);

            _tileSize = Manager.TileSet.TileSize;
            var yindex = 0f;
            var xindex = 0f;
            using (var reader = new StreamReader(stream))
            {

                //read level file
                while (!reader.EndOfStream)
                {
                    xindex = 0f;
                    //parse level file for the objects to create
                    var line = reader.ReadLine();
                    foreach (var item in line)
                    {
                        //get the legend key
                        var legendkey = Manager.Legend.FirstOrDefault(key => key.Id[0] == item);
                        //if legend key is not defined for this character then move on
                        if (legendkey == null) { xindex++; continue; }

                        var tilename = legendkey.Value;
                        //get the tile
                        var tile = Manager.TileMaps.FirstOrDefault(map => String.Compare(map.Id.ToLower(), tilename.ToLower()) == 0);
                        //if the tile is not defined for this character then move on
                        if (tile == null) { xindex++; continue; }

                        var tileposition = new Vector2(xindex * TileSize, yindex * TileSize);
                        //save the start positon
                        if (String.Compare(tilename.ToLower(), "start") == 0)
                            _startPosition = tileposition;

                        //if the tile does not have a column or row within the tile set then move on
                        if (!tile.Row.HasValue || !tile.Column.HasValue) { xindex++; continue; }

                        var tilecontainer = new Rectangle(tile.Column.Value * TileSize, tile.Row.Value * TileSize, TileSize, TileSize);

                        //todo: for now add everything to the middle layer
                        MiddlegroundLayer.Children.Add(new Tile() { Texture = TileSet, Position = tileposition, Container = tilecontainer });
                        xindex++;
                    }
                    yindex++;
                }

            }
            _levelHeight = (int)yindex * TileSize;
            _levelWidth = (int)xindex * TileSize;

            MiddlegroundLayer.Children.Add(Player);

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void UnloadContent()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            //for now we can just assume everything that can possibly be affected by gravity is on the middle layer
            //for now we can just assume everything that can possibly collide is on the middle layer
            foreach (var c in MiddlegroundLayer.Children.OfType<CollidableObject>())
            {
                ApplyGravity(gameTime, c);
            }

            foreach (var layer in Layers)
            {

                foreach (var child in layer.Children)
                {
                    if (layer.LayerIndex == LayerIndex.Middleground)
                        HandleCollisionDetection(gameTime, child);
                    child.Update(gameTime);
                }
            
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var layer in Layers)
            {
                foreach (var child in layer.Children)
                {
                    child.Draw(gameTime, spriteBatch);
                }
            }
        }
        #endregion


        /// <summary>
        /// applies gravity
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="element"></param>
        void ApplyGravity(GameTime gameTime, CollidableObject element)
        {
            //try apply gravity 

            var velocity = element.Velocity;
            var previousPosition = element.Position;
            var isOnGround = element.IsOnGround;

            //if element hasnt jumped or hasnt walked over a hole then dont apply gravity
            if (isOnGround) return;

            //if element is in mid jump, dont apply gravity, wait until the jump has reached max height 
            //at that point is jumping will be false
            if (element.IsJumping) return;

            //do gravity
            velocity.Y = GravityAcceleration;


            // Apply velocity.
            element.Position += velocity;
            element.Position = new Vector2((float)Math.Round(element.Position.X), (float)Math.Round(element.Position.Y));

       

        }

        void HandleCollisionDetection(GameTime gameTime, IDrawableObject element)
        {
            var collidableObject = element as CollidableObject;
            if (collidableObject == null) return;

            // Get the elements's bounding rectangle and find neighboring tiles.
            var bounds = new Rectangle((int)collidableObject.Position.X,
                (int)collidableObject.Position.Y,
                collidableObject.Container.Width,
                collidableObject.Container.Height);


            // Reset flag to search for ground collision.
            collidableObject.IsOnGround = false;


            int leftTile = (int)Math.Floor((float)bounds.Left / TileSize);
            int rightTile = (int)Math.Ceiling(((float)bounds.Right / TileSize)) - 1;
            int topTile = (int)Math.Floor((float)bounds.Top / TileSize);
            int bottomTile = (int)Math.Ceiling(((float)bounds.Bottom / TileSize)) - 1;

            // For each potentially colliding tile,
            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {

                    var tilex = x * TileSize;
                    var tiley = y * TileSize;
                    var tile = GetCollision(tilex, tiley);
                    if (tile == null) continue;



                    // Determine collision depth (with direction) and magnitude.
                    var tileBounds = new Rectangle((int)tile.Position.X,
                        (int)tile.Position.Y,
                        tile.Container.Width,
                        tile.Container.Height);

                    Vector2 depth = Utilities.GetIntersectionDepth(bounds, tileBounds);
                    //if depth has a value then we are either beyond the bottom bounds or top bounds
                    if (depth != Vector2.Zero)
                    {
                        float absDepthX = Math.Abs(depth.X);
                        float absDepthY = Math.Abs(depth.Y);

                        //check if the depth is not a full tile
                        if (absDepthY < absDepthX)
                        {
                            // Resolve the collision
                            // If we crossed the top of a tile, we are on the ground.
                            if (bounds.Bottom >= tileBounds.Top)
                            {
                                //try set is on ground
                                collidableObject.IsOnGround = true;
                            }

                        }
                        //clamp the y axis if on ground
                        if (collidableObject.IsOnGround)
                        {
                            // Resolve the collision along the Y axis.
                            collidableObject.Position = new Vector2(collidableObject.Position.X, collidableObject.Position.Y + depth.Y);
                            bounds = new Rectangle((int)collidableObject.Position.X,
                                (int)collidableObject.Position.Y,
                                collidableObject.Container.Width,
                                collidableObject.Container.Height);

                        }

                        if (absDepthX < absDepthY)
                        {
                            //if the tile is in the way
                            //within the height range of the bounds
                            collidableObject.Position = new Vector2(collidableObject.Position.X + depth.X, collidableObject.Position.Y);
                            bounds = new Rectangle((int)collidableObject.Position.X,
                                (int)collidableObject.Position.Y,
                                collidableObject.Container.Width,
                                collidableObject.Container.Height);
                            
                        }
                    }
                }
            }


        }

        /// <summary>
        /// checks for collision the tile at a particular location.
        /// This method handles tiles outside of the levels boundries by making it
        /// impossible to escape past the left or right edges, but allowing things
        /// to jump beyond the top of the level and fall off the bottom.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Colliding tile if there is a collision; otherwise, null.</returns>
        public Tile GetCollision(int x, int y)
        {

            // Prevent escaping past the level ends.
            if (x < 0 || x >= LevelWidth)
                return null;
            // Allow jumping past the level top and falling through the bottom.
            if (y < 0 || y >= LevelHeight)
                return null;

            //find tile that is at position x,y
            var tile = MiddlegroundLayer.Children.OfType<Tile>().FirstOrDefault(t => t.Position.X == x && t.Position.Y == y);
            return tile;
        }




        /// <summary>
        /// name of the level
        /// </summary>
        public string Name { get; private set; }

        Layer[] _layers;
        /// <summary>
        /// layers in the level
        /// </summary>
        public Layer[] Layers
        {
            get
            {
                return _layers ?? (_layers = new Layer[4] 
                    { 
                        ForegroundLayer, 
                        MiddlegroundLayer, 
                        BackgroundLayer, 
                        BackdropLayer 
                    });
            }
        }


        Layer _foregroundLayer;
        /// <summary>
        /// 
        /// </summary>
        public Layer ForegroundLayer { get { return _foregroundLayer ?? (_foregroundLayer = new Layer(LayerIndex.Foreground)); } }

        Layer _middleLayer;
        /// <summary>
        /// 
        /// </summary>
        public Layer MiddlegroundLayer { get { return _middleLayer ?? (_middleLayer = new Layer(LayerIndex.Middleground)); } }

        Layer _backgroundLayer;
        /// <summary>
        /// 
        /// </summary>
        public Layer BackgroundLayer { get { return _backgroundLayer ?? (_backgroundLayer = new Layer(LayerIndex.Background)); } }

        Layer _backdropLayer;
        /// <summary>
        /// 
        /// </summary>
        public Layer BackdropLayer { get { return _backdropLayer ?? (_backdropLayer = new Layer(LayerIndex.Background)); } }



    }
}
