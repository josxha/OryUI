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
using JsonSubTypes;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Ory.Kratos.Client.Client.OpenAPIDateConverter;
using System.Reflection;

namespace Ory.Kratos.Client.Model
{
    /// <summary>
    /// ContinueWith
    /// </summary>
    [JsonConverter(typeof(ContinueWithJsonConverter))]
    [DataContract(Name = "continueWith")]
    public partial class ContinueWith : AbstractOpenAPISchema, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContinueWith" /> class
        /// with the <see cref="ContinueWithVerificationUi" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of ContinueWithVerificationUi.</param>
        public ContinueWith(ContinueWithVerificationUi actualInstance)
        {
            this.IsNullable = false;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance ?? throw new ArgumentException("Invalid instance found. Must not be null.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinueWith" /> class
        /// with the <see cref="ContinueWithSetOrySessionToken" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of ContinueWithSetOrySessionToken.</param>
        public ContinueWith(ContinueWithSetOrySessionToken actualInstance)
        {
            this.IsNullable = false;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance ?? throw new ArgumentException("Invalid instance found. Must not be null.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinueWith" /> class
        /// with the <see cref="ContinueWithSettingsUi" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of ContinueWithSettingsUi.</param>
        public ContinueWith(ContinueWithSettingsUi actualInstance)
        {
            this.IsNullable = false;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance ?? throw new ArgumentException("Invalid instance found. Must not be null.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinueWith" /> class
        /// with the <see cref="ContinueWithRecoveryUi" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of ContinueWithRecoveryUi.</param>
        public ContinueWith(ContinueWithRecoveryUi actualInstance)
        {
            this.IsNullable = false;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance ?? throw new ArgumentException("Invalid instance found. Must not be null.");
        }


        private Object _actualInstance;

        /// <summary>
        /// Gets or Sets ActualInstance
        /// </summary>
        public override Object ActualInstance
        {
            get
            {
                return _actualInstance;
            }
            set
            {
                if (value.GetType() == typeof(ContinueWithRecoveryUi))
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(ContinueWithSetOrySessionToken))
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(ContinueWithSettingsUi))
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(ContinueWithVerificationUi))
                {
                    this._actualInstance = value;
                }
                else
                {
                    throw new ArgumentException("Invalid instance found. Must be the following types: ContinueWithRecoveryUi, ContinueWithSetOrySessionToken, ContinueWithSettingsUi, ContinueWithVerificationUi");
                }
            }
        }

        /// <summary>
        /// Get the actual instance of `ContinueWithVerificationUi`. If the actual instance is not `ContinueWithVerificationUi`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of ContinueWithVerificationUi</returns>
        public ContinueWithVerificationUi GetContinueWithVerificationUi()
        {
            return (ContinueWithVerificationUi)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `ContinueWithSetOrySessionToken`. If the actual instance is not `ContinueWithSetOrySessionToken`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of ContinueWithSetOrySessionToken</returns>
        public ContinueWithSetOrySessionToken GetContinueWithSetOrySessionToken()
        {
            return (ContinueWithSetOrySessionToken)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `ContinueWithSettingsUi`. If the actual instance is not `ContinueWithSettingsUi`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of ContinueWithSettingsUi</returns>
        public ContinueWithSettingsUi GetContinueWithSettingsUi()
        {
            return (ContinueWithSettingsUi)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `ContinueWithRecoveryUi`. If the actual instance is not `ContinueWithRecoveryUi`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of ContinueWithRecoveryUi</returns>
        public ContinueWithRecoveryUi GetContinueWithRecoveryUi()
        {
            return (ContinueWithRecoveryUi)this.ActualInstance;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ContinueWith {\n");
            sb.Append("  ActualInstance: ").Append(this.ActualInstance).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this.ActualInstance, ContinueWith.SerializerSettings);
        }

        /// <summary>
        /// Converts the JSON string into an instance of ContinueWith
        /// </summary>
        /// <param name="jsonString">JSON string</param>
        /// <returns>An instance of ContinueWith</returns>
        public static ContinueWith FromJson(string jsonString)
        {
            ContinueWith newContinueWith = null;

            if (string.IsNullOrEmpty(jsonString))
            {
                return newContinueWith;
            }
            int match = 0;
            List<string> matchedTypes = new List<string>();

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(ContinueWithRecoveryUi).GetProperty("AdditionalProperties") == null)
                {
                    newContinueWith = new ContinueWith(JsonConvert.DeserializeObject<ContinueWithRecoveryUi>(jsonString, ContinueWith.SerializerSettings));
                }
                else
                {
                    newContinueWith = new ContinueWith(JsonConvert.DeserializeObject<ContinueWithRecoveryUi>(jsonString, ContinueWith.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("ContinueWithRecoveryUi");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into ContinueWithRecoveryUi: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(ContinueWithSetOrySessionToken).GetProperty("AdditionalProperties") == null)
                {
                    newContinueWith = new ContinueWith(JsonConvert.DeserializeObject<ContinueWithSetOrySessionToken>(jsonString, ContinueWith.SerializerSettings));
                }
                else
                {
                    newContinueWith = new ContinueWith(JsonConvert.DeserializeObject<ContinueWithSetOrySessionToken>(jsonString, ContinueWith.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("ContinueWithSetOrySessionToken");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into ContinueWithSetOrySessionToken: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(ContinueWithSettingsUi).GetProperty("AdditionalProperties") == null)
                {
                    newContinueWith = new ContinueWith(JsonConvert.DeserializeObject<ContinueWithSettingsUi>(jsonString, ContinueWith.SerializerSettings));
                }
                else
                {
                    newContinueWith = new ContinueWith(JsonConvert.DeserializeObject<ContinueWithSettingsUi>(jsonString, ContinueWith.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("ContinueWithSettingsUi");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into ContinueWithSettingsUi: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(ContinueWithVerificationUi).GetProperty("AdditionalProperties") == null)
                {
                    newContinueWith = new ContinueWith(JsonConvert.DeserializeObject<ContinueWithVerificationUi>(jsonString, ContinueWith.SerializerSettings));
                }
                else
                {
                    newContinueWith = new ContinueWith(JsonConvert.DeserializeObject<ContinueWithVerificationUi>(jsonString, ContinueWith.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("ContinueWithVerificationUi");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into ContinueWithVerificationUi: {1}", jsonString, exception.ToString()));
            }

            if (match == 0)
            {
                throw new InvalidDataException("The JSON string `" + jsonString + "` cannot be deserialized into any schema defined.");
            }
            else if (match > 1)
            {
                throw new InvalidDataException("The JSON string `" + jsonString + "` incorrectly matches more than one schema (should be exactly one match): " + String.Join(",", matchedTypes));
            }

            // deserialization is considered successful at this point if no exception has been thrown.
            return newContinueWith;
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

    /// <summary>
    /// Custom JSON converter for ContinueWith
    /// </summary>
    public class ContinueWithJsonConverter : JsonConverter
    {
        /// <summary>
        /// To write the JSON string
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Object to be converted into a JSON string</param>
        /// <param name="serializer">JSON Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue((string)(typeof(ContinueWith).GetMethod("ToJson").Invoke(value, null)));
        }

        /// <summary>
        /// To convert a JSON string into an object
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="objectType">Object type</param>
        /// <param name="existingValue">Existing value</param>
        /// <param name="serializer">JSON Serializer</param>
        /// <returns>The object converted from the JSON string</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.TokenType) 
            {
                case JsonToken.StartObject:
                    return ContinueWith.FromJson(JObject.Load(reader).ToString(Formatting.None));
                case JsonToken.StartArray:
                    return ContinueWith.FromJson(JArray.Load(reader).ToString(Formatting.None));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Check if the object can be converted
        /// </summary>
        /// <param name="objectType">Object type</param>
        /// <returns>True if the object can be converted</returns>
        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }

}