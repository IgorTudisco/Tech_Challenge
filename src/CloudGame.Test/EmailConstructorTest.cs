using CloudGame.Domain.ValueObjects;

namespace CloudGame.Test
{
    public class EmailConstructorTest
    {
        [Fact]
        public void EmailConstructor_ThrowArgumentException_Test()
        {
            //arrange
            string email = "grupo6.com";

            //assert
            Assert.Throws<ArgumentException>(() => new Email(email));
        }

        [Fact]
        public void EmailConstructor_Sucesso_Test()
        {
            //arrange
            string emailString = "grupo6@gmail.com";

            //act
            Email email = new Email(emailString);

            //assert
            Assert.True(email.EmailAdressValue.Equals(emailString));
        }
    }
}
