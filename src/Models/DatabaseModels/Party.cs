using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GloomhavenTracker.Models.DatabaseModels
{
    [Table("Party")]
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPlayers { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString="{0:dd/MM/yyyy}")]
        public DateTime DateOfCreation { get; set; }
        public string Notes { get; set; }
        public int Reputation { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")] 
        public decimal Prosperity { get; set; }
        public ApplicationUser CreationUser { get; set; }
    }
}