using ATRafaelFront2.Models;
using System;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATRafaelFront2.Infra {
    public class TaskRestClient {
        private string URL_TASK_REST = "https://atrafael2functions.azurewebsites.net/";

        public IList<TaskModel> GetAll() {
            var client = new RestClient(URL_TASK_REST);

            var request = new RestRequest("api/GetAllTasks", DataFormat.Json);

            var response = client.Get<IList<TaskModel>>(request);

            return response.Data;
        }

        public TaskModel GetById(Guid id) {
            var client = new RestClient(URL_TASK_REST);

            var request = new RestRequest($"api/GetTaskById?id={id}", DataFormat.Json);

            var response = client.Get<TaskModel>(request);

            return response.Data;

        }

        public void Save(TaskModel model) {
            var client = new RestClient(URL_TASK_REST);
            var request = new RestRequest($"api/NewTask", DataFormat.Json);
            request.AddJsonBody(model);

            var response = client.Post<TaskModel>(request);

            if(response.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception("Tarefa não encontrada.");


        }

        public void Delete(Guid id) {
            var client = new RestClient(URL_TASK_REST);

            var request = new RestRequest($"api/DeleteTask?id={id}", DataFormat.Json);

            var response = client.Delete(request);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Não foi possível criar a tarefa.");

        }

        public void Update(TaskModel model) {
            var client = new RestClient(URL_TASK_REST);

            var request = new RestRequest($"api/ChangeTask", DataFormat.Json);
            request.AddJsonBody(model);

            var response = client.Put<TaskModel>(request);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Não foi possível alterar a tarefa.");

        }
    }
}
