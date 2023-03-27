using FluentValidation.Results;
using System.Net;

namespace CodeBlock.Challenge.Domain.Responses
{
    public class OperacaoCargueiroResponse
    {
        public HttpStatusCode StatusCode{ get; set; }
        public ValidationResult ValidationResult{ get; set; }

        public OperacaoCargueiroResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public OperacaoCargueiroResponse(HttpStatusCode statusCode, ValidationResult validationResult)
        {
            StatusCode = statusCode;
            ValidationResult = validationResult;
        }
    }
}
