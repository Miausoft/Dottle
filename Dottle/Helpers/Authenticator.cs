using System;
using Microsoft.AspNetCore.Http;

namespace Dottle.Helpers
{
    public class Authenticator
    {
        private int threshold;
        public int total { get; set; }

        public Authenticator(int threshold)
        {
            this.threshold = threshold;
        }
        
        public bool Authenticate(string pass, string hash, string salt)
        {
            bool authorized = PasswordManager.VerifyHashedPassword(pass, hash, salt);
            if (!authorized)
            {
                total += 1;
                if (total >= this.threshold)
                {
                    OnThresholdReached(EventArgs.Empty);
                }
            }
            else
            {
                this.total = 0;
            }
            
            return authorized;
        }
        
        protected virtual void OnThresholdReached(EventArgs e)
        {
            EventHandler handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ThresholdReached;
    }
}
