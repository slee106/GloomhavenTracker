using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GloomhavenTracker.Models.Enums;

namespace GloomhavenTracker.Models.DatabaseModels
{
    [Table("Character")]
    public class Character
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int ExperiencePoints { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [NotMapped]
        private DateTime creationDate = DateTime.Today.Date;
        public DateTime CreationDate
        {
            get
            {
                return creationDate;
            }
            set
            {
                creationDate = value;
            }
        }
        public int Gold { get; set; }
        public int Level { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? RetirementDate { get; set; }
        public int PartyId { get; set; }
        public Party Party { get; set; }
        public ICollection<CharacterItem> CharacterItems { get; set; }
        public Class Class { get; set; }
        public int NumberOfConsumablesAvailable { get; set; }
    }
}