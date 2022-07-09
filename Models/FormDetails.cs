using System.ComponentModel.DataAnnotations;

namespace Mail_X.Models
{
    public class FormDetails
    {
        [Required(ErrorMessage = "Please Enter Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please Enter End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage ="Please Enter The App Name")]
        public string AppName { get; set; }

        [Required(ErrorMessage = "Please Enter The Server Name")]
        public string ServerName { get; set; }

        [Required(ErrorMessage = "Please Enter Additional Notes")]
        public string AdditionalNotes { get; set; } 

        [Required(ErrorMessage = "Please Enter a Project Name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Please Enter a Project ID")]
        public string ProjectID{ get; set; }

        [Required(ErrorMessage = "Please Enter a Pull Request")]
        public string PullRequests { get; set; }

        public List<SignOff> SignOffs { get; set; }

        public string Environment { get; set; }

        [Required(ErrorMessage = "Please Enter a Comment")]
        public string Comments { get; set; }

    }
}
