
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.SqlServer.Models
{
    public class ExpenseDao
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseId { get; set; }

        public DateTime Date { get; set; }

        public string Nature { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Comment { get; set; }

        public int UserId { get; set; }

        public UserDao User { get; set; }
    }
}
