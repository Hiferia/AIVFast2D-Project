using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    enum BulletType { Fireball, EnemyFireball, Last}

    static class BulletsMgr
    {
        static Queue<Bullet>[] bullets;
        static int poolSize = 50;

        public static void Init()
        {
            //create pools
            bullets = new Queue<Bullet>[(int)BulletType.Last];

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Queue<Bullet>(poolSize);

                for (int b = 0; b < poolSize; b++)
                {
                    switch ((BulletType)i)
                    {
                        case BulletType.Fireball:
                            bullets[i].Enqueue(new Fireball());
                            break;
                        case BulletType.EnemyFireball:
                            bullets[i].Enqueue(new EnemyFireball());
                            break;
                    }
                }
            }
        }

        public static Bullet GetBullet(BulletType type)
        {
            Bullet b = null;

            int queueIndex = (int)type;

            if (bullets[queueIndex].Count > 0)
            {
                b = bullets[queueIndex].Dequeue();
            }

            return b;
        }

        public static void RestoreBullet(Bullet b)
        {
            b.IsActive = false;
            //remove bullets from active list

            //put bullet into the Queue
            bullets[(int)b.Type].Enqueue(b);
        }

        public static void ClearAll()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].Clear();
                //bullets[i] = null;
            }
            bullets = null;
        }
    }
}
