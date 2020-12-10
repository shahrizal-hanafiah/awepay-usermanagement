using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class FilterSortingParams
    {
        public string SearchByPhone { get; set; } = "";
        public string SearchByEmail { get; set; } = "";
        public string SortBy { get; set; } = "";
    }
}
