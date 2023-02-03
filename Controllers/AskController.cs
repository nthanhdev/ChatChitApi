using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChatChitApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AskController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Send(string prompt)
        {
            using (var client = new HttpClient())
            {
                // Add authorization header with API key
                client.DefaultRequestHeaders.Add("authorization", "Bearer YOUR_API_KEY");
                // Define request body with model, prompt, top_p, and max_tokens
                var requestBody = new
                {
                    model = "text-davinci-003",
                    prompt = prompt,
                    top_p = 1,
                    max_tokens = 4000
                };
                // Serialize request body to JSON and set as payload
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody),
                    Encoding.UTF8, "application/json");

                // Make post request to API
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/completions", content);

                // Check if API response is successful
                if (response.IsSuccessStatusCode == true)
                {
                    // Read response content as string
                    string responseString = await response.Content.ReadAsStringAsync();
                    try
                    {
                        // Deserialize response string to Models.AskResponse object
                        Models.AskResponse? askResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.AskResponse>(responseString);
                        if (askResponse == null) return Ok("Lỗi !");

                        // Return askResponse as response
                        return Ok(askResponse);
                    }
                    catch (Exception e)
                    {
                        // Return error message if exception occurs during deserialization
                        return Ok(e.Message);
                    }
                }


            }
            // Return empty response if API response is not successful

            return Ok();

        }
    }
}
