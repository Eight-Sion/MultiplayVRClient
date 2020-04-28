using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts.Service
{
    public class LoginService
    {
        public bool IsLoggingIn { get; private set; } = false;
        private UnityAction _onLogin;
        private UnityAction _onLogout;
        public LoginService(UnityAction onLogin, UnityAction onLogout)
        {
            _onLogin = onLogin;
            _onLogout = onLogout;
        }
        
    }
}
