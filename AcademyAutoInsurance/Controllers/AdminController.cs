using AcademyAutoInsurance.Models;
using AcademyAutoInsurance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcademyAutoInsurance.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (QuoteDBEntities db = new QuoteDBEntities())
            {
                var quotes = db.Quotes.ToList();

                var quoteVms = new List<QuoteVm>();
                foreach (var quote in quotes)
                {
                    var quoteVm = new QuoteVm();
                    quoteVm.Id = quote.Id;
                    quoteVm.firstName = quote.FirstName;
                    quoteVm.lastName = quote.LastName;
                    quoteVm.emailAddress = quote.EmailAddress;
                    quoteVm.quotedCost = Convert.ToDouble(quote.QuotedCost);

                    quoteVms.Add(quoteVm);
                }


                //
                return View(quoteVms);
            }
        }
    }
}