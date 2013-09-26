﻿using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class DBResources : IDisposable
    {
        #region  single ton
            private static DBResources _DBSingleton = null;

            public static DBResources Instance
            {
                get
                {
                    if (_DBSingleton == null)
                        _DBSingleton = new DBResources();

                    return _DBSingleton;
                }
            }

        #endregion 

        #region
            OrderManagerDBEntities dbContext = null;
        #endregion 

        private DBResources()
        {
            dbContext = new OrderManagerDBEntities();
        }

        #region Supporting Collections

        #region Companies

        private ObservableCollection<Company> m_Customers = null;
            public ObservableCollection<Company> Customers
            {
                get
                {
                    if (m_Customers == null)
                    {
                        m_Customers = UpdateCompaniesCollection("Customer");
                    }

                    return m_Customers;
                }
                private set
                {
                    m_Customers = value;
                }
            }

            private ObservableCollection<Company> m_Agents = null;
            public ObservableCollection<Company> Agents
            {
                get
                {
                    if (m_Agents == null)
                    {
                        m_Agents = UpdateCompaniesCollection("Agent");
                    }
                    return m_Agents;
                }
                private set
                {
                    m_Agents = value;
                }
            }

            private ObservableCollection<Company> m_Suppliers = null;
            public ObservableCollection<Company> Suppliers
            {
                get
                {
                    if (m_Suppliers == null)
                    {
                        m_Suppliers = UpdateCompaniesCollection("Supplier");
                    }
                    return m_Suppliers;
                }
                private set
                {
                    m_Suppliers = value;
                }
            }

            private ObservableCollection<Company> UpdateCompaniesCollection(string type)
            {
                return new ObservableCollection<Company>(dbContext.Companies.Where(c => c.CompanyType.Type == type)
                                                                   .Select(c => c)
                                                                   .ToList());
            }

            //Create a new Company of type passed in the arguments
            public Company CreateNewCompany(string type)
            {
                Company newCompany = dbContext.Companies.Create();


                var companyTypeID = dbContext.CompanyTypes.Where(c => c.Type == type)
                                                          .Select(c => c.CompanyTypeID)
                                                          .FirstOrDefault();
                newCompany.CompanyTypeID = companyTypeID;


                return newCompany;
            }

            public void SaveNewCompany(Company company)
            {
                dbContext.Companies.Add(company);
                Save();

                switch (company.CompanyType.Type)
                {
                    case "Customer":
                        Customers = UpdateCompaniesCollection(company.CompanyType.Type);
                        break; 
                    case "Agent":
                        Agents = UpdateCompaniesCollection(company.CompanyType.Type);
                        break;
                    case "Supplier":
                        Suppliers = UpdateCompaniesCollection(company.CompanyType.Type);
                        break;
                }
            }


            #endregion  

        #region Product, Materials and SubMaterials

        private ObservableCollection<ProductName> m_AvailableProducts = null;
        public ObservableCollection<ProductName> AvailableProducts
        {
            get
            {
                if (m_AvailableProducts == null)
                {
                    m_AvailableProducts = new ObservableCollection<ProductName>(dbContext.ProductNames.ToList());
                }
                return m_AvailableProducts;
            }
            private set
            {
                m_AvailableProducts = value;
            }
        }

        //Create a new product
        public ProductName CreateNewProduct(string newProductName)
        {
            ProductName newProduct = dbContext.ProductNames.Create();
            newProduct.Name = newProductName;

            dbContext.ProductNames.Add(newProduct);
            Save();

            m_AvailableProducts = new ObservableCollection<ProductName>(dbContext.ProductNames.ToList());

            return newProduct;
        }
       
        private ObservableCollection<MaterialName> m_AvailableMaterials = null;
        public ObservableCollection<MaterialName> AvailableMaterials
        {
            get
            {
                if (m_AvailableMaterials == null)
                {
                    m_AvailableMaterials = new ObservableCollection<MaterialName>(dbContext.MaterialNames.ToList());
                }
                return m_AvailableMaterials;
            }
            private set
            {
                m_AvailableMaterials = value;
            }
        }


        //Create a new product
        public MaterialName CreateNewMaterial(string newMaterialName)
        {
            MaterialName newMaterial = dbContext.MaterialNames.Create();
            newMaterial.Name = newMaterialName;

            dbContext.MaterialNames.Add(newMaterial);
            Save();

            AvailableMaterials = new ObservableCollection<MaterialName>(dbContext.MaterialNames.ToList()); //Refresh

            return newMaterial;
        }


        #endregion 

        #region No Add support tables

                private ObservableCollection<OrderThrough> m_OrderThroughs = null;
                public ObservableCollection<OrderThrough> OrderThroughs
                {
                    get
                    {
                        if (m_OrderThroughs == null)
                        {
                            m_OrderThroughs = new ObservableCollection<OrderThrough>(dbContext.OrderThroughs.ToList());
                        }

                        return m_OrderThroughs;
                    }
                }

                private ObservableCollection<CommissionValueType> m_CommissionValueTypes = null;
                public ObservableCollection<CommissionValueType> CommissionValueTypes
                {
                    get
                    {
                        if (m_CommissionValueTypes == null)
                        {
                            m_CommissionValueTypes = new ObservableCollection<CommissionValueType>(dbContext.CommissionValueTypes.ToList());
                        }
                        return m_CommissionValueTypes;
                    }
                }

                private ObservableCollection<Currency> m_Currencies = null;
                public ObservableCollection<Currency> Currencies
                {
                    get
                    {
                        if (m_Currencies == null)
                        {
                            m_Currencies = new ObservableCollection<Currency>(dbContext.Currencies.ToList());
                        }

                        return m_Currencies;
                    }
                }

                private ObservableCollection<UnitsOfMeasurement> m_UOMs = null;
                public ObservableCollection<UnitsOfMeasurement> UOMs
                {
                    get
                    {
                        if (m_UOMs == null)
                        {
                            m_UOMs = new ObservableCollection<UnitsOfMeasurement>(dbContext.UnitsOfMeasurements.ToList());
                        }

                        return m_UOMs;
                    }
                }


          #endregion 

        #region Orders

            public Order CreateNewOrder()
            {
                Order newOrder = dbContext.Orders.Create();
                newOrder.OrderDate = DateTime.Now;
                return newOrder;
            }

            public void CreateNewOrder(Order newOrder)
            {
                dbContext.Orders.Add(newOrder);
                Save();
            }

        #endregion 

        #endregion


        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
