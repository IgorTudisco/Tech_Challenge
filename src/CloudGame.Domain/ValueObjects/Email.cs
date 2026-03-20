using System.Net.Mail;

namespace CloudGame.Domain.ValueObjects
{
    public sealed class Email
    {

        public Email(string emailAdress)
        {
            if (!Validate(emailAdress))
                throw new ArgumentException("O e-mail é invalido", nameof(emailAdress));

            EmailAdressValue = emailAdress;
        }

        bool Validate(string emailAdress)
        {
            try
            {
                _ = new MailAddress(emailAdress);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public string EmailAdressValue { get; }
    }
}
