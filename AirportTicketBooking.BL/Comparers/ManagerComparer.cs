using System.Collections.Generic;
using AirportTicketBooking.Managers;

namespace AirportTicketBooking.Comparers
{
    public class ManagerComparer : IEqualityComparer<Manager>
    {
        public bool Equals(Manager manangerOne, Manager manangerTwo)
        {
            if (manangerOne is null || manangerTwo is null) return false;
            
            if (ReferenceEquals(manangerOne, manangerTwo)) return true;
            
            
            return (
                manangerOne.Id == manangerTwo.Id
                && manangerOne.Name == manangerTwo.Name
                && manangerOne.Email == manangerTwo.Email
                && manangerOne.Username == manangerTwo.Username
            );
        }

        public int GetHashCode(Manager obj)
        { 
            return 
                obj.Id.GetHashCode() + obj.Name.GetHashCode() + obj.Email.GetHashCode() + obj.Username.GetHashCode();
        }
    }
}