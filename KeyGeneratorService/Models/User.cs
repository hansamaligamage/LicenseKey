using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGeneratorService
{
    public class User
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public Guid UserGuid { get; set; }
        public byte[] Subscription { get; set; }
        public byte[] SaltValue { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public virtual Company Company { get; set; }
    }
}
