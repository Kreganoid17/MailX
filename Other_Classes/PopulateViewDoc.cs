using Mail_X.Models;

namespace Mail_X.Other_Classes
{
    public class PopulateViewDoc
    {

        public FormDetails Populate(int ID) {

            DBFunctions DBF = new DBFunctions();

            FormDetails FD = new FormDetails();

            FD = DBF.GetForm(ID);

            return FD;

        }



    }
}
