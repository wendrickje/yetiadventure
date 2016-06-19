using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sentius.darkFunction.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Engine.Components
{

    public class AnimatedSprite
    {
        /// <summary>
        /// The base path to the sprite sheet.
        /// </summary>
        private string _basePath;

        /// <summary>
        /// The name of the sheet loaded in this instance.
        /// </summary>
        private string _sheetName;

        /// <summary>
        /// The current frame index in the currently-playing animation.
        /// </summary>
        private int _frameIndex;

        /// <summary>
        /// The spritesheet content loaded for this instance.
        /// </summary>
        private SpriteSheet _spritesheet = null;

        /// <summary>
        /// The animation set loaded for this spritesheet.
        /// </summary>
        private SpriteAnimationDefinitionSet _animationSet = null;

        /// <summary>
        /// The currently-playing animation.
        /// </summary>
        private SpriteAnimationDefinition _currentAnimation = null;

        private double _updateInverval = 41.6666;

        private double _lastUpdateTime;

        public AnimatedSprite(string inBasePath, string inSheet)
        {
            _frameIndex = 0;
            _basePath = inBasePath;
            _sheetName = inSheet;
            _lastUpdateTime = _updateInverval;
        }

        public void LoadContent(ContentManager inContentManager)
        {
            string sheetName = string.Format("{0}/{1}/{1}Sheet", _basePath, _sheetName);
            _spritesheet = inContentManager.Load<SpriteSheet>(sheetName);

            string animName = string.Format("{0}/{1}/{1}Animations", _basePath, _sheetName);
            _animationSet = inContentManager.Load<SpriteAnimationDefinitionSet>(animName);

            _currentAnimation = _animationSet["Walk"];
        }

        public void Update(GameTime inTime)
        {
            // @TODO:
            // Must track always-incrementing animation playback index
            // Must know what your current animation sequence is
            // Update() should increment to next frame based on elapsed time
            // Draw() should draw the current frame in the current animation
        }

        public void Draw(GameTime inTime, SpriteBatch inSpritebatch)
        {
            Texture2D sheetTexture = _currentAnimation.Frames[_frameIndex].SpriteReferences[0].Sprite.SourceTexture;
            inSpritebatch.Draw(sheetTexture, Vector2.Zero, _currentAnimation.Frames[_frameIndex].SpriteReferences[0].Sprite.SourceRectangle, Color.White, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1.0f);
        }
    }
}
