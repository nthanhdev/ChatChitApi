using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatChitApi.Models
{
  
    public class AskResponse
    {
        public string Id { get; set; } = string.Empty;
        public string? Object { get; set; }
        public int Created { get; set; }
        public string Model { get; set; } = string.Empty;
        public List<Choice>? Choices { get; set; } 
        public Usage? Usage { get; set; }
    }

    public class Usage
    {
        public int Prompt_tokens { get; set; }
        public int Completion_tokens { get; set; }
        public int Total_tokens { get; set; }
    }

  public class Choice
    {
        public string Text { get; set; } = string.Empty;
        public int Index { get; set; }
        public object? Logprobs { get; set; }
        public string? Finish_reason { get; set; }
    }

}