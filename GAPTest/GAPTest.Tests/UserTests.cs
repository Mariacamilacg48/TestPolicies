using GAPTest.Web.Controllers.API;
using GAPTest.Web.Data;
using GAPTest.Web.Data.Entities;
using GAPTest.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using GAPTest.Common.Models;

namespace GAPTest.Tests
{
    public class UserTests
    {

        private static readonly HttpClient client = new HttpClient();
        public UserRequest user { get; set; }

        [SetUp]
        public void Setup()
        {
            user = new UserRequest()
            {
                Email = "mateoleon@gmail.com",
                CellPhone = "3215697478",
                Name = "Camila",
                Address = "Calle 55",
                Document = "1236547812",
                Password="1234567"
            };

        }

        [Test]
        public void Create()
        {
            var json = JsonConvert.SerializeObject(user);

            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var result = client.PutAsync("https://localhost:44353/api/Customer/PutUser", content).Result;
            Assert.AreEqual("OK", result.StatusCode.ToString());
        }
        [Test]
        public void Edit()
        {
            Assert.Pass();
        }
        [Test]
        public void Delete()
        {
            Assert.Pass();
        }
        [Test]
        public void List()
        {
            Assert.Pass();
        }
    }
}
