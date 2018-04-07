using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using TheLastColony;

namespace WorldState
{

    public class WorldGrid
    {
        public int WorldHeight { get; private set; }
        public int WorldWidth { get; private set; }
        public int Entities { get; private set; }

        public int[,] CurrentState;
        public int[,] _nextState;

        public WorldGrid(int height, int width)
        {
            WorldHeight = height;
            WorldWidth = width;

            CurrentState = new int[WorldHeight, WorldWidth];
            _nextState = new int[WorldHeight, WorldWidth];

            for (int i = 0; i < WorldHeight; i++)
            {
                for (int j = 0; j < WorldWidth; j++)
                {
                    CurrentState[i, j] = 0;
                    _nextState[i, j] = 0;
                }
            }
        }

        public void UpdateState()
        {
            for (int i = 0; i < WorldHeight; i++)
            {
                for (int j = 0; j < WorldWidth; j++)
                {
                    if (CurrentState[i, j] == 1)
                    {
                        List<Point> pendingMoves = Movement.GetValidMoves(i, j, CurrentState, WorldHeight, WorldWidth);
                        pendingMoves.Shuffle();
                        Point move = pendingMoves.DefaultIfEmpty(new Point(i, j)).First();

                        int x = move.X;
                        int y = move.Y;

                        if (CurrentState[x, y] == 0 && _nextState[x, y] == 0)
                        {
                            _nextState[x, y] = 1;
                        }
                        else if (x == i && y == j)
                        {
                            _nextState[i, j] = 1;
                        }
                        else
                        {
                            if (pendingMoves.Count > 1)
                            {
                                pendingMoves.RemoveAt(0);
                                _nextState[i, j] = 1;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < WorldHeight; i++)
            {
                for (int j = 0; j < WorldWidth; j++)
                {
                    CurrentState[i, j] = _nextState[i, j];
                    _nextState[i, j] = 0;
                }
            }

        }

        

        public void RandomEntities()
        {
            Random rand = new Random();

            for (int i = 0; i < WorldHeight; i++)
            {
                for (int j = 0; j < WorldWidth; j++)
                {
                    int occupiedChance = rand.Next(150);
                    int initialState;

                    if (occupiedChance < 5)
                    {
                        Entities += 1;
                        initialState = 1;
                    }
                    else
                    {
                        initialState = 0;
                    }

                    CurrentState[i, j] = initialState;
                }
            }
        }
    }
}
