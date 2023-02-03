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
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("authorization", "Bearer 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");

            var content = new StringContent("{\"model\": \"text-davinci-003\", \"prompt\": \"" + prompt + "\",\"temperature\": 1,\"max_tokens\": 4000}",
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/completions", content);
            Console.WriteLine(response.IsSuccessStatusCode);
            string responseString = await response.Content.ReadAsStringAsync();
            
            try
            {
                Models.AskResponse? askResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.AskResponse>(responseString); 
                if(askResponse == null) return Ok("Lỗi !"); 
                
                return Ok(askResponse);
            }catch(Exception e){
                return Ok(e.Message);
            }
            finally{
                client.Dispose();
            }

        }
    }
}