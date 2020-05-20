using System;
using System.Linq;
using Xunit;
using StoreWebApp;
using StoreWebApp.Data;
using StoreWebApp.Models;
using Microsoft.EntityFrameworkCore;
using StoreWebApp.Business_Logic;
using StoreWebApp.Data.Repositories;

namespace Testing
{
    public class UnitTesting
    {

        /// <summary>
        /// Tests the product model by adding to it and
        /// verifying that the products and their details show up in the database
        /// Uses an In Memory database for the testing
        /// </summary>
        [Fact]
        public void AddsProductToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "AddsProductToDbTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Product bar = new Product
                {
                    StoreId = 7,
                    ProductName = "bar",
                    Inventory = 5,
                    Price = 10
                };

                db.Add(bar);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                Assert.Equal(1, context.Products.Count());

                var product1 = context.Products.Where(p => p.StoreId == 7).FirstOrDefault();
                Assert.Equal(7, product1.StoreId);
                Assert.Equal(1, product1.Id);
            }
        }


        /// <summary>
        /// Tests the store model by adding to it and
        /// verifying that the stores and their details show up in the database
        /// </summary>
        [Fact]
        public void AddsStoreToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "AddsStoreToDbTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Store location = new Store
                {
                    Location = "Maryland"
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                Assert.Equal(1, context.Stores.Count());

                var store1 = context.Stores.Where(s => s.Id == 1).FirstOrDefault();
                Assert.Equal(1, store1.Id);
                Assert.Equal("Maryland", store1.Location);
            }
        }

        /// <summary>
        /// Tests the customer model by adding to it and
        /// verifying that the customers and their details show up in the database
        /// </summary>
        [Fact]
        public void AddsCustomerToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "AddsCustomerToDbTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Customer location = new Customer
                {
                    FirstName = "Michael",
                    LastName = "Hall",
                    UserName = "mbhall",
                    Password = "yes"
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                Assert.Equal(1, context.Customers.Count());

                var customer1 = context.Customers.Where(c => c.Id == 1).FirstOrDefault();
                Assert.Equal(1, customer1.Id);
                Assert.Equal("Michael", customer1.FirstName);
                Assert.Equal("Hall", customer1.LastName);
                Assert.Equal("mbhall", customer1.UserName);
                Assert.Equal("yes", customer1.Password);
            }
        }

        /// <summary>
        /// Tests the order model by adding to it and
        /// verifying that the orders and their details show up in the database
        /// </summary>
        [Fact]
        public void AddsOrderToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "AddsOrderToDbTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Order location = new Order
                {
                    CustomerId = 5,
                    ProductId = 10,
                    Quantity = 3,
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                Assert.Equal(1, context.Orders.Count());

                var order1 = context.Orders.Where(o => o.Id == 1).FirstOrDefault();
                Assert.Equal(1, order1.Id);
                Assert.Equal(5, order1.CustomerId);
                Assert.Equal(10, order1.ProductId);
                Assert.Equal(3, order1.Quantity);
            }
        }

        /// <summary>
        /// Tests the inventory check for verifying that orders do not exceed it
        /// </summary>
        [Fact]
        public void IsWithinInventoryTest()
        {
            //Arrange
            var check = new OrderLogic();

            //Act
            int inv1 = 20;
            int inv2 = 40;
            int quant1 = 30;
            int quant2 = 60;
            int quant3 = 10;

            //Assert
            Assert.False(check.IsWithinInventory(inv1, quant1));
            Assert.False(check.IsWithinInventory(inv1, quant2));
            Assert.True(check.IsWithinInventory(inv1, quant3));

            Assert.True(check.IsWithinInventory(inv2, quant1));
            Assert.False(check.IsWithinInventory(inv2, quant2));
            Assert.True(check.IsWithinInventory(inv2, quant3));
        }

        /// <summary>
        /// Tests the GetCustomers method
        /// </summary>
        [Fact]
        public void GetCustomersTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "GetCustomersTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Customer location = new Customer
                {
                    FirstName = "Michael",
                    LastName = "Hall",
                    UserName = "mbhall",
                    Password = "yes"
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new CustomerRepo();
                var customers = repo.GetCustomers(context);
                Assert.Equal(1, customers.Count());

                var customer1 = customers.Where(c => c.Id == 1).FirstOrDefault();
                Assert.Equal(1, customer1.Id);
                Assert.Equal("Michael", customer1.FirstName);
                Assert.Equal("Hall", customer1.LastName);
                Assert.Equal("mbhall", customer1.UserName);
                Assert.Equal("yes", customer1.Password);
            }
        }

        /// <summary>
        /// Tests the SearchFirstName method
        /// </summary>
        [Fact]
        public void SearchFirstNameTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "SearchFirstNameTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Customer location = new Customer
                {
                    FirstName = "Michael",
                    LastName = "Hall",
                    UserName = "mbhall",
                    Password = "yes"
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new CustomerRepo();
                var customers = repo.GetCustomers(context);
                var filter = repo.SearchFirstName(customers, "Mi");
                Assert.Equal(1, filter.Count());

                var customer1 = filter.Where(c => c.Id == 1).FirstOrDefault();
                Assert.Equal(1, customer1.Id);
                Assert.Equal("Michael", customer1.FirstName);
                Assert.Equal("Hall", customer1.LastName);
                Assert.Equal("mbhall", customer1.UserName);
                Assert.Equal("yes", customer1.Password);
            }
        }

        /// <summary>
        /// Tests the SearchLastName method
        /// </summary>
        [Fact]
        public void SearchLastNameTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "SearchLastNameTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Customer location = new Customer
                {
                    FirstName = "Michael",
                    LastName = "Hall",
                    UserName = "mbhall",
                    Password = "yes"
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new CustomerRepo();
                var customers = repo.GetCustomers(context);
                var filter = repo.SearchLastName(customers, "Ha");
                Assert.Equal(1, filter.Count());

                var customer1 = filter.Where(c => c.Id == 1).FirstOrDefault();
                Assert.Equal(1, customer1.Id);
                Assert.Equal("Michael", customer1.FirstName);
                Assert.Equal("Hall", customer1.LastName);
                Assert.Equal("mbhall", customer1.UserName);
                Assert.Equal("yes", customer1.Password);
            }
        }

        /// <summary>
        /// Tests the GetOrders method
        /// </summary>
        [Fact]
        public void GetOrdersTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "GetOrdersTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Order location = new Order
                {
                    CustomerId = 5,
                    ProductId = 10,
                    Quantity = 3,
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new OrderRepo();
                var orders = repo.GetOrders(context);
                Assert.Equal(1, orders.Count());

                var order1 = context.Orders.Where(o => o.Id == 1).FirstOrDefault();
                Assert.Equal(1, order1.Id);
                Assert.Equal(5, order1.CustomerId);
                Assert.Equal(10, order1.ProductId);
                Assert.Equal(3, order1.Quantity);
            }
        }

        /// <summary>
        /// Tests the GetOrderDetails method
        /// </summary>
        [Fact]
        public void GetOrderDetailTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "GetOrderDetailTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Store location = new Store
                {
                    Location = "Maryland"
                };

                db.Add(location);
                db.SaveChanges();

                Customer customer = new Customer
                {
                    FirstName = "Michael",
                    LastName = "Hall",
                    UserName = "mbhall",
                    Password = "yes"
                };

                db.Add(customer);
                db.SaveChanges();

                Product product = new Product
                {
                    StoreId = 1,
                    ProductName = "bar",
                    Inventory = 5,
                    Price = 10
                };

                db.Add(product);
                db.SaveChanges();

                Order order = new Order
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Quantity = 3,
                };

                db.Add(order);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new OrderRepo();
                var orderTest = repo.GetOrderDetails(context, 1);

                Assert.Equal(1, orderTest.Id);
            }
        }

        /// <summary>
        /// Tests the GetCustomerHistory method
        /// </summary>
        [Fact]
        public void GetCustomerHistoryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "GetCustomerHistoryTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Store location = new Store
                {
                    Location = "Maryland"
                };

                db.Add(location);
                db.SaveChanges();

                Customer customer = new Customer
                {
                    FirstName = "Michael",
                    LastName = "Hall",
                    UserName = "mbhall",
                    Password = "yes"
                };

                db.Add(customer);
                db.SaveChanges();

                Product product = new Product
                {
                    StoreId = 1,
                    ProductName = "bar",
                    Inventory = 5,
                    Price = 10
                };

                db.Add(product);
                db.SaveChanges();

                Order order = new Order
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Quantity = 3,
                };

                db.Add(order);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new CustomerRepo();
                var customer1 = repo.GetCustomerHistory(context, 1);
            }
        }

        /// <summary>
        /// Tests the GetProducts method
        /// </summary>
        [Fact]
        public void GetProductsTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "GetProductsTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Product product = new Product
                {
                    StoreId = 1,
                    ProductName = "bar",
                    Inventory = 5,
                    Price = 10
                };

                db.Add(product);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new ProductRepo();
                var products = repo.GetProducts(context);
                Assert.Equal(1, products.Count());

                var product1 = context.Products.Where(p => p.Id == 1).FirstOrDefault();
                Assert.Equal(1, product1.StoreId);
                Assert.Equal(1, product1.Id);
            }
        }

        /// <summary>
        /// Tests the GetInventory method
        /// </summary>
        [Fact]
        public void GetInventoryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "GetInventoryTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Product product = new Product
                {
                    StoreId = 1,
                    ProductName = "bar",
                    Inventory = 5,
                    Price = 10
                };

                db.Add(product);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new ProductRepo();
                int inventory = repo.GetInventory(context, 1);

                Assert.Equal(5, inventory);
            }
        }

        /// <summary>
        /// Tests the UpdateInventory method
        /// </summary>
        [Fact]
        public void UpdateInventoryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "UpdateInventoryTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Product product = new Product
                {
                    StoreId = 1,
                    ProductName = "bar",
                    Inventory = 5,
                    Price = 10
                };

                db.Add(product);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new ProductRepo();
                var product1 = context.Products.Where(p => p.StoreId == 1).FirstOrDefault();

                Assert.Equal(5, product1.Inventory);

                repo.UpdateInventory(context, 1, 3);
                Assert.Equal(2, product1.Inventory);
            }
        }

        /// <summary>
        /// Tests the GetProductData method
        /// </summary>
        [Fact]
        public void GetProductDataTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "GetProductDataTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Store location = new Store
                {
                    Location = "Maryland"
                };

                db.Add(location);
                db.SaveChanges();

                Product product = new Product
                {
                    StoreId = 1,
                    ProductName = "bar",
                    Inventory = 5,
                    Price = 10
                };

                db.Add(product);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new ProductRepo();
                var product1 = repo.GetProductData(context);
            }
        }

        /// <summary>
        /// Tests the GetStores method
        /// </summary>
        [Fact]
        public void GetStoresTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "GetStoresTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Store location = new Store
                {
                    Location = "Maryland"
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {
                var repo = new StoreRepo();
                var products = repo.GetStores(context);
                Assert.Equal(1, products.Count());

                var store1 = context.Stores.Where(s => s.Id == 1).FirstOrDefault();
                Assert.Equal(1, store1.Id);
                Assert.Equal("Maryland", store1.Location);
            }
        }

        /// <summary>
        /// Tests the GetStoreHistory method
        /// </summary>
        [Fact]
        public void GetStoreHistoryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "GetStoreHistoryTest")
                .Options;

            //Act
            using (var db = new StoreAppContext(options))
            {
                Store location = new Store
                {
                    Location = "Maryland"
                };

                db.Add(location);
                db.SaveChanges();

                Customer customer = new Customer
                {
                    FirstName = "Michael",
                    LastName = "Hall",
                    UserName = "mbhall",
                    Password = "yes"
                };

                db.Add(customer);
                db.SaveChanges();

                Product product = new Product
                {
                    StoreId = 1,
                    ProductName = "bar",
                    Inventory = 5,
                    Price = 10
                };

                db.Add(product);
                db.SaveChanges();

                Order order = new Order
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Quantity = 3,
                };

                db.Add(order);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreAppContext(options))
            {  
                var repo = new StoreRepo();
                var products = repo.GetStoreHistory(context, 1);
            }
        }

        /// <summary>
        /// Tests an exception for querying customers
        /// </summary>
        [Fact]
        public void CustomerQueryExceptionTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "CustomerQueryExceptionTest")
                .Options;

            //Act
            // try it with nothing in database

            //Assert
            using (var context = new StoreAppContext(options))
            {
                try
                {
                    var customer1 = context.Customers.Where(c => c.Id == 1).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// Tests an exception for querying orders
        /// </summary>
        [Fact]
        public void OrderQueryExceptionTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "OrderQueryExceptionTest")
                .Options;

            //Act
            // try it with nothing in database

            //Assert
            using (var context = new StoreAppContext(options))
            {
                try
                {
                    var order1 = context.Orders.Where(o => o.Id == 1).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// Tests an exception for querying products
        /// </summary>
        [Fact]
        public void ProductQueryExceptionTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "ProductQueryExceptionTest")
                .Options;

            //Act
            // try it with nothing in database

            //Assert
            using (var context = new StoreAppContext(options))
            {
                try
                {
                    var product1 = context.Products.Where(p => p.Id == 1).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// Tests an exception for querying stores
        /// </summary>
        [Fact]
        public void StoreQueryExceptionTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "StoreQueryExceptionTest")
                .Options;

            //Act
            // try it with nothing in database

            //Assert
            using (var context = new StoreAppContext(options))
            {
                try
                {
                    var store1 = context.Stores.Where(s => s.Id == 1).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
