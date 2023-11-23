using HRRecruitment.Entity; 

public class LoginService
{
    private List<User> users;

    public List<User> Users
    {
        get { return users; }
        set { users = value; }
    }

    public LoginService(List<User> users)
    {
        Users = users;
    }

    public bool ValidateUser(string userName, string password)
    {
        return Users.Any(u => u.Username == userName && u.Password == password);
    }
}
