using ordermanager.Interfaces_And_Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Execution
{
    public class QualityViewModel : JobExecutionViewModelBase
    {
        public bool CanModify
        {
            get { return DBResources.Instance.CurrentUser.UserRole.CanModifyExectionQuality; }
        }

        public override void AddNewRecord(DateTime date)
        {
            base.AddNewRecord(date);
        }
    }
}
