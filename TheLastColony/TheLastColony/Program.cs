using System;
using System.Collections.Generic;
using System.Linq;

namespace TheLastColony
{
    class Program
    {
        public static int CurrentYear { get; set; } = 0;
        static void Main()
        {
            List<Creature> initialList = new List<Creature>() {
            new Carnivore(),
            new Herbivore(),
            new Omnivore()
            };

            //Will not keep this list; using for testing
            List<Creature> deadList = new List<Creature>();

            foreach (Creature creature in initialList)
            {
                Console.WriteLine($"Original {creature.GetType().Name} with Gender [{creature.Gender}] and MaxAge {creature.MaxAge}");
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

            foreach (var creature in deadList.OrderBy(c => c.BirthYear))
            {
                Console.WriteLine($"{creature.GetType().Name} [{creature.Gender}] lived to be {creature.DeathYear - creature.BirthYear}, born in Year {creature.BirthYear} and died in Year {creature.DeathYear}");
            }
        }
    }
}
