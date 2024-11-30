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
			account.UpdatedAt = DateTime.UtcNow;
			dbContext.Users.Update(account);
			dbContext.SaveChanges();
		}

        public User? GetByUserName(string userName)
        {
			return dbContext.Users.FirstOrDefault(u => u.UserName == userName);
		}

		public User? GetByEmail(string email)
		{
			return dbContext.Users.FirstOrDefault(u => u.Email == email);
		}

		public User? GetByUserNameOrEmail(string identifier)
        {
			var identifierLower = identifier.ToLower();
			return dbContext.Users.FirstOrDefault(u =>
				u.UserName.ToLower() == identifierLower || u.Email.ToLower() == identifierLower);
		}

    }


}
