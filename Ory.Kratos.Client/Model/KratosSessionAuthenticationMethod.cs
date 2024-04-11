/*
 * Ory Identities API
 *
 * This is the API specification for Ory Identities with features such as registration, login, recovery, account verification, profile settings, password reset, identity management, session management, email and sms delivery, and more. 
 *
 * The version of the OpenAPI document: v1.1.0
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
    /// A singular authenticator used during authentication / login.
    /// </summary>
    [DataContract(Name = "sessionAuthenticationMethod")]
    public partial class KratosSessionAuthenticationMethod : IEquatable<KratosSessionAuthenticationMethod>, IValidatableObject
    {
        /// <summary>
        /// Defines Method
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum MethodEnum
        {
            /// <summary>
            /// Enum LinkRecovery for value: link_recovery
            /// </summary>
            [EnumMember(Value = "link_recovery")]
            LinkRecovery = 1,

            /// <summary>
            /// Enum CodeRecovery for value: code_recovery
            /// </summary>
            [EnumMember(Value = "code_recovery")]
            CodeRecovery = 2,

            /// <summary>
            /// Enum Password for value: password
            /// </summary>
            [EnumMember(Value = "password")]
            Password = 3,

            /// <summary>
            /// Enum Code for value: code
            /// </summary>
            [EnumMember(Value = "code")]
            Code = 4,

            /// <summary>
            /// Enum Totp for value: totp
            /// </summary>
            [EnumMember(Value = "totp")]
            Totp = 5,

            /// <summary>
            /// Enum Oidc for value: oidc
            /// </summary>
            [EnumMember(Value = "oidc")]
            Oidc = 6,

            /// <summary>
            /// Enum Webauthn for value: webauthn
            /// </summary>
            [EnumMember(Value = "webauthn")]
            Webauthn = 7,

            /// <summary>
            /// Enum LookupSecret for value: lookup_secret
            /// </summary>
            [EnumMember(Value = "lookup_secret")]
            LookupSecret = 8,

            /// <summary>
            /// Enum V06LegacySession for value: v0.6_legacy_session
            /// </summary>
            [EnumMember(Value = "v0.6_legacy_session")]
            V06LegacySession = 9

        }


        /// <summary>
        /// Gets or Sets Method
        /// </summary>
        [DataMember(Name = "method", EmitDefaultValue = false)]
        public MethodEnum? Method { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KratosSessionAuthenticationMethod" /> class.
        /// </summary>
        /// <param name="aal">aal.</param>
        /// <param name="completedAt">When the authentication challenge was completed..</param>
        /// <param name="method">method.</param>
        /// <param name="organization">The Organization id used for authentication.</param>
        /// <param name="provider">OIDC or SAML provider id used for authentication.</param>
        public KratosSessionAuthenticationMethod(KratosAuthenticatorAssuranceLevel aal = default(KratosAuthenticatorAssuranceLevel), DateTime completedAt = default(DateTime), MethodEnum? method = default(MethodEnum?), string organization = default(string), string provider = default(string))
        {
            this.Aal = aal;
            this.CompletedAt = completedAt;
            this.Method = method;
            this.Organization = organization;
            this.Provider = provider;
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or Sets Aal
        /// </summary>
        [DataMember(Name = "aal", EmitDefaultValue = false)]
        public KratosAuthenticatorAssuranceLevel Aal { get; set; }

        /// <summary>
        /// When the authentication challenge was completed.
        /// </summary>
        /// <value>When the authentication challenge was completed.</value>
        [DataMember(Name = "completed_at", EmitDefaultValue = false)]
        public DateTime CompletedAt { get; set; }

        /// <summary>
        /// The Organization id used for authentication
        /// </summary>
        /// <value>The Organization id used for authentication</value>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public string Organization { get; set; }

        /// <summary>
        /// OIDC or SAML provider id used for authentication
        /// </summary>
        /// <value>OIDC or SAML provider id used for authentication</value>
        [DataMember(Name = "provider", EmitDefaultValue = false)]
        public string Provider { get; set; }

        /// <summary>
        /// Gets or Sets additional properties
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class KratosSessionAuthenticationMethod {\n");
            sb.Append("  Aal: ").Append(Aal).Append("\n");
            sb.Append("  CompletedAt: ").Append(CompletedAt).Append("\n");
            sb.Append("  Method: ").Append(Method).Append("\n");
            sb.Append("  Organization: ").Append(Organization).Append("\n");
            sb.Append("  Provider: ").Append(Provider).Append("\n");
            sb.Append("  AdditionalProperties: ").Append(AdditionalProperties).Append("\n");
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
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as KratosSessionAuthenticationMethod);
        }

        /// <summary>
        /// Returns true if KratosSessionAuthenticationMethod instances are equal
        /// </summary>
        /// <param name="input">Instance of KratosSessionAuthenticationMethod to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(KratosSessionAuthenticationMethod input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Aal == input.Aal ||
                    (this.Aal != null &&
                    this.Aal.Equals(input.Aal))
                ) && 
                (
                    this.CompletedAt == input.CompletedAt ||
                    (this.CompletedAt != null &&
                    this.CompletedAt.Equals(input.CompletedAt))
                ) && 
                (
                    this.Method == input.Method ||
                    this.Method.Equals(input.Method)
                ) && 
                (
                    this.Organization == input.Organization ||
                    (this.Organization != null &&
                    this.Organization.Equals(input.Organization))
                ) && 
                (
                    this.Provider == input.Provider ||
                    (this.Provider != null &&
                    this.Provider.Equals(input.Provider))
                )
                && (this.AdditionalProperties.Count == input.AdditionalProperties.Count && !this.AdditionalProperties.Except(input.AdditionalProperties).Any());
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Aal != null)
                {
                    hashCode = (hashCode * 59) + this.Aal.GetHashCode();
                }
                if (this.CompletedAt != null)
                {
                    hashCode = (hashCode * 59) + this.CompletedAt.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Method.GetHashCode();
                if (this.Organization != null)
                {
                    hashCode = (hashCode * 59) + this.Organization.GetHashCode();
                }
                if (this.Provider != null)
                {
                    hashCode = (hashCode * 59) + this.Provider.GetHashCode();
                }
                if (this.AdditionalProperties != null)
                {
                    hashCode = (hashCode * 59) + this.AdditionalProperties.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}