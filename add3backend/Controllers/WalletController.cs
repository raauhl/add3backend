using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using add3backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;


namespace add3backend.Controllers
{
    [Route("api/[controller]")]
    public class WalletController : Controller
    {
        private readonly MyDbContext myDbContext;

        public WalletController(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        [HttpGet("getuserinfo/{walletAddress}")]
        public string GetUserAccountInfo(string walletAddress)
        {
            var customerJson = "{}";
            try
            {
                var customer = myDbContext.customers.SingleOrDefault(c => c.WalletAddress == walletAddress);
                if (customer == null)
                {
                    Console.WriteLine("User doesn't exist in database.");
                    return customerJson;
                }
                customerJson = JsonConvert.SerializeObject(customer);
                return customerJson;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return customerJson;
            }
        }

        [HttpGet("getuserlogininfo/{walletAddress}")]
        public string GetUserLoginInfo(string walletAddress)
        {
            var loginDetailsJson = "{}";
            try
            {
                var loginDetails = myDbContext.logins.Where(c => c.WalletAddress == walletAddress).ToList();
                if (loginDetails.Count == 0)
                {
                    Console.WriteLine("User doesn't exist in database.");
                    return loginDetailsJson;
                }
                loginDetailsJson = JsonConvert.SerializeObject(loginDetails);
                return loginDetailsJson;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return loginDetailsJson;
            }
        }

        [HttpGet("getusermints/{walletAddress}")]
        public string GetUserMints(string walletAddress)
        {
            var mintDetailsJson = "{}";
            try
            {
                var mintDetails = myDbContext.mints.Where(c => c.FromAddress == walletAddress).ToList();
                if (mintDetails.Count == 0)
                {
                    Console.WriteLine("No mint details for wallet :" + walletAddress);
                    return mintDetailsJson;
                }

                mintDetailsJson = JsonConvert.SerializeObject(mintDetails);
                return mintDetailsJson;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return mintDetailsJson;
            }
        }

        [HttpPost("upsertmints")]
        public IActionResult UpsertMints([FromBody] Mint mintEvent)
        {
            if (mintEvent == null)
            {
                Console.WriteLine("Invalid mintEvent!");
                return Ok();
            }

            try
            {
                myDbContext.mints.Add(mintEvent);
                myDbContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Ok();
            }
        }

        [HttpPost("upsertuserinfo")]
        public IActionResult UpsertUserInfo([FromBody] Customer customer)
        {

            if (customer == null)
            {
                Console.WriteLine("Invalid userInfo object!");
                return Ok();
            }

            try
            {
                LoginUser(customer.WalletAddress);
                var existingCustomer = myDbContext.customers.SingleOrDefault(c => c.WalletAddress == customer.WalletAddress);
                if (existingCustomer != null)
                {
                    if (string.Compare(existingCustomer.UserBalance, customer.UserBalance) != 0)
                    {
                        Console.WriteLine("User balanced updated: " + existingCustomer.UserBalance + ", -> " + customer.UserBalance);
                        existingCustomer.UserBalance = customer.UserBalance;
                    }
                    else
                    {
                        Console.WriteLine("Exsiting customer but no change in user details: ");
                    }
                }
                else
                {
                    myDbContext.customers.Add(customer);
                    Console.WriteLine("New user logged in: " + customer.WalletAddress);
                }
                myDbContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                myDbContext.SaveChanges();
                Console.WriteLine(e.ToString());
                return Ok();
            }
        }

        private void LoginUser(string? walletAddress)
        {
            var newlogin = new Login();
            newlogin.WalletAddress = walletAddress;
            newlogin.LoginTime = DateTime.UtcNow;
            myDbContext.logins.Add(newlogin);
        }
    }
}

