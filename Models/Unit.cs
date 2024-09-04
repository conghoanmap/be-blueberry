using System.ComponentModel.DataAnnotations.Schema;

namespace blueberry.Models
{
    // Bảng đơn vị tính
    [Table("Units")]
    public class Unit
    {
        public int UnitId { get; set; } // Mã đơn vị tính
        public string UnitName { get; set; } // Tên đơn vị tính
    }
}