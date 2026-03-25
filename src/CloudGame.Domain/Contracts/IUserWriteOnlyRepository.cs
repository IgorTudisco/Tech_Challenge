using CloudGame.Domain.Entities;

namespace CloudGame.Domain.Contracts;

public interface IUserWriteOnlyRepository : IWriteOnlyRepository<User,int>
{}