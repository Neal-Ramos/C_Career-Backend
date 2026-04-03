namespace Application.exeptions
{
    public class InvalidInputExeption: Exception
    {
        public InvalidInputExeption(): base("Invalid Input"){}
        public InvalidInputExeption(string Message): base(Message){}
    }
}