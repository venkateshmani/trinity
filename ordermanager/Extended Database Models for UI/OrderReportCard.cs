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
                    return Brushes.Silver;
                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(75, 229, 64, 40));// Brushes.DeepPink;
            }
        }
       
        public SolidColorBrush ReportForeColor
        {
            get
            {
                if (ProgressPercentage == 100.0M)
                    return Brushes.DodgerBlue;
                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 97, 174, 36));
            }
        }
    }
}
