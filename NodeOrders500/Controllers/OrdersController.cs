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
        // Orders database entity
        NodeOrders500Entities ordersDB = new NodeOrders500Entities();

        // returns list of salespeople by first and last name
        public IEnumerable<object> GetSalesPeople()
        {
            // selects each salesperson
            var salesPeople = from people in ordersDB.SalesPersonTables
                              select new { people.FirstName, people.LastName };
            // returns list of sales people
            return salesPeople.ToList();
        }
        
        // returns list of stores by cities
        public IEnumerable<object> GetStores()
        {
            // selects each store by city
            var cities = from stores in ordersDB.StoreTables
                              select new { stores.City };
            // returns list of cities
            return cities.ToList();
        }

        // returns sum of total sales for a selected employee
        public int GetEmpSales(string id)
        {
            // query selects all orders where salesperson last name is contained in parameter
            var cities = (from sales in ordersDB.Orders
                          where id.Contains(sales.SalesPersonTable.LastName)
                          select new { sales.pricePaid });
            // returns sum of total sales for that salesperson
            return cities.AsEnumerable().Sum(sales => sales.pricePaid);
        }

        // returns sum of total sales for a selected store
        public int GetStoreSales(string id)
        {
            // query selects all orders where city name matches parameter
            var cities = (from sales in ordersDB.Orders
                          where sales.StoreTable.City == id
                          select new { sales.pricePaid });
            // returns sum of total sales for that store
            return cities.AsEnumerable().Sum(sales => sales.pricePaid);
        }

        // returns a list of each store and their sales count from most to least
        public IEnumerable<object> GetHighestSales()
        {
            // tuple list to contain parsed data from queries
            List<Tuple<int, int, string>> storeList = new List<Tuple<int, int, string>>();

            // query to select and list all distinct stores and their cities
            var sIDs = (from s in ordersDB.StoreTables
                        select new { s.storeID, s.City }).Distinct();

            // loops through the distinct sIDs list
            foreach (var i in sIDs)
            {
                // appends new tuple with store ID and city name to the list
                // also includes a sales count of 0, which will be modified by next query
                storeList.Add(new Tuple<int, int, string>(i.storeID, 0, i.City));
            }

            // query to select all orders above $13 and creates a list with their storeID
            var sales1 = (from sales in ordersDB.Orders
                          where sales.pricePaid > 13
                          select new { sales.pricePaid, sales.storeID });

            // loops through the orders query
            foreach(var s in sales1)
            {
                var i = 0;
                // finds the matching store ID
                while(s.storeID != storeList[i].Item1)
                {
                    i++;
                }
                // modifies store tuple with the total sales count
                storeList[i] = new Tuple<int, int, string>(storeList[i].Item1, storeList[i].Item2 + 1, storeList[i].Item3);
            }
            // sorts the store list by sales count
            storeList.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            // returns the sorted store list
            return storeList;
        }
    }
}