using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Data.Interfaces
{
    public interface IEntity
    {
        long Id { get; set; }

        DateTime CreateDate { get; set; }

        DateTime DeletedDate { get; set; }

        bool Deleted { get; set; }
    }
}
