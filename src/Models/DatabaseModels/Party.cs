using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GloomhavenTracker.Models.DatabaseModels
{
    [Table("Party")]
    public class Party
    {
        public int Id { get; set; }

        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name="Number of Players")]
        [Range(0,4)]
        public int NumberOfPlayers { get; set; }

        [Display(Name="Date of Creation")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString="{0:dd/MM/yyyy}")]
        public DateTime DateOfCreation { get; set; }
        public string Notes { get; set; }

        [Range(-15,15)]
        public int Reputation { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        [Range(1,9)]
        public decimal Prosperity { get; set; }
        public ApplicationUser CreationUser { get; set; }
    }
}