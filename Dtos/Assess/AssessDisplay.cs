namespace blueberry.Dtos.Assess
{
    // Dùng khi hiển thị thông tin đánh giá, tùy chỉnh hiển thị theo ý muốn
    public class AssessDisplay
    {
        public int AssessId { get; set; } // Mã đánh giá
        public DateTime AssessDate { get; set; } = DateTime.Now; // Ngày đánh giá
        public int StarValue { get; set; } // Số sao
        public string Comment { get; set; } // Nội dung
        public string AssessBy { get; set; } // Người đánh giá
    }
}