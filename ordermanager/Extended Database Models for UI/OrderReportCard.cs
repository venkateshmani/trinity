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
                    return Brushes.WhiteSmoke;

                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 191, 46, 46));// Brushes.DeepPink;
            }
        }
       
        public SolidColorBrush ReportForeColor
        {
            get
            {
                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 17,158, 218));
            }
        }
    }
}
