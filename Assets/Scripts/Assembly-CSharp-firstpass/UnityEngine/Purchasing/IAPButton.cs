//using System;
//using UnityEngine.Events;
//using UnityEngine.UI;

//namespace UnityEngine.Purchasing
//{
//	[RequireComponent(typeof(Button))]
//	[AddComponentMenu("Unity IAP/IAP Button")]
//	[HelpURL("https://docs.unity3d.com/Manual/UnityIAP.html")]
//	public class IAPButton : MonoBehaviour
//	{
//		public enum ButtonType
//		{
//			Purchase,
//			Restore
//		}

//		[Serializable]
//		public class OnPurchaseCompletedEvent
//		{
//		}

//		[Serializable]
//		public class OnPurchaseFailedEvent
//		{
//		}

//		[HideInInspector]
//		public string productId;

//		[Tooltip("The type of this button, can be either a purchase or a restore button")]
//		public ButtonType buttonType;

//		[Tooltip("Consume the product immediately after a successful purchase")]
//		public bool consumePurchase = true;

//		[Tooltip("Event fired after a successful purchase of this product")]
//		public OnPurchaseCompletedEvent onPurchaseComplete;

//		[Tooltip("Event fired after a failed purchase of this product")]
//		public OnPurchaseFailedEvent onPurchaseFailed;

//		[Tooltip("[Optional] Displays the localized title from the app store")]
//		public Text titleText;

//		[Tooltip("[Optional] Displays the localized description from the app store")]
//		public Text descriptionText;

//		[Tooltip("[Optional] Displays the localized price from the app store")]
//		public Text priceText;

//		private void Start()
//		{
//			Button component = GetComponent<Button>();
//			if (buttonType == ButtonType.Purchase)
//			{
//				if ((bool)component)
//				{
//					component.onClick.AddListener(PurchaseProduct);
//				}
//				if (string.IsNullOrEmpty(productId))
//				{
//				}
//			}
//			else if (buttonType == ButtonType.Restore && (bool)component)
//			{
//				component.onClick.AddListener(Restore);
//			}
//		}

//		private void OnEnable()
//		{
//			if (buttonType == ButtonType.Purchase)
//			{
//				CodelessIAPStoreListener.Instance.AddButton(this);
//				if (CodelessIAPStoreListener.initializationComplete)
//				{
//					UpdateText();
//				}
//			}
//		}

//		private void OnDisable()
//		{
//			if (buttonType == ButtonType.Purchase)
//			{
//				CodelessIAPStoreListener.Instance.RemoveButton(this);
//			}
//		}

//		private void PurchaseProduct()
//		{
//			if (buttonType == ButtonType.Purchase)
//			{
//				CodelessIAPStoreListener.Instance.InitiatePurchase(productId);
//			}
//		}

//		private void Restore()
//		{
//			if (buttonType != ButtonType.Restore)
//			{
//				return;
//			}
//			if (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)
//			{
//				CodelessIAPStoreListener.Instance.ExtensionProvider.GetExtension<IMicrosoftExtensions>().RestoreTransactions();
//			}
//			else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.tvOS)
//			{
//				CodelessIAPStoreListener.Instance.ExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnTransactionsRestored);
//			}
//			else if (Application.platform == RuntimePlatform.Android && StandardPurchasingModule.Instance().appStore == AppStore.SamsungApps)
//			{
//				CodelessIAPStoreListener.Instance.ExtensionProvider.GetExtension<ISamsungAppsExtensions>().RestoreTransactions(OnTransactionsRestored);
//			}
//			else if (Application.platform == RuntimePlatform.Android && StandardPurchasingModule.Instance().appStore == AppStore.CloudMoolah)
//			{
//				CodelessIAPStoreListener.Instance.ExtensionProvider.GetExtension<IMoolahExtension>().RestoreTransactionID(delegate(RestoreTransactionIDState restoreTransactionIDState)
//				{
//					OnTransactionsRestored(restoreTransactionIDState != RestoreTransactionIDState.RestoreFailed && restoreTransactionIDState != RestoreTransactionIDState.NotKnown);
//				});
//			}
//		}

//		private void OnTransactionsRestored(bool success)
//		{
//		}

//		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
//		{
//			onPurchaseComplete.Invoke(e.purchasedProduct);
//			return (!consumePurchase) ? PurchaseProcessingResult.Pending : PurchaseProcessingResult.Complete;
//		}

//		public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
//		{
//			onPurchaseFailed.Invoke(product, reason);
//		}

//		internal void UpdateText()
//		{
//			Product product = CodelessIAPStoreListener.Instance.GetProduct(productId);
//			if (product != null)
//			{
//				if (titleText != null)
//				{
//					titleText.text = product.metadata.localizedTitle;
//				}
//				if (descriptionText != null)
//				{
//					descriptionText.text = product.metadata.localizedDescription;
//				}
//				if (priceText != null)
//				{
//					priceText.text = product.metadata.localizedPriceString;
//				}
//			}
//		}
//	}
//}
