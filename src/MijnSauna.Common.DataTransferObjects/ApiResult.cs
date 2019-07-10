using System;
using System.Collections.Generic;

namespace MijnSauna.Common.DataTransferObjects
{
    /// <summary>
    /// Interface defining a wrapper for all data returned.
    /// </summary>
    public interface IApiResult
    {
        /// <summary>
        /// The version of the API used.
        /// </summary>
        String ApiVersion { get; set; }

        /// <summary>
        /// Timestamp when the request finished.
        /// </summary>
        DateTime TimeStamp { get; set; }

        /// <summary>
        /// Duration from request to response.
        /// </summary>
        Int64 Duration { get; set; }

        /// <summary>
        /// HTTP method used on the API endpoint.
        /// </summary>
        String Method { get; set; }

        /// <summary>
        /// HTTP response status code.
        /// </summary>
        String StatusCode { get; set; }

        /// <summary>
        /// API endpoint used for the request.
        /// </summary>
        String RequestUri { get; set; }

        /// <summary>
        /// Gets or sets the correlation identifier.
        /// </summary>
        Guid? CorrelationId { get; set; }
    }

    /// <summary>
    /// Class implementing a result wrapper for all data returned.
    /// </summary>
    /// <seealso cref="IApiResult" />
    public class ApiResult : IApiResult
    {
        /// <summary>
        /// The version of the API used.
        /// </summary>
        public String ApiVersion { get; set; }

        /// <summary>
        /// Timestamp when the request finished.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Duration from request to response.
        /// </summary>
        public Int64 Duration { get; set; }

        /// <summary>
        /// HTTP method used on the API endpoint.
        /// </summary>
        public String Method { get; set; }

        /// <summary>
        /// HTTP response status code.
        /// </summary>
        public String StatusCode { get; set; }

        /// <summary>
        /// API endpoint used for the request.
        /// </summary>
        public String RequestUri { get; set; }

        /// <summary>
        /// Gets or sets the correlation identifier.
        /// </summary>
        public Guid? CorrelationId { get; set; }
    }

    /// <summary>
    /// Class implementing a generic result wrapper for all data returned.
    /// </summary>
    /// <typeparam name="T">The type of the content to wrap.</typeparam>
    /// <seealso cref="IApiResult" />
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// The HTTP response content.
        /// </summary>
        public T Content { get; set; }
    }

    /// <summary>
    /// Class implementing a validation error wrapper for API errors.
    /// </summary>
    public class ApiValidationResult : ApiResult
    {
        /// <summary>
        /// Gets or sets the validation messages.
        /// </summary>
        public List<String> ValidationMessages { get; set; }
    }

    /// <summary>
    /// Class implementing a forbidden error wrapper for API errors.
    /// </summary>
    public class ApiForbiddenResult : ApiResult
    {
        /// <summary>
        /// Gets or sets the validation messages.
        /// </summary>
        public List<String> ValidationMessages { get; set; }
    }

    /// <summary>
    /// Class implementing a result wrapper for API errors.
    /// </summary>
    public class ApiErrorResult : ApiResult
    {
        /// <summary>
        /// Message about the Exception causing the API error.
        /// </summary>
        public String ErrorMessage { get; set; }

        /// <summary>
        /// Type of the Exception causing the API error.
        /// </summary>
        public String ErrorType { get; set; }

        /// <summary>
        /// StackTrace details for the Exception causing the API error.
        /// </summary>
        public String StackTrace { get; set; }
    }
}