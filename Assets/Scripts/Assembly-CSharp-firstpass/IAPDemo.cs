//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Purchasing;
//using UnityEngine.Store;
//using UnityEngine.UI;

//[AddComponentMenu("Unity IAP/Demo")]
//public class IAPDemo : MonoBehaviour, IStoreListener
//{
//	[Serializable]
//	public class UnityChannelPurchaseError
//	{
//		public string error;

//		public UnityChannelPurchaseInfo purchaseInfo;
//	}

//	[Serializable]
//	public class UnityChannelPurchaseInfo
//	{
//		public string productCode;

//		public string gameOrderId;

//		public string orderQueryToken;
//	}

//	private class UnityChannelLoginHandler : ILoginListener
//	{
//		internal Action initializeSucceededAction;

//		internal Action<string> initializeFailedAction;

//		internal Action<UserInfo> loginSucceededAction;

//		internal Action<string> loginFailedAction;

//		public void OnInitialized()
//		{
//			initializeSucceededAction();
//		}

//		public void OnInitializeFailed(string message)
//		{
//			initializeFailedAction(message);
//		}

//		public void OnLogin(UserInfo userInfo)
//		{
//			loginSucceededAction(userInfo);
//		}

//		public void OnLoginFailed(string message)
//		{
//			loginFailedAction(message);
//		}
//	}

//	private IStoreController m_Controller;

//	private IAppleExtensions m_AppleExtensions;

//	private IMoolahExtension m_MoolahExtensions;

//	private ISamsungAppsExtensions m_SamsungExtensions;

//	private IMicrosoftExtensions m_MicrosoftExtensions;

//	private IUnityChannelExtensions m_UnityChannelExtensions;

//	private ITransactionHistoryExtensions m_TransactionHistoryExtensions;

//	private bool m_IsGooglePlayStoreSelected;

//	private bool m_IsSamsungAppsStoreSelected;

//	private bool m_IsCloudMoolahStoreSelected;

//	private bool m_IsUnityChannelSelected;

//	private string m_LastTransactionID;

//	private bool m_IsLoggedIn;

//	private UnityChannelLoginHandler unityChannelLoginHandler;

//	private bool m_FetchReceiptPayloadOnPurchase;

//	private bool m_PurchaseInProgress;

//	private Dictionary<string, IAPDemoProductUI> m_ProductUIs = new Dictionary<string, IAPDemoProductUI>();

//	public GameObject productUITemplate;

//	public RectTransform contentRect;

//	public Button restoreButton;

//	public Button loginButton;

//	public Button validateButton;

//	public Text versionText;

//	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//	{
//		m_Controller = controller;
//		m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
//		m_SamsungExtensions = extensions.GetExtension<ISamsungAppsExtensions>();
//		m_MoolahExtensions = extensions.GetExtension<IMoolahExtension>();
//		m_MicrosoftExtensions = extensions.GetExtension<IMicrosoftExtensions>();
//		m_UnityChannelExtensions = extensions.GetExtension<IUnityChannelExtensions>();
//		m_TransactionHistoryExtensions = extensions.GetExtension<ITransactionHistoryExtensions>();
//		InitUI(controller.products.all);
//		m_AppleExtensions.RegisterPurchaseDeferredListener(OnDeferred);
//		Product[] all = controller.products.all;
//		foreach (Product product in all)
//		{
//			if (product.availableToPurchase)
//			{
//			}
//		}
//		AddProductUIs(m_Controller.products.all);
//		LogProductDefinitions();
//	}

//	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
//	{
//		m_LastTransactionID = e.purchasedProduct.transactionID;
//		m_PurchaseInProgress = false;
//		if (m_IsUnityChannelSelected)
//		{
//			UnifiedReceipt unifiedReceipt = JsonUtility.FromJson<UnifiedReceipt>(e.purchasedProduct.receipt);
//			if (unifiedReceipt != null && !string.IsNullOrEmpty(unifiedReceipt.Payload))
//			{
//				UnityChannelPurchaseReceipt unityChannelPurchaseReceipt = JsonUtility.FromJson<UnityChannelPurchaseReceipt>(unifiedReceipt.Payload);
//			}
//		}
//		UpdateProductUI(e.purchasedProduct);
//		return PurchaseProcessingResult.Complete;
//	}

//	public void OnPurchaseFailed(Product item, PurchaseFailureReason r)
//	{
//		if (m_TransactionHistoryExtensions.GetLastPurchaseFailureDescription() != null)
//		{
//		}
//		if (m_IsUnityChannelSelected)
//		{
//			string lastPurchaseError = m_UnityChannelExtensions.GetLastPurchaseError();
//			UnityChannelPurchaseError unityChannelPurchaseError = JsonUtility.FromJson<UnityChannelPurchaseError>(lastPurchaseError);
//			if (unityChannelPurchaseError != null && unityChannelPurchaseError.purchaseInfo != null)
//			{
//				UnityChannelPurchaseInfo purchaseInfo = unityChannelPurchaseError.purchaseInfo;
//			}
//			if (r != PurchaseFailureReason.DuplicateTransaction)
//			{
//			}
//		}
//		m_PurchaseInProgress = false;
//	}

//	public void OnInitializeFailed(InitializationFailureReason error)
//	{
//		switch (error)
//		{
//		}
//	}

//	public void Awake()
//	{
//		StandardPurchasingModule standardPurchasingModule = StandardPurchasingModule.Instance();
//		standardPurchasingModule.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
//		ConfigurationBuilder builder = ConfigurationBuilder.Instance(standardPurchasingModule);
//		builder.Configure<IMicrosoftConfiguration>().useMockBillingSystem = false;
//		m_IsGooglePlayStoreSelected = Application.platform == RuntimePlatform.Android && standardPurchasingModule.appStore == AppStore.GooglePlay;
//		builder.Configure<IMoolahConfiguration>().appKey = "d93f4564c41d463ed3d3cd207594ee1b";
//		builder.Configure<IMoolahConfiguration>().hashKey = "cc";
//		builder.Configure<IMoolahConfiguration>().SetMode(CloudMoolahMode.AlwaysSucceed);
//		m_IsCloudMoolahStoreSelected = Application.platform == RuntimePlatform.Android && standardPurchasingModule.appStore == AppStore.CloudMoolah;
//		m_IsUnityChannelSelected = Application.platform == RuntimePlatform.Android && standardPurchasingModule.appStore == AppStore.XiaomiMiPay;
//		builder.Configure<IUnityChannelConfiguration>().fetchReceiptPayloadOnPurchase = m_FetchReceiptPayloadOnPurchase;
//		ProductCatalog productCatalog = ProductCatalog.LoadDefaultCatalog();
//		foreach (ProductCatalogItem allValidProduct in productCatalog.allValidProducts)
//		{
//			if (allValidProduct.allStoreIDs.Count > 0)
//			{
//				IDs ds = new IDs();
//				foreach (StoreID allStoreID in allValidProduct.allStoreIDs)
//				{
//					ds.Add(allStoreID.id, allStoreID.store);
//				}
//				builder.AddProduct(allValidProduct.id, allValidProduct.type, ds);
//			}
//			else
//			{
//				builder.AddProduct(allValidProduct.id, allValidProduct.type);
//			}
//		}
//		builder.AddProduct("100.gold.coins", ProductType.Consumable, new IDs
//		{
//			{ "com.unity3d.unityiap.unityiapdemo.100goldcoins.7", "MacAppStore" },
//			{ "000000596586", "TizenStore" },
//			{ "com.ff", "MoolahAppStore" },
//			{ "100.gold.coins", "AmazonApps" }
//		});
//		builder.AddProduct("500.gold.coins", ProductType.Consumable, new IDs
//		{
//			{ "com.unity3d.unityiap.unityiapdemo.500goldcoins.7", "MacAppStore" },
//			{ "000000596581", "TizenStore" },
//			{ "com.ee", "MoolahAppStore" },
//			{ "500.gold.coins", "AmazonApps" }
//		});
//		builder.AddProduct("sword", ProductType.NonConsumable, new IDs
//		{
//			{ "com.unity3d.unityiap.unityiapdemo.sword.7", "MacAppStore" },
//			{ "000000596583", "TizenStore" },
//			{ "sword", "AmazonApps" }
//		});
//		builder.Configure<ISamsungAppsConfiguration>().SetMode(SamsungAppsMode.AlwaysSucceed);
//		m_IsSamsungAppsStoreSelected = Application.platform == RuntimePlatform.Android && standardPurchasingModule.appStore == AppStore.SamsungApps;
//		builder.Configure<ITizenStoreConfiguration>().SetGroupId("100000085616");
//		Action initializeUnityIap = delegate
//		{
//			UnityPurchasing.Initialize(this, builder);
//		};
//		if (!m_IsUnityChannelSelected)
//		{
//			initializeUnityIap();
//			return;
//		}
//		AppInfo appInfo = new AppInfo();
//		appInfo.appId = "abc123appId";
//		appInfo.appKey = "efg456appKey";
//		appInfo.clientId = "hij789clientId";
//		appInfo.clientKey = "klm012clientKey";
//		appInfo.debug = false;
//		unityChannelLoginHandler = new UnityChannelLoginHandler();
//		unityChannelLoginHandler.initializeFailedAction = delegate
//		{
//		};
//		unityChannelLoginHandler.initializeSucceededAction = delegate
//		{
//			initializeUnityIap();
//		};
//		StoreService.Initialize(appInfo, unityChannelLoginHandler);
//	}

//	private void OnTransactionsRestored(bool success)
//	{
//	}

//	private void OnDeferred(Product item)
//	{
//	}

//	private void InitUI(IEnumerable<Product> items)
//	{
//		restoreButton.gameObject.SetActive(NeedRestoreButton());
//		loginButton.gameObject.SetActive(NeedLoginButton());
//		validateButton.gameObject.SetActive(NeedValidateButton());
//		ClearProductUIs();
//		restoreButton.onClick.AddListener(RestoreButtonClick);
//		loginButton.onClick.AddListener(LoginButtonClick);
//		validateButton.onClick.AddListener(ValidateButtonClick);
//		versionText.text = "Unity version: " + Application.unityVersion + "\nIAP version: 1.20.0";
//	}

//	public void PurchaseButtonClick(string productID)
//	{
//		if (!m_PurchaseInProgress && m_Controller != null && m_Controller.products.WithID(productID) != null)
//		{
//			if (!NeedLoginButton() || !m_IsLoggedIn)
//			{
//			}
//			m_PurchaseInProgress = true;
//			m_Controller.InitiatePurchase(m_Controller.products.WithID(productID), "aDemoDeveloperPayload");
//		}
//	}

//	public void RestoreButtonClick()
//	{
//		if (m_IsCloudMoolahStoreSelected)
//		{
//			if (m_IsLoggedIn)
//			{
//				m_MoolahExtensions.RestoreTransactionID(delegate(RestoreTransactionIDState restoreTransactionIDState)
//				{
//					bool success = restoreTransactionIDState != RestoreTransactionIDState.RestoreFailed && restoreTransactionIDState != RestoreTransactionIDState.NotKnown;
//					OnTransactionsRestored(success);
//				});
//			}
//		}
//		else if (m_IsSamsungAppsStoreSelected)
//		{
//			m_SamsungExtensions.RestoreTransactions(OnTransactionsRestored);
//		}
//		else if (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)
//		{
//			m_MicrosoftExtensions.RestoreTransactions();
//		}
//		else
//		{
//			m_AppleExtensions.RestoreTransactions(OnTransactionsRestored);
//		}
//	}

//	public void LoginButtonClick()
//	{
//		if (m_IsUnityChannelSelected)
//		{
//			unityChannelLoginHandler.loginSucceededAction = delegate
//			{
//				m_IsLoggedIn = true;
//			};
//			unityChannelLoginHandler.loginFailedAction = delegate
//			{
//				m_IsLoggedIn = false;
//			};
//			StoreService.Login(unityChannelLoginHandler);
//		}
//	}

//	public void ValidateButtonClick()
//	{
//		if (m_IsUnityChannelSelected)
//		{
//			string lastTransactionID = m_LastTransactionID;
//			m_UnityChannelExtensions.ValidateReceipt(lastTransactionID, delegate
//			{
//			});
//		}
//	}

//	private void ClearProductUIs()
//	{
//		foreach (KeyValuePair<string, IAPDemoProductUI> productUI in m_ProductUIs)
//		{
//			UnityEngine.Object.Destroy(productUI.Value.gameObject);
//		}
//		m_ProductUIs.Clear();
//	}

//	private void AddProductUIs(Product[] products)
//	{
//		ClearProductUIs();
//		RectTransform component = productUITemplate.GetComponent<RectTransform>();
//		float height = component.rect.height;
//		Vector3 localPosition = component.localPosition;
//		contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)products.Length * height);
//		foreach (Product product in products)
//		{
//			GameObject gameObject = UnityEngine.Object.Instantiate(productUITemplate.gameObject);
//			gameObject.transform.SetParent(productUITemplate.transform.parent, worldPositionStays: false);
//			RectTransform component2 = gameObject.GetComponent<RectTransform>();
//			component2.localPosition = localPosition;
//			localPosition += Vector3.down * height;
//			gameObject.SetActive(value: true);
//			IAPDemoProductUI component3 = gameObject.GetComponent<IAPDemoProductUI>();
//			component3.SetProduct(product, PurchaseButtonClick);
//			m_ProductUIs[product.definition.id] = component3;
//		}
//	}

//	private void UpdateProductUI(Product p)
//	{
//		if (m_ProductUIs.ContainsKey(p.definition.id))
//		{
//			m_ProductUIs[p.definition.id].SetProduct(p, PurchaseButtonClick);
//		}
//	}

//	private void UpdateProductPendingUI(Product p, int secondsRemaining)
//	{
//		if (m_ProductUIs.ContainsKey(p.definition.id))
//		{
//			m_ProductUIs[p.definition.id].SetPendingTime(secondsRemaining);
//		}
//	}

//	private bool NeedRestoreButton()
//	{
//		return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.tvOS || Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM || m_IsSamsungAppsStoreSelected || m_IsCloudMoolahStoreSelected;
//	}

//	private bool NeedLoginButton()
//	{
//		return m_IsUnityChannelSelected;
//	}

//	private bool NeedValidateButton()
//	{
//		return m_IsUnityChannelSelected;
//	}

//	private void LogProductDefinitions()
//	{
//		Product[] all = m_Controller.products.all;
//		Product[] array = all;
//		foreach (Product product in array)
//		{
//		}
//	}
//}
