
namespace Application.exeptions
{
    public class ConflictExeption: Exception
    {
        public ConflictExeption():base("Database Conflict"){}
        public ConflictExeption(string Message):base(Message){}
    }
}