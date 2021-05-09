using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NodeOrders500.Controllers
{
    public class OrdersController : ApiController
    {
        // GET: Orders
        NodeOrders500Entities ordersDB = new NodeOrders500Entities();
        
        public IEnumerable<object> GetSalesPeople()
        {
            var salesPeople = from people in ordersDB.SalesPersonTables
                              select new { people.FirstName, people.LastName };

            var salesPeopleData = salesPeople;

            var x = salesPeopleData.ToList();
            return x;
        }
        
        public IEnumerable<object> GetStores()
        {
            var cities = from stores in ordersDB.StoreTables
                              select new { stores.City };

            var citiesData = cities;

            var x = citiesData.ToList();
            return x;
        }

        public int GetEmpSales(string id)
        {

            var cities = (from sales in ordersDB.Orders
                         where id.Contains(sales.SalesPersonTable.LastName)
                          select new { sales.pricePaid });

            return cities.AsEnumerable().Sum(sales => sales.pricePaid);
        }

        public int GetStoreSales(string id)
        {

            var cities = (from sales in ordersDB.Orders
                          where sales.StoreTable.City == id
                          select new { sales.pricePaid });

            return cities.AsEnumerable().Sum(sales => sales.pricePaid);
        }

        public IEnumerable<object> GetHighestSales()
        {
            List<Tuple<int, int, string>> storeList = new List<Tuple<int, int, string>>();

            var sIDs = (from s in ordersDB.StoreTables
                        select new { s.storeID, s.City }).Distinct();

            foreach (var i in sIDs)
            {
                storeList.Add(new Tuple<int, int, string>(i.storeID, 0, i.City));
            }

            var sales1 = (from sales in ordersDB.Orders
                          where sales.pricePaid > 13
                          select new { sales.pricePaid, sales.storeID });

            foreach(var s in sales1)
            {
                var i = 0;
                while(s.storeID != storeList[i].Item1)
                {
                    i++;
                }
                storeList[i] = new Tuple<int, int, string>(storeList[i].Item1, storeList[i].Item2 + 1, storeList[i].Item3);
            }
            var result = storeList.OrderBy(x => x.Item2).ToList();
            return result;
        }
    }
}