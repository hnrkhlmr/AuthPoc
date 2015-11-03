using AuthPoc.ServiceAccess.API;
using AuthPoc.ServiceAccess.Auth;

namespace AuthPoc.ServiceAccess
{
    public class WebClientsFactory
    {
        private RoleWebClient _roleWebClient;
        public RoleWebClient RoleWebClient { get { return _roleWebClient ?? (_roleWebClient = new RoleWebClient()); } }

        private AccountWebClient _accountWebClient;
        public AccountWebClient AccountWebClient { get { return _accountWebClient ?? (_accountWebClient = new AccountWebClient()); } }

        private ValuesWebClient _valuesWebClient;
        public ValuesWebClient ValuesWebClient { get { return _valuesWebClient ?? (_valuesWebClient = new ValuesWebClient()); } }
       
        private DepartmentsWebClient _departmentsWebClient;
        public DepartmentsWebClient DepartmentsWebClient { get { return _departmentsWebClient ?? (_departmentsWebClient = new DepartmentsWebClient()); } }

        private AuthWebClient _authWebClient;
        public AuthWebClient AuthWebClient { get { return _authWebClient ?? (_authWebClient = new AuthWebClient()); } }

        private UserInfoWebClient _userInfoWebClient;
        public UserInfoWebClient UserInfoWebClient { get { return _userInfoWebClient ?? (_userInfoWebClient = new UserInfoWebClient()); } }

    }
}
