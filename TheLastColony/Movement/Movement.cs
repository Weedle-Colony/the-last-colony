using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldState;

namespace Movement
{
    class Movement
    {
        public List<Point> GetValidMoves(int positionX, int positionY, WorldGrid worldGrid)
        {
            List<Point> validMoves = new List<Point>();
            Random rand = new Random();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int neighborX = positionX + i;
                    int neighborY = positionY + j;

                    if (neighborX > 0 && neighborX < worldGrid.WorldHeight &&
                        neighborY > 0 && neighborY < worldGrid.WorldWidth)
                    {
                        if (worldGrid.CurrentState[positionX, positionY] == 1 && worldGrid.CurrentState[neighborX, neighborY] == 0)
                        {
                            validMoves.Add(new Point(neighborX, neighborY));
                        }
                    }
                }
            }

            if (validMoves.Count > 0)
            {
                validMoves.Shuffle();

                return validMoves;
            }

            return validMoves;
        }
    }
}
}
