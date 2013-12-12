using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ordermanager.DatabaseModel
{
    public partial class OrderReportCard
    {
        public SolidColorBrush ReportBackColor
        {
            get
            {
                if (RequiredFinishDate <= DBResources.Instance.GetServerTime())
                    return Brushes.White;
                return Brushes.Red;
            }
        }

        public SolidColorBrush ReportForeColor
        {
            get
            {
                if (ProgressPercentage == 100.0M)
                    return Brushes.Blue;
                ProgressPercentage = 20.0M;
                return Brushes.Green;
            }
        }        
    }
}
