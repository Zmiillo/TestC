using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestC.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Document> Documents { get; set; }
        public DocumentType()
        {
            Documents = new List<Document>();
        }

    }
}