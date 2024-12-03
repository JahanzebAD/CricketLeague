//using System.Collections.Generic;

//namespace UnityEngine.Purchasing
//{
//	public class CodelessIAPStoreListener : IStoreListener
//	{
//		private static CodelessIAPStoreListener instance;

//		private List<IAPButton> activeButtons = new List<IAPButton>();

//		private List<IAPListener> activeListeners = new List<IAPListener>();

//		private static bool unityPurchasingInitialized;

//		protected IStoreController controller;

//		protected IExtensionProvider extensions;

//		protected ProductCatalog catalog;

//		public static bool initializationComplete;

//		public static CodelessIAPStoreListener Instance
//		{
//			get
//			{
//				if (instance == null)
//				{
//					CreateCodelessIAPStoreListenerInstance();
//				}
//				return instance;
//			}
//		}

//		public IStoreController StoreController => controller;

//		public IExtensionProvider ExtensionProvider => extensions;

//		private CodelessIAPStoreListener()
//		{
//			catalog = ProductCatalog.LoadDefaultCatalog();
//		}

//		[RuntimeInitializeOnLoadMethod]
//		private static void InitializeCodelessPurchasingOnLoad()
//		{
//			ProductCatalog productCatalog = ProductCatalog.LoadDefaultCatalog();
//			if (productCatalog.enableCodelessAutoInitialization && !productCatalog.IsEmpty() && instance == null)
//			{
//				CreateCodelessIAPStoreListenerInstance();
//			}
//		}

//		private static void InitializePurchasing()
//		{
//			StandardPurchasingModule standardPurchasingModule = StandardPurchasingModule.Instance();
//			standardPurchasingModule.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
//			ConfigurationBuilder builder = ConfigurationBuilder.Instance(standardPurchasingModule);
//			IAPConfigurationHelper.PopulateConfigurationBuilder(ref builder, instance.catalog);
//			UnityPurchasing.Initialize(instance, builder);
//			unityPurchasingInitialized = true;
//		}

//		private static void CreateCodelessIAPStoreListenerInstance()
//		{
//			instance = new CodelessIAPStoreListener();
//			if (!unityPurchasingInitialized)
//			{
//				InitializePurchasing();
//			}
//		}

//		public bool HasProductInCatalog(string productID)
//		{
//			foreach (ProductCatalogItem allProduct in catalog.allProducts)
//			{
//				if (allProduct.id == productID)
//				{
//					return true;
//				}
//			}
//			return false;
//		}

//		public Product GetProduct(string productID)
//		{
//			if (controller != null && controller.products != null && !string.IsNullOrEmpty(productID))
//			{
//				return controller.products.WithID(productID);
//			}
//			return null;
//		}

//		public void AddButton(IAPButton button)
//		{
//			activeButtons.Add(button);
//		}

//		public void RemoveButton(IAPButton button)
//		{
//			activeButtons.Remove(button);
//		}

//		public void AddListener(IAPListener listener)
//		{
//			activeListeners.Add(listener);
//		}

//		public void RemoveListener(IAPListener listener)
//		{
//			activeListeners.Remove(listener);
//		}

//		public void InitiatePurchase(string productID)
//		{
//			if (controller == null)
//			{
//				foreach (IAPButton activeButton in activeButtons)
//				{
//					if (activeButton.productId == productID)
//					{
//						activeButton.OnPurchaseFailed(null, PurchaseFailureReason.PurchasingUnavailable);
//					}
//				}
//			}
//			else
//			{
//				controller.InitiatePurchase(productID);
//			}
//		}

//		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//		{
//			initializationComplete = true;
//			this.controller = controller;
//			this.extensions = extensions;
//			foreach (IAPButton activeButton in activeButtons)
//			{
//				activeButton.UpdateText();
//			}
//		}

//		public void OnInitializeFailed(InitializationFailureReason error)
//		{
//		}

//		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
//		{
//			bool flag = false;
//			bool flag2 = false;
//			foreach (IAPButton activeButton in activeButtons)
//			{
//				if (activeButton.productId == e.purchasedProduct.definition.id)
//				{
//					if (activeButton.ProcessPurchase(e) == PurchaseProcessingResult.Complete)
//					{
//						flag = true;
//					}
//					flag2 = true;
//				}
//			}
//			foreach (IAPListener activeListener in activeListeners)
//			{
//				if (activeListener.ProcessPurchase(e) == PurchaseProcessingResult.Complete)
//				{
//					flag = true;
//				}
//				flag2 = true;
//			}
//			if (!flag2)
//			{
//			}
//			return (!flag) ? PurchaseProcessingResult.Pending : PurchaseProcessingResult.Complete;
//		}

//		public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
//		{
//			bool flag = false;
//			foreach (IAPButton activeButton in activeButtons)
//			{
//				if (activeButton.productId == product.definition.id)
//				{
//					activeButton.OnPurchaseFailed(product, reason);
//					flag = true;
//				}
//			}
//			foreach (IAPListener activeListener in activeListeners)
//			{
//				activeListener.OnPurchaseFailed(product, reason);
//				flag = true;
//			}
//			if (flag)
//			{
//			}
//		}
//	}
//}
