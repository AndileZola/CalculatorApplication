using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CalculatorApplication
{
    /// <summary>
    /// Summary description for CalculatorService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CalculatorService : System.Web.Services.WebService
    {       
        [WebMethod]
        public string BuyCalculator(int BuyerId, int calculatorId)
        {

            using (WeighUpEntities _context = new WeighUpEntities())
            {
                var calc = _context.Calculators.SingleOrDefault(x => x.CalculatorId == calculatorId);
                calc.BuyerId = BuyerId;
                var isSold = _context.SaveChanges() > 0;
                return isSold ? $"{calc.CalculatorName} sold successfully" : $"Failed to sell {calc.CalculatorName}";
            }               
        }
        [WebMethod]
        public string AddCalculator(int sellerId,string brand, string model,decimal price)
        {
            using (WeighUpEntities _context = new WeighUpEntities())
            {
                Calculator calculator = new Calculator
                {
                    CalculatorName = brand,
                    Model = model,
                    Price = price.ToString(),
                    SellerId = sellerId,
                    CreateDate = DateTime.Now
                };
                _context.Calculators.Add(calculator);
                var isSold = _context.SaveChanges() > 0;
                return isSold ? $"{brand} added successfully" : $"Adding {brand} failed";
            }
        }
        [WebMethod]
        public string RemoveCalculator(int calculatorId)
        {
            using (WeighUpEntities _context = new WeighUpEntities())
            {
                var calc = _context.Calculators.SingleOrDefault(x => x.CalculatorId == calculatorId);
                _context.Calculators.Remove(calc);
                var isSold = _context.SaveChanges() > 0;
                return isSold ? $"{calc.CalculatorName} Removed successfully" : $"Removing {calc.CalculatorName} failed";
            }
        }
        [WebMethod]
        public string UpdateCalculator(int calculatorId,string brand, string model, decimal price)
        {
            using (WeighUpEntities _context = new WeighUpEntities())
            {
                var calc = _context.Calculators.SingleOrDefault(x => x.CalculatorId == calculatorId);
                calc.CalculatorName = brand ?? calc.CalculatorName;
                calc.Model = model ?? calc.Model;
                calc.Price = price.ToString() ?? calc.Price;
                //_context.Calculators.Attach(calc);
                var isUpdated = _context.SaveChanges() > 0;
                return isUpdated ? $"{brand} Updated successfully" : $"Removing {brand} failed";
            }
        }
    }
}
