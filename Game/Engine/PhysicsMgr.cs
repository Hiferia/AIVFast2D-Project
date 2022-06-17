﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProgFineAnno
{
    static class PhysicsMgr
    {
        static List<RigidBody> items;
        static Collision collisionInfo;

        static PhysicsMgr()
        {
            items = new List<RigidBody>();
        }

        public static void AddItem(RigidBody item)
        {
            items.Add(item);
        }

        public static void RemoveItem(RigidBody item)
        {
            items.Remove(item);
        }

        public static void Update()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IsActive)
                {
                    items[i].Update();
                }
            }
        }

        public static void CheckCollisions()
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                if (items[i].IsCollisionsAffected && items[i].IsActive)
                {
                    //check collisions with next items
                    for (int j = i + 1; j < items.Count; j++)
                    {
                        if (items[j].IsCollisionsAffected && items[j].IsActive)
                        {
                            //check if one of two rigid bodies is interested in collision
                            bool firstCheck = items[i].CollisionTypeMatches(items[j].Type);
                            bool secondCheck = items[j].CollisionTypeMatches(items[i].Type);

                            if ((firstCheck || secondCheck) && items[i].Collides(items[j], ref collisionInfo))
                            {
                                //Console.WriteLine(items[i].Type + " Collides with " + items[j].Type);

                                if (firstCheck)
                                {
                                    //items[i].GameObject.OnCollide(items[j].GameObject);
                                    //Console.WriteLine("collided");
                                    collisionInfo.Collider = items[j].GameObject;
                                    items[i].GameObject.OnCollide(collisionInfo);
                                }

                                if (secondCheck)
                                {
                                    //items[j].GameObject.OnCollide(items[i].GameObject);
                                    //Console.WriteLine("collided");
                                    collisionInfo.Collider = items[i].GameObject;
                                    items[j].GameObject.OnCollide(collisionInfo);

                                }
                            }
                        }
                    }
                }
            }
        }

        public static void ShowColliders()
        {
            foreach(RigidBody each in items)
            {
                if (!each.IsActive) continue;

                if (each.Collider is BoxCollider)
                {
                    BoxCollider bc = (BoxCollider)each.Collider;
                    Vector2 pos = each.Position;
                    Painter.DrawRect((int)pos.X, (int)pos.Y, (int)bc.Width, (int)bc.Height);
                } else  if (each.Collider is CircleCollider)
                {
                    CircleCollider cc = (CircleCollider)each.Collider;
                    Vector2 pos = each.Position;
                    Painter.DrawCircle((int)pos.X, (int)pos.Y, (int)cc.Radius);
                }
            }
        }

        public static void ClearAll()
        {
            items.Clear();
        }
    }
}
