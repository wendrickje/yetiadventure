using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace YetiAdventure.Engine
{
    public class TextureManager
    {
        protected TextureManager()
        {
            mLoadedTextures = new List<EngineTexture>();
        }

        /// <summary>
        /// Provide the global ContentManager to the TextureManager once during initialization.
        /// </summary>
        /// <param name="inContentManager">The global content manager for the game.</param>
        public static void Initialize(ContentManager inContentManager)
        {
            TextureManager manager = Instance();
            manager.mContentManager = inContentManager;
        }

        public static TextureManager Instance()
        {
            if (sTextureManagerInstance == null)
            {
                sTextureManagerInstance = new TextureManager();
            }
            return sTextureManagerInstance;
        }

        public EngineTexture LoadTexture(string inAssetString)
        {
            // Attempt to reference a texture if it has already been loaded
            for (int textureIndex = 0; textureIndex < mLoadedTextures.Count; textureIndex++)
            {
                if (mLoadedTextures[textureIndex].AssetString.Equals(inAssetString))
                {
                    return mLoadedTextures[textureIndex];
                }
            }

            // ...Otherwise load it from the ContentManager and add the loaded texture to the list.
            Texture2D textureReference = mContentManager.Load<Texture2D>(inAssetString);
            EngineTexture resultTexture = new EngineTexture(inAssetString, textureReference);
            mLoadedTextures.Add(resultTexture);
            return resultTexture;
        }
        public ContentManager ContentManager { get { return mContentManager; } }

        // Static member instance
        protected static TextureManager sTextureManagerInstance;

        protected List<EngineTexture> mLoadedTextures;
        protected ContentManager mContentManager;
    }
}
