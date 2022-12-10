using System;
using System.Text.Json;
using c_sharp_angular.Helpers;

namespace c_sharp_angular.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response,
            PaginationHeader header)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(header,
                jsonOptions));

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");


        }
    }
}

