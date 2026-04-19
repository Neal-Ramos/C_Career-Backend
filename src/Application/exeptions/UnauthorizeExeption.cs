
namespace Application.exeptions
{
    public class UnauthorizeExeption: Exception
    {
        public UnauthorizeExeption(): base("UNAUTHORIZED"){}
        public UnauthorizeExeption(string Message): base(Message){}
    }
}