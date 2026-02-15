using Microsoft.AspNetCore.Identity;

namespace PurrfectShot.Web.Infrastructure
{
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric()
            => new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Паролата трябва да съдържа поне един символ (напр. !, @, #)." };

        public override IdentityError PasswordRequiresDigit()
            => new() { Code = nameof(PasswordRequiresDigit), Description = "Паролата трябва да съдържа поне една цифра ('0'-'9')." };

        public override IdentityError PasswordRequiresUpper()
            => new() { Code = nameof(PasswordRequiresUpper), Description = "Паролата трябва да съдържа поне една главна буква ('A'-'Z')." };
    }
}
