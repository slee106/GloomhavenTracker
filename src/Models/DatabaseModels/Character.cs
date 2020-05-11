namespace GloomhavenTracker.Models.DatabaseModels
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Party Party { get; set; }
    }
}