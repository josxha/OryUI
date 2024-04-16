/*
 * Ory Identities API
 *
 * This is the API specification for Ory Identities with features such as registration, login, recovery, account verification, profile settings, password reset, identity management, session management, email and sms delivery, and more. 
 *
 * Contact: office@ory.sh
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Ory.Kratos.Client.Client.OpenAPIDateConverter;

namespace Ory.Kratos.Client.Model
{
    /// <summary>
    /// GenericError
    /// </summary>
    [DataContract(Name = "genericError")]
    public partial class GenericError : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericError" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GenericError() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericError" /> class.
        /// </summary>
        /// <param name="code">The status code.</param>
        /// <param name="debug">Debug information  This field is often not exposed to protect against leaking sensitive information..</param>
        /// <param name="details">Further error details.</param>
        /// <param name="id">The error ID  Useful when trying to identify various errors in application logic..</param>
        /// <param name="message">Error message  The error&#39;s message. (required).</param>
        /// <param name="reason">A human-readable reason for the error.</param>
        /// <param name="request">The request ID  The request ID is often exposed internally in order to trace errors across service architectures. This is often a UUID..</param>
        /// <param name="status">The status description.</param>
        public GenericError(long code = default(long), string debug = default(string), Object details = default(Object), string id = default(string), string message = default(string), string reason = default(string), string request = default(string), string status = default(string))
        {
            // to ensure "message" is required (not null)
            if (message == null)
            {
                throw new ArgumentNullException("message is a required property for GenericError and cannot be null");
            }
            this.Message = message;
            this.Code = code;
            this.Debug = debug;
            this.Details = details;
            this.Id = id;
            this.Reason = reason;
            this.Request = request;
            this.Status = status;
        }

        /// <summary>
        /// The status code
        /// </summary>
        /// <value>The status code</value>
        /// <example>404</example>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public long Code { get; set; }

        /// <summary>
        /// Debug information  This field is often not exposed to protect against leaking sensitive information.
        /// </summary>
        /// <value>Debug information  This field is often not exposed to protect against leaking sensitive information.</value>
        /// <example>SQL field &quot;foo&quot; is not a bool.</example>
        [DataMember(Name = "debug", EmitDefaultValue = false)]
        public string Debug { get; set; }

        /// <summary>
        /// Further error details
        /// </summary>
        /// <value>Further error details</value>
        [DataMember(Name = "details", EmitDefaultValue = false)]
        public Object Details { get; set; }

        /// <summary>
        /// The error ID  Useful when trying to identify various errors in application logic.
        /// </summary>
        /// <value>The error ID  Useful when trying to identify various errors in application logic.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Error message  The error&#39;s message.
        /// </summary>
        /// <value>Error message  The error&#39;s message.</value>
        /// <example>The resource could not be found</example>
        [DataMember(Name = "message", IsRequired = true, EmitDefaultValue = true)]
        public string Message { get; set; }

        /// <summary>
        /// A human-readable reason for the error
        /// </summary>
        /// <value>A human-readable reason for the error</value>
        /// <example>User with ID 1234 does not exist.</example>
        [DataMember(Name = "reason", EmitDefaultValue = false)]
        public string Reason { get; set; }

        /// <summary>
        /// The request ID  The request ID is often exposed internally in order to trace errors across service architectures. This is often a UUID.
        /// </summary>
        /// <value>The request ID  The request ID is often exposed internally in order to trace errors across service architectures. This is often a UUID.</value>
        /// <example>d7ef54b1-ec15-46e6-bccb-524b82c035e6</example>
        [DataMember(Name = "request", EmitDefaultValue = false)]
        public string Request { get; set; }

        /// <summary>
        /// The status description
        /// </summary>
        /// <value>The status description</value>
        /// <example>Not Found</example>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GenericError {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Debug: ").Append(Debug).Append("\n");
            sb.Append("  Details: ").Append(Details).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  Reason: ").Append(Reason).Append("\n");
            sb.Append("  Request: ").Append(Request).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}