using Dapper;
using System.Data;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;
    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    //add a user
    public async Task<Guid> Create(User user)
    {
        string sqlQueryUsers = "INSERT INTO Users (UserGuid, Username, Email, Password, CreatedDate) VALUES (@UserGuid, @Username, @Email, @Password, @CreatedDate)";
        var parametersUsers = new DynamicParameters();

        Guid userGuid = Guid.NewGuid();

        parametersUsers.Add("UserGuid", userGuid, DbType.Guid);
        parametersUsers.Add("Username", user.Username, DbType.String);
        parametersUsers.Add("Email", user.Email, DbType.String);
        parametersUsers.Add("Password", user.Password, DbType.String);
        parametersUsers.Add("CreatedDate", DateTime.UtcNow, DbType.DateTime);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(sqlQueryUsers, parametersUsers);
        }

        return userGuid;
    }

    //get by email
    public async Task<User> GetByUserEmail(String userEmail)
    {
        string sqlQuery = "SELECT * FROM Users WHERE Email = @EmailParam";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<User>(sqlQuery, new { EmailParam = userEmail });
        }
    }

    //get all users
    public async Task<IEnumerable<User>> GetAll()
    {
        string sqlQuery = "SELECT * FROM Users";
        using (var connection = _context.CreateConnection())
        {
            var users = await connection.QueryAsync<User>(sqlQuery);
            return users.ToList();
        }
    }

    //get user by GUID
    public async Task<User> GetByUserGuid(Guid userGuid)
    {
        string sqlQuery = "SELECT * FROM Users WHERE UserGuid = @UserGuidParam";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleAsync<User>(sqlQuery, new { UserGuidParam = userGuid });
        }
    }

    public async Task<User> GetByCredentials(String userEmail, String userPassword)
    {
        string sqlQuery = "SELECT * FROM Users WHERE Email = @EmailParam AND Password = @PasswordParam";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<User>(sqlQuery, new { EmailParam = userEmail, PasswordParam = userPassword});            
        }
    }

}
