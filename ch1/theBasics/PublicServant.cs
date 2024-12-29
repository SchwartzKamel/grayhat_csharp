using System;

namespace ch1_the_basics
{
    public abstract class PublicServant
    {
        public int PensionAmount { get; set; }
        public delegate void DriveToPlaceOfInterestDelegate();
        public DriveToPlaceOfInterestDelegate DriveToPlaceOfInterest { get; set; }
    }
}