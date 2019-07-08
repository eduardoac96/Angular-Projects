using System;
using System.Collections.Generic;
using System.Text;

namespace LearnU_DomainEntities
{
    public class PaginatedDto<T>
    {
        public int TotalRecords { get; set; }
        public IEnumerable<T> FilteredRecords { get; set; }
    }
}
