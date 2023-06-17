﻿
namespace database.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uniqueness { get; set; }
        public string LogoConcept { get; set; }
    }
}
