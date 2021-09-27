using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository.Database;
using Newtonsoft.Json;

namespace ATRafael2
{
    public static class GetAllTasks
    {
        [FunctionName("GetAllTasks")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var repository = new TaskDatabase();

            var result = repository.GetAll();

            return new OkObjectResult(result);
        }
    }
}
