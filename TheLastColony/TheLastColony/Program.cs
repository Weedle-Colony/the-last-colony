using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TheLastColony
{
    class Program
    {
        public static int CurrentYear { get; set; } = 0;
        static void Main()
        {
            Console.WriteLine("\n\n Welcome to The Last Colony test suite!");
            
            Menu();

            return;
        }

        static void Menu()
        {
            string divider = "-----------------------------------------------";

            while (true)
            {
                Console.WriteLine($"\n\n{divider}");
                Console.WriteLine("Test Menu");
                Console.WriteLine(divider);
                Console.WriteLine("1. Test Lifecycle Component");
                Console.WriteLine("Q. Quit Test Suite");
                Console.WriteLine(divider);
                Console.Write("\nWhat tests would you like to run? (Use test number): ");
                char input = Console.ReadKey().KeyChar;
                input = char.ToUpper(input);

                /** NOTE: The Thread.Sleep(500) calls are just to add a pause to execution
                 *        to avoid the current option from immediately spitting out all of 
                 *        its data.
                 */
                switch (input)
                {
                    case '1':
                        Console.WriteLine("\n\nRunning Lifecycle Component Test...\n\n");
                        Thread.Sleep(500);
                        Console.Clear();

                        TestLifecycle();

                        break;
                    case 'Q':
                        Console.WriteLine("\n\nQuitting test suite...\n\n");
                        Thread.Sleep(500);

                        return;
                    default:
                        Console.Write($"\n\nSorry, that was an invalid option! Try again using a valid option.\n");
                        break;
                }
            }
        }

        static void TestLifecycle()
        {
            CurrentYear = 0;
            Carnivore.Count = 1;
            Herbivore.Count = 1;
            Omnivore.Count = 1;

            List<Creature> initialList = new List<Creature>() {
            new Carnivore(),
            new Herbivore(),
            new Omnivore()
            };

            //Will not keep this list; using for testing
            List<Creature> deadList = new List<Creature>();

            foreach (Creature creature in initialList)
            {
                Console.WriteLine($"{creature.Name} with Gender [{creature.Gender}] and MaxAge {creature.MaxAge}");
            }


            while (initialList.Any() && CurrentYear < 1000)
            {
                //Death     
                if (initialList.Any(c => c.Age == c.MaxAge))
                {
                    foreach (Creature creature in initialList.Where(c => c.Age == c.MaxAge))
                    {
                        creature.DeathYear = CurrentYear;
                        deadList.Add(creature);
                    }

                    initialList.RemoveAll(c => c.Age == c.MaxAge);
                }

                //Reproduction (if there are any fertile females)
                initialList.AddRange(Creature.ProduceOffspring(initialList));

                //New year
                CurrentYear++;
            }

            Console.WriteLine($"\n{deadList.Count()} creatures self-sustained until Year: " + --CurrentYear);

            foreach (Creature creature in deadList.OrderBy(c => c.BirthYear))
            {
                Console.WriteLine($"{creature.Name} [{creature.Gender}] lived to be {creature.DeathYear - creature.BirthYear}, born in Year {creature.BirthYear} and died in Year {creature.DeathYear}");
            }

            return;
        }
    }
}
