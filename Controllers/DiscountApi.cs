using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    [ApiController]
    public class DiscountApi : ControllerBase
    {
        private readonly INorthwindRepository _db;

        public DiscountApi(INorthwindRepository db)
        {
            _db = db;
        }
        //todo: Add validation attributes
        [HttpGet]
        [Route("/api/discounts/")]
        public IActionResult Get() =>
            _db.Discounts is null
                ? (IActionResult) NotFound()
                : Ok(_db.Discounts);

        [HttpGet]
        [Route("/api/discounts/{id}/")]
        public IActionResult Get(int id) =>
            _db.Discounts.Any(d => d.DiscountId == id)
                ? Ok(_db.Discounts.First(d => d.DiscountId == id))
                : (IActionResult) NotFound();

        [HttpPost]
        [Route("/api/addDiscount/")]
        public IActionResult Add([FromBody] Discount model)
        {
            if (ValidateDiscount(model) || model.DiscountId != 0)
                return BadRequest(model);
            var counter = 0;
            int rand;
            while (true)
            {
                if (counter++ == 10) return StatusCode(400);
                rand = new Random().Next(1, 9999);
                if (!_db.Discounts.Any(d => d.Code == rand)) break;
            }

            model.Code = rand;
            _db.AddDiscount(model);
            return Redirect("/Discount/Index");
        }

        [HttpPost]
        [Route("/api/editDiscount/")]
        public IActionResult Edit(Discount model)
        {
            if (ValidateDiscount(model) || model.DiscountId is 0)
                return BadRequest(model);

            var cur = _db.Discounts.First(d => d.DiscountId == model.DiscountId);
            var props = cur.GetType().GetProperties();
            var toUpd = new Discount();
            foreach (var prop in props)
            {
                var curVal = prop.GetValue(cur);
                if (prop.Name is "DiscountId" ||
                    prop.Name is "Code")
                {
                    prop.SetValue(toUpd, curVal);
                    continue;
                }

                var modVal = prop.GetValue(model);
                prop.SetValue(toUpd,
                    modVal != curVal ? modVal : curVal);
            }

            _db.EditDiscount(toUpd);
            return Redirect("/Discount/Index");
        }

        [HttpGet]
        [Route("/api/deleteDiscount/{id}/")]
        public IActionResult Delete(int id)
        {
            if (_db.Discounts.Any(d => d.DiscountId == id))
                _db.DeleteDiscount(id);
            else
                return NotFound();

            return Redirect("/Discount/Index");
        }

        private bool ValidateDiscount(Discount model) =>
            string.IsNullOrWhiteSpace(model.Title)       ||
            string.IsNullOrWhiteSpace(model.Description) ||
            model.StartTime > DateTime.Now               ||
            model.EndTime   < DateTime.Now               ||
            model.EndTime   < model.StartTime            ||
            model.DiscountPercent is 0M                  ||
            model.ProductId is 0;
    }
}