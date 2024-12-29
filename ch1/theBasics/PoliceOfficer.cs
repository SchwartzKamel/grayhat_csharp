using System;

namespace ch1_the_basics
{
    public class PoliceOfficer : PublicServant, IPerson
    {
        private bool _hasEmergency;
        public PoliceOfficer(string name, int age, bool _hasEmergency = false)
        {
            this.Name = name;
            this.Age = age;
            this.HasEmergency = _hasEmergency;

            if (this.HasEmergency)
            {
                this.DriveToPlaceOfInterest += delegate
                {
                    Console.WriteLine("Driving the police car with siren");
                    GetInPoliceCar();
                    TurnOnSiren();
                    FollowDirection();
                };
            } else
            {
                this.DriveToPlaceOfInterest += delegate
                {
                    Console.WriteLine("Driving the police car");
                    GetInPoliceCar();
                    FollowDirection();
                };
            }
        }

        // Implement the IPerson interface
        public string Name { get; set; }
        public int Age { get; set; }

        public bool HasEmergency
        {
            get { return _hasEmergency; }
            set { _hasEmergency = value; }
        }

        public void GetInPoliceCar() {}
        public void TurnOnSiren() {}
        public void FollowDirection() {}
    }
}