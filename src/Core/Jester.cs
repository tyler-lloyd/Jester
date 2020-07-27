using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Model;
using Moq;
using Moq.Protected;

namespace Jester
{
    public class Jester : IBaseAddress, IJesterClient
    {
        private const string Localhost = "https://localhost";
        private Uri _baseAddress;
        private readonly Mock<HttpMessageHandler> _handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        private Expression<Func<HttpRequestMessage, bool>> _func;
        private Jester() { }

        public static IBaseAddress Begin()
        {
            return new Jester();
        }

        public IJesterClient WithBaseAddress(Uri uri)
        {
            _baseAddress = uri;
            return this;
        }

        public IJesterClient WithDefaultClient()
        {
            _baseAddress = new Uri(Localhost);
            return this;
        }

        public IJesterClient OnGet(string endpoint, string returns)
        {
            _handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(returns)
                });
            _handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("[]")
                });
            return this;
        }

        public IJesterClient OnGet(HttpRequestMessage httpRequest)
        {
            return this;
        }

        public HttpClient BuildClient()
        {
            return new HttpClient(_handler.Object)
            {
                BaseAddress = _baseAddress
            };
        }

    }
}
