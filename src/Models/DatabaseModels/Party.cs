using System;
using System.ComponentModel.DataAnnotations;

namespace GloomhavenTracker.Models.DatabaseModels
{
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPlayers { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Notes { get; set; }
        public int Reputation { get; set; }
        public decimal Prosperity { get; set; }
        public ApplicationUser CreationUser { get; set; }
    }
}