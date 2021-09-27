using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Database;
using Repository.Model;

namespace ATRafael2
{
    public static class CreateTask
    {
        [FunctionName("NewTask")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            MyTask dataTaskJson = JsonConvert.DeserializeObject<MyTask>(requestBody);
            dataTaskJson.Id = Guid.NewGuid();
            var repository = new TaskDatabase();

            await repository.Save(dataTaskJson);

            return new CreatedResult($"", dataTaskJson);

        }
    }
}
