using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class PaginatedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
        public bool HasPreviousPage  => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
