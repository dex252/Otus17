namespace DelegatesSample.Tests.Models
{
    public class Human: IEquatable<Human>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Human(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public bool Equals(Human? other)
        {
            if (other == null) 
                return false;
            
            return this.Name == other.Name && this.Age == other.Age;
        }
    }
}
