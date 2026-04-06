
namespace Application.exeptions
{
    public class NotFoundExeption: Exception
    {
        public NotFoundExeption(): base("Data Not Found"){}
        public NotFoundExeption(string Message): base(Message){}
    }
}