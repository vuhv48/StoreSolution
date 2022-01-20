namespace StoreSolution.WebApp.Helper
{
    public class ApiController
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:7053");
            return Client;
        }

    }
}
