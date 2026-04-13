namespace Domain.Entities
{
    public class JobsEditHistory
    {
        public int Id {get; private set;}
        public Guid EditId {get; private set;} = new Guid();
        public DateTime DateEdited {get; set;}
        public string? EditSummary {get; set;}

        //Relations
        public Guid? EditorId {get; set;}
        public AdminAccounts? EditedBy {get; set;}
        public Guid? JobId {get; set;}
        public Jobs? Job {get; set;}
    }
}