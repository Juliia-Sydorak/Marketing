using System;

namespace MarketingDAL.Entities
{
    public class User
    {
        public int UserID { get; set; } 
        public string FullName { get; set; }    
        public string Email { get; set; }           
        public string UserRole { get; set; }      
        public DateTime RegistrationDate { get; set; }
    }
}

