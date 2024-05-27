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
    /// HydraOAuth2LogoutRequest
    /// </summary>
    [DataContract(Name = "oAuth2LogoutRequest")]
    public partial class HydraOAuth2LogoutRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraOAuth2LogoutRequest" /> class.
        /// </summary>
        /// <param name="challenge">Challenge is the identifier (\&quot;logout challenge\&quot;) of the logout authentication request. It is used to identify the session..</param>
        /// <param name="varClient">varClient.</param>
        /// <param name="requestUrl">RequestURL is the original Logout URL requested..</param>
        /// <param name="rpInitiated">RPInitiated is set to true if the request was initiated by a Relying Party (RP), also known as an OAuth 2.0 Client..</param>
        /// <param name="sid">SessionID is the login session ID that was requested to log out..</param>
        /// <param name="subject">Subject is the user for whom the logout was request..</param>
        public HydraOAuth2LogoutRequest(string challenge = default(string), HydraOAuth2Client varClient = default(HydraOAuth2Client), string requestUrl = default(string), bool rpInitiated = default(bool), string sid = default(string), string subject = default(string))
        {
            this.Challenge = challenge;
            this.VarClient = varClient;
            this.RequestUrl = requestUrl;
            this.RpInitiated = rpInitiated;
            this.Sid = sid;
            this.Subject = subject;
        }

        /// <summary>
        /// Challenge is the identifier (\&quot;logout challenge\&quot;) of the logout authentication request. It is used to identify the session.
        /// </summary>
        /// <value>Challenge is the identifier (\&quot;logout challenge\&quot;) of the logout authentication request. It is used to identify the session.</value>
        [DataMember(Name = "challenge", EmitDefaultValue = false)]
        public string Challenge { get; set; }

        /// <summary>
        /// Gets or Sets VarClient
        /// </summary>
        [DataMember(Name = "client", EmitDefaultValue = false)]
        public HydraOAuth2Client VarClient { get; set; }

        /// <summary>
        /// RequestURL is the original Logout URL requested.
        /// </summary>
        /// <value>RequestURL is the original Logout URL requested.</value>
        [DataMember(Name = "request_url", EmitDefaultValue = false)]
        public string RequestUrl { get; set; }

        /// <summary>
        /// RPInitiated is set to true if the request was initiated by a Relying Party (RP), also known as an OAuth 2.0 Client.
        /// </summary>
        /// <value>RPInitiated is set to true if the request was initiated by a Relying Party (RP), also known as an OAuth 2.0 Client.</value>
        [DataMember(Name = "rp_initiated", EmitDefaultValue = true)]
        public bool RpInitiated { get; set; }

        /// <summary>
        /// SessionID is the login session ID that was requested to log out.
        /// </summary>
        /// <value>SessionID is the login session ID that was requested to log out.</value>
        [DataMember(Name = "sid", EmitDefaultValue = false)]
        public string Sid { get; set; }

        /// <summary>
        /// Subject is the user for whom the logout was request.
        /// </summary>
        /// <value>Subject is the user for whom the logout was request.</value>
        [DataMember(Name = "subject", EmitDefaultValue = false)]
        public string Subject { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class HydraOAuth2LogoutRequest {\n");
            sb.Append("  Challenge: ").Append(Challenge).Append("\n");
            sb.Append("  VarClient: ").Append(VarClient).Append("\n");
            sb.Append("  RequestUrl: ").Append(RequestUrl).Append("\n");
            sb.Append("  RpInitiated: ").Append(RpInitiated).Append("\n");
            sb.Append("  Sid: ").Append(Sid).Append("\n");
            sb.Append("  Subject: ").Append(Subject).Append("\n");
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