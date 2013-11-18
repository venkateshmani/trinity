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
                }

                return m_ExistingAssets;
            }
        }

        public Asset AddNewAsset(Asset asset)
        {
            Asset newlyAddedAsset = DBResources.Instance.Context.Assets.Add(asset);
            DBResources.Instance.Save();

            if (ExistingAssets != null)
            {
                ExistingAssets.Add(newlyAddedAsset);
            }

            return newlyAddedAsset;
        }

    }
}
