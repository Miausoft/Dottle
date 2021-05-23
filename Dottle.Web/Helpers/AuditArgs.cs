using System;

namespace Dottle.Helpers
{
    public class AuditArgs : EventArgs
    {
        private string message;

        public AuditArgs(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get
            {
                return message;
            }
        }
    }
}
