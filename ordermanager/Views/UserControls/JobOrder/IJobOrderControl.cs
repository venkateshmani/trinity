using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Views.UserControls.JobOrderControls
{
    interface IJobOrderControl 
    {
        bool Generate();
        bool Submit();
        bool Approve();
        bool Reject();
        bool ShowPDF();
        bool Discard();
    }
}
