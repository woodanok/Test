using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.ViewModels
{
    public class PaginationModel
    {
        public Int32 PageCount { get; set; } = 0;
        public Int32 CurrentPage { get; set; } = 1;
    }
}
