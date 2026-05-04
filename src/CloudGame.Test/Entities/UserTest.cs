using CloudGame.Domain.Entities;

namespace CloudGame.Test.Entities;

public class UserTest
{
    [Fact]
    public void ShouldInvalidateUserDataWhenDeleteDataMethodIsCalled_Test()
    {
        var user = new User("John Doe", "john.doe@example", "password123", new DateTime(1990, 1, 1), false);

        user.DeleteData();

        Assert.Equal("@deleted", user.Name);
        Assert.Equal("@deleted", user.Password);
        Assert.False(user.Active);
        Assert.Equal(new DateTime(1950, 1, 1), user.BirthDate);
    }
}
