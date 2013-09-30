using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Extended_Database_Models
{
    public class PurchaseOrder
    {
        ObservableCollection<ProductMaterialItem> m_Items;
        ObservableCollection<SubMaterial> m_SubMaterials;
        MaterialName m_MaterialName;
        public PurchaseOrder(MaterialName materialName, ObservableCollection<SubMaterial> availableSubMaterials, ObservableCollection<ProductMaterialItem> productMaterialItems)
        {
            m_MaterialName = materialName;
            m_SubMaterials = availableSubMaterials;
            m_Items = productMaterialItems;
        }

        public ObservableCollection<ProductMaterialItem> ProductMaterialItemsWrapper
        {
            get { return m_Items; }
            set { m_Items = value; }
        }
        public ObservableCollection<SubMaterial> AvailableSubMaterials
        {
            get { return m_SubMaterials; }
            set { m_SubMaterials = value; }
        }
    }
}
