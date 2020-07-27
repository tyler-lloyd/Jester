using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace Jester
{
    public interface IBaseAddress
    {
        IJesterClient WithBaseAddress(Uri uri);

        IJesterClient WithDefaultClient();
    }

    public interface IJesterClient
    {
        IJesterClient OnGet(string endpoint, string returns);

        IJesterClient OnGet(HttpRequestMessage httpRequest);

        HttpClient BuildClient();
    }
}
