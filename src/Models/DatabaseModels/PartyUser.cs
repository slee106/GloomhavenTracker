namespace GloomhavenTracker.Models.DatabaseModels
{
    public class PartyUser
    {
        public int Id { get; set; }
        public int PartyId { get; set; }
        public string UserId { get; set; }
    }
}