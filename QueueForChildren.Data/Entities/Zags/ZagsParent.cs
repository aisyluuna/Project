using QueueForChildren.Data.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace QueueForChildren.Data.Entities.Zags
{
    public class ZagsParent : BaseEntity
    {
        [MaxLength(12)]
        public string INN { get; set; } = null!;     
    }
}
