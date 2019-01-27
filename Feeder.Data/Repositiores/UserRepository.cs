namespace Feeder.Data.Repositiores
{
    using Feeder.Core.User;
    using Feeder.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private FeederContext _context;

        public UserRepository(FeederContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(long userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);

            return user;
        }
        
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public User AuthenticateAsync(User user, string password)
        {
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Password is Reguired");
            }

            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.UpdatePasswordHash(passwordHash);
            user.UpdatePasswordSalt(passwordSalt);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Value can't be Null or Whitespace, only string.");
            }

            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password can't be null or empty", "password");
            }

            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            }

            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");
            }

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int idx = 0; idx < computedHash.Length; idx++)
                {
                    if (computedHash[idx] != storedHash[idx])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
