using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.SqlServer.Models
{
    public class UserDao
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Currency { get; set; }
        public IEnumerable<ExpenseDao> Expences { get; set; }
    }
}