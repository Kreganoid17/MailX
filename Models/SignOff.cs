using System.ComponentModel.DataAnnotations;

namespace Mail_X.Models
{
    public class SignOff
    {
        public int FormID { get; set; }

        public string SignName { get; set; }

        public DateTime SignDate { get; set; }

        public string Comments  { get; set; }

        public string DeptName { get; set; }

        public string EmpID { get; set; }


    }
}
