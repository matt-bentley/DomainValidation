using CSharpFunctionalExtensions;
using DomainValidation.Application.ReadModels;
using DomainValidation.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace DomainValidation.Api.Controllers.Abstract
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ApiController : ControllerBase
    {
        protected new IActionResult NotFound()
        {
            return NotFound(Errors.General.NotFound());
        }

        protected IActionResult NotFound(Error error)
        {
            return NotFound(Envelope.Create(error, HttpStatusCode.NotFound));
        }

        protected IActionResult Error(Error error)
        {
            if (error.Equals(Errors.General.NotFound()))
            {
                return NotFound(error);
            }
            else
            {
                return BadRequest(Envelope.Create(error, HttpStatusCode.BadRequest));
            }
        }

        protected IActionResult CreatedResult(string actionName, Result<CreatedReadModel, Error> result)
        {
            return CreatedResult(actionName, result, (createdResult) => new { id = result.Value.Id });
        }

        protected IActionResult CreatedResult(string actionName, Result<CreatedReadModel, Error> result, Func<CreatedReadModel, object> routeValuesFactory)
        {
            if (result.IsFailure)
            {
                return Error(result.Error);
            }
            return CreatedAtAction(actionName, routeValuesFactory(result.Value), result.Value);
        }

        protected IActionResult NoContentResult(UnitResult<Error> result)
        {
            if (result.IsFailure)
            {
                return Error(result.Error);
            }
            return NoContent();
        }
    }

    public class Envelope
    {
        public int Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public string TraceId { get; set; }

        protected Envelope(int status, string errorCode, string errorMessage, DateTime timestamp, string traceId)
        {
            Status = status;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Timestamp = timestamp;
            TraceId = traceId;
        }

        protected Envelope()
        {

        }

        public static Envelope Create(Error error, HttpStatusCode statusCode)
        {
            return new Envelope((int)statusCode, error.Code, error.Message, DateTime.UtcNow, Activity.Current.Id);
        }
    }
}
