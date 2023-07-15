namespace DelegatesSample.Tests.Models
{
    public class Alien : IEquatable<Alien>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Rank { get; set; }

        public Alien(string name, int age, int rank)
        {
            Name = name;
            Age = age;
            Rank = rank;
        }

        public bool Equals(Alien? other)
        {
            if (other == null)
                return false;

            return this.Name == other.Name && this.Age == other.Age && this.Rank == other.Rank;
        }
    }
}
