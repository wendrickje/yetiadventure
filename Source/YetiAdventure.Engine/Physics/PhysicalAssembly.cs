using System;
using System.Collections.Generic;
using System.Xml.Linq;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YetiAdventure.Engine.Physics
{
    public class PhysicalAssembly
    {
        /*
        private List<PhysicalObject> mObjects = new List<PhysicalObject>();    // Joints are attached to bodies.
        private List<Joint> mJoints = new List<Joint>();                        // Bodies are attached to joints.

        public PhysicalAssembly() {
            mObjects = new List<PhysicalObject>();
            mJoints = new List<Joint>();
        }
        public PhysicalAssembly(XElement inElement)
        {
            // Make sure people aren't digging too deep here
            if (inElement.Name.Equals("Bodies")) inElement = inElement.Parent;
            Load(inElement);
        }

        public List<PhysicalObject> Objects {
            get { return mObjects; }
        }

        public List<Joint> Joints {
            get { return mJoints; }
        }

        // This function calls the other save function LOLWUT
        
        public void Save(string filename) {
            XDocument doc = new XDocument();
            doc.Add(new XElement("Assembly"));
            XElement root = doc.Element("Assembly");
            Save(root);
            doc.Save(filename);
        }
        public void Save(XElement inElement) {

        }
        public void Save(XElement inElement) {
            XElement bodies = new XElement("Bodies");
            foreach (PhysicalObject obj in mObjects) {
                obj.Save(bodies);
            }
            XElement joints = new XElement("Joints");
            foreach (Joint joint in PhysicsEngine.GetSingleton().PhysicsWorld.JointList)
            {
                // Save the joints correctly
                XElement jointElem = new XElement("Joint");
                // Find the array index of the joint
                int aIndex = 0, bIndex = 0;
                for (int i = 0; i < mObjects.Count; i++) {
                    if (joint.BodyA == mObjects[i].PhysicsBody) {
                        aIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < mObjects.Count; i++) {
                    if (joint.BodyB == mObjects[i].PhysicsBody) {
                        bIndex = i;
                        break;
                    }
                }
                jointElem.Add(new XElement("BodyA", aIndex));
                jointElem.Add(new XElement("BodyB", bIndex));
                // We do the anchors and such based on the joint type
                XElement type = new XElement("Type", (int)joint.JointType);
                jointElem.Add(type);
                switch (joint.JointType) {
                    case JointType.Revolute:
                    {
                        RevoluteJoint rj = (RevoluteJoint)joint;

                        XElement anchorAElem = new XElement("AnchorA");
                        anchorAElem.Add(new XElement("x", rj.LocalAnchorA.X),
                            new XElement("y", rj.LocalAnchorA.Y));
                        XElement anchorBElem = new XElement("AnchorB");
                        anchorBElem.Add(new XElement("x", rj.LocalAnchorB.X),
                            new XElement("y", rj.LocalAnchorB.Y));

                        jointElem.Add(anchorAElem);
                        jointElem.Add(anchorBElem);
                        break;
                    }
                    case JointType.Weld: 
                    {
                        WeldJoint rj = (WeldJoint)joint;

                        XElement anchorAElem = new XElement("AnchorA");
                        anchorAElem.Add(new XElement("x", rj.LocalAnchorA.X),
                            new XElement("y", rj.LocalAnchorA.Y));
                        XElement anchorBElem = new XElement("AnchorB");
                        anchorBElem.Add(new XElement("x", rj.LocalAnchorB.X),
                            new XElement("y", rj.LocalAnchorB.Y));

                        jointElem.Add(anchorAElem);
                        jointElem.Add(anchorBElem);
                        break;
                    }
                }
                joints.Add(jointElem);
            }
            inElement.Add(bodies, joints);
        }
        public void Load(XElement inElement)
        {
            foreach(XElement e in inElement.Element("Bodies").Elements("Body"))
            {
                PhysicalObject p = new PhysicalObject(e);
                mObjects.Add(p);
            }
            PhysicsEngine engine = PhysicsEngine.GetSingleton();
            // Figure out the index of the bodies where the joints are pointing
            foreach (XElement e in inElement.Element("Joints").Elements("Joint"))
            {
                int aIndex = Convert.ToInt32(e.Element("BodyA").Value),
                    bIndex = Convert.ToInt32(e.Element("BodyB").Value);
                Body a = mObjects[aIndex].PhysicsBody,
                    b = mObjects[bIndex].PhysicsBody;
                // What kind of joint? JointType was converted to an int in order to save
                JointType type = (JointType)Convert.ToInt32(e.Element("Type").Value);
                switch (type) {
                    case JointType.Revolute: 
                    {
                        // Get the anchor points
                        //XElement anchorElem = e.Element("AnchorA");
                        //Vector2 anchorA = new Vector2((float)Convert.ToDouble(anchorElem.Element("x").Value),
                        //    (float)Convert.ToDouble(anchorElem.Element("y").Value));
                        XElement anchorElem = e.Element("AnchorB");
                        Vector2 anchorB = new Vector2((float)Convert.ToDouble(anchorElem.Element("x").Value),
                            (float)Convert.ToDouble(anchorElem.Element("y").Value));
                        // Create the joint
                        RevoluteJoint joint = JointFactory.CreateRevoluteJoint(PhysicsEngine.GetSingleton().PhysicsWorld, a, b, anchorB);
                        break;
                    }
                    case JointType.Weld: 
                    {
                        // Get the anchor points
                        XElement anchorElem = e.Element("AnchorA");
                        Vector2 anchorA = new Vector2((float)Convert.ToDouble(anchorElem.Element("x").Value),
                            (float)Convert.ToDouble(anchorElem.Element("y").Value));
                        XElement anchorElemB = e.Element("AnchorB");
                        Vector2 anchorB = new Vector2((float)Convert.ToDouble(anchorElemB.Element("x").Value),
                            (float)Convert.ToDouble(anchorElemB.Element("y").Value));
                        // Create the joint
                        WeldJoint joint = JointFactory.CreateWeldJoint(PhysicsEngine.GetSingleton().PhysicsWorld, a, b, anchorA, anchorB);
                        break;
                    }
                    default:
                        // :(
                        break;
                }
            }

            engine.PhysicsWorld.ProcessChanges();
        }

        // Render an assembly based on world location and texture.
        public void Render(SpriteBatch inBatch)
        {
            // Render each object. The order is based on how they were created in EventHorizon.
            Vector2 half = Vector2.Zero;
            foreach (PhysicalObject obj in mObjects)
            {
                Texture2D tex = obj.Texture.Texture;
                half = Vector2.UnitX * tex.Width + Vector2.UnitY * tex.Height; 
                Body physBody = obj.PhysicsBody;
                inBatch.Draw(obj.Texture.Texture, physBody.WorldCenter, null, Color.White, physBody.Rotation, half / 2.0f, 1.0f, SpriteEffects.None, 1.0f);
            }
        }
        */
    }
}
