using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CecilioNolenAuditReview
{
    class Emp
    {
        private int empID;
        private string empUName;
        private string empFName;
        private string empLName;
        private string empRole;

        public int EmpID { get; set; }
        public string EmpUName { get; set; }
        public string EmpFName { get; set; }
        public string EmpLName { get; set; }
        public string EmpRole { get; set; }

        public Emp(int id, string uname, string fname, string lname, string role)
        {
            EmpID = id;
            EmpUName = uname;
            EmpFName = fname;
            EmpLName = lname;
            EmpRole = role;
        }

        public override string ToString()
        {
            return EmpUName + "-" + EmpRole;

        }
    }
}
