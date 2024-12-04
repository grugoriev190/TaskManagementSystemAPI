using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Auth
{
    public class AccountService
    {
		private readonly AccountRepository accountRepository;
		private readonly JwtService jwtService;

		public AccountService(AccountRepository accountRepository, JwtService jwtService)
		{
			this.accountRepository = accountRepository;
			this.jwtService = jwtService;
		}
		public void Register(string userName, string email, string password)
        {
			// Перевірка, чи не використовується вже email або ім'я користувача
			if (accountRepository.GetByUserName(email) != null)
            {
                throw new Exception("Email already in use.");
            }

			if (accountRepository.GetByUserName(userName) != null)
			{
				throw new Exception("Username already in use.");
			}

			// Перевірка пароля
			if (password.Length < 8 ||
			   !password.Any(char.IsUpper) ||
			   !password.Any(char.IsDigit) ||
			   !password.Any(c => !char.IsLetterOrDigit(c)))
			{
				throw new Exception("Password must be at least 8 characters long, contain an uppercase letter, a number, and a special character.");
			}


			// Створення нового користувача
			var account = new User
            {
                UserName = userName,
                Email = email,
                Id = Guid.NewGuid(),
            };
            Console.WriteLine($"Registering user: {email}");
            var passHash = new PasswordHasher<User>().HashPassword(account, password);
            account.PasswordHash = passHash;
            accountRepository.Add(account);
            Console.WriteLine($"User registered: {email}");
        }


        public string Login(string identifier, string password)
        {


            Console.WriteLine($"Attempting to login with identifier: {identifier}");
            var account = accountRepository.GetByUserNameOrEmail(identifier);

            if (account == null)
            {
                throw new Exception("User not found");
            }


            var result = new PasswordHasher<User>().VerifyHashedPassword(account, account.PasswordHash, password);

            if (result == PasswordVerificationResult.Success)
            {
                Console.WriteLine($"Login successful for: {identifier}");
                return jwtService.GenerateToken(account);
            }
            else
            {
                Console.WriteLine($"Unauthorized login attempt for: {identifier}");
                throw new Exception("Unauthorized");
            }
        }

		public void DeleteAccount(Guid userId)
		{
			Console.WriteLine($"Attempting to delete user with ID: {userId}");
			accountRepository.Delete(userId);
			Console.WriteLine($"User deleted successfully: {userId}");
		}

		// Метод для оновлення даних користувача
		public void UpdateAccount(Guid userId, string? newUserName, string? newEmail, string? newPassword)
		{
			Console.WriteLine($"Attempting to update user with ID: {userId}");
			var account = accountRepository.GetByUserNameOrEmail(newEmail ?? newUserName ?? string.Empty);

			// Перевірка та оновлення даних користувача
			if (account == null || account.Id != userId)
			{
				account = accountRepository.GetByUserId(userId);
				if (account == null)
				{
					throw new Exception("User not found.");
				}
			}

			if (!string.IsNullOrEmpty(newUserName))
			{
				if (accountRepository.GetByUserName(newUserName) != null)
				{
					throw new Exception("Username already in use.");
				}
				account.UserName = newUserName;
			}

			if (!string.IsNullOrEmpty(newEmail))
			{
				if (accountRepository.GetByEmail(newEmail) != null)
				{
					throw new Exception("Email already in use.");
				}
				account.Email = newEmail;
			}

			if (!string.IsNullOrEmpty(newPassword))
			{
				if (newPassword.Length < 8 ||
				   !newPassword.Any(char.IsUpper) ||
				   !newPassword.Any(char.IsDigit) ||
				   !newPassword.Any(c => !char.IsLetterOrDigit(c)))
				{
					throw new Exception("Password must meet security criteria.");
				}
				var passHash = new PasswordHasher<User>().HashPassword(account, newPassword);
				account.PasswordHash = passHash;
			}

			accountRepository.Update(account);
			Console.WriteLine($"User updated successfully: {userId}");
		}
	}
}
