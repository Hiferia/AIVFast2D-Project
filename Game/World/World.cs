using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Graph02;

namespace ProgFineAnno
{
    class World
    {
        private int[,] grid; // matrice di righe e colonne
        private int Rows;
        private int Cols;

        private GridGraph graph;

        private int numTiles = 10;

        private int busyWeight = 100;

        public void Init(int r, int c)
        {

            grid = new int[r, c];


            Rows = r;
            Cols = c;
            //Rows = 10;
            //Cols = 17;

            //grid init
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    grid[y, x] = 1;
                }
            }

            for (int i = 0; i < TileMgr.TilePositions.Count; i++)
            {
                int y = (int)TileMgr.TilePositions[i].Y;
                int x = (int)TileMgr.TilePositions[i].X;

                grid[y, x] = 100;
            }

            graph = new GridGraph(grid);
        }
        private void GetRandomFreeNodeCoord(out int r, out int c)
        {
            do
            {
                r = RandomGenerator.GetRandomInt(0, Rows);
                c = RandomGenerator.GetRandomInt(0, Cols);
            } while (grid[r, c] != 100); //perchè se è diverso da 1 vuol dire che è già occupato, ci sta già un tile
        }
        private void CreateTile(int y, int x) //y = righe, x = colonne
        {
            grid[y, x] = busyWeight;

            Tile t = new Tile("crate");
            t.Position = new Vector2(x + 0.5f, y + 0.5f);//il +0.5 è per mettere il pivot al centro dell'unità
        }
        public Node GetRandomFreeNode()
        {
            int randR, randC;
            GetRandomFreeNodeCoord(out randR, out randC);

            return graph.NodeAt(randR, randC);
        }
        public static Vector2 GetNodePosition(Node n)
        {
            //return new Vector2((n.Position.col + 0.5f) * 0.375f, (n.Position.row + 0.5f) * 0.375f); //controllare 0.3f         0.375 = è lo scale da 128 a 48 
            return new Vector2((n.Position.col + 0.5f), (n.Position.row + 0.5f)); //controllare 0.3f         0.375 = è lo scale da 128 a 48 
        }
        public Node GetNodeAtPosition(Vector2 position)
        {
            int x = (int)(position.X); //*2.66666f
            int y = (int)(position.Y);

            if (x < 0 || x >= Cols || y < 0 || y >= Rows)
            {
                return null;
            }
            if (grid[y, x] == 100)
            {
                return null;
            }
            Node n = graph.NodeAt(y, x);
            return n;
        }
    }
}
