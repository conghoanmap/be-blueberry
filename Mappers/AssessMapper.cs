using blueberry.Dtos.Assess;
using blueberry.Models;

namespace blueberry.Mappers
{
    public static class AssessMapper
    {
        public static Assess RequestToModel(this AssessRequest assessRequest)
        {
            return new Assess
            {
                StarValue = assessRequest.StarValue,
                Comment = assessRequest.Comment
            };
        }
        public static AssessDisplay ModelToDisplay(this Assess assess)
        {
            return new AssessDisplay
            {
                AssessId = assess.AssessId,
                StarValue = assess.StarValue,
                Comment = assess.Comment,
                AssessDate = assess.AssessDate,
                AssessBy = assess.AppUser?.FullName ?? "Ẩn danh"
            };
        }
    }
}