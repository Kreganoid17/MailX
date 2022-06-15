using System.ComponentModel.DataAnnotations;

namespace Mail_X.Models
{
    public class MarkAsDoneDetails
    {
        [Required(ErrorMessage = "Please Enter A Valid DevOps ID")]
        public string DevOpsID { get; set; }

        [Required(ErrorMessage = "Please Enter A Valid DevOps Password")]
        public string Password { get; set; }

    }
}
