using System;
using System.Collections.Generic;
using AuthPoc.DTO;
//using AuthPoc.DTO.UserInfo;
using AuthPoc.ServiceAccess.@base;

namespace AuthPoc.ServiceAccess.API
{
    public class DepartmentsWebClient : WebClientWrapperBase
    {
        public DepartmentsWebClient() : base(WebClientEndpoints.DepartmentsApiBaseUrl) { }
        public DepartmentsWebClient(string token) : base(WebClientEndpoints.DepartmentsApiBaseUrl, token) { }

        //public IList<DepartmentDTO> GetDepartments()
        //{
        //    return Get<List<DepartmentDTO>>();
        //}

        //public DepartmentDTO FindByName(string name)
        //{
        //    return Get<DepartmentDTO>(string.Format("?name={0}", name));
        //}
    }
}