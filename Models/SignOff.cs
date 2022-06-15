using System.ComponentModel.DataAnnotations;

namespace Mail_X.Models
{
    public class SignOff
    {
        [Required(ErrorMessage = "Please Enter Signatures Name")]
        public string SignName { get; set; }

        [Required(ErrorMessage = "Please Enter Signature")]
        public string Signature { get; set; }

        public DateTime SignDate { get; set; }

        public string Comments  { get; set; }

        [Required(ErrorMessage = "Please Enter Department Name")]
        public string DeptName { get; set; }


    }
}
