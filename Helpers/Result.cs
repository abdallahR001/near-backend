using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Helpers
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool ISuccess { get; set; }
        public string Message { get; set; }
        public static Result<T> Success(string message, T value)
        {
            return new Result<T>{ ISuccess = true, Message = message, Data = value };
        }
        public static Result<T> Failure(string message)
        {
            return new Result<T> { ISuccess = false, Message = message };
        }        
    }
}