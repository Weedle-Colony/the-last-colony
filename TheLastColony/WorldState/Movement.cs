using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldState
{
    class Movement
    {



        public static List<Point> GetValidMoves(int positionX, int positionY, int[,] CurrentState, int WorldHeight, int WorldWidth)
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

                    if (neighborX > 0 && neighborX < WorldHeight &&
                        neighborY > 0 && neighborY < WorldWidth)
                    {
                        if (CurrentState[positionX, positionY] == 1 && CurrentState[neighborX, neighborY] == 0)
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
