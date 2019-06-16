using Task3.BLL.BO;

namespace Task3.BLL.Interfaces
{
    public interface IEmail
    {
        bool SendMail(EmailData data);
    }
}