using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace YetiAdventure.Engine
{
    /// <summary>
    /// Given to the TextureManager with a valid Asset String, this will be populated with a reference to a texture and a valid ID.
    /// </summary>
    public class EngineTexture
    {
        private Texture2D mTextureHandle;
        private string mAssetString;

        public EngineTexture(string inAssetString, Texture2D inTextureHandle)
        {
            mAssetString = inAssetString;
            mTextureHandle = inTextureHandle;
        }

        public Texture2D Texture
        {
            get { return mTextureHandle; }
        }

        public string AssetString
        {
            get { return mAssetString; }
        }
    }
}