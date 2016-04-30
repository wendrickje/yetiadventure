using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YetiAdventure.Common;

namespace YetiAdventure.Components
{
    /// <summary>
    /// A camera that can pan, zoom, and rotate.
    /// The camera is constrained to fit inside the bounds defined by the map.
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// The Game's GraphicsDevice. Necessary for checking viewport dimensions.
        /// </summary>
        private GraphicsDevice mGraphicsDevice;

        /// <summary>
        /// The camera angle around the Z-axis. Specified in radians.
        /// </summary>
        private float mRotationAngle;

        /// <summary>
        /// The amount of zoom for the camera. Normal camera view = 1.0f.
        /// </summary>
        private float mZoomCoefficient;

        /// <summary>
        /// A bounding rectangle that the camera has to fit within.
        /// </summary>
        private Rectangle? mBoundingRectangle;
        
        /// <summary>
        /// The worldspace position that the camera will attempt to target.
        /// 
        /// </summary>
        private Vector2 mTargetPosition;

        /// <summary>
        /// The point that the center of the camera is currently looking at.
        /// There may be a target position set, but it's only the position that the camera will attempt to look at.
        /// The actual position will always be set to a corrected coordinate where the camera fits in the level bounds.
        /// </summary>
        private Vector2 mActualPosition;

        /// <summary>
        /// The object that's being targeted by the camera.
        /// </summary>
        private CollidableObject mTarget;

        /// <summary>
        /// Constructor to initialize a new instance of the Camera class.
        /// </summary>
        /// <param name="inGraphicsDevice"></param>
        public Camera(GraphicsDevice inGraphicsDevice)
        {
            mGraphicsDevice = inGraphicsDevice;
            mRotationAngle = 0.0f;
            mZoomCoefficient = 1.0f;
            float halfScreenWidth = mGraphicsDevice.Viewport.Width / 2.0f;
            float halfScreenHeight = mGraphicsDevice.Viewport.Height / 2.0f;
            mTargetPosition = new Vector2(halfScreenWidth, halfScreenHeight);
            mBoundingRectangle = null;
        }

        #region Properties
        /// <summary>
        /// Get or set the rotation angle for the camera.
        /// </summary>
        public float RotationAngle
        {
            get { return mRotationAngle; }
            set { mRotationAngle = value; }
        }

        /// <summary>
        /// Get or set the zoom coefficient for the camera.
        /// </summary>
        public float Zoom
        {
            get { return mZoomCoefficient; }
            set { mZoomCoefficient = value; }
        }

        /// <summary>
        /// This is the position that the camera will attempt to target.
        /// </summary>
        public Vector2 TargetPosition
        {
            get { return mTargetPosition; }
            set { mTargetPosition = value; }
        }

        /// <summary>
        /// The point that the camera is focusing on.
        /// If null, the camera will be static, or moved manually.
        /// </summary>
        public CollidableObject Target
        {
            get { return mTarget; }
            set { mTarget = value; }
        }

        public Rectangle? BoundingRectangle
        {
            get { return mBoundingRectangle; }
            set
            {
                // @TODO: Figure out if the rectangle bounds are valid. (This rectangle needs to be larger than the viewport dimensions to work correctly.)
                mBoundingRectangle = value;
            }
        }
#endregion

        /// <summary>
        /// This function computes a world space position from a given screen space position.
        /// Input: A coordinate within the range [0 to WindowWidth, 0 to WindowHeight]
        /// </summary>
        /// <param name="inScreen">A coordinate in screenspace.</param>
        /// <returns>The position, in worldspace, of the input screenspace coordinate.</returns>
        public Vector2 ConvertScreenToWorld(Vector2 inScreen)
        {
            Matrix inverseTransform = Matrix.Invert(Transform);
            return Vector2.Transform(inScreen, inverseTransform);
        }

        /// <summary>
        /// Compute a screen space coordinate for a given coordinate in world space.
        /// Input: Any coordinate within the world.
        /// Output: The equivalent coordinate in screenspace. If the worldspace coordinate is onscreen, the output will fall
        /// within the range [0 to WindowWidth, 0 to WindowHeight]. Otherwise, the screenspace coord will be offscreen.
        /// </summary>
        /// <param name="inWorldPosition">A position in worldspace to convert.</param>
        /// <returns>A screenspace position.</returns>
        public Vector2 ConvertWorldToScreen(Vector2 inWorldPosition)
        {
            return Vector2.Transform(inWorldPosition, Transform);
        }

        /// <summary>
        /// Update the camera. If there's a target object, the camera will calculate a new position.
        /// </summary>
        /// <param name="inGametime">The elapsed time since last update.</param>
        public void Update(GameTime inGametime)
        {
            // @ZOOMTEST
            //Zoom = (float)(Math.Sin(inGametime.TotalGameTime.TotalSeconds) + 2.0f);
            // @ROTATIONTEST
            //RotationAngle += (float)inGametime.ElapsedGameTime.Milliseconds / 1000.0f;
            // Figure out if the camera should be tracking an object. If so, attempt to follow it.
            if (mTarget != null)
            {
                mTargetPosition = mTarget.Position;
            }

            // Start with the target position, and then determine if it's valid.
            mActualPosition = mTargetPosition;
            Rectangle viewportRectangle = mGraphicsDevice.Viewport.Bounds;
            Vector2 widthAndHeightScaled = new Vector2(viewportRectangle.Width, viewportRectangle.Height) * (1 / mZoomCoefficient);
            Vector2 halfScaledDimension = widthAndHeightScaled * 0.5f;
            float left = mTargetPosition.X - (widthAndHeightScaled.X / 2.0f);
            float top = mTargetPosition.Y - (widthAndHeightScaled.Y / 2.0f);

            if (BoundingRectangle != null)
            {
                Rectangle bounds = BoundingRectangle.Value;
                if (left < bounds.X)
                {
                    // Clamp sliding to the left
                    mActualPosition.X = halfScaledDimension.X;
                }
                if ((left + widthAndHeightScaled.X) > bounds.Width)
                {
                    // Clamp sliding to right.
                    mActualPosition.X = bounds.Width - halfScaledDimension.X;
                }

                if (top < bounds.Y)
                {
                    // Clamp to top.
                    mActualPosition.Y = halfScaledDimension.Y;
                }
                if ((top + widthAndHeightScaled.Y) > bounds.Height)
                {
                    // Clamp to bottom.
                    mActualPosition.Y = bounds.Height - halfScaledDimension.Y;
                }
            }

            // Make sure that the camera is within the bounds of the Level. We don't want to see anything outside of the level bounds.
        }

        public string printMatrix()
        {
            return string.Format("\n{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n",
            Transform.M11, Transform.M12, Transform.M13, Transform.M14,
            Transform.M21, Transform.M22, Transform.M23, Transform.M24,
            Transform.M31, Transform.M32, Transform.M33, Transform.M34,
            Transform.M41, Transform.M42, Transform.M43, Transform.M44);
        }

        /// <summary>
        /// The transform is computed based on the translation, scale, and rotation.
        /// @PERFORMANCE: Have some kind of "IsDirty" flag to only recompute the transform when it has changed.
        /// </summary>
        public Matrix Transform
        {
            get
            {
                float halfScreenWidth = mGraphicsDevice.Viewport.Width / 2.0f;
                float halfScreenHeight = mGraphicsDevice.Viewport.Height / 2.0f;
                Vector3 translation = new Vector3(-mActualPosition, 0.0f);
                return
                    Matrix.CreateTranslation(translation) *
                    Matrix.CreateScale(mZoomCoefficient, mZoomCoefficient, 1.0f) *
                    Matrix.CreateFromAxisAngle(Vector3.UnitZ, mRotationAngle) *
                    Matrix.CreateTranslation(new Vector3(halfScreenWidth, halfScreenHeight, 0));
            }
        }
    }
}
