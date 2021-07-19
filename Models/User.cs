using System;

namespace AnimeList.Models
{
    public class Users : BaseModel
    {
        public string Name {get;set;}
        public string User {get;set;}
        public string Password {get;set;}
        public string UserRole { get; set; }
    }
}