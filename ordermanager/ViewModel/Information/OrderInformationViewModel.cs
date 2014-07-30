using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Information
{
    public class OrderInformationViewModel : ViewModelBase
    {
        public OrderInformationViewModel(Order order)
        {
            SetOrder(order);
        }

        

        private ObservableCollection<MaterialsFromStock> m_MaterialsFromStock = null;
        public ObservableCollection<MaterialsFromStock> MaterialsFromStock
        {
            get
            {
                if (m_MaterialsFromStock == null)
                {
                    m_MaterialsFromStock = new ObservableCollection<MaterialsFromStock>(Order.MaterialsFromStocks);
                }

                return m_MaterialsFromStock;
            }
        }

        private Dictionary<string, MaterialsFromStockSummary> materialSummaryDict = null;
        public ObservableCollection<MaterialsFromStockSummary> MaterialsSummary
        {
            get
            {
                //if (materialSummaryDict == null)
                //{
                //    materialSummaryDict = new Dictionary<string, MaterialsFromStockSummary>();
                //    foreach (MaterialsFromStock mfs in MaterialsFromStock)
                //    {
                //        MaterialsFromStockSummary summary = null;
                //        if (materialSummaryDict.ContainsKey(mfs.SubMaterial.Name))
                //        {
                //            summary = materialSummaryDict[mfs.SubMaterial.Name];
                //        }
                //        else
                //        {
                //            summary = new MaterialsFromStockSummary(mfs.SubMaterial);
                //            materialSummaryDict.Add(mfs.SubMaterial.Name, summary);
                //        }

                //        summary.Quantity += mfs.Quantity;
                //    }
                //}

                return new ObservableCollection<MaterialsFromStockSummary>(materialSummaryDict.Values);
            }
        }


    }
}
