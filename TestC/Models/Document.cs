using System;
using System.ComponentModel.DataAnnotations;

namespace TestC.Models
{
    public enum SortState
    {
        DocumentTypeAsc,    
        DocumentTypeDesc,   
        DepartmentAsc, 
        DepartmentDesc,   
        StageAsc, 
        StageDesc,
        DocumentNumberAsc,    
        DocumentNumberDesc,   
        BarcodeAsc, 
        BarcodeDesc,   
        DocumentDateTimeAsc, 
        DocumentDateTimeDesc
    }
    public enum Stage
    {
        A, B, C
    }

    public static class StageExtensions
    {
        public static string GetString(this Stage me)
        {
            switch (me)
            {
                case Stage.A:
                    return "Введен в архив";
                case Stage.B:
                    return "Возвращен на уточнение";
                case Stage.C:
                    return "Принят к учету";
                default:
                    return "ОТСУТСТВУЕТ ЗАПИСЬ В СПРАВОЧНИКЕ";
            }
        }
    }

    public class Document
    {
        public int Id { get; set; }
        public int? DocumentTypeId { get; set; } 
        public virtual string DocumentType { get; set; }
        public Stage Stage { get; set; }
        public int? RoleId { get; set; } 
        public virtual string Role { get; set; }
        public int? DepartmentId { get; set; } 
        public virtual string Department { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public DateTime DateTimeChange { get; set; }
        public DateTime DocumentDateTime { get; set; }
        public string? NumberSupply { get; set; }
        [Required(ErrorMessage = "Введите номер")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Номер слишком короткий или слишком длинный")]
        public string DocumentNumber { get; set; }
        public virtual string Barcode
        {
            get { return string.Concat("SSSW",Id.ToString()); }
            set { value = string.Concat("SSSW",Id.ToString()); }
        }
        public string Note { get; set; }

        public Document() 
        { 
            Stage = Stage.A; 
            DateTimeCreate = DateTime.Now; 
        }
    }
}