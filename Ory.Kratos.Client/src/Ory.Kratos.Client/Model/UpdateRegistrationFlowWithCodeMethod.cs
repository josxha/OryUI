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
    /// Update Registration Flow with Code Method
    /// </summary>
    [DataContract(Name = "updateRegistrationFlowWithCodeMethod")]
    public partial class UpdateRegistrationFlowWithCodeMethod : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRegistrationFlowWithCodeMethod" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected UpdateRegistrationFlowWithCodeMethod() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRegistrationFlowWithCodeMethod" /> class.
        /// </summary>
        /// <param name="code">The OTP Code sent to the user.</param>
        /// <param name="csrfToken">The CSRF Token.</param>
        /// <param name="method">Method to use  This field must be set to &#x60;code&#x60; when using the code method. (required).</param>
        /// <param name="resend">Resend restarts the flow with a new code.</param>
        /// <param name="traits">The identity&#39;s traits (required).</param>
        /// <param name="transientPayload">Transient data to pass along to any webhooks.</param>
        public UpdateRegistrationFlowWithCodeMethod(string code = default(string), string csrfToken = default(string), string method = default(string), string resend = default(string), Object traits = default(Object), Object transientPayload = default(Object))
        {
            // to ensure "method" is required (not null)
            if (method == null)
            {
                throw new ArgumentNullException("method is a required property for UpdateRegistrationFlowWithCodeMethod and cannot be null");
            }
            this.Method = method;
            // to ensure "traits" is required (not null)
            if (traits == null)
            {
                throw new ArgumentNullException("traits is a required property for UpdateRegistrationFlowWithCodeMethod and cannot be null");
            }
            this.Traits = traits;
            this.Code = code;
            this.CsrfToken = csrfToken;
            this.Resend = resend;
            this.TransientPayload = transientPayload;
        }

        /// <summary>
        /// The OTP Code sent to the user
        /// </summary>
        /// <value>The OTP Code sent to the user</value>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        /// <summary>
        /// The CSRF Token
        /// </summary>
        /// <value>The CSRF Token</value>
        [DataMember(Name = "csrf_token", EmitDefaultValue = false)]
        public string CsrfToken { get; set; }

        /// <summary>
        /// Method to use  This field must be set to &#x60;code&#x60; when using the code method.
        /// </summary>
        /// <value>Method to use  This field must be set to &#x60;code&#x60; when using the code method.</value>
        [DataMember(Name = "method", IsRequired = true, EmitDefaultValue = true)]
        public string Method { get; set; }

        /// <summary>
        /// Resend restarts the flow with a new code
        /// </summary>
        /// <value>Resend restarts the flow with a new code</value>
        [DataMember(Name = "resend", EmitDefaultValue = false)]
        public string Resend { get; set; }

        /// <summary>
        /// The identity&#39;s traits
        /// </summary>
        /// <value>The identity&#39;s traits</value>
        [DataMember(Name = "traits", IsRequired = true, EmitDefaultValue = true)]
        public Object Traits { get; set; }

        /// <summary>
        /// Transient data to pass along to any webhooks
        /// </summary>
        /// <value>Transient data to pass along to any webhooks</value>
        [DataMember(Name = "transient_payload", EmitDefaultValue = false)]
        public Object TransientPayload { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateRegistrationFlowWithCodeMethod {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  CsrfToken: ").Append(CsrfToken).Append("\n");
            sb.Append("  Method: ").Append(Method).Append("\n");
            sb.Append("  Resend: ").Append(Resend).Append("\n");
            sb.Append("  Traits: ").Append(Traits).Append("\n");
            sb.Append("  TransientPayload: ").Append(TransientPayload).Append("\n");
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