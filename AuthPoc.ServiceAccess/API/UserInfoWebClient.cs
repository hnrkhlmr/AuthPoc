using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
//using System.Web.Configuration;
using Newtonsoft.Json;
using AuthPoc.DTO;
//using AuthPoc.DTO.UserInfo;
using AuthPoc.ServiceAccess.@base;

namespace AuthPoc.ServiceAccess.API
{
    public class UserInfoWebClient : WebClientWrapperBase
    {
        public UserInfoWebClient() : base(WebClientEndpoints.UserInfoBaseUrl) { }
        public UserInfoWebClient(string token) : base(WebClientEndpoints.UserInfoBaseUrl, token) { }

        //public UserInfoDTO FindById(int userId)
        //{
        //    return Get<UserInfoDTO>(userId.ToString());
        //}

        //public UserInfoDTO Add(UserInfoDTO request)
        //{
        //    return Post<UserInfoDTO>(string.Empty, JsonConvert.SerializeObject(request));
        //}

        //public UserInfoDTO UpdateUser(UserInfoDTO dto)
        //{
        //    return Put<UserInfoDTO>(dto.UserInfoId, JsonConvert.SerializeObject(dto));
        //}
    }
}