using System;
using System.Collections.Generic;

namespace Model
{
    public class Result
    {
        public bool Succeeded { get; set; } = false;
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public Result(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public Result()
        {

        }
    }
}