using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TriviaApp.Core.Models;

namespace TriviaApp.Core
{
    public class DataServiceQuestions
    {
        public static async Task<Question> GetQuestions(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(queryString);

            Question data = null;
            if (response != null)
            {
                data = JsonConvert.DeserializeObject<Question>(response);
                return data;
            }
            return null;
        }
    }
}
