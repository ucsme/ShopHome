using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ShopHome.Models;
using Microsoft.AspNetCore.Http;

namespace ShopHome.Repository
{
    public class ShopHomeRepository
    {
        List<Users> UserDetails;
        List<Product> products;
        public ShopHomeRepository()
        {
            UserDetails = new List<Users> {
            new Users {UserName="Albert",Password= "Albert@home" },
            new Users {UserName="John",Password= "John@home" },
            new Users {UserName="Steve",Password= "Steve@home" }
             };
            products = new List<Product>();
            //{
            //    new Product{ProductID=101,ProductName="Apple MacBook", Price=55973,QuantityAvaliable=23},
            //    new Product{ProductID=102,ProductName="Microsoft Surface Pro", Price=33978,QuantityAvaliable=123},
            //    new Product{ProductID=103,ProductName="The Power of Mind", Price=100,QuantityAvaliable=223},
            //    new Product{ProductID=104,ProductName="The 3 Mistakes of My Life", Price=200,QuantityAvaliable=153},
            //    new Product{ProductID=105,ProductName="Samsung Galaxy Pro", Price=15973,QuantityAvaliable=63},
            //    new Product{ProductID=106,ProductName="OnePlus 6", Price=60973,QuantityAvaliable=13},
            //};
        }
        public int ValidateUser(string uName, string password)
        {
            int status = 0;
            Users checkUser = UserDetails.Find(user => (user.UserName == uName) && (user.Password == password));
            if (checkUser != null)
            {
                status = 1;
            }
            return status;
        }

        public List<Product> ReadProducts(string jsonfilepath)
        {
            StreamReader reader = new StreamReader(jsonfilepath);
            String JSONtxt = reader.ReadToEnd();
            //String JSONtxt = File.ReadAllText(jsonfilepath);
            products = JsonConvert.DeserializeObject<List<Product>>(JSONtxt);
            reader.Close();
            return products;
        }
        public int AddProduct(Product product, string jsonfilepath,List<Product> products)
        {
            int status = 0;
            try
            {
                //products=  this.ReadProducts(jsonfilepath);
                StreamReader reader = new StreamReader(jsonfilepath);
                String jsonData = reader.ReadToEnd();
                JObject productData = (JObject)JsonConvert.DeserializeObject(jsonData);
                JArray productArray = (JArray)productData[0];

                productArray.AddAfterSelf(product);
                //products = JsonConvert.DeserializeObject<List<Product>>(JSONtxt);
                //products.Add(product);
                using (StreamWriter writer =new StreamWriter(jsonfilepath))
                {
                    //var prods = JsonConvert.SerializeObject(products);
                    writer.WriteLine(productArray);
                }
                status = 1;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                status = -1;
            }
            return status;
        }


        public int AddPurchaseDetails(Purchase purchaseProduct, string jsonfilepath)
        {
            purchaseProduct.PurchaseId = new Random().Next(1, 1000);
            StreamReader reader = new StreamReader(jsonfilepath);
            String jsonData = reader.ReadToEnd();
            if (jsonData == null)
            {
                StreamWriter writer = new StreamWriter(jsonfilepath);
                writer.Write(purchaseProduct);
            }
            else
            {
                JObject purchaseData = (JObject)JsonConvert.DeserializeObject(jsonData);
                JArray purchaseArray = (JArray)purchaseData[0];
                purchaseArray.AddAfterSelf(purchaseProduct);
                StreamWriter writer = new StreamWriter(jsonfilepath);
                writer.Write(purchaseProduct);
            }

            return purchaseProduct.PurchaseId;
        }
    }
}