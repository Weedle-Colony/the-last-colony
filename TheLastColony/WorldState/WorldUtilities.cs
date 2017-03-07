using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using TheLastColony;

namespace WorldState
{
    public static class WorldUtilities
    {
/*
        private static System.Timers.Timer updateLoop;
*/
        private static ConsoleKeyInfo stopKey;
        private static WorldGrid worldGrid;

        public static void TestPrintGrid()
        {
            worldGrid = new WorldGrid(25, 50);
            worldGrid.RandomEntities();

            //Task updateLoop = UpdateLoop();

            
            do
            {
                PrintWorldGrid(worldGrid.CurrentState);
                worldGrid.UpdateState();
                Thread.Sleep(500);
            } while (true);

        }

        public static void PrintWorldGrid(int[,] currentWorldState)
        {
            Console.Clear();

            int x = 0;
            int rowWidth = currentWorldState.GetUpperBound(1) + 1;
            
            StringBuilder worldGridOutput = new StringBuilder();

            foreach (var nodeState in currentWorldState)
            {
                worldGridOutput.Append(nodeState == 1 ? "c" : "·");
                x++;

                if (x >= rowWidth)
                {
                    x = 0;
                    worldGridOutput.AppendLine();
                }
            }

            Console.Write(worldGridOutput.ToString());
        }

        private static void PrintLoop()
        {
            PrintWorldGrid(worldGrid.CurrentState);
            worldGrid.UpdateState();
            Thread.Sleep(500);
        }

        /*public static async Task UpdateLoop()
        {
            Task updateTask = Task.Run(new Action(PrintLoop));

            while (stopKey.Key != ConsoleKey.X)
                stopKey = Console.ReadKey(true);

            await updateTask;
        }*/

        /**
         * The Shuffle below is an implementation of a Shuffle method
         * for Lists based on the Fisher-Yates shuffle algorithm. It's used
         * primarily to shuffle lists so that each move can be chosen pseudorandomly
         * and eliminated if a conflict occurs.
         */
        private static Random rand = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int num = list.Count;

            while (num > 1)
            {
                num--;
                int aux = rand.Next(num + 1);

                T value = list[aux];
                list[aux] = list[num];
                list[num] = value;
            }
        }

    }
}
