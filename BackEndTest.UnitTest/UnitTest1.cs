using System;
using Xunit;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEndTest.UnitTest
{
    public class UnitTest1
    {
        private static readonly HttpClient client = new HttpClient();

        [Fact]
        public async void TestGetUserById()
        {
            var query = new Dictionary<string, string>
            {
                { "query", "query {userById(id: 1) { id name }}"}
            };
            var json = JsonConvert.SerializeObject(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8000/graphql", content);            

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.True(responseString.Contains("Geraldo Rivieira"));

        }
        [Fact]
        public async void TestGetMovementUserById()
        {
            var query = new Dictionary<string, string>
            {
                { "query", "query {movementByUserId(user: 3) { id }}"}
            };
            var json = JsonConvert.SerializeObject(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8000/graphql", content);            

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(responseString);
            string name = (string)jsonObject["data"]["movementByUserId"][0]["id"];

            Assert.Equal("4", name);

        }

        [Fact]
        public async void TestGetBalanceBetweenDates()
        {
            var query = new Dictionary<string, string>
            {
                { "query", "query { balanceBetweenDates(start: \"2018-10-02 07:00:00.000\", end:\"2020-10-02 09:00:00.000\") }"}
            };
            var json = JsonConvert.SerializeObject(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8000/graphql", content);            

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(responseString);

            string name = (string)jsonObject["data"]["balanceBetweenDates"];

            Assert.Equal("2600", name);

        }
        [Fact]
        public async void TestGetAllMovements()
        {
            var query = new Dictionary<string, string>
            {
                { "query", "query {movements { id, user, date, amount, type }}"}
            };
            var json = JsonConvert.SerializeObject(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8000/graphql", content);            

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(responseString);

            string id = (string)jsonObject["data"]["movements"][0]["id"];
            string user = (string)jsonObject["data"]["movements"][0]["user"];
            string date = (string)jsonObject["data"]["movements"][0]["date"];
            string amount = (string)jsonObject["data"]["movements"][0]["amount"];
            string type = (string)jsonObject["data"]["movements"][0]["type"];
            string idSecond = (string)jsonObject["data"]["movements"][1]["id"];
            string userSecond = (string)jsonObject["data"]["movements"][1]["user"];
            string dateSecond = (string)jsonObject["data"]["movements"][1]["date"];
            string amountSecond = (string)jsonObject["data"]["movements"][1]["amount"];
            string typeSecond = (string)jsonObject["data"]["movements"][1]["type"];

            Assert.Equal("1", id);
            Assert.Equal("1", user);
            Assert.Equal("2019-10-02 08:00:00.000", date);
            Assert.Equal("4000", amount);
            Assert.Equal("IN", type);
            Assert.Equal("2", idSecond);
            Assert.Equal("1", userSecond);
            Assert.Equal("2019-10-03 14:30:00.000", dateSecond);
            Assert.Equal("250", amountSecond);
            Assert.Equal("OUT", typeSecond);
        }

        [Fact]
        public async void TestGetAllUsers()
        {
            var query = new Dictionary<string, string>
            {
                { "query", "query {users { id, name, salary }}"}
            };
            var json = JsonConvert.SerializeObject(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8000/graphql", content);            

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(responseString);

            string id = (string)jsonObject["data"]["users"][0]["id"];
            string name = (string)jsonObject["data"]["users"][0]["name"];
            string salary = (string)jsonObject["data"]["users"][0]["salary"];
            string idSecond = (string)jsonObject["data"]["users"][1]["id"];
            string nameSecond = (string)jsonObject["data"]["users"][1]["name"];
            string salarySecond = (string)jsonObject["data"]["users"][1]["salary"];

            Assert.Equal("1", id);
            Assert.Equal("Geraldo Rivieira", name);
            Assert.Equal("5000", salary);
            Assert.Equal("2", idSecond);
            Assert.Equal("Jennifer Rivieira", nameSecond);
            Assert.Equal("5500", salarySecond);
        }

        //[Fact]
        public async void TestAddUser()
        {
            var query = new Dictionary<string, string>
            {
                { "query", "mutation ($user: UserInputType) {addUser(user: $user) { name }}"},
                { "variables", "\"user\": { \"name\": \"José\", \"salary\": 4000 }\""} 

            };
            var json = JsonConvert.SerializeObject(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8000/graphql", content);            

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(responseString);

            string name = (string)jsonObject["data"]["addUser"]["name"];

            Assert.Equal("José", name);

        }
        [Fact]
        public async void TestRemoveUser()
        {
            var query = new Dictionary<string, string>
            {
                { "query", "mutation {removeUser(id:5) }"},
            };
            var json = JsonConvert.SerializeObject(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8000/graphql", content);            

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(responseString);

            string id = (string)jsonObject["data"]["removeUser"];

            Assert.Equal("5", id);

        }
    }
}
