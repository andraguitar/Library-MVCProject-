namespace BLL.Interfaces
{
    public interface IMessage
    {
        void Send(string mail, string subject, string message);
    }
}
