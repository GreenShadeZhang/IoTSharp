using System;
namespace Verdure.Qinglan.Models
{
	public class Result
	{
		public Result()
		{
		}
	}

	public class Result<T>
	{
		public string Msg { get; set; } = string.Empty;

		public int Code { get; set; }

        public T? Data { get; set; }

        public static Result<T> FromData(T data)
        {
			return new Result<T>()
            {
				Code = 0,
				Data = data
            };
        }
    }
}

