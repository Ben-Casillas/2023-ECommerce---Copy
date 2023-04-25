public interface IUserRepository
{
    public Task<Guid> Create(User user); //create user

    public Task<User> GetByUserEmail(String userEmail); //get user by email

    public Task<IEnumerable<User>> GetAll(); //get all users

    public Task<User> GetByUserGuid(Guid userGuid); //get user my GUID

    public Task<User> GetByCredentials(String userEmail, String userPassword);
}