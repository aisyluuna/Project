using System.Collections.Generic;

namespace QueueForChildren.Data.Dtos.SchoolQueue
{
    public class SchoolSelectDto
    {
        public IReadOnlyList<SchoolDto> Schools = new List<SchoolDto>();
        
        public long ChildId { get; set; }
    }
}