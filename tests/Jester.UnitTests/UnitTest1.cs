using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application;
using Jester;
using Moq;
using NUnit.Framework;

namespace Jester.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ApiTestNoMocking()
        {
            var service = new MyService(new HttpClient { BaseAddress = new Uri("https://jsonplaceholder.typicode.com") });

            var content = await service.GetItem();

            Assert.IsTrue(content.Count > 1);
        }

        [Test]
        public async Task ApiTestWithMocking()
        {
            var mockClient = Jester
                .Begin()
                .WithDefaultClient()
                .OnGet("/comments", "[]")
                .BuildClient();

            var service = new MyService(mockClient);
            var c = await service.GetItem();
            
            Assert.AreEqual(0, c.Count);

            c = await service.PostItem();
        }
    }
}