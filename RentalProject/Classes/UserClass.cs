using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalProject.Classes
{
    public class UserClass
    {


        private int UserId;
        public int _UserId
        {
            get { return UserId; }
            set { UserId = value; }
        }

        private string Name;
        public string _Name
        {
            get { return Name; }
            set { Name = value; }
        }

        private string Photo;
        public string _Photo
        {
            get { return Photo; }
            set { Photo = value; }
        }

      

        private string Email;
        public string _Email
        {
            get { return Email; }
            set { Email = value; }
        }

        private string UserName;
        public string _UserName
        {
            get { return UserName; }
            set { UserName = value; }
        }

        private string Password;
        public string _Password
        {
            get { return Password; }
            set { Password = value; }
        }

        private int OrganizationId;
        public int _OrganizationId
        {
            get { return OrganizationId; }
            set { OrganizationId = value; }
        }

        private bool RememberMe;
        public bool _RememberMe
        {
            get { return RememberMe; }
            set { RememberMe = value; }
        }

        public List<int> _UserMoudules;

        public UserClass(int _UserId, string  _FullName, string _UserName, string _Password
             , int _OrganizationId, string _Email, bool _rememberMe, List<int> UserModules, string _Photo)
        {
            UserId = _UserId;
            Name = _FullName;
            UserName = _UserName;
            Password = _Password;
            OrganizationId = _OrganizationId;
            Email = _Email;
            RememberMe = _rememberMe;
            _UserMoudules = UserModules;
            Photo = _Photo;
        }

        public UserClass()
        {
        }
    }
}