using TaskManagementSystem.Models;

namespace TaskManagementSystem.Auth
{
    public class AccountRepository
    {
        private static IDictionary<string, User> Accounts = new Dictionary<string, User>();

        public void Add(User account)
        {
            Accounts[account.Email] = account;
        }
        public void Update(User account)
        {
            account.UpdatedAt = DateTime.UtcNow;
            Accounts[account.Email] = account;
        }

        public User? GetByUserName(string email)
        {
            return Accounts.TryGetValue(email, out var account) ? account : null;
        }

        public User? GetByUserNameOrEmail(string identifier)
        {
            return Accounts.Values.FirstOrDefault(account =>
                account.UserName.Equals(identifier, StringComparison.OrdinalIgnoreCase) ||
                account.Email.Equals(identifier, StringComparison.OrdinalIgnoreCase));
        }

    }


}
