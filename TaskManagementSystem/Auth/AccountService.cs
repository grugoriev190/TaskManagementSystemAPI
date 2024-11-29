using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Auth
{
    public class AccountService(AccountRepository accountRepository, JwtService jwtService)
    {
        public void Register(string userName, string email, string password)
        {

            if (accountRepository.GetByUserName(email) != null)
            {
                throw new Exception("Email already in use.");
            }

            if (password.Length < 8 ||
                !password.Any(char.IsUpper) ||
                !password.Any(char.IsDigit) ||
                !password.Any(c => !char.IsLetterOrDigit(c)))
            {
                throw new Exception("Password must be at least 8 characters long, contain an uppercase letter, a number, and a special character.");
            }



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
    }
}
