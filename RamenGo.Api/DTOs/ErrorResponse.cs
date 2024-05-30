using Microsoft.AspNetCore.Mvc;

namespace RamenGo.Api.DTOs
{
    public class ErrorResponse
    {
        public string Error { get; set; }

        public ErrorResponse(string message)
        {
            Error = message;
        }
    }
}
