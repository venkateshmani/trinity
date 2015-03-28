using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Views.ReportViewers
{
    public interface IReportViewer
    {
        bool GeneratePDF(string fileName);
        ICollection ReportItems { get; set; }
    }
}