using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Graph;
using Proxa.Models;

namespace Proxa.Helpers
{
    public static class ValidationHelper
    {
        public static ValidationResult ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || !IsValidEmail(user.Email))
            {
                return new ValidationResult("Invalid email address.");
            }

            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                return new ValidationResult("Password cannot be empty.");
            }

            if (user.Credits < 0)
            {
                return new ValidationResult("Credits cannot be negative.");
            }

            if (string.IsNullOrEmpty(user.SubscriptionType))
            {
                return new ValidationResult("Subscription type cannot be empty.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateProxy(Proxy proxy)
        {
            if (string.IsNullOrEmpty(proxy.IPAddress))
            {
                return new ValidationResult("IP address cannot be empty.");
            }

            if (proxy.Port <= 0 || proxy.Port > 65535)
            {
                return new ValidationResult("Invalid port number.");
            }

            if (string.IsNullOrEmpty(proxy.Type))
            {
                return new ValidationResult("Proxy type cannot be empty.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateList(List list)
        {
            if (string.IsNullOrEmpty(list.Name))
            {
                return new ValidationResult("List name cannot be empty.");
            }

            return ValidationResult.Success;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
