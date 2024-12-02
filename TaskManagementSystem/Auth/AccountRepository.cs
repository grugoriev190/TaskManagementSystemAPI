using TaskManagementSystem.Models;

namespace TaskManagementSystem.Auth
{
    public class AccountRepository
    {
		private readonly AppDbContext dbContext;

		public AccountRepository(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public void Add(User account)
        {
			dbContext.Users.Add(account);
			dbContext.SaveChanges();
		}
        public void Update(User account)
        {
			var existingAccount = dbContext.Users.FirstOrDefault(u => u.Id == account.Id);
			if (existingAccount != null)
			{
				existingAccount.UserName = account.UserName;
				existingAccount.Email = account.Email;
				existingAccount.PasswordHash = account.PasswordHash;
				existingAccount.UpdatedAt = DateTime.UtcNow;

				dbContext.Users.Update(existingAccount);
				dbContext.SaveChanges();
			}
			else
			{
				throw new Exception("User not found.");
			}
		}

		public void Delete(Guid userId)
		{
			var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
			if (user != null)
			{
				dbContext.Users.Remove(user);
				dbContext.SaveChanges();
			}
			else
			{
				throw new Exception("User not found.");
			}
		}

		public User? GetByUserName(string userName)
        {
			return dbContext.Users.FirstOrDefault(u => u.UserName == userName);
		}

		public User? GetByEmail(string email)
		{
			return dbContext.Users.FirstOrDefault(u => u.Email == email);
		}

		public User? GetByUserId(Guid userId)
		{
			return dbContext.Users.FirstOrDefault(u => u.Id == userId);
		}

		public User? GetByUserNameOrEmail(string identifier)
        {
			var identifierLower = identifier.ToLower();
			return dbContext.Users.FirstOrDefault(u =>
				u.UserName.ToLower() == identifierLower || u.Email.ToLower() == identifierLower);
		}

    }


}
