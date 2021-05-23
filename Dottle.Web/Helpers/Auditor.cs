namespace Dottle.Helpers
{
    public class Auditor
    {
        public delegate void SignInHandler(object myObject, 
            AuditArgs myArgs);

        public event SignInHandler OnAudit;

        private string message;

        public string Message
        {
            set
            {
                message = value;

                if (message.Length != 0)
                {
                    AuditArgs myArgs = new AuditArgs(message + " has signed in.");

                    OnAudit(this, myArgs);
                }
            }
        }

    }
}
