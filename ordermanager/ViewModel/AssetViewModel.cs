using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class AssetViewModel
    {
        public AssetViewModel()
        {

        }

        private ObservableCollection<Asset> m_ExistingAssets = null;
        public ObservableCollection<Asset> ExistingAssets
        {
            get
            {
                if (m_ExistingAssets == null)
                {
                    m_ExistingAssets = new ObservableCollection<Asset>(DBResources.Instance.Context.Assets.ToList());
                    TotalAssetValue = 0;
                    foreach (Asset asset in m_ExistingAssets)
                    {
                        TotalAssetValue += asset.ValueInINR;
                    }
                }

                return m_ExistingAssets;
            }
        }

        private decimal m_TotalAssetValue = 0;
        public decimal TotalAssetValue
        {
            get
            {
                return m_TotalAssetValue;
            }
            set
            {
                m_TotalAssetValue = value;
            }
        }


        public void DeleteAsset(Asset asset)
        {
            DBResources.Instance.Context.Assets.Remove(asset);
            DBResources.Instance.Save();

            if (ExistingAssets != null)
            {
                ExistingAssets.Remove(asset);
                TotalAssetValue -= asset.ValueInINR;
            }
        }

        public Asset AddNewAsset(Asset asset)
        {
            Asset newlyAddedAsset = DBResources.Instance.Context.Assets.Add(asset);
            DBResources.Instance.Save();

            if (ExistingAssets != null)
            {
                ExistingAssets.Add(newlyAddedAsset);
                TotalAssetValue += newlyAddedAsset.ValueInINR;
            }

            return newlyAddedAsset;
        }

    }
}
