using System.Collections.Generic;

namespace TestC.Models
{
    public class IndexView
    {
        public IEnumerable<Document> Documents { get; set; }
        public FilterView FilterView { get; set; }
        public SortView SortView { get; set; }
    }
}