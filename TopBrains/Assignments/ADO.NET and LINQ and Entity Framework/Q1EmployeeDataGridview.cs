using System;
using System.Linq;
using System.Xml.Linq;

public partial class EmployeeData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string xmlString = @"
            <Employees>
                <Employee>
                    <Name>Thomas</Name>
                    <Designation>Executive</Designation>
                    <Department>Accounts</Department>
                    <Salary>5000</Salary>
                </Employee>
                <Employee>
                    <Name>Wills</Name>
                    <Designation>Manager</Designation>
                    <Department>Accounts</Department>
                    <Salary>24000</Salary>
                </Employee>
                <Employee>
                    <Name>Brod</Name>
                    <Designation>Manager</Designation>
                    <Department>Finance</Department>
                    <Salary>28000</Salary>
                </Employee>
                <Employee>
                    <Name>Smith</Name>
                    <Designation>Analyst</Designation>
                    <Department>Finance</Department>
                    <Salary>21000</Salary>
                </Employee>
            </Employees>";

            XDocument xdoc = XDocument.Parse(xmlString);

            var res = from emp in xdoc.Root.Elements("Employee")
                      where emp.Element("Department").Value == "Finance" &&
                            Convert.ToInt32(emp.Element("Salary").Value) > 25000
                      select new
                      {
                          EmployeeName = emp.Element("Name").Value,
                          Department = emp.Element("Department").Value,
                          Salary = emp.Element("Salary").Value
                      };

            GridView1.DataSource = res.ToList();
            GridView1.DataBind();
        }
    }
}