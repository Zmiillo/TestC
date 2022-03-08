using System.Collections.Generic;

namespace TestC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Document> Documents { get; set; }
        public Department()
        {
            Documents = new List<Document>();
        }
    }
}