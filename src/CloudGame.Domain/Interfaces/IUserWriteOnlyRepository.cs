using CloudGame.Domain.Entities;

namespace CloudGame.Domain.Interfaces;

public interface IUserWriteOnlyRepository : IWriteOnlyRepository<User,int>
{}