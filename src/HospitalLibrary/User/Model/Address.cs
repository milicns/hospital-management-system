using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.User.Model
{
    [Owned]
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        
        public Address(){}
    }
}