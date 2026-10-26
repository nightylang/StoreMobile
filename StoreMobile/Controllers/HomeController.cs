using Microsoft.AspNetCore.Mvc;
using StoreMobile.Models;
using System.Diagnostics;

namespace StoreMobile.Controllers
{
    public class HomeController : Controller
        {
                public IActionResult Index()
                        {
                                    return View();
                                            }

                                                    public IActionResult Products()
                                                            {
                                                                        return View();
                                                                                }

                                                                                        public IActionResult About()
                                                                                                {
                                                                                                            return View();
                                                                                                                    }

                                                                                                                            public IActionResult Login()
                                                                                                                                    {
                                                                                                                                                return View();
                                                                                                                                                        }

                                                                                                                                                                public IActionResult Profile(UserModel id)
                                                                                                                                                                        {
                                                                                                                                                                                    return View(id);
                                                                                                                                                                                            }


                                                                                                                                                                                                    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
                                                                                                                                                                                                            public IActionResult Error()
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            