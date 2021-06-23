using System;

namespace AnimeList.Models
{
    public class Anime : BaseModel
    {
        public string Title {get;set;}
        public string Descripcion {get;set;}
        public DateTime StartYear {get;set;}
        public Nullable<DateTime> EndYear {get;set;}
    }
}