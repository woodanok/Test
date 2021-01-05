using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.ViewModels
{
    public class LoginModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Не указан логин")]
        public String Login { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
