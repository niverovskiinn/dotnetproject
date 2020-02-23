namespace Messenger.Services.Utility
{
    
        public class RegForm
        {

            public string Login { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public string Birthday { get; set; }
        }

        public class SignInForm
        {
            public string Login { get; set; }
            public string Password { get; set; }

        }

        public class SignOutForm
        {
           public string Token { get; set; }
           public int Id { get; set; }
        }




}