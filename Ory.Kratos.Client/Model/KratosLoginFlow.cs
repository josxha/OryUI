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
    /// This object represents a login flow. A login flow is initiated at the \&quot;Initiate Login API / Browser Flow\&quot; endpoint by a client.  Once a login flow is completed successfully, a session cookie or session token will be issued.
    /// </summary>
    [DataContract(Name = "loginFlow")]
    public partial class KratosLoginFlow : IValidatableObject
    {
        /// <summary>
        /// The active login method  If set contains the login method used. If the flow is new, it is unset. password CredentialsTypePassword oidc CredentialsTypeOIDC totp CredentialsTypeTOTP lookup_secret CredentialsTypeLookup webauthn CredentialsTypeWebAuthn code CredentialsTypeCodeAuth link_recovery CredentialsTypeRecoveryLink  CredentialsTypeRecoveryLink is a special credential type linked to the link strategy (recovery flow).  It is not used within the credentials object itself. code_recovery CredentialsTypeRecoveryCode
        /// </summary>
        /// <value>The active login method  If set contains the login method used. If the flow is new, it is unset. password CredentialsTypePassword oidc CredentialsTypeOIDC totp CredentialsTypeTOTP lookup_secret CredentialsTypeLookup webauthn CredentialsTypeWebAuthn code CredentialsTypeCodeAuth link_recovery CredentialsTypeRecoveryLink  CredentialsTypeRecoveryLink is a special credential type linked to the link strategy (recovery flow).  It is not used within the credentials object itself. code_recovery CredentialsTypeRecoveryCode</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ActiveEnum
        {
            /// <summary>
            /// Enum Password for value: password
            /// </summary>
            [EnumMember(Value = "password")]
            Password = 1,

            /// <summary>
            /// Enum Oidc for value: oidc
            /// </summary>
            [EnumMember(Value = "oidc")]
            Oidc = 2,

            /// <summary>
            /// Enum Totp for value: totp
            /// </summary>
            [EnumMember(Value = "totp")]
            Totp = 3,

            /// <summary>
            /// Enum LookupSecret for value: lookup_secret
            /// </summary>
            [EnumMember(Value = "lookup_secret")]
            LookupSecret = 4,

            /// <summary>
            /// Enum Webauthn for value: webauthn
            /// </summary>
            [EnumMember(Value = "webauthn")]
            Webauthn = 5,

            /// <summary>
            /// Enum Code for value: code
            /// </summary>
            [EnumMember(Value = "code")]
            Code = 6,

            /// <summary>
            /// Enum LinkRecovery for value: link_recovery
            /// </summary>
            [EnumMember(Value = "link_recovery")]
            LinkRecovery = 7,

            /// <summary>
            /// Enum CodeRecovery for value: code_recovery
            /// </summary>
            [EnumMember(Value = "code_recovery")]
            CodeRecovery = 8
        }


        /// <summary>
        /// The active login method  If set contains the login method used. If the flow is new, it is unset. password CredentialsTypePassword oidc CredentialsTypeOIDC totp CredentialsTypeTOTP lookup_secret CredentialsTypeLookup webauthn CredentialsTypeWebAuthn code CredentialsTypeCodeAuth link_recovery CredentialsTypeRecoveryLink  CredentialsTypeRecoveryLink is a special credential type linked to the link strategy (recovery flow).  It is not used within the credentials object itself. code_recovery CredentialsTypeRecoveryCode
        /// </summary>
        /// <value>The active login method  If set contains the login method used. If the flow is new, it is unset. password CredentialsTypePassword oidc CredentialsTypeOIDC totp CredentialsTypeTOTP lookup_secret CredentialsTypeLookup webauthn CredentialsTypeWebAuthn code CredentialsTypeCodeAuth link_recovery CredentialsTypeRecoveryLink  CredentialsTypeRecoveryLink is a special credential type linked to the link strategy (recovery flow).  It is not used within the credentials object itself. code_recovery CredentialsTypeRecoveryCode</value>
        [DataMember(Name = "active", EmitDefaultValue = false)]
        public ActiveEnum? Active { get; set; }

        /// <summary>
        /// Gets or Sets RequestedAal
        /// </summary>
        [DataMember(Name = "requested_aal", EmitDefaultValue = false)]
        public KratosAuthenticatorAssuranceLevel? RequestedAal { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KratosLoginFlow" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected KratosLoginFlow() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="KratosLoginFlow" /> class.
        /// </summary>
        /// <param name="active">The active login method  If set contains the login method used. If the flow is new, it is unset. password CredentialsTypePassword oidc CredentialsTypeOIDC totp CredentialsTypeTOTP lookup_secret CredentialsTypeLookup webauthn CredentialsTypeWebAuthn code CredentialsTypeCodeAuth link_recovery CredentialsTypeRecoveryLink  CredentialsTypeRecoveryLink is a special credential type linked to the link strategy (recovery flow).  It is not used within the credentials object itself. code_recovery CredentialsTypeRecoveryCode.</param>
        /// <param name="createdAt">CreatedAt is a helper struct field for gobuffalo.pop..</param>
        /// <param name="expiresAt">ExpiresAt is the time (UTC) when the flow expires. If the user still wishes to log in, a new flow has to be initiated. (required).</param>
        /// <param name="id">ID represents the flow&#39;s unique ID. When performing the login flow, this represents the id in the login UI&#39;s query parameter: http://&lt;selfservice.flows.login.ui_url&gt;/?flow&#x3D;&lt;flow_id&gt; (required).</param>
        /// <param name="issuedAt">IssuedAt is the time (UTC) when the flow started. (required).</param>
        /// <param name="oauth2LoginChallenge">Ory OAuth 2.0 Login Challenge.  This value is set using the &#x60;login_challenge&#x60; query parameter of the registration and login endpoints. If set will cooperate with Ory OAuth2 and OpenID to act as an OAuth2 server / OpenID Provider..</param>
        /// <param name="oauth2LoginRequest">oauth2LoginRequest.</param>
        /// <param name="organizationId">organizationId.</param>
        /// <param name="refresh">Refresh stores whether this login flow should enforce re-authentication..</param>
        /// <param name="requestUrl">RequestURL is the initial URL that was requested from Ory Kratos. It can be used to forward information contained in the URL&#39;s path or query for example. (required).</param>
        /// <param name="requestedAal">requestedAal.</param>
        /// <param name="returnTo">ReturnTo contains the requested return_to URL..</param>
        /// <param name="sessionTokenExchangeCode">SessionTokenExchangeCode holds the secret code that the client can use to retrieve a session token after the login flow has been completed. This is only set if the client has requested a session token exchange code, and if the flow is of type \&quot;api\&quot;, and only on creating the login flow..</param>
        /// <param name="state">State represents the state of this request:  choose_method: ask the user to choose a method to sign in with sent_email: the email has been sent to the user passed_challenge: the request was successful and the login challenge was passed. (required).</param>
        /// <param name="type">The flow type can either be &#x60;api&#x60; or &#x60;browser&#x60;. (required).</param>
        /// <param name="ui">ui (required).</param>
        /// <param name="updatedAt">UpdatedAt is a helper struct field for gobuffalo.pop..</param>
        public KratosLoginFlow(ActiveEnum? active = default(ActiveEnum?), DateTime createdAt = default(DateTime), DateTime expiresAt = default(DateTime), string id = default(string), DateTime issuedAt = default(DateTime), string oauth2LoginChallenge = default(string), KratosOAuth2LoginRequest oauth2LoginRequest = default(KratosOAuth2LoginRequest), string organizationId = default(string), bool refresh = default(bool), string requestUrl = default(string), KratosAuthenticatorAssuranceLevel? requestedAal = default(KratosAuthenticatorAssuranceLevel?), string returnTo = default(string), string sessionTokenExchangeCode = default(string), Object state = default(Object), string type = default(string), KratosUiContainer ui = default(KratosUiContainer), DateTime updatedAt = default(DateTime))
        {
            this.ExpiresAt = expiresAt;
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new ArgumentNullException("id is a required property for KratosLoginFlow and cannot be null");
            }
            this.Id = id;
            this.IssuedAt = issuedAt;
            // to ensure "requestUrl" is required (not null)
            if (requestUrl == null)
            {
                throw new ArgumentNullException("requestUrl is a required property for KratosLoginFlow and cannot be null");
            }
            this.RequestUrl = requestUrl;
            // to ensure "state" is required (not null)
            if (state == null)
            {
                throw new ArgumentNullException("state is a required property for KratosLoginFlow and cannot be null");
            }
            this.State = state;
            // to ensure "type" is required (not null)
            if (type == null)
            {
                throw new ArgumentNullException("type is a required property for KratosLoginFlow and cannot be null");
            }
            this.Type = type;
            // to ensure "ui" is required (not null)
            if (ui == null)
            {
                throw new ArgumentNullException("ui is a required property for KratosLoginFlow and cannot be null");
            }
            this.Ui = ui;
            this.Active = active;
            this.CreatedAt = createdAt;
            this.Oauth2LoginChallenge = oauth2LoginChallenge;
            this.Oauth2LoginRequest = oauth2LoginRequest;
            this.OrganizationId = organizationId;
            this.Refresh = refresh;
            this.RequestedAal = requestedAal;
            this.ReturnTo = returnTo;
            this.SessionTokenExchangeCode = sessionTokenExchangeCode;
            this.UpdatedAt = updatedAt;
        }

        /// <summary>
        /// CreatedAt is a helper struct field for gobuffalo.pop.
        /// </summary>
        /// <value>CreatedAt is a helper struct field for gobuffalo.pop.</value>
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// ExpiresAt is the time (UTC) when the flow expires. If the user still wishes to log in, a new flow has to be initiated.
        /// </summary>
        /// <value>ExpiresAt is the time (UTC) when the flow expires. If the user still wishes to log in, a new flow has to be initiated.</value>
        [DataMember(Name = "expires_at", IsRequired = true, EmitDefaultValue = true)]
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// ID represents the flow&#39;s unique ID. When performing the login flow, this represents the id in the login UI&#39;s query parameter: http://&lt;selfservice.flows.login.ui_url&gt;/?flow&#x3D;&lt;flow_id&gt;
        /// </summary>
        /// <value>ID represents the flow&#39;s unique ID. When performing the login flow, this represents the id in the login UI&#39;s query parameter: http://&lt;selfservice.flows.login.ui_url&gt;/?flow&#x3D;&lt;flow_id&gt;</value>
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public string Id { get; set; }

        /// <summary>
        /// IssuedAt is the time (UTC) when the flow started.
        /// </summary>
        /// <value>IssuedAt is the time (UTC) when the flow started.</value>
        [DataMember(Name = "issued_at", IsRequired = true, EmitDefaultValue = true)]
        public DateTime IssuedAt { get; set; }

        /// <summary>
        /// Ory OAuth 2.0 Login Challenge.  This value is set using the &#x60;login_challenge&#x60; query parameter of the registration and login endpoints. If set will cooperate with Ory OAuth2 and OpenID to act as an OAuth2 server / OpenID Provider.
        /// </summary>
        /// <value>Ory OAuth 2.0 Login Challenge.  This value is set using the &#x60;login_challenge&#x60; query parameter of the registration and login endpoints. If set will cooperate with Ory OAuth2 and OpenID to act as an OAuth2 server / OpenID Provider.</value>
        [DataMember(Name = "oauth2_login_challenge", EmitDefaultValue = false)]
        public string Oauth2LoginChallenge { get; set; }

        /// <summary>
        /// Gets or Sets Oauth2LoginRequest
        /// </summary>
        [DataMember(Name = "oauth2_login_request", EmitDefaultValue = false)]
        public KratosOAuth2LoginRequest Oauth2LoginRequest { get; set; }

        /// <summary>
        /// Gets or Sets OrganizationId
        /// </summary>
        [DataMember(Name = "organization_id", EmitDefaultValue = true)]
        public string OrganizationId { get; set; }

        /// <summary>
        /// Refresh stores whether this login flow should enforce re-authentication.
        /// </summary>
        /// <value>Refresh stores whether this login flow should enforce re-authentication.</value>
        [DataMember(Name = "refresh", EmitDefaultValue = true)]
        public bool Refresh { get; set; }

        /// <summary>
        /// RequestURL is the initial URL that was requested from Ory Kratos. It can be used to forward information contained in the URL&#39;s path or query for example.
        /// </summary>
        /// <value>RequestURL is the initial URL that was requested from Ory Kratos. It can be used to forward information contained in the URL&#39;s path or query for example.</value>
        [DataMember(Name = "request_url", IsRequired = true, EmitDefaultValue = true)]
        public string RequestUrl { get; set; }

        /// <summary>
        /// ReturnTo contains the requested return_to URL.
        /// </summary>
        /// <value>ReturnTo contains the requested return_to URL.</value>
        [DataMember(Name = "return_to", EmitDefaultValue = false)]
        public string ReturnTo { get; set; }

        /// <summary>
        /// SessionTokenExchangeCode holds the secret code that the client can use to retrieve a session token after the login flow has been completed. This is only set if the client has requested a session token exchange code, and if the flow is of type \&quot;api\&quot;, and only on creating the login flow.
        /// </summary>
        /// <value>SessionTokenExchangeCode holds the secret code that the client can use to retrieve a session token after the login flow has been completed. This is only set if the client has requested a session token exchange code, and if the flow is of type \&quot;api\&quot;, and only on creating the login flow.</value>
        [DataMember(Name = "session_token_exchange_code", EmitDefaultValue = false)]
        public string SessionTokenExchangeCode { get; set; }

        /// <summary>
        /// State represents the state of this request:  choose_method: ask the user to choose a method to sign in with sent_email: the email has been sent to the user passed_challenge: the request was successful and the login challenge was passed.
        /// </summary>
        /// <value>State represents the state of this request:  choose_method: ask the user to choose a method to sign in with sent_email: the email has been sent to the user passed_challenge: the request was successful and the login challenge was passed.</value>
        [DataMember(Name = "state", IsRequired = true, EmitDefaultValue = true)]
        public Object State { get; set; }

        /// <summary>
        /// The flow type can either be &#x60;api&#x60; or &#x60;browser&#x60;.
        /// </summary>
        /// <value>The flow type can either be &#x60;api&#x60; or &#x60;browser&#x60;.</value>
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets Ui
        /// </summary>
        [DataMember(Name = "ui", IsRequired = true, EmitDefaultValue = true)]
        public KratosUiContainer Ui { get; set; }

        /// <summary>
        /// UpdatedAt is a helper struct field for gobuffalo.pop.
        /// </summary>
        /// <value>UpdatedAt is a helper struct field for gobuffalo.pop.</value>
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class KratosLoginFlow {\n");
            sb.Append("  Active: ").Append(Active).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  ExpiresAt: ").Append(ExpiresAt).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  IssuedAt: ").Append(IssuedAt).Append("\n");
            sb.Append("  Oauth2LoginChallenge: ").Append(Oauth2LoginChallenge).Append("\n");
            sb.Append("  Oauth2LoginRequest: ").Append(Oauth2LoginRequest).Append("\n");
            sb.Append("  OrganizationId: ").Append(OrganizationId).Append("\n");
            sb.Append("  Refresh: ").Append(Refresh).Append("\n");
            sb.Append("  RequestUrl: ").Append(RequestUrl).Append("\n");
            sb.Append("  RequestedAal: ").Append(RequestedAal).Append("\n");
            sb.Append("  ReturnTo: ").Append(ReturnTo).Append("\n");
            sb.Append("  SessionTokenExchangeCode: ").Append(SessionTokenExchangeCode).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Ui: ").Append(Ui).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
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