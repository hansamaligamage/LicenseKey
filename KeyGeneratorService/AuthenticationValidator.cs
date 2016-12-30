using System.Security.Cryptography;
using System.Linq;
using System;

namespace KeyGeneratorService
{
    public class AuthenticationValidator
    {

        KeyGeneratorContext context = new KeyGeneratorContext();

        public bool GenerateSubscriptionKey(string userName, int companyCode)
        {
            bool isSuccess = false;
            byte[] salt = GenerateSalt();
            Company company = context.Companies.Where(c => c.Code == companyCode).FirstOrDefault();
            User user = context.Users.Where(u => u.CompanyId == company.Id && u.UserName == userName).FirstOrDefault();
            string[] hashKeys = GenerateHashKey(userName, companyCode).Split(':');
            Rfc2898DeriveBytes value = new Rfc2898DeriveBytes(hashKeys[0] + hashKeys[1], salt);
            byte[] key = value.GetBytes(64);
            user.Subscription = key;
            user.SaltValue = salt;
            isSuccess = context.SaveChanges() > 0;
            return isSuccess;
        }

        private static byte[] GenerateSalt ()
        {
            int saltLength = 32;
            byte[] salt = new byte[saltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }

        private string GenerateHashKey(string userName, int companyCode)
        {
            string key = string.Empty;
            Company company = context.Companies.Where(c => c.Code == companyCode).FirstOrDefault();
            User user = context.Users.Where(u => u.CompanyId == company.Id && u.UserName == userName).FirstOrDefault();
            if (company != null && user != null)
                key = company.Name + ":" + user.UserGuid;
            return key;
        }
        public bool ValidateSubscriptionKey(string userName, int companyCode)
        {
            bool isValid = false;
            byte[] subscription = new byte[64];
            byte[] saltValue = new byte[32];
            Company company = context.Companies.Where(c => c.Code == companyCode).FirstOrDefault();
            if (company != null)
            {
                User user = context.Users.Where(u => u.CompanyId == company.Id && u.UserName == userName).FirstOrDefault();
                if (user != null)
                {
                    subscription = user.Subscription;
                    saltValue = user.SaltValue;
                }
                string[] hashKeys = GenerateHashKey(userName, companyCode).Split(':');
                if (hashKeys.Length == 2)
                {
                    Rfc2898DeriveBytes value = new Rfc2898DeriveBytes(hashKeys[0] + hashKeys[1], saltValue);
                    byte[] key = value.GetBytes(64);
                    bool result = subscription.SequenceEqual(key);
                    if (result && user.ExpiryDate >= DateTime.Now)
                        isValid = true;
                }
            }
            return isValid;
        }

    }
}
