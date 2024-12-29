using System;

namespace ch1_the_basics
{
    public class Firefighter : PublicServant, IPerson
    {
        public Firefighter(string name, int age)
        {
            this.Name = name;
            this.Age = age;

            this.DriveToPlaceOfInterest += delegate
            {
                Console.WriteLine("Drving the firetruck");
                GetInFiretruck();
                TurnOnSiren();
                FollowDirection();
            };
        }

        // Implement the IPerson interface
        public string Name { get; set; }
        public int Age { get; set; }

        public void GetInFiretruck() {}
        public void TurnOnSiren() {}
        public void FollowDirection() {}
    }
}