using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Repository.Model;

namespace Repository.Database {
    public class TaskDatabase {

        private const string ConnectionString = "AccountEndpoint=https://atrafael2.documents.azure.com:443/;AccountKey=hN32oCXC2nKm0Mt5pZFzciheYKc0X1vTpRD9hCgA0KoGVHOrIMXEdrRJWOEc6ct9RgbuOD1BwsBypMRkY5N3Iw==;";
        private const string Database = "at-container";
        private const string Container = "todo-list";

        private CosmosClient CosmosClient { get; set; }

        public TaskDatabase() {
            this.CosmosClient = new CosmosClient(ConnectionString);
        }

        public List<MyTask> GetAll() {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");

            var result = new List<MyTask>();

            var queryResult = container.GetItemQueryIterator<MyTask>(queryDefinition);

            while(queryResult.HasMoreResults) {
                FeedResponse<MyTask> currentResultSet = queryResult.ReadNextAsync().Result;
                result.AddRange(currentResultSet.Resource);
            }

            return result;
        }

        public MyTask GetById(Guid id) {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c where c.id = '{id}'");

            var result = new List<MyTask>();

            var queryResult = container.GetItemQueryIterator<MyTask>(queryDefinition);

            while(queryResult.HasMoreResults) {
                FeedResponse<MyTask> currentResultSet = queryResult.ReadNextAsync().Result;
                result.AddRange(currentResultSet.Resource);
            }

            return result.FirstOrDefault();
        }

        public async Task Save(MyTask obj) {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.CreateItemAsync<MyTask>(obj, new PartitionKey(obj.PartitionKey));
        }

        public async Task Delete(MyTask obj) {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.DeleteItemAsync<MyTask>(obj.Id.ToString(), new PartitionKey(obj.PartitionKey));
        }

        public async Task Update(MyTask obj) {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.ReplaceItemAsync<MyTask>(obj, obj.Id.ToString(), new PartitionKey(obj.PartitionKey));
        }

    }
}
