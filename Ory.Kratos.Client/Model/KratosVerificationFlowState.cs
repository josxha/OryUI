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
    /// The state represents the state of the verification flow.  choose_method: ask the user to choose a method (e.g. recover account via email) sent_email: the email has been sent to the user passed_challenge: the request was successful and the recovery challenge was passed.
    /// </summary>
    /// <value>The state represents the state of the verification flow.  choose_method: ask the user to choose a method (e.g. recover account via email) sent_email: the email has been sent to the user passed_challenge: the request was successful and the recovery challenge was passed.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KratosVerificationFlowState
    {
        /// <summary>
        /// Enum ChooseMethod for value: choose_method
        /// </summary>
        [EnumMember(Value = "choose_method")]
        ChooseMethod = 1,

        /// <summary>
        /// Enum SentEmail for value: sent_email
        /// </summary>
        [EnumMember(Value = "sent_email")]
        SentEmail = 2,

        /// <summary>
        /// Enum PassedChallenge for value: passed_challenge
        /// </summary>
        [EnumMember(Value = "passed_challenge")]
        PassedChallenge = 3
    }

}