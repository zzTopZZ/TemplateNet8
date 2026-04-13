using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateNet8.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? Error { get; }

        protected Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, null);
        public static Result Failure(string error) => new Result(false, error);

        public static Result<T> Success<T>(T value) => new Result<T>(value, true, null);
        public static Result<T> Failure<T>(string error) => new Result<T>(default!, false, error);


    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        public T Value => IsSuccess ? _value! : throw new InvalidOperationException("Não é possível acessar o valor de um Result com falha.");

        protected internal Result(T? value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            _value = value;
        }
    }
}
