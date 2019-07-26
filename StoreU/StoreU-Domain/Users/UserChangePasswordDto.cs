
using System;
using System.ComponentModel.DataAnnotations;

namespace StoreU_DomainEntities.Users
{
    public class UserChangePasswordDto
    {
        [Required(ErrorMessage = "Usuario no valido")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El token del usuario es invalido")]
        public string UserToken { get; set; }

        [Required(ErrorMessage = "Contraseña no puede estar vacia")]
        public string PasswordRaw { get; set; }
        [Required(ErrorMessage = "Confirmación de contraseña no puede estar vacia")]
        public string PasswordConfirmation { get; set; }
    }
}
