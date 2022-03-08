using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestC.Models;

namespace TestC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private DocContext db;
        private Stage stage;

        public HomeController(DocContext context,ILogger<HomeController> logger)
        {
            db = context;
            if (db.DocumentTypes.Count() == 0)
            {
                DocumentType documentType1 = new DocumentType {Name = "Сертификат качества"};
                DocumentType documentType2 = new DocumentType {Name = "Сертификат качества на погрузку"};
                DocumentType documentType3 = new DocumentType {Name = "Приказ на отгрузку"};
                DocumentType documentType4 = new DocumentType {Name = "Приложение"};
                Department department1 = new Department {Name = "ТЭСЦ-1"};
                Department department2 = new Department {Name = "ТЭСЦ-3"};
                Department department3 = new Department {Name = "ТЭСЦ-5"};
                Department department4 = new Department {Name = "КПЦ"};
                Role role1 = new Role {Type = "Администратор"};
                Role role2 = new Role {Type = "Бухгалтер"};
                Role role3 = new Role {Type = "Архивист"};
                db.DocumentTypes.AddRange(documentType1,documentType2,documentType3,documentType4);
                db.Departments.AddRange(department1,department2,department3,department4);
                db.Roles.AddRange(role1,role2,role3);
                db.SaveChanges(); 
            }
        }

        public async Task<IActionResult> Index(int? department, int? id, Stage s,int role = 1, SortState sortOrder = SortState.DocumentDateTimeDesc)
        {
            IQueryable<Document> documents = db.Documents;
            
            if (department != null && department != 0)
            {
                documents = documents.Where(f => f.DepartmentId == department);
            }
            
            if (id != null && id != 0)
            {
                Document document = documents.Where(f => f.Id == id).FirstOrDefault();
                document.RoleId = role;
                document.Role = db.Roles.Find(role).Type;
                document.Stage = s;
                db.Update(document);
                db.SaveChanges();
            }

            switch (sortOrder)
            {
                case SortState.DocumentTypeDesc:
                    documents = documents.OrderByDescending(s => s.DocumentType);
                    break;
                case SortState.DocumentTypeAsc:
                    documents=documents.OrderBy(s => s.DocumentType);
                    break;
                case SortState.DepartmentDesc:
                    documents = documents.OrderByDescending(s => s.Department);
                    break;
                case SortState.DepartmentAsc:
                    documents=documents.OrderBy(s => s.Department);
                    break;
                case SortState.StageDesc:
                    documents=documents.OrderByDescending(s => s.Stage);
                    break;
                case SortState.StageAsc:
                    documents=documents.OrderBy(s => s.Stage);
                    break;
                case SortState.DocumentNumberDesc:
                    documents = documents.OrderByDescending(s => s.DocumentNumber);
                    break;
                case SortState.DocumentNumberAsc:
                    documents=documents.OrderBy(s => s.DocumentNumber);
                    break;
                case SortState.BarcodeDesc:
                    documents=documents.OrderByDescending(s => s.Barcode);
                    break;
                case SortState.BarcodeAsc:
                    documents=documents.OrderBy(s => s.Barcode);
                    break;
                case SortState.DocumentDateTimeAsc:
                    documents = documents.OrderBy(s => s.DocumentDateTime);
                    break;
                default:
                    documents = documents.OrderByDescending(s => s.DocumentDateTime);
                    break;
            }

            List<SelectListItem> departments = new List<SelectListItem>();
            departments.Add(new SelectListItem{Text="Все",Value="0",Selected = true});
            foreach (Department item in db.Departments)
            {
                departments.Add(new SelectListItem{Text=item.Name,Value=item.Id.ToString()});
            }
            ViewData["Department"] = departments;
            List<SelectListItem> roles = new List<SelectListItem>();
            foreach (Role item in db.Roles)
            {
                roles.Add(new SelectListItem{Text=item.Type,Value=item.Id.ToString()});
            }
            ViewData["role"] = roles;
            ViewBag.roleSelect = role;
            var items = await documents.ToListAsync();
            
            IndexView viewModel = new IndexView
               {
                   SortView = new SortView(sortOrder),
                   FilterView = new FilterView(db.Departments.ToList(), department),
                   Documents = items
               };
             return View(viewModel);
        }

        public IActionResult Create()
        {
            List<SelectListItem> types = new List<SelectListItem>();
            foreach (DocumentType item in db.DocumentTypes)
            {
                types.Add(new SelectListItem{Text=item.Name,Value=item.Id.ToString()});
            }
            ViewData["DocumentType"] = types;
            List<SelectListItem> departments = new List<SelectListItem>();
            foreach (Department item in db.Departments)
            {
                departments.Add(new SelectListItem{Text=item.Name,Value=item.Id.ToString()});
            }
            ViewBag.Department = departments;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Document document)
        {
            document.DateTimeChange = DateTime.Now;
            document.DocumentTypeId = Convert.ToInt32(Request.Form["DocumentType"]);
            document.DocumentType = db.DocumentTypes.Find(Convert.ToInt32(Request.Form["DocumentType"])).Name;
            document.DepartmentId = Convert.ToInt32(Request.Form["Department"]);
            document.Department = db.Departments.Find(Convert.ToInt32(Request.Form["Department"])).Name;
            db.Documents.Add(document);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id!=null)
            {
                Document document = await db.Documents.FirstOrDefaultAsync(p=>p.Id==id);
                if (document != null)
                {
                    List<SelectListItem> types = new List<SelectListItem>();
                    foreach (DocumentType item in db.DocumentTypes)
                    {
                        types.Add(new SelectListItem {Text = item.Name, Value = item.Id.ToString()});
                    }
                    int index = types.FindIndex(l => l.Value == document.DocumentTypeId.ToString());
                    types[index].Selected = true;
                    ViewData["DocumentType"] = types;
                    List<SelectListItem> departments = new List<SelectListItem>();
                    foreach (Department item in db.Departments)
                    {
                        departments.Add(new SelectListItem {Text = item.Name, Value = item.Id.ToString()});
                    }
                    index = departments.FindIndex(l => l.Value == document.DepartmentId.ToString());
                    departments[index].Selected = true;
                    ViewBag.Department = departments;
                    stage = document.Stage;
                    return View(document);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Document document)
        {
            document.DateTimeChange = DateTime.Now;
            document.Stage = stage;
            document.DocumentTypeId = Convert.ToInt32(Request.Form["DocumentType"]);
            document.DocumentType = db.DocumentTypes.Find(Convert.ToInt32(Request.Form["DocumentType"])).Name;
            document.DepartmentId = Convert.ToInt32(Request.Form["Department"]);
            document.Department = db.Departments.Find(Convert.ToInt32(Request.Form["Department"])).Name;
            db.Documents.Update(document);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Document document = await db.Documents.FirstOrDefaultAsync(p => p.Id == id);
                if (document != null)
                    return View(document);
            }
            return NotFound();
        }
 
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Document document = await db.Documents.FirstOrDefaultAsync(p => p.Id == id);
                if (document != null)
                {
                    db.Documents.Remove(document);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}