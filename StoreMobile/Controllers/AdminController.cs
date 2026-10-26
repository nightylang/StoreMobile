using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using StoreMobileV2.Data;
using StoreMobileV2.Models;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace StoreMobileV2.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[Route("admin")]
    public class AdminController : Controller
    {
        private readonly UserRepo _repoUser;
        private readonly ProductRepo _repoProd;
        private readonly TableRepo _repoTable;

        public AdminController(UserRepo repoUser, ProductRepo repoProd, TableRepo repoTable)
        {
            _repoUser = repoUser;
            _repoProd = repoProd;
            _repoTable = repoTable;
        }

        //[Route(" ")]
        public IActionResult Index()
        {
            int totalProducts = _repoProd.GetProductCount();
            int totalUsers = _repoUser.GetUserCount();

            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalProducts = totalProducts;
            
            return View();
        }

        //[Route("products")]
        public IActionResult Products()
        {
            List<ProductModel> productList = _repoProd.GetAll().ToList();
            return View(productList);
        }

        //[Route("model")]
        public IActionResult Model()
        {
            List<AllModel.Model> models = _repoTable.GetAllModel().ToList();
            return View(models);
        }

        //[Route("category")]
        public IActionResult Category()
        {
            List<AllModel.Category> models = _repoTable.GetAllCate().ToList();
            return View(models);
        }

        //[Route("users")]

        public IActionResult Users()
        {
            List<UserModel> users = _repoUser.GetAll();
            return View(users);
        }

        // User

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel user, IFormFile ImageFile)
        {
            if (string.IsNullOrWhiteSpace(user.username))
            {
                ViewBag.Error = "User name is required.";
                return View(user);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "users");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = $"user_{Guid.NewGuid()}.webp";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var inputStream = ImageFile.OpenReadStream())
                {
                    using (var image = await SixLabors.ImageSharp.Image.LoadAsync(inputStream))
                    {
                        var encoder = new WebpEncoder { Quality = 80 };
                        await image.SaveAsync(filePath, encoder);
                    }
                }

                user.Image = uniqueFileName;
            }
            var newUser = new UserModel
            {
                email = user.email,
                username = user.username,
                password = user.password, // Ideally hashed before saving
                ID_role = "2",               // Default role (e.g., 2 = standard user)
                CreatedAt = DateTime.UtcNow
            };
            var hasher = new PasswordHasher<UserModel>();
            newUser.password = hasher.HashPassword(newUser, user.password);
            _repoUser.Create(newUser);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            UserModel? user = _repoUser.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.username))
            {
                ViewBag.Error = "User name is required.";
                return View(user);
            }
            _repoUser.Update(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            UserModel? user = _repoUser.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteUserConfirmed(int id)
        {
            _repoUser.Delete(id);
            return RedirectToAction("Index");
        }


        // Product 

        [HttpPost]
        public async Task<IActionResult> CreateProd(ProductModel prod, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "product");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = $"product_{Guid.NewGuid()}.webp";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var inputStream = ImageFile.OpenReadStream())
                {
                    using (var image = await SixLabors.ImageSharp.Image.LoadAsync(inputStream))
                    {
                        var encoder = new WebpEncoder { Quality = 80 };
                        await image.SaveAsync(filePath, encoder);
                    }
                }

                prod.id_image = uniqueFileName;
            }
            _repoProd.Create(prod);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateProd()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditProd(int id)
        {
            ProductModel prod = _repoProd.GetById(id);
            if (prod == null)
            {
                return RedirectToAction("Index");
            }
            var allModelsList = _repoTable.GetAllModel().ToList();
            var allCateList = _repoTable.GetAllCate().ToList();

            var modelTuple = Tuple.Create(prod, allModelsList, allCateList);
            return View(modelTuple);
        }

        [HttpPost]
        public IActionResult EditProd(ProductModel prod)
        {
            if (string.IsNullOrWhiteSpace(prod.product_name))
            {
                ViewBag.Error = "Product name is required.";
                var allModelsList = _repoTable.GetAllModel().ToList();

                var modelTuple = Tuple.Create(prod, allModelsList);
                return View(modelTuple);
            }
            _repoProd.Update(prod);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteProd(int id)
        {
            ProductModel? prod = _repoProd.GetById(id);
            if (prod == null)
            {
                return NotFound();
            }
            return View(prod);
        }

        [HttpPost]
        public IActionResult DeleteProdConfirmed(int id)
        {
            _repoProd.Delete(id);
            return RedirectToAction("Index");
        }

        // Model 

        [HttpPost]
        public async Task<IActionResult> CreateModel(ProductModel prod, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "product");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = $"product_{Guid.NewGuid()}.webp";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var inputStream = ImageFile.OpenReadStream())
                {
                    using (var image = await SixLabors.ImageSharp.Image.LoadAsync(inputStream))
                    {
                        var encoder = new WebpEncoder { Quality = 80 };
                        await image.SaveAsync(filePath, encoder);
                    }
                }

                prod.id_image = uniqueFileName;
            }
            _repoProd.Create(prod);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateModel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditModel(int id)
        {
            ProductModel prod = _repoProd.GetById(id);
            if (prod == null)
            {
                return RedirectToAction("Index");
            }
            var allModelsList = _repoTable.GetAllModel().ToList();
            var allCateList = _repoTable.GetAllCate().ToList();

            var modelTuple = Tuple.Create(prod, allModelsList, allCateList);
            return View(modelTuple);
        }

        [HttpPost]
        public IActionResult EditModel(ProductModel prod)
        {
            if (string.IsNullOrWhiteSpace(prod.product_name))
            {
                ViewBag.Error = "Product name is required.";
                var allModelsList = _repoTable.GetAllModel().ToList();

                var modelTuple = Tuple.Create(prod, allModelsList);
                return View(modelTuple);
            }
            _repoProd.Update(prod);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteModel(int id)
        {
            ProductModel? prod = _repoProd.GetById(id);
            if (prod == null)
            {
                return NotFound();
            }
            return View(prod);
        }

        [HttpPost]
        public IActionResult DeleteModelConfirmed(int id)
        {
            _repoProd.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(AllModel.Category cates)
        {
            if (string.IsNullOrWhiteSpace(cates.Name))
            {
                ViewBag.Error = "category name is required.";
                return View(cates);
            }
            _repoTable.CreateCategory(cates);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
