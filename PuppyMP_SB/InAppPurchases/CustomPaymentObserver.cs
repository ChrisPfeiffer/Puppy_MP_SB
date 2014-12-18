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
			           theManager.CompleteTransaction(transaction);
			            break;
			        case SKPaymentTransactionState.Failed:
			           theManager.FailedTransaction(transaction);
			            break;
			        case SKPaymentTransactionState.Restored:
			            theManager.RestoreTransaction(transaction);
			            break;
				default:
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

