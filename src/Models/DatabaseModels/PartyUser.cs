namespace GloomhavenTracker.Models.DatabaseModels
{
    public class PartyUser
    {
        public int PartyId { get; set; }
        public Party Party { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}