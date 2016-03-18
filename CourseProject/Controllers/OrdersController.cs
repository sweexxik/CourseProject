using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Repositories;

namespace CourseProject.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        private readonly IUnitOfWork db = new EfUnitOfWork();

        [Authorize]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var queryString = Request.GetQueryNameValuePairs();

            String userName = String.Empty;

            foreach (var pair in queryString)
            {
                if (pair.Key == "username")
                    userName = pair.Value;
            }

            IEnumerable<Creative> creatives;

            var user = await db.FindUser(userName);

            creatives = db.Creatives.Find(x=>x.UserId == user.Id.ToString());

          
            return Ok(creatives);
         

            //return Ok(Order.CreateOrders());
        }

    }

    #region Helpers

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCity { get; set; }
        public Boolean IsShipped { get; set; }

        public static List<Order> CreateOrders()
        {
            var orderList = new List<Order>
            {
                new Order {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
                new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
                new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
                new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
            };

            return orderList;
        }
    }
}
#endregion