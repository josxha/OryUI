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
    /// Is sent when a flow is expired
    /// </summary>
    [DataContract(Name = "selfServiceFlowExpiredError")]
    public partial class SelfServiceFlowExpiredError : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfServiceFlowExpiredError" /> class.
        /// </summary>
        /// <param name="error">error.</param>
        /// <param name="expiredAt">When the flow has expired.</param>
        /// <param name="since">A Duration represents the elapsed time between two instants as an int64 nanosecond count. The representation limits the largest representable duration to approximately 290 years..</param>
        /// <param name="useFlowId">The flow ID that should be used for the new flow as it contains the correct messages..</param>
        public SelfServiceFlowExpiredError(GenericError error = default(GenericError), DateTime expiredAt = default(DateTime), long since = default(long), string useFlowId = default(string))
        {
            this.Error = error;
            this.ExpiredAt = expiredAt;
            this.Since = since;
            this.UseFlowId = useFlowId;
        }

        /// <summary>
        /// Gets or Sets Error
        /// </summary>
        [DataMember(Name = "error", EmitDefaultValue = false)]
        public GenericError Error { get; set; }

        /// <summary>
        /// When the flow has expired
        /// </summary>
        /// <value>When the flow has expired</value>
        [DataMember(Name = "expired_at", EmitDefaultValue = false)]
        public DateTime ExpiredAt { get; set; }

        /// <summary>
        /// A Duration represents the elapsed time between two instants as an int64 nanosecond count. The representation limits the largest representable duration to approximately 290 years.
        /// </summary>
        /// <value>A Duration represents the elapsed time between two instants as an int64 nanosecond count. The representation limits the largest representable duration to approximately 290 years.</value>
        [DataMember(Name = "since", EmitDefaultValue = false)]
        public long Since { get; set; }

        /// <summary>
        /// The flow ID that should be used for the new flow as it contains the correct messages.
        /// </summary>
        /// <value>The flow ID that should be used for the new flow as it contains the correct messages.</value>
        [DataMember(Name = "use_flow_id", EmitDefaultValue = false)]
        public string UseFlowId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class SelfServiceFlowExpiredError {\n");
            sb.Append("  Error: ").Append(Error).Append("\n");
            sb.Append("  ExpiredAt: ").Append(ExpiredAt).Append("\n");
            sb.Append("  Since: ").Append(Since).Append("\n");
            sb.Append("  UseFlowId: ").Append(UseFlowId).Append("\n");
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