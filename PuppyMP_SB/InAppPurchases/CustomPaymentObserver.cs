using System;
using System.Linq;
using MonoTouch.StoreKit;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace speechTherapy {
	internal class CustomPaymentObserver : SKPaymentTransactionObserver {
		private InAppPurchaseManager theManager;
		
		public CustomPaymentObserver(InAppPurchaseManager manager)
		{
			theManager = manager;
		}
		
		// called when the transaction status is updated
		public override void UpdatedTransactions (SKPaymentQueue queue, SKPaymentTransaction[] transactions)
		{
			Console.WriteLine ("Updating Transactions in Custom Payment Observer");
			foreach (SKPaymentTransaction transaction in transactions)
			{
			    switch (transaction.TransactionState)
			    {
				case SKPaymentTransactionState.Purchased:
					theManager.CompleteTransaction (transaction);
					Console.WriteLine ("purchased status");
			            break;
				case SKPaymentTransactionState.Failed:
					theManager.FailedTransaction (transaction);
					Console.WriteLine ("failed status");
			            break;
				case SKPaymentTransactionState.Restored:
					theManager.RestoreTransaction (transaction);
					Console.WriteLine ("restored status");
			            break;
				default:
					Console.WriteLine ("other status");
			            break;
			    }
			}
		}
		
		public override void PaymentQueueRestoreCompletedTransactionsFinished (SKPaymentQueue queue)
		{
			// Restore succeeded
			Console.WriteLine(" ** RESTORE PaymentQueueRestoreCompletedTransactionsFinished ");
		}
		public override void RestoreCompletedTransactionsFailedWithError (SKPaymentQueue queue, NSError error)
		{
			// Restore failed somewhere...
			Console.WriteLine(" ** RESTORE RestoreCompletedTransactionsFailedWithError " + error.LocalizedDescription);
		}
	}
}

