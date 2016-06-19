using System;
using FarseerPhysics.Common;

namespace YetiAdventure.Engine
{
    /// <summary>
    /// The baseclass of anything in a level.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// The current transformation of the entity.
        /// @TODO: Add a scaling coefficient for rendering.
        /// </summary>
        private Transform mTransform;

        public Transform Transform
        {
            get { return mTransform; }
            set
            {
                Transform original = mTransform;
                mTransform = value;
                OnTransformUpdated(original, mTransform);
            }
        }

        public Entity()
        {

        }

        protected virtual void OnTransformUpdated(Transform inOriginalTransform, Transform inNewTransform)
        {

        }
    }
}
