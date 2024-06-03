using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DemExam.Service.HashData
{
    public class HashService
    {
        public string HashData(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));

                return Convert.ToBase64String(bytes);
            }
        }

        public bool VerifyHashedData(string hashedData, string data)
        {
            if (hashedData == null)
            {
                return false;
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));

                string computedHash = Convert.ToBase64String(bytes);

                return hashedData.Equals(computedHash);
            }
        }

        public string GenerateHashData(string trash)
        {
            DateTime time = DateTime.Now;

            string hashedData = time.Ticks.ToString() + trash;

            return HashData(hashedData);
        }
    }
}
