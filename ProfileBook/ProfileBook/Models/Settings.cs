using SQLite;

namespace ProfileBook.Models
{
    [Table("Settings")]
    public class Settings : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
