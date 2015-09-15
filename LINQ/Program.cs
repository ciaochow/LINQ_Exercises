using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace LINQ
{
    internal class Program
    {
        /* Practice your LINQ!
         * You can use the methods in Data Loader to load products, customers, and some sample numbers
         * 
         * NumbersA, NumbersB, and NumbersC contain some ints
         * 
         * The product data is flat, with just product information
         * 
         * The customer data is hierarchical as customers have zero to many orders
         */

        private static void Main()
        {
            //E01_PrintOutOfStock();
            //E02_InStockMoreThan3();
            //E03_WAcustnameorder();
            //E04_NamesOfProducts();
            //E05_UnitPriceIncrease25();
            //E06_ProductNameUpperCase();
            //E07_EvenNoUnitsInStock();
            //E08_NewSequenceOfProducts();
            //E09_NumbersBCReturnsAllPairs();
            //E10_TotalLessThan500();
            //E11_First3Elements();
            //E12_3ordersWA();
            //E13_Skip3NumbersA();
            //E14_Skip2OrdersFromWACustomers();
            //E15_NumbersCLessThan6();
            //E16_NumbersCLessThanArrayLength();
            //E17_NumbersCMod3();
            //E18_ProductsAlphaByName();
            //E19_UnitsInStockDescending();
            //E20_SortProductList();
            //E21_ReverseNumbersC();
            //E22_NumbersCMod5();
            //E23_ProductsbyCategory();
            //E24_OrdersByYearMonth();
            //E25_ProdCategoryNames();
            //E26_UniqueNumbersAB();
            //E27_SharedValuesAB();
            //E28_NumANotInNumB();
            //E29_ProductID12();
            //E30_IsProductID789Valid();
            //E31_OutOfStock();      
            //E32_NumbersLessThan9();
            //E33_AllProductsInStock();
            //E34_NumberOfOdds();
            //E35_CountOfOrders();
            //E36_CategoryProductCount();
            //E37_DisplayTotalUnits();
            //E38_DisplayLowestPriceProduct();
            //E39_DisplayHighestPriceProduct();
            //E40_DisplayAvgPriceofProduct();
            Console.ReadLine();
        }
        // Make a query that returns all pairs of numbers from both arrays such that the number
        // from numbersB is less than the number from numbersC.

        private static void E09_NumbersBCReturnsAllPairs()
        {
            //var arrayB = DataLoader.NumbersB;
            //var arrayC = DataLoader.NumbersC;
            var results = DataLoader.NumbersB.Select((n, i) => new
            { B = n, C = DataLoader.NumbersC[i]})
            .Where(item => item.B < item.C);

            foreach (var c in results)
            {
                Console.WriteLine(c.B + " , " + c.C);
            }
        }

        private static void E40_DisplayAvgPriceofProduct()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                group product by product.Category
                into g
                select new
                {
                    category = g.Key,
                    lowestpriceproduct = g.Average(p => p.UnitPrice)
                };
            foreach (var item in results)
            {
                Console.WriteLine("Category: {0} - Average Price Product: {1:F}",
                    item.category, item.lowestpriceproduct);
            }
        }

        // Display the highest priced product in each category.
        private static void E39_DisplayHighestPriceProduct()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                group product by product.Category
                into g
                select new
                {
                    category = g.Key,
                    lowestpriceproduct = g.Max(p => p.UnitPrice)
                };
            foreach (var item in results)
            {
                Console.WriteLine("Category: {0} - Highest Price Product: {1:F}",
                    item.category, item.lowestpriceproduct);
            }
        }

        // Display the lowest priced product in each category.
        private static void E38_DisplayLowestPriceProduct()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                group product by product.Category
                into g
                select new
                {
                    category = g.Key,
                    lowestpriceproduct = g.Min(p => p.UnitPrice)
                };
            foreach (var item in results)
            {
                Console.WriteLine("Category: {0} - Lowest Price Product: {1:F}",
                    item.category, item.lowestpriceproduct);
            }
        }

        // Display the total units in stock for each category.
        private static void E37_DisplayTotalUnits()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                group product by product.Category
                into g
                select new
                {
                    category = g.Key,
                    countOfUnits = g.Sum(p => p.UnitsInStock)
                };
            foreach (var item in results)
            {
                Console.WriteLine("Category: {0} - Total Units In Stock: {1}", item.category, item.countOfUnits);
            }
        }

        // Display a list of categories and the count of their products.
        private static void E36_CategoryProductCount()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                group product by product.Category
                into g
                select new
                {
                    category = g.Key,
                    countOfProducts = g.Key
                };
            Console.WriteLine("---- Categories and the count of their products ----");
            Console.WriteLine();
            foreach (var v in results.Distinct())
            {
                Console.WriteLine("Category: {0} - Product Count: {1}", v.category, v.countOfProducts);
            }
        }

        // Display a list of CustomerIDs and only the count of their orders.
        private static void E35_CountOfOrders()
        {
            var orders = DataLoader.LoadCustomers();
            var results = from customers in orders
                select new
                {
                    custid = customers.CustomerID,
                    ordercount = customers.Orders.Count()
                };
            foreach (var item in results)
            {
                Console.WriteLine("Customer ID: {0} - Number of Orders: {1}", item.custid, item.ordercount);
            }
        }

        // Count the number of odds in NumbersA.
        private static void E34_NumberOfOdds()
        {
            var arrayA = DataLoader.NumbersA;
            var result = from r in arrayA
                where (r%2 != 0)
                select r;
            foreach (var s in result)
            {
                Console.WriteLine(s);
            }
        }

        // Get a grouped a list of products only for categories that have all of their products in stock.
        private static void E33_AllProductsInStock()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                where product.UnitsInStock > 0
                group product by product.Category
                into g
                select new
                {
                    category = g.Key,
                    products = g
                };
            Console.WriteLine("Categories where all their products are in stock");
            foreach (var v in results)
            {
                Console.WriteLine(v.category);
                foreach (var c in v.products)
                {
                    Console.WriteLine("\t" + c.ProductName);
                }
            }
        }

        // Determine if NumbersB contains only numbers less than 9.
        private static void E32_NumbersLessThan9()
        {
            var arrayB = DataLoader.NumbersB;
            bool check = arrayB.Any(u => u < 9);
            foreach (var item in arrayB.Distinct())
            {
                Console.WriteLine(check);
            }
        }

        // Get a list of categories that have at least one product out of stock.
        private static void E31_OutOfStock()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                where product.UnitsInStock.Equals(0)
                select new
                {
                    uniquecategory = product.Category
                };
            Console.WriteLine("Categories with at least one product out of stock");
            foreach (var v in results.Distinct())
            {
                Console.WriteLine(v.uniquecategory);
            }
        }

        // Write code to check if ProductID 789 exists.
        private static void E30_IsProductID789Valid()
        {
            var products = DataLoader.LoadProducts();
            bool check = products.Any(x => x.ProductID == 789);
            Console.WriteLine("Does ProductID 789 exist: {0}", check);
        }

        // Select only the first product with ProductID = 12 (not a list).
        private static void E29_ProductID12()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                where product.ProductID.Equals(12)
                select new
                {
                    product.ProductName,
                    product12 = product.ProductID
                };
            foreach (var v in results)
            {
                Console.WriteLine("Product Name: {0}", v.ProductName);
                Console.WriteLine("ProductID: {0}", v.product12);
            }
        }

        // Get a list of values in NumbersA that are not also in NumbersB.
        private static void E28_NumANotInNumB()
        {
            var arrayA = DataLoader.NumbersA;
            var arrayB = DataLoader.NumbersB;
            var listexception = arrayA.Except(arrayB);
            foreach (var v in listexception)
            {
                Console.WriteLine(v);
            }
        }

        // Get a list of the shared values from NumbersA and NumbersB.
        private static void E27_SharedValuesAB()
        {
            var arrayA = DataLoader.NumbersA;
            var arrayB = DataLoader.NumbersB;
            var listcommon = arrayA.Intersect(arrayB);
            foreach (var v in listcommon)
            {
                Console.WriteLine(v);
            }
        }

        // Get a list of unique values from NumbersA and NumbersB.
        private static void E26_UniqueNumbersAB()
        {
            var arrayA = DataLoader.NumbersA;
            var arrayB = DataLoader.NumbersB;
            var resultsA = arrayA.Distinct();
            Console.WriteLine("-- NumbersA unique values --");
            foreach (var results in resultsA)
            {
                Console.WriteLine(results);
            }
            Console.WriteLine();
            Console.WriteLine("-- NumbersB unique values --");
            var resultsB = arrayB.Distinct();
            foreach (var results in resultsB)
            {
                Console.WriteLine(results);
            }
        }

        // Create a list of unique product category names.
        private static void E25_ProdCategoryNames()
        {
            var products = DataLoader.LoadProducts();
            var results = products.Select(prod => prod.Category).Distinct();
            Console.WriteLine("--- List of Unique Product Category Names ---");
            Console.WriteLine();
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }

        // Group customer orders by year, then by month.
        private static void E24_OrdersByYearMonth()
        {
            var customers = DataLoader.LoadCustomers();
            var results = from customer in customers
                from order in customer.Orders
                group order by new {order.OrderDate.Year, order.OrderDate.Month}
                into g
                select new
                {
                    key = g.Key,
                    value = g
                };
            foreach (var r in results)
            {
                Console.WriteLine(r.key.Year);
                Console.WriteLine(r.key.Month);
                foreach (var order in r.value)
                {
                    Console.WriteLine(order.OrderID);
                }
            }
        }

        // Display products by Category.
        private static void E23_ProductsbyCategory()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                orderby product.Category
                select new
                {
                    product.ProductName,
                    product.Category
                };

            Console.WriteLine("--- List of Products by Category ---");
            Console.WriteLine();
            foreach (var product in results)
            {
                Console.Write("{0}:  {1}", product.Category, product.ProductName);
                Console.WriteLine();
            }
        }

        // Display the elements of NumbersC grouped by their remainder when divided by 5.
        private static void E22_NumbersCMod5()
        {
            var numbersarray = DataLoader.NumbersC;
            var grouping = numbersarray.GroupBy(a => a%5);
            foreach (var group in grouping)
            {
                Console.WriteLine("Key: {0}", group.Key);
                foreach (var g in group)
                {
                    Console.WriteLine("Number: {0}", g);
                }
            }
        }

        // Reverse NumbersC.
        private static void E21_ReverseNumbersC()
        {
            var numbersarray = DataLoader.NumbersC;
            var results = numbersarray.Reverse();

            foreach (var r in results)
            {
                Console.WriteLine(r);
            }
        }

        // Sort the list of products, first by category, and then by unit price, from highest to lowest.
        private static void E20_SortProductList()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                orderby product.Category, product.UnitPrice descending
                select new
                {
                    product.ProductName,
                    product.Category,
                    priceofunitsorder = product.UnitPrice
                };

            Console.WriteLine("--- List of Products by Category then Unit Price (highest to lowest) ---");
            foreach (var product in results)
            {
                Console.Write("{0}:  {1} - {2:F}", product.Category, product.ProductName,
                    product.priceofunitsorder);
                Console.WriteLine();
            }
        }

        // Order products by UnitsInStock descending.
        private static void E19_UnitsInStockDescending()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                orderby product.UnitsInStock descending
                select new
                {
                    descendingunits = product.UnitsInStock,
                    product.ProductName
                };
            Console.WriteLine("---- List of Product Names by UnitsInStock (Descending) -----");
            foreach (var product in results)
            {
                Console.WriteLine();
                Console.WriteLine(product.ProductName);
                Console.WriteLine(product.descendingunits);
            }
        }

        // Order products alphabetically by name.
        private static void E18_ProductsAlphaByName()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                orderby product.ProductName
                select new
                {
                    alphabetical = product.ProductName
                };
            Console.WriteLine("-------- List of Product Names(Alphabetical) ---------");
            foreach (var product in results)
            {
                Console.WriteLine(product.alphabetical);
            }
        }


        // Return elements from NumbersC starting from the first element divisible by 3.
        private static void E17_NumbersCMod3()
        {
            var numbersarray = DataLoader.NumbersC;
            var results = numbersarray.SkipWhile(val => val%3 != 0);
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }

        // Return elements starting from the beginning of NumbersC until a number is hit that 
        // is less than its position in the array.
        private static void E16_NumbersCLessThanArrayLength()
        {
            var numbersarray = DataLoader.NumbersC;
            //var test = numbersarray;

            var results = numbersarray.TakeWhile((val, index) => val > index);

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }

        // Get all the elements in NumbersC from the beginning until an element is greater or 
        // equal to 6.
        private static void E15_NumbersCLessThan6()
        {
            var numbersarray = DataLoader.NumbersC;
            var test = numbersarray.TakeWhile(items => items < 6);
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
        }

        // Get all except the first two orders from customers in Washington.
        private static void E14_Skip2OrdersFromWACustomers()
        {
            var orders = DataLoader.LoadCustomers();
            var results = from customers in orders
                where customers.Region == "WA"
                select new
                {
                    custname = customers.CompanyName,
                    skip2orders = customers.Orders.Skip(2)
                };
            foreach (var item in results)
            {
                Console.WriteLine(item.custname);
                foreach (var c in item.skip2orders)
                {
                    Console.WriteLine(c.OrderID);
                }

            }
        }

        // Skip the first 3 elements of NumbersA.
        private static void E13_Skip3NumbersA()
        {
            var numbersarray = DataLoader.NumbersA;
            var test = numbersarray.Skip(3);
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
        }

        // Get only the first 3 orders from customers in Washington.
        private static void E12_3ordersWA()
        {
            var orders = DataLoader.LoadCustomers();
            var results = from customers in orders
                where customers.Region == "WA"
                select new
                {
                    custname = customers.CompanyName,
                    first3orders = customers.Orders.Take(3)
                };
            foreach (var item in results)
            {
                Console.WriteLine(item.custname);
                foreach (var c in item.first3orders)
                {
                    Console.WriteLine(c.OrderID);
                }

            }

        }

        // Write a query to take only the first 3 elements from NumbersA.
        private static void E11_First3Elements()
        {
            var numbersarray = DataLoader.NumbersA;
            var test = numbersarray.Take(3);
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
        }

        // Select CustomerID, OrderID, and Total where the order total is less than 500.00.
        private static void E10_TotalLessThan500()
        {
            var customers = DataLoader.LoadCustomers();
            var customerIDs = customers.Select(i => new
            {
                i.CustomerID,
                custom = i.Orders.Where(o => o.Total < 500m)
            });
            Console.Clear();
            Console.WriteLine("-------- Customers ---------");
            foreach (var customer in customerIDs)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Customer ID# {0}", customer.CustomerID);
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var c in customer.custom)
                {
                    Console.WriteLine("Order ID# {0}", c.OrderID);
                    Console.WriteLine("Total: ${0}", c.Total);
                }
            }
        }

        // Create a new sequence of products with ProductName, Category, and rename UnitPrice
        // to Price.
        private static void E08_NewSequenceOfProducts()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                select new
                {
                    product.ProductName,
                    product.Category,
                    price = product.UnitPrice
                };
            Console.WriteLine("------ Products UnitPrice Renamed to Price -------");
            Console.WriteLine();
            foreach (var item in results)
            {
                Console.WriteLine(item.ProductName);
                Console.WriteLine(item.Category);
                Console.WriteLine("Price: ${0:F}", item.price);
                Console.WriteLine();
            }
        }

        // Create a new sequence with products with even numbers of units in stock.
        private static void E07_EvenNoUnitsInStock()
        {
            var products = DataLoader.LoadProducts();
            var checkstock = products.Where(p => p.UnitsInStock%2 == 0);
            Console.WriteLine("----- Products with even numbers of units in stock -----");
            Console.WriteLine();
            foreach (var item in checkstock)
            {
                if (item.UnitsInStock == 0)
                    continue;
                Console.WriteLine("Product name {0} has {1} units in stock.", item.ProductName,
                    item.UnitsInStock);
            }
        }

        // Create a new sequence of just product names in all upper case.
        private static void E06_ProductNameUpperCase()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                select new
                {
                    UpperCase = product.ProductName.ToUpper()
                };
            Console.WriteLine("Product names in upper case:");
            foreach (var item in results)
            {
                Console.WriteLine(item.UpperCase);
            }
        }

        //Create a new sequence of products and unit prices where the unit prices are increased
        // by 25%.
        private static void E05_UnitPriceIncrease25()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                where product.UnitPrice > 0
                select new
                {
                    product.ProductName,
                    IncreasedPrice = product.UnitPrice*1.25M
                };
            foreach (var item in results)
            {
                Console.WriteLine("Product name {0} has a new unit price " +
                                  "of ${1:F} .", item.ProductName, item.IncreasedPrice);
            }
        }

        // Create a new sequence with just the names of the products.
        private static void E04_NamesOfProducts()
        {
            var products = DataLoader.LoadProducts();
            var results = products.Where(p => p.ProductName != "");
            foreach (var product in results)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        // Find all customers in Washington, print their name then their orders. (Region == "WA")
        private static void E03_WAcustnameorder()
        {
            var products = DataLoader.LoadCustomers();
            var results = products.Where(p => p.Region == "WA");
            foreach (var customer in results)
            {
                Console.WriteLine(customer.CompanyName);
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine(order.OrderID);
                }
            }
        }

        private static void E01_PrintOutOfStock()
        {
            var products = DataLoader.LoadProducts();

            var results = products.Where(p => p.UnitsInStock == 0);

            foreach (var product in results)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        // Find all products that are in stock and cost more than 3.00 per unit.
        private static void E02_InStockMoreThan3()
        {
            var products = DataLoader.LoadProducts();
            var results = products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3);

            foreach (var product in results)
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}

