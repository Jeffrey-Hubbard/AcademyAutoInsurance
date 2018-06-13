using AcademyAutoInsurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace AcademyAutoInsurance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Quote(int QueryTypeID, string firstName, string lastName, string emailAddress,
                                    DateTime dateOfBirth, string DUI, string fullCoverage,
                                    int carYear, string carMake, string carModel,
                                    int speedingTickets)
        {
            using (QuoteDBEntities db = new QuoteDBEntities())
            {
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

                var quote = new Quote();
                quote.FirstName = firstName;
                quote.LastName = lastName;
                quote.EmailAddress = emailAddress;
                quote.DateOfBirth = dateOfBirth;
                quote.DUI = Convert.ToBoolean( DUI);
                quote.FullCoverage = Convert.ToBoolean( fullCoverage);
                quote.CarYear = carYear;
                quote.CarMake = ti.ToTitleCase( carMake);
                quote.CarModel = ti.ToTitleCase( carModel);
                quote.SpeedingTickets = speedingTickets;


                double quoteValue = Convert.ToDouble( db.Costs.Where(x => x.Id == QueryTypeID).First().BaseCost) ; // start with the base cost
                if (quote.DateOfBirth > DateTime.Now.AddYears(-18))
                {
                    quoteValue += Convert.ToDouble(db.Costs.Where(x => x.Id == QueryTypeID).First().Under18YO); // add extra if younger than 18 y.o.
                }
                if (quote.DateOfBirth > DateTime.Now.AddYears(-25))
                {
                    quoteValue += Convert.ToDouble(db.Costs.Where(x => x.Id == QueryTypeID).First().Under25YO); // add extra if younger than 25 y.o.
                }
                if (quote.DateOfBirth < DateTime.Now.AddYears(-100))
                {
                    quoteValue += Convert.ToDouble(db.Costs.Where(x => x.Id == QueryTypeID).First().Over100YO);
                }
                if (quote.CarYear < db.Costs.Where(x => x.Id == QueryTypeID).First().OldCarYear)
                {
                    quoteValue += Convert.ToDouble(db.Costs.Where(x => x.Id == QueryTypeID).First().OldCarCost);
                }
                if (quote.CarYear > db.Costs.Where(x => x.Id == QueryTypeID).First().NewCarYear)
                {
                    quoteValue += Convert.ToDouble(db.Costs.Where(x => x.Id == QueryTypeID).First().NewCarCost);
                }

                var makeExtraCost = (from b in db.MakeCosts
                                    where b.Make == quote.CarMake
                                    select b.MakeCost1).ToList().Max();

                if (makeExtraCost != null)
                {
                    quoteValue += Convert.ToDouble(makeExtraCost);
                }

                var modelExtraCost = (from b in db.ModelCosts
                                     where b.Model == quote.CarModel
                                     select b.ModelCost1).ToList().Max();

                if (modelExtraCost != null )
                {
                    quoteValue += Convert.ToDouble( modelExtraCost);
                }

                quoteValue += (Convert.ToDouble( db.Costs.Where(x => x.Id == QueryTypeID).First().PerSpeedingTicketCost) * Convert.ToDouble( quote.SpeedingTickets));

                if (quote.DUI == true)
                {
                    quoteValue += quoteValue * Convert.ToDouble(db.Costs.Where(x => x.Id == QueryTypeID).First().DUIPercent / 100);
                }

                if (quote.FullCoverage == true)
                {
                    quoteValue += quoteValue * Convert.ToDouble(db.Costs.Where(x => x.Id == QueryTypeID).First().FullCoveragePercent) / 100;
                }

                quote.QuotedCost = Math.Round( Convert.ToDecimal( quoteValue), 2);

                db.Quotes.Add(quote);
                db.SaveChanges();

                ViewBag.QuotedCost = quote.QuotedCost;
            }




            return View();
        }

    }
}