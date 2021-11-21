using CohortTestWebAPI.Analitycs;
using CohortTestWebAPI.DAL;
using CohortTestWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CohortTestWebAPI.Controllers {


    [ApiController]
    [Route("api/analitycs/[controller]/{year:int}")]
    public class CohortController : Controller {
        private IUnitOfWork uow;
        public CohortController(IUnitOfWork uow) {
            this.uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int year) {
            var orders = await uow.Orders.GetAll(
                o => o.DateAdded.Year == year,
                o => o.OrderBy(order => order.PhoneNumber).ThenBy(order => order.DateAdded)
            );
            if (orders.Count() == 0) {
                return Content($"data not found");
            }
            var analyzer = new CohortAnalyzer<Order>(orders);
            analyzer.Proccess();
            string data = (string) analyzer.Result.Data;
            string[] dataRows = data.Split(Environment.NewLine);

            string[] title = new string[13];
            title[1] = $"{year}";
            string[] header = new string[] { "", "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь", };

            StringBuilder report = new StringBuilder();
            report.AppendJoin(";", title);
            report.AppendLine();

            report.AppendJoin(";", header);

            for (int i = 0; i < dataRows.Length; i++) {
                report.AppendLine();
                report.Append($"{ header[i + 1]};");
                report.Append(dataRows[i]);
            }

            return Ok(report.ToString());
        }
    }
}