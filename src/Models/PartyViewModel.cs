using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GloomhavenTracker.Models
{
    [Table("Party")]
    public class PartyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Range(0, 4)]
        public int NumberOfPlayers { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfCreation { get; set; }
        public string Notes { get; set; }

        [Range(0, 15)]
        public int Reputation { get; set; }

        [Range(0,8)]
        [DisplayFormat(DataFormatString = "{0:#,0.0#}")]
        public decimal Prosperity { get; set; }
        public ApplicationUser CreationUser { get; set; }
    }
}