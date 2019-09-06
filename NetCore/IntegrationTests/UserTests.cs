using Core.Common.Model;
using Core.Common.Model.Search;
using Core.Users.Query.Request;
using Core.Users.Query.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Transactions;

namespace IntegrationTests
{
    [TestClass]
    public class UserTests
    {
        private readonly HttpClient _client;

        private TransactionScope _transactionScope;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Transaction.Current.Rollback();
            _transactionScope.Dispose();
        }

        public UserTests()
        {
            _client = TestHelper.GetClient();
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TestHelper.GetToken());
        }

        [TestMethod]
        public async Task GetUserById()
        {
            HttpResponseMessage httpResponse = await _client.GetAsync("api/users/1");

            var response = TestHelper.GetResponseContent<Response<UserDetails>>(httpResponse);

            Assert.AreEqual(1, response.Data.Id);
            Assert.AreEqual("admin@gmail.com", response.Data.Email);
        }

        [TestMethod]
        public async Task Search()
        {
            HttpResponseMessage httpResponse = await _client.PostAsJsonAsync("api/users/search", new UserQuery
            {
            });

            var response = TestHelper.GetResponseContent<Response<SearchResponse<UserDetails>>>(httpResponse);

            Assert.AreNotSame(0, response.Data.TotalCount);
        }
    }
}
