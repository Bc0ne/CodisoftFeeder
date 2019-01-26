using System;

namespace Feeder.Core.User
{
    public class User
    {
        public long Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public byte[] PasswordHash { get; private set; }

        public byte[] PasswordSalt { get; private set; }

        public void UpdatePasswordHash(byte[] passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void UpdatePasswordSalt(byte[] passwordSalt)
        {
            PasswordSalt = passwordSalt;
        }

        public static User New(string firstName, string lastName, string email)
        {
            return new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }
    }
}
