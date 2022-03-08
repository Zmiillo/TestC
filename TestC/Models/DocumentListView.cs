using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestC.Models
{
    public class DocumentListView
    {
        public IEnumerable<Document> Documents { get; set; }
        public SelectList Departments { get; set; }
    }
}