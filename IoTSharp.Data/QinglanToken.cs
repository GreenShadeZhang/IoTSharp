using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSharp.Data
{
    public class QinglanToken
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public string Scope { get; set; } = string.Empty;

        public string TokenType { get; set; } = string.Empty;

        public DateTime? CreateDate { get; set; }

        public int ExpiresIn { get; set; }

        public Customer Customer { get; set; }

        public Tenant Tenant { get; set; }

        public bool Deleted { get; set; }
    }
}
