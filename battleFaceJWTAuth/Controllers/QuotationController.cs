using battleFaceJWTAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleFaceJWTAuth.Controllers
{
    [Authorize]
    public class QuotationController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<QuoteResult> Index(QuotationViewModel model)
        {
            return new QuoteResult();
        }
    }
}
