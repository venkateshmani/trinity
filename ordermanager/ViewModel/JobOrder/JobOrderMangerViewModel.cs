using ordermanager.DatabaseModel;
using ordermanager.Extended_Database_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.JobOrderControls
{
    public class JobOrderManagerViewModel : ViewModelBase
    {
        public JobOrderManagerViewModel(Order order)
        {
            this.Order = order;
        }

        public void ReLoad()
        {
            m_JobOrderInfos = null;
        }

        private ObservableCollection<IJobOrderInfo> m_JobOrderInfos = null;
        public ObservableCollection<IJobOrderInfo> JobOrderInfos
        {
            get
            {
                if (m_JobOrderInfos == null)
                {
                    m_JobOrderInfos = new ObservableCollection<IJobOrderInfo>(Order.DyeingJOes);
                }

                return m_JobOrderInfos;
            }
        }

        private IJobOrderInfo m_SelectedJobOrderInfo = null;
        public IJobOrderInfo SelectedJobOrderInfo
        {
            get
            {
                return m_SelectedJobOrderInfo;
            }
            set
            {
                m_SelectedJobOrderInfo = value;
            }
        }
    }
}
