using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities.Zags
{
    public class ZagsChild : BaseEntity
    {
        public string Serial { get; set; } = default!;

        public string Number { get; set; } = default!;
    }
}
