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
    /// KratosIsReady503Response
    /// </summary>
    [DataContract(Name = "isReady_503_response")]
    public partial class KratosIsReady503Response : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KratosIsReady503Response" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected KratosIsReady503Response() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="KratosIsReady503Response" /> class.
        /// </summary>
        /// <param name="errors">Errors contains a list of errors that caused the not ready status. (required).</param>
        public KratosIsReady503Response(Dictionary<string, string> errors = default(Dictionary<string, string>))
        {
            // to ensure "errors" is required (not null)
            if (errors == null)
            {
                throw new ArgumentNullException("errors is a required property for KratosIsReady503Response and cannot be null");
            }
            this.Errors = errors;
        }

        /// <summary>
        /// Errors contains a list of errors that caused the not ready status.
        /// </summary>
        /// <value>Errors contains a list of errors that caused the not ready status.</value>
        [DataMember(Name = "errors", IsRequired = true, EmitDefaultValue = true)]
        public Dictionary<string, string> Errors { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class KratosIsReady503Response {\n");
            sb.Append("  Errors: ").Append(Errors).Append("\n");
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
