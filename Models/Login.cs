using System.ComponentModel.DataAnnotations;

namespace Mail_X.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please Enter a Valid Employee Code")]
        public string EmpID { get; set; }

        [Required(ErrorMessage = "Please Enter a Valid Password")]
        public string Password { get; set; }

    }
}
