using System;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;

namespace AuthPoc.ServiceAccess.API
{
    public class ApiResultMessage
    {
        private string _code;

        public ApiResultMessage()
        {
        }

        public ApiResultMessage(ApiResultCode code, string message = null)
        {
            Code = code;
            Message = message;
        }

        [JsonProperty(PropertyName = "Code")]
        public ApiResultCode Code
        {
            get
            {
                ApiResultCode result;
                Enum.TryParse(_code, out result);
                return result;
            }
            set { _code = value.ToString(); }
        }

        public string Message { get; set; }

        [JsonIgnore]
        public string PrettyMessage
        {
            get
            {
                switch (Code)
                {
                    case ApiResultCode.BadRequest:
                        return string.Format("Felaktigt anrop till bakomliggande tjänst [{0}]", Message);
                    case ApiResultCode.UserNotFound:
                        return string.Format("Användare kunde ej hittas [{0}]", Message);
                    case ApiResultCode.ResponseStreamNull:
                        return string.Format("Saknar svar från servern [{0}]", Message);
                    case ApiResultCode.NotAllowedDomain:
                        return "Mottagararens epostaddress tillhör inte en tillåten domän";
                    case ApiResultCode.DatabaseUpdateError:
                        return "Det uppstod problem vid uppdatering av databasen";
                    case ApiResultCode.ExceptionInEmailGateway:
                        return "Det uppstod ett fel vid sändning av e-post";
                    case ApiResultCode.CouldNotFetchDepartments:
                        return "Det gick inte att hämta avdelningar";
                    case ApiResultCode.CouldNotFetchDepartment:
                        return "Det gick inte att hämta avdelning";
                    case ApiResultCode.CouldNotFetchEmployers:
                        return "Det gick inte att hämta arbetsgivare";
                    case ApiResultCode.CouldNotFetchEmployer:
                        return "Det gick inte att hämta arbetsgivare";
                    case ApiResultCode.CouldNotFetchJobTitles:
                        return "Det gick inte att hämta yrkesroller (titlar)";
                    case ApiResultCode.CouldNotFetchJobTitle:
                        return "Det gick inte att hämta yrkesroll (titel)";
                    case ApiResultCode.CouldNotFetchLaserPointers:
                        return "Det gick inte att hämta laserpekare";
                    case ApiResultCode.CouldNotFetchLaserPointer:
                        return "Det gick inte att hämta laserpekare";
                    case ApiResultCode.CouldNotUpdateLaserPointer:
                        return "Det gick inte att uppdatera laserpekare";
                    case ApiResultCode.CouldNotCreateLaserPointer:
                        return "Det gick inte att skapa laserpekare";
                    case ApiResultCode.CouldNotDeleteLaserPointer:
                        return "Det gick inte att ta bort laserpekare";
                    case ApiResultCode.CouldNotCreateYttrande:
                        return "Det gick inte att skapa yttrande";
                    case ApiResultCode.CouldNotFetchYttranden:
                        return "Det gick inte att hämta yttranden";
                    case ApiResultCode.CouldNotResendYttrandedokument:
                        return "Det gick inte att återsända yttrandet";
                    case ApiResultCode.CouldNotFetchYttrandedokument:
                        return "Det gick inte att hämta yttrandet";
                    case ApiResultCode.CouldNotFetchRegions:
                        return "Det gick inte att hämta regioner";
                    case ApiResultCode.CouldNotFetchRegion:
                        return "Det gick inte att hämta region";
                    case ApiResultCode.CouldNotUpdateUserInfo:
                        return "Det gick inte att uppdatera användarinformation";
                    case ApiResultCode.CouldNotCreateUserInfo:
                        return "Det gick inte att skapa användarinformation";
                    default:
                        return string.Format(
                            "Oväntat fel, vänligen kontakta support! Ge följande info [API-felkod: {0}, Meddelande: \"{1}\"]",
                            Code,
                            Message
                        );
                }
            }
        }
    }

    public enum ApiResultCode
    {
        BadRequest = 100000,
        UserNotFound = 100100,
        ResponseStreamNull = 100200,

        ExceptionInEmailGateway = 100300,
        NotAllowedDomain = 100310,

        DatabaseUpdateError = 100400,

        CouldNotFetchDepartments = 100600,
        CouldNotFetchDepartment = 100610,

        CouldNotFetchEmployers = 100700,
        CouldNotFetchEmployer = 100710,

        CouldNotFetchJobTitles = 100800,
        CouldNotFetchJobTitle = 100810,

        CouldNotFetchLaserPointers = 100900,
        CouldNotFetchLaserPointer = 100910,
        CouldNotUpdateLaserPointer = 100920,
        CouldNotCreateLaserPointer = 100930,
        CouldNotDeleteLaserPointer = 100940,

        CouldNotCreateYttrande = 101000,
        CouldNotFetchYttranden = 101010,
        CouldNotFetchYttrande = 101020,

        CouldNotResendYttrandedokument = 101100,
        CouldNotFetchYttrandedokument = 101110,

        CouldNotFetchRegions = 101200,
        CouldNotFetchRegion = 101210,

        CouldNotUpdateUserInfo = 101300,
        CouldNotCreateUserInfo = 101310,
    }

    public enum ApiResultMessages
    {
        [Description("INVALID MODEL STATE")]
        InvalidModelState,
        [Description("ACTS AS WRONG USER")]
        ActsAsWrongUser
    }
}