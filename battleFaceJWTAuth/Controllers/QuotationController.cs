using battleFaceDataAccess;
using battleFaceDataAccess.Models;
using battleFaceJWTAuth.Helpers;
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
        private ApplicationDBContext _db;
        public QuotationController(ApplicationDBContext context)
        {
            _db = context;
        }
        
        public IActionResult Index()
        {
            QuotationViewModel model = new QuotationViewModel();
            model.CurrencyCodes = StringHelper.GetCurrencyCodes();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Index([FromBody] QuotationViewModel model)
        {
            var errors = model.Validate();
            
            if(errors.Count > 0)
            {
                return BadRequest(errors);
            }

            QuoteResult result = model.GetResult();
            result.currency_id = model.currency_id;
            try
            {
                Quote quote = new Quote { Amount = result.total, UserId = HttpContext.User.Identity.Name };
                _db.Add(quote);
                _db.SaveChanges();
                result.quotation_id = quote.Id;
                return new ObjectResult(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
