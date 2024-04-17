/*
 * Ory Hydra API
 *
 * Documentation for all of Ory Hydra's APIs. 
 *
 * Contact: hi@ory.sh
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
using OpenAPIDateConverter = Ory.Hydra.Client.Client.OpenAPIDateConverter;

namespace Ory.Hydra.Client.Model
{
    /// <summary>
    /// OAuth2 JWT Bearer Grant Type Issuer Trusted JSON Web Key
    /// </summary>
    [DataContract(Name = "trustedOAuth2JwtGrantJsonWebKey")]
    public partial class TrustedOAuth2JwtGrantJsonWebKey : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrustedOAuth2JwtGrantJsonWebKey" /> class.
        /// </summary>
        /// <param name="kid">The \&quot;key_id\&quot; is key unique identifier (same as kid header in jws/jwt)..</param>
        /// <param name="set">The \&quot;set\&quot; is basically a name for a group(set) of keys. Will be the same as \&quot;issuer\&quot; in grant..</param>
        public TrustedOAuth2JwtGrantJsonWebKey(string kid = default(string), string set = default(string))
        {
            this.Kid = kid;
            this.Set = set;
        }

        /// <summary>
        /// The \&quot;key_id\&quot; is key unique identifier (same as kid header in jws/jwt).
        /// </summary>
        /// <value>The \&quot;key_id\&quot; is key unique identifier (same as kid header in jws/jwt).</value>
        /// <example>123e4567-e89b-12d3-a456-426655440000</example>
        [DataMember(Name = "kid", EmitDefaultValue = false)]
        public string Kid { get; set; }

        /// <summary>
        /// The \&quot;set\&quot; is basically a name for a group(set) of keys. Will be the same as \&quot;issuer\&quot; in grant.
        /// </summary>
        /// <value>The \&quot;set\&quot; is basically a name for a group(set) of keys. Will be the same as \&quot;issuer\&quot; in grant.</value>
        /// <example>https://jwt-idp.example.com</example>
        [DataMember(Name = "set", EmitDefaultValue = false)]
        public string Set { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class TrustedOAuth2JwtGrantJsonWebKey {\n");
            sb.Append("  Kid: ").Append(Kid).Append("\n");
            sb.Append("  Set: ").Append(Set).Append("\n");
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
