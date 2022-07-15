namespace Mail_X.Models
{
    public class History
    {

        public string EmpID { get; set; }

        public string EmpName { get; set; }

        public DateTime DateCreated = DateTime.Now;

        public string DeptID  { get; set; }

        public string Description { get; set; }

    }
}
