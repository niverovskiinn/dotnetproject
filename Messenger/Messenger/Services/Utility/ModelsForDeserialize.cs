namespace Messenger.Services.Utility
{
    
        public class SignUpForm
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
    
        public class CreateDialogueForm
        {
            public string Token { get; set; }
            public int Id { get; set; }
            public int IdUser { get; set; }
        }
        
        
        public class RemoveDialogueForm
        {
            public string Token { get; set; }
            public int Id { get; set; }
            public int IdDial { get; set; }
        }
        
        public class SendMessageForm
        {
            public string Token { get; set; }
            public int Id { get; set; }
            public int IdDial { get; set; }
            public string Data { get; set; }

        }
        
        public class RemoveMessageForm
        {
            public string Token { get; set; }
            public int Id { get; set; }
            public int IdMess { get; set; }

        }

        public class EditMessageForm
        {
            public string Token { get; set; }
            public int Id { get; set; }
            public int IdMess { get; set; }
            public string Data { get; set; }

        }

}