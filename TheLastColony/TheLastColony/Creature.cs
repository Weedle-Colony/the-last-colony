using System;
using System.Collections.Generic;
using System.Linq;

namespace TheLastColony
{
    class Creature
    {
        public char Gender { get; private set; }
        public int BirthYear { get; private set; }
        public int ReproductionRate { get; private set; }
        public int Age => Program.CurrentYear - BirthYear;
        public int MaxAge { get; private set; }
        //Will not keep DeathYear property; using for testing
        public int DeathYear { get; set; }

        private static readonly Random _random = new Random();
        private static List<Creature> _offspringList = new List<Creature>();

        public Creature()
        {
            BirthYear = Program.CurrentYear;
            ComputeMaxAgeAndGender();          
        }

        private void ComputeMaxAgeAndGender()
        {
            int lifeExpectancyBracket = _random.Next(100);

            //50% chance to be either Male or Female
            Gender = lifeExpectancyBracket % 2 == 0 ? 'M' : 'F';

            //60% chance to live past 50 years.
            //Can yield 1-5 baby creatures.
            if (lifeExpectancyBracket > 40)
            {
                MaxAge = _random.Next(50, 80);
                ReproductionRate = _random.Next(15, 49);
            }
            else
            {
                //30% chance to live between 20 and 50 years.
                //Can yield 1-3 baby creatures.
                if (lifeExpectancyBracket > 10 && lifeExpectancyBracket <= 40)
                {
                    MaxAge = _random.Next(20, 49);
                    ReproductionRate = _random.Next(15, 19);
                }

                //10% chance to live under 20 years.
                //Can yield up to 1 baby creature.
                else
                {
                    MaxAge = _random.Next(1, 19);
                    ReproductionRate = _random.Next(15, 19);
                }
            }
        }

        public static List<Creature> ProduceOffspring(List<Creature> creatureList)
        {
            _offspringList.Clear();
            
            var motherList = creatureList.Where(c => c.Gender == 'F' && (c.Age >= c.ReproductionRate) && c.Age % c.ReproductionRate == 0);

            if(motherList.Count() == 0)
            {
                return _offspringList;
            }
            else
            {
                foreach (var creature in motherList)
                {
                    _offspringList.Add(CreateCreature(creature.GetType().Name));
                }
                return _offspringList;
            }
        }

        private static Creature CreateCreature(string creatureType)
        {
            switch (creatureType)
            {
                case "Carnivore":
                    return new Carnivore();
                case "Herbivore":
                    return new Herbivore(); 
                case "Omnivore":
                    return new Omnivore();              
            }

            return null;
        }

        public void Eat()
        {
            //Difficult to say how much a creature gains at this point.
            MaxAge += 2;
        }
    }

    class Carnivore : Creature
    {
    }

    class Herbivore : Creature
    {
    }

    class Omnivore : Creature
    {
    }
}
