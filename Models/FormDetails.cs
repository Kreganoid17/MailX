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

        [Required(ErrorMessage = "Please Enter The Testing Requirements")]
        public string AdditionalNotes { get; set; } 

        [Required(ErrorMessage = "Please Enter The People Involved In The Sign Off")]
        public string SignOff { get; set; }

        public List<SignOff> SignOffs { get; set; }  

        public List<string> PullRequests { get; set; } 

    }
}
