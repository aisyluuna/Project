using System.Collections.Generic;

namespace QueueForChildren.Data.Dtos.KindergartenQueue
{
    public class KindergartenSelectDto
    {
        public IReadOnlyList<KindergartenDto> Kindergartens = new List<KindergartenDto>();
        
        public long ChildId { get; set; }
    }
}