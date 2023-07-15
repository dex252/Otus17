using DelegatesSample.Tests.Models;

namespace DelegatesSample.Tests
{
    public partial class GetMaxTest
    {
        public static object[][] HumanDataSet =>
          new[]
          {
                new object[]
                {   
                    new List<Human>(7)
                    {
                        new Human("Василий", 20),
                        new Human("Иван", 24),
                        new Human("Константин", 25),
                        new Human("Геннадий", 30),
                        new Human("Николай", 40),
                        new Human("Мария", 32),
                        new Human("Кристина", 25)
                    },
                    ConvertToFloat<Human>,
                    new Human("Николай", 40)
                },
                new object[]
                {
                    new List<Human>(1)
                    {
                        new Human("Василий", 20),
                    },
                    ConvertToFloat<Human>,
                    new Human("Василий", 20)
                },
                new object[]
                {
                    new List<Human>(3)
                    {
                        new Human("Иван", 24),
                        new Human("Василий", 20),
                        new Human("Иван", 24),
                    },
                    ConvertToFloat<Human>,
                    new Human("Иван", 24)
                },
                new object[]
                {
                    new List<Human>(0),
                    ConvertToFloat<Human>,
                    null
                },
          };

        public static object[][] AlienDataSet =>
         new[]
         {
                new object[]
                {
                    new List<Alien>(7)
                    {
                        new Alien("Оптимус", 3, 1),
                        new Alien("Октулус", 2, 2),
                        new Alien("Септимус", 10, 0),
                        new Alien("Виридий", 5, 1),
                        new Alien("Каракулус", 3, 3),
                        new Alien("Олигарус", 1, 1),
                        new Alien("Дерексус", 2, 3)
                    },
                    ConvertToFloat<Alien>,
                    new Alien("Каракулус", 3, 3),
                },
                new object[]
                {
                    new List<Alien>(7)
                    {
                        new Alien("Оптимус", 3, 1),
                        new Alien("Октулус", 2, 2),
                        new Alien("Септимус", 10, 0),
                        new Alien("Виридий", 5, 1),
                        new Alien("Каракулус", 3, 3),
                        new Alien("Олигарус", 1, 1),
                        new Alien("Зеро", 10, 2)
                    },
                    ConvertToFloat<Alien>,
                   new Alien("Зеро", 10, 2)
                },
                 new object[]
                {
                    new List<Alien>(8)
                    {
                        new Alien("Оптимус", 3, 1),
                        new Alien("Октулус", 2, 2),
                        new Alien("Септимус", 10, 0),
                        new Alien("Виридий", 5, 1),
                        new Alien("Зеро-Оптимус-Октулус-Септимус-Виридий-Каракулус-Олигарус-Многобукавус", 1, 1),
                        new Alien("Каракулус", 3, 3),
                        new Alien("Олигарус", 1, 1),
                        new Alien("Зеро", 10, 2)
                    },
                    ConvertToFloat<Alien>,
                   new Alien("Зеро-Оптимус-Октулус-Септимус-Виридий-Каракулус-Олигарус-Многобукавус", 1, 1)
                },
                new object[]
                {
                    new List<Alien>(1)
                    {
                        new Alien("Оптимус", 3, 1),
                    },
                    ConvertToFloat<Alien>,
                     new Alien("Оптимус", 3, 1)
                },
                new object[]
                {
                    new List<Alien>(3)
                    {
                        new Alien("Оптимус", 3, 1),
                        new Alien("Зеро", 3, 1),
                        new Alien("Оптимус", 3, 1)
                    },
                    ConvertToFloat<Alien>,
                   new Alien("Оптимус", 3, 1)
                },
                new object[]
                {
                    new List<Alien>(0),
                    ConvertToFloat<Alien>,
                    null
                },
         };

        public static float ConvertToFloat<T>(T converted)
        {
            if (converted == null)
                return float.MinValue;

            if (converted is Human)
            {
                return (converted as Human).Age;
            }

            if (converted is Alien)
            {
                var obj = converted as Alien;
                var nameCount = obj.Name?.Count();
                var globalAge = obj.Age * obj.Rank;

                return (int)nameCount + globalAge;
            }

            var hash = converted.GetHashCode();
            return hash;
        }
    }
}
