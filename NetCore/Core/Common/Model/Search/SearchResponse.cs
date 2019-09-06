using System;
using System.Collections.Generic;

namespace Core.Common.Model.Search
{
    public class SearchResponse<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public int TotalPages =>
            (int)Math.Floor((double)TotalCount / (double)PageSize) + 1;

        public int TotalCount { get; set; }

        public List<T> Result { get; set; }
    }
}
