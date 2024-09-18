using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagment.Models;
using EmployeeManagment.ViewModels;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using NuGet.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol;
using System.Text.Json;
using Azure.Core;
using static System.Reflection.Metadata.BlobBuilder;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.NetworkInformation;
using Mono.TextTemplating;
using Azure;
using Microsoft.EntityFrameworkCore.Metadata;
using Humanizer;
using System.Composition;
namespace EmployeeManagment.Controllers
{
    // [Authorize]
    public class HomeController : Controller
    {

        private IEmployeeRepository _employeeRepository;
        //private  IHostEnvironment hostEnvironment;
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly IAddItemRepository addItemRepository;
        private readonly IitemtypeRepository iitemtypeRepository;
        private readonly IFullItemRepository fullItemRepository;
        private readonly IExpensesRespository expensesRespository1;

        //private readonly ILogger logger;
        public HomeController(IEmployeeRepository employeeRepository,
                             IHostingEnvironment hostingEnvironment, IAddItemRepository addItemRepository,
                             IitemtypeRepository iitemtypeRepository, IFullItemRepository fullItemRepository, IExpensesRespository expensesRespository)
        {
            _employeeRepository = employeeRepository;
            _hostEnvironment = hostingEnvironment;
            this.addItemRepository = addItemRepository;
            this.iitemtypeRepository = iitemtypeRepository;
            this.fullItemRepository = fullItemRepository;
            this.expensesRespository1 = expensesRespository;
        }
        [HttpGet]
        public IActionResult Cahser()
        {
            return View();

        }
        public IActionResult CahserGetDetails()
        {
            var model = fullItemRepository.GetAll();

            return Json(model);
        }



      //  [BindProperty]
      //  public bindprModel bindprModel { get; set; } 
        [AllowAnonymous]
        //[HttpPost]
        //public IActionResult a()
        //{
        //    return Ok($"{this.bindprModel.name},{this.bindprModel.id},{this.bindprModel.address}");

        //   // return "hello";
        //}
    
    public IActionResult addCahser(string[] ItemName, string[] itemtype, string[] itemprice, string[] Qut, string S_table)
        {
            var rand = new Random();
            var codege = rand.Next(10000, 1000000);
            if (ItemName[0] != null || itemtype[0] != null || itemprice[0] != null || Qut[0] != null)
            {
                for (int i = 0; i < ItemName.Length; i++)
                {
                    int total = int.Parse(itemprice[i]) * int.Parse(Qut[i]);

                    FullItem fullItems = new FullItem
                    {
                        ItemName = ItemName[i],
                        ItemType = itemtype[i],
                        ItemPrice = itemprice[i],
                        Qunantity = Qut[i],
                        Total = Convert.ToString(total),
                        T_number = S_table,
                        Code = Convert.ToString(codege),
                        Status = "0",
                    };
                    fullItemRepository.add(fullItems);
                }
                return Json("response");
            }
            return View();
        }
        public IActionResult Cahserdelete(int id)
        {
            Models.FullItem fullItem = fullItemRepository.GetFull(id);
            fullItemRepository.delete(id);
            return RedirectToAction("Cahser");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ItemType()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ItemType(ItemTypecreateViewModel model)
        {

            if (ModelState.IsValid)
            {

                ItemType itemTypes = new ItemType
                {
                    Name = model.Name,

                };
                iitemtypeRepository.add(itemTypes);
                return RedirectToAction("ItemTypeDetails");
            }
            return View();

        }
        public IActionResult ItemTypeDetails()
        {

            return View();

        }
        public IActionResult ItemTypeGetDetails()
        {
            var model = iitemtypeRepository.GetAll();
            return Json(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ItemTypeEdit(int id)
        {

            ItemType itemTypee = iitemtypeRepository.GettypeItem(id);

            ItemTypeEditViewModel itemTypeEditViewModel = new ItemTypeEditViewModel()
            {
                Name = itemTypee.Name,

            };

            return View(itemTypeEditViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ItemTypeEdit(ItemTypeEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                ItemType itemType = iitemtypeRepository.GettypeItem(model.Id);
                itemType.Name = model.Name;
                iitemtypeRepository.update(itemType);
                return RedirectToAction("ItemTypeDetails");
            }
            return View();

        }

        public IActionResult ItemTypeDelete(int id)
        {


            ItemType itemdelete = iitemtypeRepository.GettypeItem(id);
            iitemtypeRepository.delete(id);
            return RedirectToAction("ItemTypeDetails");

        }
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            //return _employeeRepository.GetEmployee(1).Department
            AddItemItemTypeViewModel model = new AddItemItemTypeViewModel();
            model.typeItemlist = iitemtypeRepository.GetAll();
            model.addItemlist = addItemRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult additem()
        {
            ItemViewItemCreateViewModel model = new ItemViewItemCreateViewModel();
            model.ItemType = iitemtypeRepository.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddItem(ItemCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.AddItem newitem = new Models.AddItem
                {
                    ItemName = model.ItemName,
                    ItemPrice = model.ItemPrice,
                    ItemType = model.ItemType,
                };
                addItemRepository.add(newitem);
                return Json("Data has saved");

            }
            else
            {
                return Json("Data hasn,t saved");

            }

        }

        public IActionResult ItemDetails()
        {

            return View();

        }
        public IActionResult GetItemDetails()
        {
            var model = addItemRepository.GetAll();
            return Json(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ItemEdit(int id)
        {
            Models.AddItem addItem = addItemRepository.GetItem(id);

            ItemEditViewModel itemEditViewModel = new ItemEditViewModel()
            {
                ItemName = addItem.ItemName,
                ItemType = addItem.ItemType,
                ItemPrice = addItem.ItemPrice,

            };
            return View(itemEditViewModel);

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ItemEdit(ItemEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.AddItem addItem = addItemRepository.GetItem(model.Id);
                addItem.ItemName = model.ItemName;
                addItem.ItemType = model.ItemType;
                addItem.ItemPrice = model.ItemPrice;
                addItemRepository.update(addItem);
                return Json("response");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ItemDetete(int id)
        {
            Models.AddItem addItem = addItemRepository.GetItem(id);
            addItemRepository.delete(id);
            return RedirectToAction("Itemdetails");
        }
        public IActionResult Home()
        {
            var model = _employeeRepository.GetAll();
            return View(model);

        }
        [Route("Home/Details/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            //throw new NotImplementedException("Details View is not found");
            //logger.LogTrace("logtrace");
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetuailsViewModel homeDetuailsViewModel = new HomeDetuailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details",
            };
            return View(homeDetuailsViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);

            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,

            };
            return View(employeeEditViewModel);
        }

        public IActionResult Edit(EmployeeEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                _employeeRepository.update(employee);
                return RedirectToAction("Index");
            }

            return View();

        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                string UniuqeFileName = null;
                if (model.Photo != null)
                {

                    string UploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                    UniuqeFileName = Guid.NewGuid().ToString() + "-" + model.Photo.FileName;
                    string filepath = Path.Combine(UniuqeFileName, UploadsFolder);
                    //model.Photo.CopyTo(new FileStream(filepath,FileMode.Create));
                }
                Employee newemployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    photopat = UniuqeFileName
                };
                _employeeRepository.add(newemployee);
                return RedirectToAction("Details", new { id = newemployee.Id });
            }
            return View();


        }

        [HttpGet]
        public IActionResult Expenses()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Expenses(ExpensesCreateViewModel model)
        {
            if (model.ExName !=null)
            {
                Expenses expenses = new Expenses
                {
                    ExName = model.ExName,
                    ExValue = model.ExValue,
                    ExType = model.ExType,
                    Exdate =DateTime.Now.ToString("yyyy-MM-d").FormatWith("yyyy-MM-d"),
            };
                expensesRespository1.add(expenses);
                return Json("Data has saved");

            }
            else
            {
                return Json("Data hasn,t saved");

            }


        }

        [HttpGet]
        public IActionResult GetItemExpenses()
        {
            var model = expensesRespository1.GetExpenses();
            return Json(model);

        }
        [HttpGet]
        public IActionResult ExpensesDelete(int id)
        {
            Expenses expenses=expensesRespository1.GetItem(id);
            expensesRespository1.delete(id);
            return RedirectToAction("Expenses");
        }
        [HttpGet]
        public IActionResult ExpensesEdit(int id )
        {
            Expenses expenses = expensesRespository1.GetItem(id);
            ExpensesEditViewModel expensesEditViewModel = new ExpensesEditViewModel()
            {
             ExName= expenses.ExName,
             ExType= expenses.ExType,   
             ExValue= expenses.ExValue,
             Exdate= expenses.Exdate
            };
            return View(expensesEditViewModel);
        }

        [HttpPost]
        public IActionResult ExpensesEdit(ExpensesEditViewModel model)
        {
            if (model.ExName!=null)
            {
                Expenses expenses = expensesRespository1.GetItem(model.id);
                expenses.ExName = model.ExName;
                expenses.ExValue = model.ExValue;
                expenses.ExType = model.ExType;
                expensesRespository1.update(expenses);
                return RedirectToAction("Expenses");

            }
            return View();

        }
        [HttpGet]
        public IActionResult ReportOfCasher()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetreportDeatils()
        {
            var model1 = fullItemRepository.GetAll();
            return Json(model1);
        }

        [HttpGet]
        public IActionResult RportOfExpenses()
        {

            return View();
        }




    }
}
