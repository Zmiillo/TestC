using System.Collections.Generic;

namespace TestC.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual List<Document> Documents { get; set; }
        public Role()
        {
            Documents = new List<Document>();
        }
    }
}