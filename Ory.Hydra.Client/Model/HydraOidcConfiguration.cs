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
    /// Includes links to several endpoints (for example &#x60;/oauth2/token&#x60;) and exposes information on supported signature algorithms among others.
    /// </summary>
    [DataContract(Name = "oidcConfiguration")]
    public partial class HydraOidcConfiguration : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraOidcConfiguration" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected HydraOidcConfiguration() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraOidcConfiguration" /> class.
        /// </summary>
        /// <param name="authorizationEndpoint">OAuth 2.0 Authorization Endpoint URL (required).</param>
        /// <param name="backchannelLogoutSessionSupported">OpenID Connect Back-Channel Logout Session Required  Boolean value specifying whether the OP can pass a sid (session ID) Claim in the Logout Token to identify the RP session with the OP. If supported, the sid Claim is also included in ID Tokens issued by the OP.</param>
        /// <param name="backchannelLogoutSupported">OpenID Connect Back-Channel Logout Supported  Boolean value specifying whether the OP supports back-channel logout, with true indicating support..</param>
        /// <param name="claimsParameterSupported">OpenID Connect Claims Parameter Parameter Supported  Boolean value specifying whether the OP supports use of the claims parameter, with true indicating support..</param>
        /// <param name="claimsSupported">OpenID Connect Supported Claims  JSON array containing a list of the Claim Names of the Claims that the OpenID Provider MAY be able to supply values for. Note that for privacy or other reasons, this might not be an exhaustive list..</param>
        /// <param name="codeChallengeMethodsSupported">OAuth 2.0 PKCE Supported Code Challenge Methods  JSON array containing a list of Proof Key for Code Exchange (PKCE) [RFC7636] code challenge methods supported by this authorization server..</param>
        /// <param name="credentialsEndpointDraft00">OpenID Connect Verifiable Credentials Endpoint  Contains the URL of the Verifiable Credentials Endpoint..</param>
        /// <param name="credentialsSupportedDraft00">OpenID Connect Verifiable Credentials Supported  JSON array containing a list of the Verifiable Credentials supported by this authorization server..</param>
        /// <param name="endSessionEndpoint">OpenID Connect End-Session Endpoint  URL at the OP to which an RP can perform a redirect to request that the End-User be logged out at the OP..</param>
        /// <param name="frontchannelLogoutSessionSupported">OpenID Connect Front-Channel Logout Session Required  Boolean value specifying whether the OP can pass iss (issuer) and sid (session ID) query parameters to identify the RP session with the OP when the frontchannel_logout_uri is used. If supported, the sid Claim is also included in ID Tokens issued by the OP..</param>
        /// <param name="frontchannelLogoutSupported">OpenID Connect Front-Channel Logout Supported  Boolean value specifying whether the OP supports HTTP-based logout, with true indicating support..</param>
        /// <param name="grantTypesSupported">OAuth 2.0 Supported Grant Types  JSON array containing a list of the OAuth 2.0 Grant Type values that this OP supports..</param>
        /// <param name="idTokenSignedResponseAlg">OpenID Connect Default ID Token Signing Algorithms  Algorithm used to sign OpenID Connect ID Tokens. (required).</param>
        /// <param name="idTokenSigningAlgValuesSupported">OpenID Connect Supported ID Token Signing Algorithms  JSON array containing a list of the JWS signing algorithms (alg values) supported by the OP for the ID Token to encode the Claims in a JWT. (required).</param>
        /// <param name="issuer">OpenID Connect Issuer URL  An URL using the https scheme with no query or fragment component that the OP asserts as its IssuerURL Identifier. If IssuerURL discovery is supported , this value MUST be identical to the issuer value returned by WebFinger. This also MUST be identical to the iss Claim value in ID Tokens issued from this IssuerURL. (required).</param>
        /// <param name="jwksUri">OpenID Connect Well-Known JSON Web Keys URL  URL of the OP&#39;s JSON Web Key Set [JWK] document. This contains the signing key(s) the RP uses to validate signatures from the OP. The JWK Set MAY also contain the Server&#39;s encryption key(s), which are used by RPs to encrypt requests to the Server. When both signing and encryption keys are made available, a use (Key Use) parameter value is REQUIRED for all keys in the referenced JWK Set to indicate each key&#39;s intended usage. Although some algorithms allow the same key to be used for both signatures and encryption, doing so is NOT RECOMMENDED, as it is less secure. The JWK x5c parameter MAY be used to provide X.509 representations of keys provided. When used, the bare key values MUST still be present and MUST match those in the certificate. (required).</param>
        /// <param name="registrationEndpoint">OpenID Connect Dynamic Client Registration Endpoint URL.</param>
        /// <param name="requestObjectSigningAlgValuesSupported">OpenID Connect Supported Request Object Signing Algorithms  JSON array containing a list of the JWS signing algorithms (alg values) supported by the OP for Request Objects, which are described in Section 6.1 of OpenID Connect Core 1.0 [OpenID.Core]. These algorithms are used both when the Request Object is passed by value (using the request parameter) and when it is passed by reference (using the request_uri parameter)..</param>
        /// <param name="requestParameterSupported">OpenID Connect Request Parameter Supported  Boolean value specifying whether the OP supports use of the request parameter, with true indicating support..</param>
        /// <param name="requestUriParameterSupported">OpenID Connect Request URI Parameter Supported  Boolean value specifying whether the OP supports use of the request_uri parameter, with true indicating support..</param>
        /// <param name="requireRequestUriRegistration">OpenID Connect Requires Request URI Registration  Boolean value specifying whether the OP requires any request_uri values used to be pre-registered using the request_uris registration parameter..</param>
        /// <param name="responseModesSupported">OAuth 2.0 Supported Response Modes  JSON array containing a list of the OAuth 2.0 response_mode values that this OP supports..</param>
        /// <param name="responseTypesSupported">OAuth 2.0 Supported Response Types  JSON array containing a list of the OAuth 2.0 response_type values that this OP supports. Dynamic OpenID Providers MUST support the code, id_token, and the token id_token Response Type values. (required).</param>
        /// <param name="revocationEndpoint">OAuth 2.0 Token Revocation URL  URL of the authorization server&#39;s OAuth 2.0 revocation endpoint..</param>
        /// <param name="scopesSupported">OAuth 2.0 Supported Scope Values  JSON array containing a list of the OAuth 2.0 [RFC6749] scope values that this server supports. The server MUST support the openid scope value. Servers MAY choose not to advertise some supported scope values even when this parameter is used.</param>
        /// <param name="subjectTypesSupported">OpenID Connect Supported Subject Types  JSON array containing a list of the Subject Identifier types that this OP supports. Valid types include pairwise and public. (required).</param>
        /// <param name="tokenEndpoint">OAuth 2.0 Token Endpoint URL (required).</param>
        /// <param name="tokenEndpointAuthMethodsSupported">OAuth 2.0 Supported Client Authentication Methods  JSON array containing a list of Client Authentication methods supported by this Token Endpoint. The options are client_secret_post, client_secret_basic, client_secret_jwt, and private_key_jwt, as described in Section 9 of OpenID Connect Core 1.0.</param>
        /// <param name="userinfoEndpoint">OpenID Connect Userinfo URL  URL of the OP&#39;s UserInfo Endpoint..</param>
        /// <param name="userinfoSignedResponseAlg">OpenID Connect User Userinfo Signing Algorithm  Algorithm used to sign OpenID Connect Userinfo Responses. (required).</param>
        /// <param name="userinfoSigningAlgValuesSupported">OpenID Connect Supported Userinfo Signing Algorithm  JSON array containing a list of the JWS [JWS] signing algorithms (alg values) [JWA] supported by the UserInfo Endpoint to encode the Claims in a JWT [JWT]..</param>
        public HydraOidcConfiguration(string authorizationEndpoint = default(string), bool backchannelLogoutSessionSupported = default(bool), bool backchannelLogoutSupported = default(bool), bool claimsParameterSupported = default(bool), List<string> claimsSupported = default(List<string>), List<string> codeChallengeMethodsSupported = default(List<string>), string credentialsEndpointDraft00 = default(string), List<HydraCredentialSupportedDraft00> credentialsSupportedDraft00 = default(List<HydraCredentialSupportedDraft00>), string endSessionEndpoint = default(string), bool frontchannelLogoutSessionSupported = default(bool), bool frontchannelLogoutSupported = default(bool), List<string> grantTypesSupported = default(List<string>), List<string> idTokenSignedResponseAlg = default(List<string>), List<string> idTokenSigningAlgValuesSupported = default(List<string>), string issuer = default(string), string jwksUri = default(string), string registrationEndpoint = default(string), List<string> requestObjectSigningAlgValuesSupported = default(List<string>), bool requestParameterSupported = default(bool), bool requestUriParameterSupported = default(bool), bool requireRequestUriRegistration = default(bool), List<string> responseModesSupported = default(List<string>), List<string> responseTypesSupported = default(List<string>), string revocationEndpoint = default(string), List<string> scopesSupported = default(List<string>), List<string> subjectTypesSupported = default(List<string>), string tokenEndpoint = default(string), List<string> tokenEndpointAuthMethodsSupported = default(List<string>), string userinfoEndpoint = default(string), List<string> userinfoSignedResponseAlg = default(List<string>), List<string> userinfoSigningAlgValuesSupported = default(List<string>))
        {
            // to ensure "authorizationEndpoint" is required (not null)
            if (authorizationEndpoint == null)
            {
                throw new ArgumentNullException("authorizationEndpoint is a required property for HydraOidcConfiguration and cannot be null");
            }
            this.AuthorizationEndpoint = authorizationEndpoint;
            // to ensure "idTokenSignedResponseAlg" is required (not null)
            if (idTokenSignedResponseAlg == null)
            {
                throw new ArgumentNullException("idTokenSignedResponseAlg is a required property for HydraOidcConfiguration and cannot be null");
            }
            this.IdTokenSignedResponseAlg = idTokenSignedResponseAlg;
            // to ensure "idTokenSigningAlgValuesSupported" is required (not null)
            if (idTokenSigningAlgValuesSupported == null)
            {
                throw new ArgumentNullException("idTokenSigningAlgValuesSupported is a required property for HydraOidcConfiguration and cannot be null");
            }
            this.IdTokenSigningAlgValuesSupported = idTokenSigningAlgValuesSupported;
            // to ensure "issuer" is required (not null)
            if (issuer == null)
            {
                throw new ArgumentNullException("issuer is a required property for HydraOidcConfiguration and cannot be null");
            }
            this.Issuer = issuer;
            // to ensure "jwksUri" is required (not null)
            if (jwksUri == null)
            {
                throw new ArgumentNullException("jwksUri is a required property for HydraOidcConfiguration and cannot be null");
            }
            this.JwksUri = jwksUri;
            // to ensure "responseTypesSupported" is required (not null)
            if (responseTypesSupported == null)
            {
                throw new ArgumentNullException("responseTypesSupported is a required property for HydraOidcConfiguration and cannot be null");
            }
            this.ResponseTypesSupported = responseTypesSupported;
            // to ensure "subjectTypesSupported" is required (not null)
            if (subjectTypesSupported == null)
            {
                throw new ArgumentNullException("subjectTypesSupported is a required property for HydraOidcConfiguration and cannot be null");
            }
            this.SubjectTypesSupported = subjectTypesSupported;
            // to ensure "tokenEndpoint" is required (not null)
            if (tokenEndpoint == null)
            {
                throw new ArgumentNullException("tokenEndpoint is a required property for HydraOidcConfiguration and cannot be null");
            }
            this.TokenEndpoint = tokenEndpoint;
            // to ensure "userinfoSignedResponseAlg" is required (not null)
            if (userinfoSignedResponseAlg == null)
            {
                throw new ArgumentNullException("userinfoSignedResponseAlg is a required property for HydraOidcConfiguration and cannot be null");
            }
            this.UserinfoSignedResponseAlg = userinfoSignedResponseAlg;
            this.BackchannelLogoutSessionSupported = backchannelLogoutSessionSupported;
            this.BackchannelLogoutSupported = backchannelLogoutSupported;
            this.ClaimsParameterSupported = claimsParameterSupported;
            this.ClaimsSupported = claimsSupported;
            this.CodeChallengeMethodsSupported = codeChallengeMethodsSupported;
            this.CredentialsEndpointDraft00 = credentialsEndpointDraft00;
            this.CredentialsSupportedDraft00 = credentialsSupportedDraft00;
            this.EndSessionEndpoint = endSessionEndpoint;
            this.FrontchannelLogoutSessionSupported = frontchannelLogoutSessionSupported;
            this.FrontchannelLogoutSupported = frontchannelLogoutSupported;
            this.GrantTypesSupported = grantTypesSupported;
            this.RegistrationEndpoint = registrationEndpoint;
            this.RequestObjectSigningAlgValuesSupported = requestObjectSigningAlgValuesSupported;
            this.RequestParameterSupported = requestParameterSupported;
            this.RequestUriParameterSupported = requestUriParameterSupported;
            this.RequireRequestUriRegistration = requireRequestUriRegistration;
            this.ResponseModesSupported = responseModesSupported;
            this.RevocationEndpoint = revocationEndpoint;
            this.ScopesSupported = scopesSupported;
            this.TokenEndpointAuthMethodsSupported = tokenEndpointAuthMethodsSupported;
            this.UserinfoEndpoint = userinfoEndpoint;
            this.UserinfoSigningAlgValuesSupported = userinfoSigningAlgValuesSupported;
        }

        /// <summary>
        /// OAuth 2.0 Authorization Endpoint URL
        /// </summary>
        /// <value>OAuth 2.0 Authorization Endpoint URL</value>
        /// <example>https://playground.ory.sh/ory-hydra/public/oauth2/auth</example>
        [DataMember(Name = "authorization_endpoint", IsRequired = true, EmitDefaultValue = true)]
        public string AuthorizationEndpoint { get; set; }

        /// <summary>
        /// OpenID Connect Back-Channel Logout Session Required  Boolean value specifying whether the OP can pass a sid (session ID) Claim in the Logout Token to identify the RP session with the OP. If supported, the sid Claim is also included in ID Tokens issued by the OP
        /// </summary>
        /// <value>OpenID Connect Back-Channel Logout Session Required  Boolean value specifying whether the OP can pass a sid (session ID) Claim in the Logout Token to identify the RP session with the OP. If supported, the sid Claim is also included in ID Tokens issued by the OP</value>
        [DataMember(Name = "backchannel_logout_session_supported", EmitDefaultValue = true)]
        public bool BackchannelLogoutSessionSupported { get; set; }

        /// <summary>
        /// OpenID Connect Back-Channel Logout Supported  Boolean value specifying whether the OP supports back-channel logout, with true indicating support.
        /// </summary>
        /// <value>OpenID Connect Back-Channel Logout Supported  Boolean value specifying whether the OP supports back-channel logout, with true indicating support.</value>
        [DataMember(Name = "backchannel_logout_supported", EmitDefaultValue = true)]
        public bool BackchannelLogoutSupported { get; set; }

        /// <summary>
        /// OpenID Connect Claims Parameter Parameter Supported  Boolean value specifying whether the OP supports use of the claims parameter, with true indicating support.
        /// </summary>
        /// <value>OpenID Connect Claims Parameter Parameter Supported  Boolean value specifying whether the OP supports use of the claims parameter, with true indicating support.</value>
        [DataMember(Name = "claims_parameter_supported", EmitDefaultValue = true)]
        public bool ClaimsParameterSupported { get; set; }

        /// <summary>
        /// OpenID Connect Supported Claims  JSON array containing a list of the Claim Names of the Claims that the OpenID Provider MAY be able to supply values for. Note that for privacy or other reasons, this might not be an exhaustive list.
        /// </summary>
        /// <value>OpenID Connect Supported Claims  JSON array containing a list of the Claim Names of the Claims that the OpenID Provider MAY be able to supply values for. Note that for privacy or other reasons, this might not be an exhaustive list.</value>
        [DataMember(Name = "claims_supported", EmitDefaultValue = false)]
        public List<string> ClaimsSupported { get; set; }

        /// <summary>
        /// OAuth 2.0 PKCE Supported Code Challenge Methods  JSON array containing a list of Proof Key for Code Exchange (PKCE) [RFC7636] code challenge methods supported by this authorization server.
        /// </summary>
        /// <value>OAuth 2.0 PKCE Supported Code Challenge Methods  JSON array containing a list of Proof Key for Code Exchange (PKCE) [RFC7636] code challenge methods supported by this authorization server.</value>
        [DataMember(Name = "code_challenge_methods_supported", EmitDefaultValue = false)]
        public List<string> CodeChallengeMethodsSupported { get; set; }

        /// <summary>
        /// OpenID Connect Verifiable Credentials Endpoint  Contains the URL of the Verifiable Credentials Endpoint.
        /// </summary>
        /// <value>OpenID Connect Verifiable Credentials Endpoint  Contains the URL of the Verifiable Credentials Endpoint.</value>
        [DataMember(Name = "credentials_endpoint_draft_00", EmitDefaultValue = false)]
        public string CredentialsEndpointDraft00 { get; set; }

        /// <summary>
        /// OpenID Connect Verifiable Credentials Supported  JSON array containing a list of the Verifiable Credentials supported by this authorization server.
        /// </summary>
        /// <value>OpenID Connect Verifiable Credentials Supported  JSON array containing a list of the Verifiable Credentials supported by this authorization server.</value>
        [DataMember(Name = "credentials_supported_draft_00", EmitDefaultValue = false)]
        public List<HydraCredentialSupportedDraft00> CredentialsSupportedDraft00 { get; set; }

        /// <summary>
        /// OpenID Connect End-Session Endpoint  URL at the OP to which an RP can perform a redirect to request that the End-User be logged out at the OP.
        /// </summary>
        /// <value>OpenID Connect End-Session Endpoint  URL at the OP to which an RP can perform a redirect to request that the End-User be logged out at the OP.</value>
        [DataMember(Name = "end_session_endpoint", EmitDefaultValue = false)]
        public string EndSessionEndpoint { get; set; }

        /// <summary>
        /// OpenID Connect Front-Channel Logout Session Required  Boolean value specifying whether the OP can pass iss (issuer) and sid (session ID) query parameters to identify the RP session with the OP when the frontchannel_logout_uri is used. If supported, the sid Claim is also included in ID Tokens issued by the OP.
        /// </summary>
        /// <value>OpenID Connect Front-Channel Logout Session Required  Boolean value specifying whether the OP can pass iss (issuer) and sid (session ID) query parameters to identify the RP session with the OP when the frontchannel_logout_uri is used. If supported, the sid Claim is also included in ID Tokens issued by the OP.</value>
        [DataMember(Name = "frontchannel_logout_session_supported", EmitDefaultValue = true)]
        public bool FrontchannelLogoutSessionSupported { get; set; }

        /// <summary>
        /// OpenID Connect Front-Channel Logout Supported  Boolean value specifying whether the OP supports HTTP-based logout, with true indicating support.
        /// </summary>
        /// <value>OpenID Connect Front-Channel Logout Supported  Boolean value specifying whether the OP supports HTTP-based logout, with true indicating support.</value>
        [DataMember(Name = "frontchannel_logout_supported", EmitDefaultValue = true)]
        public bool FrontchannelLogoutSupported { get; set; }

        /// <summary>
        /// OAuth 2.0 Supported Grant Types  JSON array containing a list of the OAuth 2.0 Grant Type values that this OP supports.
        /// </summary>
        /// <value>OAuth 2.0 Supported Grant Types  JSON array containing a list of the OAuth 2.0 Grant Type values that this OP supports.</value>
        [DataMember(Name = "grant_types_supported", EmitDefaultValue = false)]
        public List<string> GrantTypesSupported { get; set; }

        /// <summary>
        /// OpenID Connect Default ID Token Signing Algorithms  Algorithm used to sign OpenID Connect ID Tokens.
        /// </summary>
        /// <value>OpenID Connect Default ID Token Signing Algorithms  Algorithm used to sign OpenID Connect ID Tokens.</value>
        [DataMember(Name = "id_token_signed_response_alg", IsRequired = true, EmitDefaultValue = true)]
        public List<string> IdTokenSignedResponseAlg { get; set; }

        /// <summary>
        /// OpenID Connect Supported ID Token Signing Algorithms  JSON array containing a list of the JWS signing algorithms (alg values) supported by the OP for the ID Token to encode the Claims in a JWT.
        /// </summary>
        /// <value>OpenID Connect Supported ID Token Signing Algorithms  JSON array containing a list of the JWS signing algorithms (alg values) supported by the OP for the ID Token to encode the Claims in a JWT.</value>
        [DataMember(Name = "id_token_signing_alg_values_supported", IsRequired = true, EmitDefaultValue = true)]
        public List<string> IdTokenSigningAlgValuesSupported { get; set; }

        /// <summary>
        /// OpenID Connect Issuer URL  An URL using the https scheme with no query or fragment component that the OP asserts as its IssuerURL Identifier. If IssuerURL discovery is supported , this value MUST be identical to the issuer value returned by WebFinger. This also MUST be identical to the iss Claim value in ID Tokens issued from this IssuerURL.
        /// </summary>
        /// <value>OpenID Connect Issuer URL  An URL using the https scheme with no query or fragment component that the OP asserts as its IssuerURL Identifier. If IssuerURL discovery is supported , this value MUST be identical to the issuer value returned by WebFinger. This also MUST be identical to the iss Claim value in ID Tokens issued from this IssuerURL.</value>
        /// <example>https://playground.ory.sh/ory-hydra/public/</example>
        [DataMember(Name = "issuer", IsRequired = true, EmitDefaultValue = true)]
        public string Issuer { get; set; }

        /// <summary>
        /// OpenID Connect Well-Known JSON Web Keys URL  URL of the OP&#39;s JSON Web Key Set [JWK] document. This contains the signing key(s) the RP uses to validate signatures from the OP. The JWK Set MAY also contain the Server&#39;s encryption key(s), which are used by RPs to encrypt requests to the Server. When both signing and encryption keys are made available, a use (Key Use) parameter value is REQUIRED for all keys in the referenced JWK Set to indicate each key&#39;s intended usage. Although some algorithms allow the same key to be used for both signatures and encryption, doing so is NOT RECOMMENDED, as it is less secure. The JWK x5c parameter MAY be used to provide X.509 representations of keys provided. When used, the bare key values MUST still be present and MUST match those in the certificate.
        /// </summary>
        /// <value>OpenID Connect Well-Known JSON Web Keys URL  URL of the OP&#39;s JSON Web Key Set [JWK] document. This contains the signing key(s) the RP uses to validate signatures from the OP. The JWK Set MAY also contain the Server&#39;s encryption key(s), which are used by RPs to encrypt requests to the Server. When both signing and encryption keys are made available, a use (Key Use) parameter value is REQUIRED for all keys in the referenced JWK Set to indicate each key&#39;s intended usage. Although some algorithms allow the same key to be used for both signatures and encryption, doing so is NOT RECOMMENDED, as it is less secure. The JWK x5c parameter MAY be used to provide X.509 representations of keys provided. When used, the bare key values MUST still be present and MUST match those in the certificate.</value>
        /// <example>https://{slug}.projects.oryapis.com/.well-known/jwks.json</example>
        [DataMember(Name = "jwks_uri", IsRequired = true, EmitDefaultValue = true)]
        public string JwksUri { get; set; }

        /// <summary>
        /// OpenID Connect Dynamic Client Registration Endpoint URL
        /// </summary>
        /// <value>OpenID Connect Dynamic Client Registration Endpoint URL</value>
        /// <example>https://playground.ory.sh/ory-hydra/admin/client</example>
        [DataMember(Name = "registration_endpoint", EmitDefaultValue = false)]
        public string RegistrationEndpoint { get; set; }

        /// <summary>
        /// OpenID Connect Supported Request Object Signing Algorithms  JSON array containing a list of the JWS signing algorithms (alg values) supported by the OP for Request Objects, which are described in Section 6.1 of OpenID Connect Core 1.0 [OpenID.Core]. These algorithms are used both when the Request Object is passed by value (using the request parameter) and when it is passed by reference (using the request_uri parameter).
        /// </summary>
        /// <value>OpenID Connect Supported Request Object Signing Algorithms  JSON array containing a list of the JWS signing algorithms (alg values) supported by the OP for Request Objects, which are described in Section 6.1 of OpenID Connect Core 1.0 [OpenID.Core]. These algorithms are used both when the Request Object is passed by value (using the request parameter) and when it is passed by reference (using the request_uri parameter).</value>
        [DataMember(Name = "request_object_signing_alg_values_supported", EmitDefaultValue = false)]
        public List<string> RequestObjectSigningAlgValuesSupported { get; set; }

        /// <summary>
        /// OpenID Connect Request Parameter Supported  Boolean value specifying whether the OP supports use of the request parameter, with true indicating support.
        /// </summary>
        /// <value>OpenID Connect Request Parameter Supported  Boolean value specifying whether the OP supports use of the request parameter, with true indicating support.</value>
        [DataMember(Name = "request_parameter_supported", EmitDefaultValue = true)]
        public bool RequestParameterSupported { get; set; }

        /// <summary>
        /// OpenID Connect Request URI Parameter Supported  Boolean value specifying whether the OP supports use of the request_uri parameter, with true indicating support.
        /// </summary>
        /// <value>OpenID Connect Request URI Parameter Supported  Boolean value specifying whether the OP supports use of the request_uri parameter, with true indicating support.</value>
        [DataMember(Name = "request_uri_parameter_supported", EmitDefaultValue = true)]
        public bool RequestUriParameterSupported { get; set; }

        /// <summary>
        /// OpenID Connect Requires Request URI Registration  Boolean value specifying whether the OP requires any request_uri values used to be pre-registered using the request_uris registration parameter.
        /// </summary>
        /// <value>OpenID Connect Requires Request URI Registration  Boolean value specifying whether the OP requires any request_uri values used to be pre-registered using the request_uris registration parameter.</value>
        [DataMember(Name = "require_request_uri_registration", EmitDefaultValue = true)]
        public bool RequireRequestUriRegistration { get; set; }

        /// <summary>
        /// OAuth 2.0 Supported Response Modes  JSON array containing a list of the OAuth 2.0 response_mode values that this OP supports.
        /// </summary>
        /// <value>OAuth 2.0 Supported Response Modes  JSON array containing a list of the OAuth 2.0 response_mode values that this OP supports.</value>
        [DataMember(Name = "response_modes_supported", EmitDefaultValue = false)]
        public List<string> ResponseModesSupported { get; set; }

        /// <summary>
        /// OAuth 2.0 Supported Response Types  JSON array containing a list of the OAuth 2.0 response_type values that this OP supports. Dynamic OpenID Providers MUST support the code, id_token, and the token id_token Response Type values.
        /// </summary>
        /// <value>OAuth 2.0 Supported Response Types  JSON array containing a list of the OAuth 2.0 response_type values that this OP supports. Dynamic OpenID Providers MUST support the code, id_token, and the token id_token Response Type values.</value>
        [DataMember(Name = "response_types_supported", IsRequired = true, EmitDefaultValue = true)]
        public List<string> ResponseTypesSupported { get; set; }

        /// <summary>
        /// OAuth 2.0 Token Revocation URL  URL of the authorization server&#39;s OAuth 2.0 revocation endpoint.
        /// </summary>
        /// <value>OAuth 2.0 Token Revocation URL  URL of the authorization server&#39;s OAuth 2.0 revocation endpoint.</value>
        [DataMember(Name = "revocation_endpoint", EmitDefaultValue = false)]
        public string RevocationEndpoint { get; set; }

        /// <summary>
        /// OAuth 2.0 Supported Scope Values  JSON array containing a list of the OAuth 2.0 [RFC6749] scope values that this server supports. The server MUST support the openid scope value. Servers MAY choose not to advertise some supported scope values even when this parameter is used
        /// </summary>
        /// <value>OAuth 2.0 Supported Scope Values  JSON array containing a list of the OAuth 2.0 [RFC6749] scope values that this server supports. The server MUST support the openid scope value. Servers MAY choose not to advertise some supported scope values even when this parameter is used</value>
        [DataMember(Name = "scopes_supported", EmitDefaultValue = false)]
        public List<string> ScopesSupported { get; set; }

        /// <summary>
        /// OpenID Connect Supported Subject Types  JSON array containing a list of the Subject Identifier types that this OP supports. Valid types include pairwise and public.
        /// </summary>
        /// <value>OpenID Connect Supported Subject Types  JSON array containing a list of the Subject Identifier types that this OP supports. Valid types include pairwise and public.</value>
        [DataMember(Name = "subject_types_supported", IsRequired = true, EmitDefaultValue = true)]
        public List<string> SubjectTypesSupported { get; set; }

        /// <summary>
        /// OAuth 2.0 Token Endpoint URL
        /// </summary>
        /// <value>OAuth 2.0 Token Endpoint URL</value>
        /// <example>https://playground.ory.sh/ory-hydra/public/oauth2/token</example>
        [DataMember(Name = "token_endpoint", IsRequired = true, EmitDefaultValue = true)]
        public string TokenEndpoint { get; set; }

        /// <summary>
        /// OAuth 2.0 Supported Client Authentication Methods  JSON array containing a list of Client Authentication methods supported by this Token Endpoint. The options are client_secret_post, client_secret_basic, client_secret_jwt, and private_key_jwt, as described in Section 9 of OpenID Connect Core 1.0
        /// </summary>
        /// <value>OAuth 2.0 Supported Client Authentication Methods  JSON array containing a list of Client Authentication methods supported by this Token Endpoint. The options are client_secret_post, client_secret_basic, client_secret_jwt, and private_key_jwt, as described in Section 9 of OpenID Connect Core 1.0</value>
        [DataMember(Name = "token_endpoint_auth_methods_supported", EmitDefaultValue = false)]
        public List<string> TokenEndpointAuthMethodsSupported { get; set; }

        /// <summary>
        /// OpenID Connect Userinfo URL  URL of the OP&#39;s UserInfo Endpoint.
        /// </summary>
        /// <value>OpenID Connect Userinfo URL  URL of the OP&#39;s UserInfo Endpoint.</value>
        [DataMember(Name = "userinfo_endpoint", EmitDefaultValue = false)]
        public string UserinfoEndpoint { get; set; }

        /// <summary>
        /// OpenID Connect User Userinfo Signing Algorithm  Algorithm used to sign OpenID Connect Userinfo Responses.
        /// </summary>
        /// <value>OpenID Connect User Userinfo Signing Algorithm  Algorithm used to sign OpenID Connect Userinfo Responses.</value>
        [DataMember(Name = "userinfo_signed_response_alg", IsRequired = true, EmitDefaultValue = true)]
        public List<string> UserinfoSignedResponseAlg { get; set; }

        /// <summary>
        /// OpenID Connect Supported Userinfo Signing Algorithm  JSON array containing a list of the JWS [JWS] signing algorithms (alg values) [JWA] supported by the UserInfo Endpoint to encode the Claims in a JWT [JWT].
        /// </summary>
        /// <value>OpenID Connect Supported Userinfo Signing Algorithm  JSON array containing a list of the JWS [JWS] signing algorithms (alg values) [JWA] supported by the UserInfo Endpoint to encode the Claims in a JWT [JWT].</value>
        [DataMember(Name = "userinfo_signing_alg_values_supported", EmitDefaultValue = false)]
        public List<string> UserinfoSigningAlgValuesSupported { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class HydraOidcConfiguration {\n");
            sb.Append("  AuthorizationEndpoint: ").Append(AuthorizationEndpoint).Append("\n");
            sb.Append("  BackchannelLogoutSessionSupported: ").Append(BackchannelLogoutSessionSupported).Append("\n");
            sb.Append("  BackchannelLogoutSupported: ").Append(BackchannelLogoutSupported).Append("\n");
            sb.Append("  ClaimsParameterSupported: ").Append(ClaimsParameterSupported).Append("\n");
            sb.Append("  ClaimsSupported: ").Append(ClaimsSupported).Append("\n");
            sb.Append("  CodeChallengeMethodsSupported: ").Append(CodeChallengeMethodsSupported).Append("\n");
            sb.Append("  CredentialsEndpointDraft00: ").Append(CredentialsEndpointDraft00).Append("\n");
            sb.Append("  CredentialsSupportedDraft00: ").Append(CredentialsSupportedDraft00).Append("\n");
            sb.Append("  EndSessionEndpoint: ").Append(EndSessionEndpoint).Append("\n");
            sb.Append("  FrontchannelLogoutSessionSupported: ").Append(FrontchannelLogoutSessionSupported).Append("\n");
            sb.Append("  FrontchannelLogoutSupported: ").Append(FrontchannelLogoutSupported).Append("\n");
            sb.Append("  GrantTypesSupported: ").Append(GrantTypesSupported).Append("\n");
            sb.Append("  IdTokenSignedResponseAlg: ").Append(IdTokenSignedResponseAlg).Append("\n");
            sb.Append("  IdTokenSigningAlgValuesSupported: ").Append(IdTokenSigningAlgValuesSupported).Append("\n");
            sb.Append("  Issuer: ").Append(Issuer).Append("\n");
            sb.Append("  JwksUri: ").Append(JwksUri).Append("\n");
            sb.Append("  RegistrationEndpoint: ").Append(RegistrationEndpoint).Append("\n");
            sb.Append("  RequestObjectSigningAlgValuesSupported: ").Append(RequestObjectSigningAlgValuesSupported).Append("\n");
            sb.Append("  RequestParameterSupported: ").Append(RequestParameterSupported).Append("\n");
            sb.Append("  RequestUriParameterSupported: ").Append(RequestUriParameterSupported).Append("\n");
            sb.Append("  RequireRequestUriRegistration: ").Append(RequireRequestUriRegistration).Append("\n");
            sb.Append("  ResponseModesSupported: ").Append(ResponseModesSupported).Append("\n");
            sb.Append("  ResponseTypesSupported: ").Append(ResponseTypesSupported).Append("\n");
            sb.Append("  RevocationEndpoint: ").Append(RevocationEndpoint).Append("\n");
            sb.Append("  ScopesSupported: ").Append(ScopesSupported).Append("\n");
            sb.Append("  SubjectTypesSupported: ").Append(SubjectTypesSupported).Append("\n");
            sb.Append("  TokenEndpoint: ").Append(TokenEndpoint).Append("\n");
            sb.Append("  TokenEndpointAuthMethodsSupported: ").Append(TokenEndpointAuthMethodsSupported).Append("\n");
            sb.Append("  UserinfoEndpoint: ").Append(UserinfoEndpoint).Append("\n");
            sb.Append("  UserinfoSignedResponseAlg: ").Append(UserinfoSignedResponseAlg).Append("\n");
            sb.Append("  UserinfoSigningAlgValuesSupported: ").Append(UserinfoSigningAlgValuesSupported).Append("\n");
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
