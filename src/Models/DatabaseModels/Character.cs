using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GloomhavenTracker.Models.DatabaseModels
{
    [Table("Character")]
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExperiencePoints { get; set; }
        public DateTime CreationDate { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public DateTime RetirementDate { get; set; }
        public Party Party { get; set; }
    }
}