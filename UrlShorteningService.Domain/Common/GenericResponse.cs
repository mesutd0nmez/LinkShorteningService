using System;
using UrlShorteningService.Domain.DTOs;
using System.Text.Json.Serialization;

namespace UrlShorteningService.Domain.Common
{
	public class GenericResponse<TResponse> where TResponse : class
    {
        public TResponse Data { get; private set; }
        public int StatusCode { get; private set; }
        public ErrorDto Error { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        public static GenericResponse<TResponse> Success(TResponse data, int statusCode)
        {
            return new GenericResponse<TResponse> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static GenericResponse<TResponse> Success(int statusCode)
        {
            return new GenericResponse<TResponse> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static GenericResponse<TResponse> Fail(ErrorDto errorDto, int statusCode)
        {
            return new GenericResponse<TResponse> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }

        public static GenericResponse<TResponse> Fail(string errMessage, int statusCode, bool isShow = true)
        {
            return new GenericResponse<TResponse> { Error = new ErrorDto(errMessage, isShow), StatusCode = statusCode, IsSuccessful = false };
        }
    }
}

