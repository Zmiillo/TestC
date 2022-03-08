namespace TestC.Models
{
    public class SortView
    {
        public SortState DocumentType { get; private set; } // значение для сортировки по имени
        public SortState Department { get; private set; }    // значение для сортировки по возрасту
        public SortState Stage { get; private set; }   // значение для сортировки по компании
        public SortState DocumentNumber { get; private set; } // значение для сортировки по имени
        public SortState Barcode { get; private set; }    // значение для сортировки по возрасту
        public SortState DocumentDateTime { get; private set; }   // значение для сортировки по компании
        public SortState Current { get; private set; }     // текущее значение сортировки
 
        public SortView(SortState sortOrder)
        {
            DocumentType = sortOrder==SortState.DocumentTypeAsc ? SortState.DocumentTypeDesc : SortState.DocumentTypeAsc;
            Department = sortOrder == SortState.DepartmentAsc ? SortState.DepartmentDesc : SortState.DepartmentAsc;
            Stage = sortOrder == SortState.StageAsc ? SortState.StageDesc : SortState.StageAsc;
            DocumentNumber = sortOrder==SortState.DocumentNumberAsc ? SortState.DocumentNumberDesc : SortState.DocumentNumberAsc;
            Barcode = sortOrder == SortState.BarcodeAsc ? SortState.BarcodeDesc : SortState.BarcodeAsc;
            DocumentDateTime = sortOrder == SortState.DocumentDateTimeAsc ? SortState.DocumentDateTimeDesc : SortState.DocumentDateTimeAsc;
            Current = sortOrder;
        }
    }
}