using System.Linq;
using base_ = com.clover.sdk.v3.base_;
using customers = com.clover.sdk.v3.customers;
using grpc = Clover.Grpc;
using merchant = com.clover.sdk.v3.merchant;
using order = com.clover.sdk.v3.order;
using payments = com.clover.sdk.v3.payments;
using remotepay = com.clover.remotepay.sdk;
using transport = com.clover.remotepay.transport;

namespace MockGrpcPayDisplay
{
    public static partial class Translate
    {
        public static grpc.AccountType From(payments.AccountType src)
        {
            switch (src)
            {
                case payments.AccountType.CREDIT:
                    return grpc.AccountType.Credit;
                case payments.AccountType.DEBIT:
                    return grpc.AccountType.Debit;
                case payments.AccountType.CHECKING:
                    return grpc.AccountType.Checking;
                case payments.AccountType.SAVINGS:
                    return grpc.AccountType.Savings;
                default:
                    return grpc.AccountType.Unknown;
            }
        }

        public static grpc.AdditionalChargeAmount From(payments.AdditionalChargeAmount src) =>
            new grpc.AdditionalChargeAmount
            {
                Amount = From(src.amount),
                Id = From(src.id),
                Type = From(src.type),
            };

        public static grpc.AdditionalChargeType From(payments.AdditionalChargeType src)
        {
            switch (src)
            {
                case payments.AdditionalChargeType.INTERAC:
                    return grpc.AdditionalChargeType.Interac;
                default:
                    return grpc.AdditionalChargeType.Unknown;
            }
        }

        public static grpc.AppTracking From(payments.AppTracking src)
        {
            if (src == null) return null;
            return new grpc.AppTracking
            {
                ApplicationId = From(src.applicationID),
                ApplicationName = From(src.applicationID),
                ApplicationVersion = From(src.applicationVersion),
                CreditRef = From(src.creditRef),
                CreditRefundRef = From(src.creditRefundRef),
                DeveloperAppId = From(src.developerAppId),
                PaymentRef = From(src.paymentRef),
                RefundRef = From(src.refundRef),
                SourceSdk = From(src.sourceSDK),
                SourceSdkVersion = From(src.sourceSDKVersion),
            };
        }

        public static grpc.AvsResult From(payments.AVSResult src)
        {
            switch (src)
            {
                case payments.AVSResult.SUCCESS:
                    return grpc.AvsResult.Success;
                case payments.AVSResult.ZIP_CODE_MATCH:
                    return grpc.AvsResult.ZipCodeMatch;
                case payments.AVSResult.ZIP_CODE_MATCH_ADDRESS_NOT_CHECKED:
                    return grpc.AvsResult.ZipCodeMatchAddressNotChecked;
                case payments.AVSResult.ADDRESS_MATCH:
                    return grpc.AvsResult.AddressMatch;
                case payments.AVSResult.ADDRESS_MATCH_ZIP_NOT_CHECKED:
                    return grpc.AvsResult.AddressMatchZipNotChecked;
                case payments.AVSResult.NEITHER_MATCH:
                    return grpc.AvsResult.NeitherMatch;
                case payments.AVSResult.SERVICE_FAILURE:
                    return grpc.AvsResult.ServiceFailure;
                case payments.AVSResult.SERVICE_UNAVAILABLE:
                    return grpc.AvsResult.ServiceUnavailable;
                case payments.AVSResult.NOT_CHECKED:
                    return grpc.AvsResult.NotChecked;
                case payments.AVSResult.ZIP_CODE_NOT_MATCHED_ADDRESS_NOT_CHECKED:
                    return grpc.AvsResult.ZipCodeNotMatchedAddressNotChecked;
                case payments.AVSResult.ADDRESS_NOT_MATCHED_ZIP_CODE_NOT_CHECKED:
                    return grpc.AvsResult.AddressNotMatchedZipCodeNotChecked;
                default:
                    return grpc.AvsResult.Unknown;
            }
        }

        public static grpc.CardEntryType From(payments.CardEntryType src)
        {
            switch (src)
            {
                case payments.CardEntryType.SWIPED:
                    return grpc.CardEntryType.Swiped;
                case payments.CardEntryType.KEYED:
                    return grpc.CardEntryType.Keyed;
                case payments.CardEntryType.VOICE:
                    return grpc.CardEntryType.Voice;
                case payments.CardEntryType.VAULTED:
                    return grpc.CardEntryType.Vaulted;
                case payments.CardEntryType.OFFLINE_SWIPED:
                    return grpc.CardEntryType.OfflineSwiped;
                case payments.CardEntryType.OFFLINE_KEYED:
                    return grpc.CardEntryType.OfflineKeyed;
                case payments.CardEntryType.EMV_CONTACT:
                    return grpc.CardEntryType.EmvContact;
                case payments.CardEntryType.EMV_CONTACTLESS:
                    return grpc.CardEntryType.EmvContactless;
                case payments.CardEntryType.MSD_CONTACTLESS:
                    return grpc.CardEntryType.MsdContactless;
                case payments.CardEntryType.PINPAD_MANUAL_ENTRY:
                    return grpc.CardEntryType.PinpadManualEntry;
                default:
                    return grpc.CardEntryType.Unknown;
            }
        }

        public static grpc.CardTransaction From(payments.CardTransaction src)
        {
            if (src == null) return null;
            var result = new grpc.CardTransaction
            {
                PaymentRef = From(src.paymentRef),
                CreditRef = From(src.creditRef),
                CardType = From(src.cardType),
                EntryType = From(src.entryType),
                First6 = From(src.first6),
                Last4 = From(src.last4),
                Type = From(src.type),
                AuthCode = From(src.authCode),
                ReferenceId = From(src.referenceId),
                TransactionNo = From(src.transactionNo),
                State = From(src.state),
                BegBalance = From(src.begBalance),
                EndBalance = From(src.endBalance),
                AvsResult = From(src.avsResult),
                CardholderName = From(src.cardholderName),
                Token = From(src.token),
                VaultedCard = From(src.vaultedCard),
            };
            result.Extra.Add(src.extra);
            return result;
        }

        public static grpc.CardTransactionState From(payments.CardTransactionState src)
        {
            switch (src)
            {
                case payments.CardTransactionState.PENDING:
                    return grpc.CardTransactionState.Pending;
                case payments.CardTransactionState.CLOSED:
                    return grpc.CardTransactionState.Closed;
                default:
                    return grpc.CardTransactionState.Unknown;
            }
        }

        public static grpc.CardTransactionType From(payments.CardTransactionType src)
        {
            switch (src)
            {
                case payments.CardTransactionType.AUTH:
                    return grpc.CardTransactionType.Auth;
                case payments.CardTransactionType.PREAUTH:
                    return grpc.CardTransactionType.PreAuth;
                case payments.CardTransactionType.PREAUTHCAPTURE:
                    return grpc.CardTransactionType.PreAuthCapture;
                case payments.CardTransactionType.ADJUST:
                    return grpc.CardTransactionType.Adjust;
                case payments.CardTransactionType.VOID:
                    return grpc.CardTransactionType.Void;
                case payments.CardTransactionType.VOIDRETURN:
                    return grpc.CardTransactionType.VoidReturn;
                case payments.CardTransactionType.RETURN:
                    return grpc.CardTransactionType.Return;
                case payments.CardTransactionType.REFUND:
                    return grpc.CardTransactionType.Refund;
                case payments.CardTransactionType.NAKEDREFUND:
                    return grpc.CardTransactionType.NakedRefund;
                case payments.CardTransactionType.GETBALANCE:
                    return grpc.CardTransactionType.GetBalance;
                case payments.CardTransactionType.BATCHCLOSE:
                    return grpc.CardTransactionType.BatchClose;
                case payments.CardTransactionType.ACTIVATE:
                    return grpc.CardTransactionType.Activate;
                case payments.CardTransactionType.BALANCE_LOCK:
                    return grpc.CardTransactionType.BalanceLock;
                case payments.CardTransactionType.LOAD:
                    return grpc.CardTransactionType.Load;
                case payments.CardTransactionType.CASHOUT:
                    return grpc.CardTransactionType.Cashout;
                case payments.CardTransactionType.CASHOUT_ACTIVE_STATUS:
                    return grpc.CardTransactionType.CashoutActiveStatus;
                case payments.CardTransactionType.REDEMPTION:
                    return grpc.CardTransactionType.Redemption;
                case payments.CardTransactionType.REDEMPTION_UNLOCK:
                    return grpc.CardTransactionType.RedemptionUnlock;
                case payments.CardTransactionType.RELOAD:
                    return grpc.CardTransactionType.Reload;
                default:
                    return grpc.CardTransactionType.Unknown;
            }
        }

        public static grpc.CardType From(payments.CardType src)
        {
            switch (src)
            {
                case payments.CardType.VISA:
                    return grpc.CardType.Visa;
                case payments.CardType.MC:
                    return grpc.CardType.Mc;
                case payments.CardType.AMEX:
                    return grpc.CardType.Amex;
                case payments.CardType.DISCOVER:
                    return grpc.CardType.Discover;
                case payments.CardType.DINERS_CLUB:
                    return grpc.CardType.DinersClub;
                case payments.CardType.JCB:
                    return grpc.CardType.Jcb;
                case payments.CardType.MAESTRO:
                    return grpc.CardType.Maestro;
                case payments.CardType.SOLO:
                    return grpc.CardType.Solo;
                case payments.CardType.LASER:
                    return grpc.CardType.Laser;
                case payments.CardType.CHINA_UNION_PAY:
                    return grpc.CardType.ChinaUnionPay;
                case payments.CardType.CARTE_BLANCHE:
                    return grpc.CardType.CarteBlanche;
                case payments.CardType.UNKNOWN:
                    return grpc.CardType.Unknown;
                case payments.CardType.GIFT_CARD:
                    return grpc.CardType.GiftCard;
                case payments.CardType.EBT:
                    return grpc.CardType.Ebt;
                case payments.CardType.INTERAC:
                    return grpc.CardType.Interac;
                case payments.CardType.OTHER:
                    return grpc.CardType.Other;
                default:
                    return grpc.CardType.Unknown;
            }
        }

        public static grpc.CashAdvanceCustomerIdentification From(payments.CashAdvanceCustomerIdentification src)
        {
            if (src == null) return null;
            return new grpc.CashAdvanceCustomerIdentification
            {
                IdType = From(src.idType),
                SerialNumber = From(src.serialNumber),
                MaskedSerialNumber = From(src.maskedSerialNumber),
                EncryptedSerialNumber = From(src.encryptedSerialNumber),
                ExpirationDate = From(src.expirationDate),
                IssuingState = From(src.issuingState),
                IssuingCountry = From(src.issuingCountry),
                CustomerName = From(src.customerName),
                AddressStreet1 = From(src.addressStreet1),
                AddressStreet2 = From(src.addressStreet2),
                AddressCity = From(src.addressCity),
                AddressState = From(src.addressState),
                AddressZipCode = From(src.addressZipCode),
                AddressCountry = From(src.addressCountry),
            };
        }

        public static grpc.CashAdvanceExtra From(payments.CashAdvanceExtra src)
        {
            if (src == null) return null;
            return new grpc.CashAdvanceExtra
            {
                CashAdvanceCustomerIdentification = From(src.cashAdvanceCustomerIdentification),
                CashAdvanceSerialNum = From(src.cashAdvanceSerialNum),
                PaymentRef = From(src.paymentRef),
            };
        }

        public static grpc.Challenge From(transport.Challenge src)
        {
            if (src == null) return null;
            return new grpc.Challenge
            {
                Message = From(src.message),
                Type = From(src.type),
                Reason = From(src.reason),
            };
        }

        public static grpc.ChallengeType From(transport.ChallengeType src)
        {
            switch (src)
            {
                case transport.ChallengeType.DUPLICATE_CHALLENGE:
                    return grpc.ChallengeType.DuplicateChallenge;
                case transport.ChallengeType.OFFLINE_CHALLENGE:
                    return grpc.ChallengeType.OfflineChallenge;
                default:
                    return grpc.ChallengeType.Unknown;
            }
        }

        public static grpc.ConfirmPaymentRequest From(remotepay.ConfirmPaymentRequest src)
        {
            if (src == null) return null;
            var result = new grpc.ConfirmPaymentRequest
            {
                Payment = From(src.Payment),
            };
            src.Challenges?.Select(o => From(o))?.ToList()?.ForEach(o => result.Challenges.Add(o));
            return result;
        }

        public static grpc.DataEntryLocation From(payments.DataEntryLocation src)
        {
            switch (src)
            {
                case payments.DataEntryLocation.ON_SCREEN:
                    return grpc.DataEntryLocation.OnScreen;
                case payments.DataEntryLocation.ON_PAPER:
                    return grpc.DataEntryLocation.OnPaper;
                case payments.DataEntryLocation.NONE:
                    return grpc.DataEntryLocation.None;
                default:
                    return grpc.DataEntryLocation.Unknown;
            }
        }

        public static grpc.DataEntryLocation From(payments.DataEntryLocation? src) => From(src.GetValueOrDefault());

        public static grpc.DccInfo From(payments.DCCInfo src)
        {
            if (src == null) return null;
            return new grpc.DccInfo
            {
                InquiryRateId = From(src.inquiryRateId),
                DccApplied = From(src.dccApplied),
                ForeignCurrencyCode = From(src.foreignCurrencyCode),
                ForeignAmount = From(src.foreignAmount),
                ExchangeRate = From(src.exchangeRate),
                MarginRatePercentage = From(src.marginRatePercentage),
                ExchangeRateSourceName = From(src.exchangeRateSourceName),
                ExchangeRateSourceTimeStamp = From(src.ExchangeRateSourceTimeStamp),
                PaymentRef = From(src.paymentRef),
                CreditRef = From(src.creditRef),
            };
        }

        public static grpc.DeviceEvent From(remotepay.CloverDeviceEvent src)
        {
            if (src == null) return null;
            var result = new grpc.DeviceEvent
            {
                Code = From(src.Code),
                EventState = From(src.EventState),
                Message = From(src.Message),
            };
            src.InputOptions?.Select(o => From(o))?.ToList()?.ForEach(o => result.InputOptions.Add(o));
            return result;
        }

        public static grpc.DeviceEventState From(remotepay.CloverDeviceEvent.DeviceEventState src)
        {
            switch (src)
            {
                case remotepay.CloverDeviceEvent.DeviceEventState.START:
                    return grpc.DeviceEventState.Start;
                case remotepay.CloverDeviceEvent.DeviceEventState.FAILED:
                    return grpc.DeviceEventState.Failed;
                case remotepay.CloverDeviceEvent.DeviceEventState.FATAL:
                    return grpc.DeviceEventState.Fatal;
                case remotepay.CloverDeviceEvent.DeviceEventState.TRY_AGAIN:
                    return grpc.DeviceEventState.TryAgain;
                case remotepay.CloverDeviceEvent.DeviceEventState.INPUT_ERROR:
                    return grpc.DeviceEventState.InputError;
                case remotepay.CloverDeviceEvent.DeviceEventState.PIN_BYPASS_CONFIRM:
                    return grpc.DeviceEventState.PinBypassConfirm;
                case remotepay.CloverDeviceEvent.DeviceEventState.CANCELED:
                    return grpc.DeviceEventState.Canceled;
                case remotepay.CloverDeviceEvent.DeviceEventState.TIMED_OUT:
                    return grpc.DeviceEventState.TimedOut;
                case remotepay.CloverDeviceEvent.DeviceEventState.DECLINED:
                    return grpc.DeviceEventState.Declined;
                case remotepay.CloverDeviceEvent.DeviceEventState.VOIDED:
                    return grpc.DeviceEventState.Voided;
                case remotepay.CloverDeviceEvent.DeviceEventState.CONFIGURING:
                    return grpc.DeviceEventState.Configuring;
                case remotepay.CloverDeviceEvent.DeviceEventState.PROCESSING:
                    return grpc.DeviceEventState.Processing;
                case remotepay.CloverDeviceEvent.DeviceEventState.REMOVE_CARD:
                    return grpc.DeviceEventState.RemoveCard;
                case remotepay.CloverDeviceEvent.DeviceEventState.PROCESSING_GO_ONLINE:
                    return grpc.DeviceEventState.ProcessingGoOnline;
                case remotepay.CloverDeviceEvent.DeviceEventState.PROCESSING_CREDIT:
                    return grpc.DeviceEventState.ProcessingCredit;
                case remotepay.CloverDeviceEvent.DeviceEventState.PROCESSING_SWIPE:
                    return grpc.DeviceEventState.ProcessingSwipe;
                case remotepay.CloverDeviceEvent.DeviceEventState.SELECT_APPLICATION:
                    return grpc.DeviceEventState.SelectApplication;
                case remotepay.CloverDeviceEvent.DeviceEventState.PIN_PAD:
                    return grpc.DeviceEventState.PinPad;
                case remotepay.CloverDeviceEvent.DeviceEventState.MANUAL_CARD_NUMBER:
                    return grpc.DeviceEventState.ManualCardNumber;
                case remotepay.CloverDeviceEvent.DeviceEventState.MANUAL_CARD_CVV:
                    return grpc.DeviceEventState.ManualCardCvv;
                case remotepay.CloverDeviceEvent.DeviceEventState.MANUAL_CARD_CVV_UNREADABLE:
                    return grpc.DeviceEventState.ManualCardCvvUnreadable;
                case remotepay.CloverDeviceEvent.DeviceEventState.MANUAL_CARD_EXPIRATION:
                    return grpc.DeviceEventState.ManualCardExpiration;
                case remotepay.CloverDeviceEvent.DeviceEventState.SELECT_ACCOUNT:
                    return grpc.DeviceEventState.SelectAccount;
                case remotepay.CloverDeviceEvent.DeviceEventState.CASHBACK_CONFIRM:
                    return grpc.DeviceEventState.CashbackConfirm;
                case remotepay.CloverDeviceEvent.DeviceEventState.CASHBACK_SELECT:
                    return grpc.DeviceEventState.CashbackSelect;
                case remotepay.CloverDeviceEvent.DeviceEventState.CONTACTLESS_TAP_REQUIRED:
                    return grpc.DeviceEventState.ContactlessTapRequired;
                case remotepay.CloverDeviceEvent.DeviceEventState.VOICE_REFERRAL_RESULT:
                    return grpc.DeviceEventState.VoiceReferralResult;
                case remotepay.CloverDeviceEvent.DeviceEventState.CONFIRM_PARTIAL_AUTH:
                    return grpc.DeviceEventState.ConfirmPartialAuth;
                case remotepay.CloverDeviceEvent.DeviceEventState.PACKET_EXCEPTION:
                    return grpc.DeviceEventState.PacketException;
                case remotepay.CloverDeviceEvent.DeviceEventState.CONFIRM_DUPLICATE_CHECK:
                    return grpc.DeviceEventState.ConfirmDuplicateCheck;
                case remotepay.CloverDeviceEvent.DeviceEventState.VERIFY_SIGNATURE_ON_PAPER:
                    return grpc.DeviceEventState.VerifySignatureOnPaper;
                case remotepay.CloverDeviceEvent.DeviceEventState.VERIFY_SIGNATURE_ON_PAPER_CONFIRM_VOID:
                    return grpc.DeviceEventState.VerifySignatureOnPaperConfirmVoid;
                case remotepay.CloverDeviceEvent.DeviceEventState.VERIFY_SIGNATURE_ON_SCREEN:
                    return grpc.DeviceEventState.VerifySignatureOnScreen;
                case remotepay.CloverDeviceEvent.DeviceEventState.VERIFY_SIGNATURE_ON_SCREEN_CONFIRM_VOID:
                    return grpc.DeviceEventState.VerifySignatureOnScreenConfirmVoid;
                case remotepay.CloverDeviceEvent.DeviceEventState.ADD_SIGNATURE:
                    return grpc.DeviceEventState.AddSignature;
                case remotepay.CloverDeviceEvent.DeviceEventState.SIGNATURE_ON_SCREEN_FALLBACK:
                    return grpc.DeviceEventState.SignatureOnScreenFallback;
                case remotepay.CloverDeviceEvent.DeviceEventState.RETURN_TO_MERCHANT:
                    return grpc.DeviceEventState.ReturnToMerchant;
                case remotepay.CloverDeviceEvent.DeviceEventState.SIGNATURE_REJECT:
                    return grpc.DeviceEventState.SignatureReject;
                case remotepay.CloverDeviceEvent.DeviceEventState.ADD_SIGNATURE_CANCEL_CONFIRM:
                    return grpc.DeviceEventState.AddSignatureCancelConfirm;
                case remotepay.CloverDeviceEvent.DeviceEventState.STARTING_CUSTOM_ACTIVITY:
                    return grpc.DeviceEventState.StartingCustomActivity;
                case remotepay.CloverDeviceEvent.DeviceEventState.CUSTOM_ACTIVITY:
                    return grpc.DeviceEventState.CustomActivity;
                case remotepay.CloverDeviceEvent.DeviceEventState.ADD_TIP:
                    return grpc.DeviceEventState.AddTip;
                case remotepay.CloverDeviceEvent.DeviceEventState.RECEIPT_OPTIONS:
                    return grpc.DeviceEventState.ReceiptOptions;
                case remotepay.CloverDeviceEvent.DeviceEventState.HANDLE_TENDER:
                    return grpc.DeviceEventState.HandleTender;
                case remotepay.CloverDeviceEvent.DeviceEventState.SELECT_WITHDRAW_FROM_ACCOUNT:
                    return grpc.DeviceEventState.SelectWithdrawFromAccount;
                case remotepay.CloverDeviceEvent.DeviceEventState.VERIFY_SURCHARGES:
                    return grpc.DeviceEventState.VerifySurcharges;
                case remotepay.CloverDeviceEvent.DeviceEventState.VOID_CONFIRM:
                    return grpc.DeviceEventState.VoidConfirm;
                case remotepay.CloverDeviceEvent.DeviceEventState.ENTER_PAN_LAST_FOUR:
                    return grpc.DeviceEventState.EnterPanLastFour;
                case remotepay.CloverDeviceEvent.DeviceEventState.ERROR_SCREEN:
                    return grpc.DeviceEventState.ErrorScreen;
                case remotepay.CloverDeviceEvent.DeviceEventState.FISCAL_INVOICE_NUMBER:
                    return grpc.DeviceEventState.FiscalInvoiceNumber;
                case remotepay.CloverDeviceEvent.DeviceEventState.ENTER_INSTALLMENTS:
                    return grpc.DeviceEventState.EnterInstallments;
                case remotepay.CloverDeviceEvent.DeviceEventState.SELECT_INSTALLMENT_PLAN:
                    return grpc.DeviceEventState.SelectInstallmentPlan;
                case remotepay.CloverDeviceEvent.DeviceEventState.ENTER_INSTALLMENT_CODE:
                    return grpc.DeviceEventState.EnterInstallmentCode;
                case remotepay.CloverDeviceEvent.DeviceEventState.PERSONAL_ID_ENTRY:
                    return grpc.DeviceEventState.PersonalIdEntry;
                case remotepay.CloverDeviceEvent.DeviceEventState.PERSONAL_ID_ENTRY_PAS:
                    return grpc.DeviceEventState.PersonalIdEntryPas;
                case remotepay.CloverDeviceEvent.DeviceEventState.SWIPE_CVV_ENTRY:
                    return grpc.DeviceEventState.SwipeCvvEntry;
                case remotepay.CloverDeviceEvent.DeviceEventState.SIGNATURE_CUSTOMER_MODE:
                    return grpc.DeviceEventState.SignatureCustomerMode;
                case remotepay.CloverDeviceEvent.DeviceEventState.MANUAL_ENTRY_FALLBACK:
                    return grpc.DeviceEventState.ManualEntryFallback;
                case remotepay.CloverDeviceEvent.DeviceEventState.SELECT_MULTI_MID:
                    return grpc.DeviceEventState.SelectMultiMid;
                default:
                    return default(grpc.DeviceEventState);
            }
        }

        public static grpc.DeviceInfo From(remotepay.DeviceInfo src)
        {
            if (src == null) return null;
            return new grpc.DeviceInfo
            {
                Name = From(src.Name),
                Serial = From(src.Serial),
                Model = From(src.Model),
                SupportsAcks = From(src.SupportsAcks),
            };
        }

        public static grpc.ExternalDeviceState From(remotepay.ExternalDeviceState src)
        {
            switch (src)
            {
                case remotepay.ExternalDeviceState.UNKNOWN:
                    return grpc.ExternalDeviceState.Unknown;
                case remotepay.ExternalDeviceState.IDLE:
                    return grpc.ExternalDeviceState.Idle;
                case remotepay.ExternalDeviceState.BUSY:
                    return grpc.ExternalDeviceState.Busy;
                case remotepay.ExternalDeviceState.WAITING_FOR_POS:
                    return grpc.ExternalDeviceState.WaitingForPos;
                case remotepay.ExternalDeviceState.WAITING_FOR_CUSTOMER:
                    return grpc.ExternalDeviceState.WaitingForCustomer;
                default:
                    return grpc.ExternalDeviceState.Unknown;
            }
        }

        public static grpc.GermanInfo From(payments.GermanInfo src)
        {
            if (src == null) return null;
            return new grpc.GermanInfo
            {
                CardTrack2 = From(src.cardTrack2),
                CardSequenceNumber = From(src.cardSequenceNumber),
                TransactionCaseGermany = From(src.transactionCaseGermany),
                TransactionTypeGermany = From(src.transactionTypeGermany),
                TerminalId = From(src.terminalID),
                TraceNumber = From(src.traceNumber),
                OldTraceNumber = From(src.oldTraceNumber),
                ReceiptNumber = From(src.receiptNumber),
                TransactionAid = From(src.transactionAID),
                TransactionMsApp = From(src.transactionMSApp),
                TransactionScriptResults = From(src.transactionScriptResults),
                ReceiptType = From(src.receiptType),
                CustomerTransactionDolValues = From(src.customerTransactionDOLValues),
                MerchantTransactionDolValues = From(src.merchantTransactionDOLValues),
                MerchantJournalDol = From(src.merchantJournalDOL),
                MerchantJournalDolValues = From(src.merchantJournalDOLValues),
                ConfigMerchantId = From(src.configMerchantId),
                ConfigProductLabel = From(src.configProductLabel),
                HostResponseAidParBmp53 = From(src.hostResponseAidParBMP53),
                HostResponsePrintDataBm60 = From(src.hostResponsePrintDataBM60),
                SepaElvReceiptFormat = From(src.sepaElvReceiptFormat),
                SepaElvExtAppLabel = From(src.sepaElvExtAppLabel),
                SepaElvPreNotification = From(src.sepaElvPreNotification),
                SepaElvMandate = From(src.sepaElvMandate),
                SepaElvCreditorId = From(src.sepaElvCreditorId),
                SepaElvMandateId = From(src.sepaElvMandateId),
                SepaElvIban = From(src.sepaElvIban),
                PaymentRef = From(src.paymentRef),
                CreditRef = From(src.creditRef),
                RefundRef = From(src.refundRef),
                CreditRefundRef = From(src.creditRefundRef),
            };
        }

        public static grpc.IdentityDocument From(customers.IdentityDocument src)
        {
            if (src == null) return null;
            return new grpc.IdentityDocument
            {
                Number = From(src.Number),
                Type = From(src.Type),
            };
        }

        public static grpc.IdType From(payments.IdType src)
        {
            switch (src)
            {
                case payments.IdType.DRIVERS_LICENSE:
                    return grpc.IdType.DriversLicense;
                case payments.IdType.PASSPORT:
                    return grpc.IdType.Passport;
                default:
                    return grpc.IdType.Unknown;
            }
        }

        public static grpc.InputOption From(transport.InputOption src)
        {
            if (src == null) return null;
            return new grpc.InputOption
            {
                Description = From(src.description),
                KeyPress = From(src.keyPress),
            };
        }

        public static grpc.KeyPress From(transport.KeyPress src)
        {
            switch (src)
            {
                case transport.KeyPress.NONE:
                    return grpc.KeyPress.None;
                case transport.KeyPress.ENTER:
                    return grpc.KeyPress.Enter;
                case transport.KeyPress.ESC:
                    return grpc.KeyPress.Esc;
                case transport.KeyPress.BACKSPACE:
                    return grpc.KeyPress.Backspace;
                case transport.KeyPress.TAB:
                    return grpc.KeyPress.Tab;
                case transport.KeyPress.STAR:
                    return grpc.KeyPress.Star;
                case transport.KeyPress.BUTTON_1:
                    return grpc.KeyPress.Button1;
                case transport.KeyPress.BUTTON_2:
                    return grpc.KeyPress.Button2;
                case transport.KeyPress.BUTTON_3:
                    return grpc.KeyPress.Button3;
                case transport.KeyPress.BUTTON_4:
                    return grpc.KeyPress.Button4;
                case transport.KeyPress.BUTTON_5:
                    return grpc.KeyPress.Button5;
                case transport.KeyPress.BUTTON_6:
                    return grpc.KeyPress.Button6;
                case transport.KeyPress.DIGIT_1:
                    return grpc.KeyPress.Digit1;
                case transport.KeyPress.DIGIT_2:
                    return grpc.KeyPress.Digit2;
                case transport.KeyPress.DIGIT_3:
                    return grpc.KeyPress.Digit3;
                case transport.KeyPress.DIGIT_4:
                    return grpc.KeyPress.Digit4;
                case transport.KeyPress.DIGIT_5:
                    return grpc.KeyPress.Digit5;
                case transport.KeyPress.DIGIT_6:
                    return grpc.KeyPress.Digit6;
                case transport.KeyPress.DIGIT_7:
                    return grpc.KeyPress.Digit7;
                case transport.KeyPress.DIGIT_8:
                    return grpc.KeyPress.Digit8;
                case transport.KeyPress.DIGIT_9:
                    return grpc.KeyPress.Digit9;
                case transport.KeyPress.DIGIT_0:
                    return grpc.KeyPress.Digit0;
                default:
                    return default(grpc.KeyPress);
            }
        }

        public static grpc.LineItemPayment From(payments.LineItemPayment src)
        {
            if (src == null) return null;
            return new grpc.LineItemPayment
            {
                Id = From(src.id),
                LineItemRef = From(src.lineItemRef),
                PaymentRef = From(src.paymentRef),
                Percentage = From(src.percentage),
                BinName = From(src.binName),
                Refunded = From(src.refunded),
            };
        }

        public static grpc.MerchantInfo From(remotepay.MerchantInfo src)
        {
            if (src == null) return null;
            return new grpc.MerchantInfo
            {
                Device = From(src.Device),
                MerchantId = From(src.merchantID),
                MerchantName = From(src.merchantName),
                MerchantMId = From(src.merchantMId),
                SupportsPreAuths = From(src.supportsPreAuths),
                SupportsVaultCards = From(src.supportsVaultCards),
                SupportsManualRefunds = From(src.supportsManualRefunds),
                SupportsTipAdjust = From(src.supportsTipAdjust),
                SupportsRemoteConfirmation = From(src.supportsRemoteConfirmation),
                SupportsNakedCredit = From(src.supportsNakedCredit),
                SupportsMultiPayToken = From(src.supportsMultiPayToken),
                SupportsAcknowledgement = From(src.supportsAcknowledgement),
                SupportsVoidPaymentResponse = From(src.supportsVoidPaymentResponse),
                SupportsPreAuth = From(src.supportsPreAuth),
                SupportsAuth = From(src.supportsAuth),
                SupportsVaultCard = From(src.supportsVaultCard),
            };
        }

        public static grpc.Payment From(payments.Payment src)
        {
            if (src == null) return null;
            var result = new grpc.Payment
            {
                Amount = From(src.amount),
                AppTracking = From(src.appTracking),
                CardTransaction = From(src.cardTransaction),
                CashAdvanceExtra = From(src.cashAdvanceExtra),
                CashbackAmount = From(src.cashbackAmount),
                CashTendered = From(src.cashTendered),
                ClientCreatedTime = From(src.clientCreatedTime),
                CreatedTime = From(src.createdTime),
                DccInfo = From(src.dccInfo),
                Device = From(src.device),
                Employee = From(src.employee),
                ExternalPaymentId = From(src.externalPaymentId),
                ExternalReferenceId = From(src.externalReferenceId),
                GermanInfo = From(src.germanInfo),
                Id = From(src.id),
                Merchant = From(src.merchant),
                ModifiedTime = From(src.modifiedTime),
                Note = From(src.note),
                Offline = From(src.offline),
                Order = From(src.order),
                Result = From(src.result),
                ServiceCharge = From(src.serviceCharge),
                SignatureDisclaimer = From(src.signatureDisclaimer),
                TaxAmount = From(src.taxAmount),
                Tender = From(src.tender),
                TipAmount = From(src.tipAmount),
                TransactionInfo = From(src.transactionInfo),
                TransactionSettings = From(src.transactionSettings),
                VoidReason = From(src.voidReason),
            };
            src.additionalCharges?.Select(o => From(o))?.ToList()?.ForEach(o => result.AdditionalCharges.Add(o));
            src.lineItemPayments?.Select(o => From(o))?.ToList()?.ForEach(o => result.LineItemPayments.Add(o));
            src.refunds?.Select(o => From(o))?.ToList()?.ForEach(o => result.Refunds.Add(o));
            src.taxRates?.Select(o => From(o))?.ToList()?.ForEach(o => result.TaxRates.Add(o));
            return result;
        }

        public static grpc.PaymentResult From(payments.Result src)
        {
            switch (src)
            {
                case payments.Result.SUCCESS:
                    return grpc.PaymentResult.Success;
                case payments.Result.FAIL:
                    return grpc.PaymentResult.Fail;
                case payments.Result.INITIATED:
                    return grpc.PaymentResult.Initiated;
                case payments.Result.VOIDED:
                    return grpc.PaymentResult.Voided;
                case payments.Result.VOIDING:
                    return grpc.PaymentResult.Voiding;
                case payments.Result.AUTH:
                    return grpc.PaymentResult.Auth;
                case payments.Result.AUTH_COMPLETED:
                    return grpc.PaymentResult.AuthCompleted;
                default:
                    return grpc.PaymentResult.Unknown;
            }
        }

        public static grpc.PaymentTaxRate From(payments.PaymentTaxRate src)
        {
            if (src == null) return null;
            return new grpc.PaymentTaxRate
            {
                Id = From(src.id),
                PaymentRef = From(src.paymentRef),
                Name = From(src.name),
                Rate = From(src.rate),
                IsDefault = From(src.isDefault),
                TaxableAmount = From(src.taxableAmount),
            };
        }

        public static grpc.Point From(transport.Signature2.Point src)
        {
            if (src == null) return null;
            return new grpc.Point
            {
                X = From(src.x),
                Y = From(src.y),
            };
        }

        public static grpc.Reference From(base_.Reference src)
        {
            if (src == null) return null;
            return new grpc.Reference
            {
                Id = From(src.id),
            };
        }

        public static grpc.Refund From(payments.Refund src)
        {
            if (src == null) return null;
            var result = new grpc.Refund
            {
                Id = From(src.id),
                OrderRef = From(src.orderRef),
                Device = From(src.device),
                Amount = From(src.amount),
                TaxAmount = From(src.taxAmount),
                CreatedTime = From(src.createdTime),
                ClientCreatedTime = From(src.clientCreatedTime),
                Payment = From(src.payment),
                Employee = From(src.employee),
                OverrideMerchantTender = From(src.overrideMerchantTender),
                ServiceChargeAmount = From(src.serviceChargeAmount),
            };
            src.lineItems?.Select(o => From(o))?.ToList()?.ForEach(o => result.LineItems.Add(o));
            src.taxableAmountRates?.Select(o => From(o))?.ToList()?.ForEach(o => result.TaxableAmountRates.Add(o));
            return result;
        }

        public static grpc.ResetDeviceResponse From(remotepay.ResetDeviceResponse src)
        {
            if (src == null) return null;
            return new grpc.ResetDeviceResponse
            {
                State = From(src.State),
                // BaseResponse
                Message = From(src.Message),
                Reason = From(src.Reason),
                Result = From(src.Result),
                Success = From(src.Success),
            };
        }

        public static grpc.ResponseCode From(remotepay.ResponseCode src)
        {
            switch (src)
            {
                case remotepay.ResponseCode.SUCCESS:
                    return grpc.ResponseCode.Success;
                case remotepay.ResponseCode.FAIL:
                    return grpc.ResponseCode.Fail;
                case remotepay.ResponseCode.UNSUPPORTED:
                    return grpc.ResponseCode.Unsupported;
                case remotepay.ResponseCode.CANCEL:
                    return grpc.ResponseCode.Cancel;
                case remotepay.ResponseCode.ERROR:
                    return grpc.ResponseCode.Error;
                default:
                    return grpc.ResponseCode.Unknown;
            }
        }

        public static grpc.ReversalReason From(payments.ReversalReason src)
        {
            switch (src)
            {
                case payments.ReversalReason.CHIP_DECLINE:
                    return grpc.ReversalReason.ChipDecline;
                case payments.ReversalReason.CARDHOLDER_CANCELLATION:
                    return grpc.ReversalReason.CardholderCancellation;
                case payments.ReversalReason.COMMUNICATION_ERROR:
                    return grpc.ReversalReason.CommunicationError;
                case payments.ReversalReason.OTHER_REASON:
                    return grpc.ReversalReason.OtherReason;
                default:
                    return grpc.ReversalReason.Unknown;
            }
        }

        public static grpc.SaleResponse From(remotepay.SaleResponse src)
        {
            if (src == null) return null;
            return new grpc.SaleResponse
            {
                // PaymentResponse
                Payment = From(src.Payment),
                Signature = From(src.Signature),
                // BaseResponse
                Message = From(src.Message),
                Reason = From(src.Reason),
                Result = From(src.Result),
                Success = From(src.Success),
            };
        }

        public static grpc.SelectedService From(payments.SelectedService src)
        {
            switch (src)
            {
                case payments.SelectedService.NONE:
                    return grpc.SelectedService.None;
                case payments.SelectedService.PAYMENT:
                    return grpc.SelectedService.Payment;
                case payments.SelectedService.REFUND:
                    return grpc.SelectedService.Refund;
                case payments.SelectedService.CANCELLATION:
                    return grpc.SelectedService.Cancellation;
                case payments.SelectedService.PRE_AUTH:
                    return grpc.SelectedService.PreAuth;
                case payments.SelectedService.UPDATE_PRE_AUTH:
                    return grpc.SelectedService.UpdatePreAuth;
                case payments.SelectedService.PAYMENT_COMPLETION:
                    return grpc.SelectedService.PaymentCompletion;
                case payments.SelectedService.CASH_ADVANCE:
                    return grpc.SelectedService.CashAdvance;
                case payments.SelectedService.DEFERRED_PAYMENT:
                    return grpc.SelectedService.DeferredPayment;
                case payments.SelectedService.DEFERRED_PAYMENT_COMPLETION:
                    return grpc.SelectedService.DeferredPaymentCompletion;
                case payments.SelectedService.VOICE_AUTHORISATION:
                    return grpc.SelectedService.VoiceAuthorization;
                case payments.SelectedService.CARDHOLDER_DETECTION:
                    return grpc.SelectedService.CardholderDetection;
                default:
                    return grpc.SelectedService.Unknown;
            }
        }

        public static grpc.ServiceChargeAmount From(payments.ServiceChargeAmount src)
        {
            if (src == null) return null;
            return new grpc.ServiceChargeAmount
            {
                Id = From(src.id),
                Name = From(src.name),
                Amount = From(src.amount),
                PaymentRef = From(src.paymentRef),
            };
        }

        public static grpc.Signature From(transport.Signature2 src)
        {
            if (src == null) return null;
            var result = new grpc.Signature
            {
                Height = From(src.height),
                Width = From(src.width),
            };
            src.strokes?.Select(o => From(o))?.ToList()?.ForEach(o => result.Strokes.Add(o));
            return result;
        }

        public static grpc.SignatureDisclaimer From(payments.SignatureDisclaimer src)
        {
            if (src == null) return null;
            var result = new grpc.SignatureDisclaimer
            {
                DisclaimerText = From(src.disclaimerText),
                PaymentRef = From(src.paymentRef),
            };

            result.DisclaimerValues.Add(src.disclaimerValues);
            return result;
        }

        public static grpc.Stroke From(transport.Signature2.Stroke src)
        {
            if (src == null) return null;
            var result = new grpc.Stroke();
            src.points?.Select(o => From(o))?.ToList()?.ForEach(o => result.Points.Add(o));
            return result;
        }

        public static grpc.TaxableAmountRate From(payments.TaxableAmountRate src)
        {
            if (src == null) return null;
            return new grpc.TaxableAmountRate
            {
                Id = From(src.id),
                Name = From(src.name),
                TaxableAmount = From(src.taxableAmount),
                Rate = From(src.rate),
                TaxAmount = From(src.taxAmount),
                IsVat = From(src.isVat),
            };
        }

        public static grpc.TipMode From(payments.TipMode src)
        {
            switch (src)
            {
                case payments.TipMode.TIP_PROVIDED:
                    return grpc.TipMode.TipProvided;
                case payments.TipMode.ON_SCREEN_BEFORE_PAYMENT:
                    return grpc.TipMode.OnScreenBeforePayment;
                case payments.TipMode.ON_SCREEN_AFTER_PAYMENT:
                    return grpc.TipMode.OnScreenAfterPayment;
                case payments.TipMode.ON_PAPER:
                    return grpc.TipMode.OnPaper;
                case payments.TipMode.NO_TIP:
                    return grpc.TipMode.NoTip;
                default:
                    return grpc.TipMode.Unknown;
            }
        }

        public static grpc.TipMode From(payments.TipMode? src) => From(src.GetValueOrDefault());

        public static grpc.TipSuggestion From(merchant.TipSuggestion src)
        {
            if (src == null) return null;
            return new grpc.TipSuggestion
            {
                Id = From(src.id),
                Name = From(src.name),
                Percentage = From(src.percentage),
                IsEnabled = From(src.isEnabled),
            };
        }

        public static grpc.Tender From(base_.Tender src)
        {
            if (src == null) return null;
            return new grpc.Tender
            {
                Id = From(src.id),
                Editable = From(src.editable),
                LabelKey = From(src.labelKey),
                Label = From(src.label),
                OpensCashDrawer = From(src.opensCashDrawer),
                SupportsTipping = From(src.supportsTipping),
                Enabled = From(src.enabled),
                Visible = From(src.visible),
                Instructions = From(src.instructions),
            };
        }

        public static grpc.TransactionFormat From(payments.TxFormat src)
        {
            switch (src)
            {
                case payments.TxFormat.DEFAULT:
                    return grpc.TransactionFormat.Default;
                case payments.TxFormat.NEXO:
                    return grpc.TransactionFormat.Nexo;
                default:
                    return grpc.TransactionFormat.Unknown;
            }
        }

        public static grpc.TransactionInfo From(payments.TransactionInfo src)
        {
            if (src == null) return null;
            return new grpc.TransactionInfo
            {
                LanguageIndicator = From(src.languageIndicator),
                TransactionLocale = From(src.transactionLocale),
                AccountSelection = From(src.accountSelection),
                PaymentRef = From(src.paymentRef),
                CreditRef = From(src.creditRef),
                RefundRef = From(src.refundRef),
                CreditRefundRef = From(src.creditRefundRef),
                FiscalInvoiceNumber = From(src.fiscalInvoiceNumber),
                InstallmentsQuantity = From(src.installmentsQuantity),
                InstallmentsPlanCode = From(src.installmentsPlanCode),
                InstallmentsPlanId = From(src.installmentsPlanId),
                InstallmentsPlanDesc = From(src.installmentsPlanDesc),
                CardTypeLabel = From(src.cardTypeLabel),
                Stan = From(src.stan),
                IdentityDocument = From(src.identityDocument),
                BatchNumber = From(src.batchNumber),
                ReceiptNumber = From(src.receiptNumber),
                ReversalStan = From(src.reversalStan),
                ReversalMac = From(src.reversalMac),
                ReversalMacKsn = From(src.reversalMacKsn),
                TerminalIdentification = From(src.terminalIdentification),
                MerchantIdentifier = From(src.merchantIdentifier),
                MerchantNameLocation = From(src.merchantNameLocation),
                MaskedTrack2 = From(src.maskedTrack2),
                ReceiptExtraData = From(src.receiptExtraData),
                SelectedService = From(src.selectedService),
                TransactionResult = From(src.transactionResult),
                TransactionTags = From(src.transactionTags),
                TxFormat = From(src.txFormat),
                PanMask = From(src.panMask),
                TransactionSequenceCounter = From(src.transactionSequenceCounter),
                ApplicationPanSequenceNumber = From(src.applicationPanSequenceNumber),
                ReversalReason = From(src.reversalReason),
                IsTokenBasedTx = From(src.isTokenBasedTx),
                OrigTransactionSequenceCounter = From(src.origTransactionSequenceCounter),
                TransactionSequenceCounterUpdate = From(src.transactionSequenceCounterUpdate),
            };
        }

        public static grpc.TransactionResult From(payments.TransactionResult src)
        {
            switch (src)
            {
                case payments.TransactionResult.APPROVED:
                    return grpc.TransactionResult.Approved;
                case payments.TransactionResult.DECLINED:
                    return grpc.TransactionResult.Declined;
                case payments.TransactionResult.ABORTED:
                    return grpc.TransactionResult.Aborted;
                case payments.TransactionResult.VOICE_AUTHORISATION:
                    return grpc.TransactionResult.VoiceAuthorization;
                case payments.TransactionResult.PAYMENT_PART_ONLY:
                    return grpc.TransactionResult.PaymentPartOnly;
                case payments.TransactionResult.PARTIALLY_APPROVED:
                    return grpc.TransactionResult.PartiallyApproved;
                case payments.TransactionResult.NONE:
                    return grpc.TransactionResult.None;
                default:
                    return grpc.TransactionResult.Unknown;
            }
        }

        public static grpc.TransactionSettings From(payments.TransactionSettings src)
        {
            if (src == null) return null;
            var result = new grpc.TransactionSettings
            {
                CardEntryMethods = From(src.cardEntryMethods),
                DisableCashBack = From(src.disableCashBack),
                CloverShouldHandleReceipts = From(src.cloverShouldHandleReceipts),
                ForcePinEntryOnSwipe = From(src.forcePinEntryOnSwipe),
                DisableRestartTransactionOnFailure = From(src.disableRestartTransactionOnFailure),
                AllowOfflinePayment = From(src.allowOfflinePayment),
                ApproveOfflinePaymentWithoutPrompt = From(src.approveOfflinePaymentWithoutPrompt),
                SignatureThreshold = From(src.signatureThreshold),
                SignatureEntryLocation = From(src.signatureEntryLocation),
                TipMode = From(src.tipMode),
                TippableAmount = From(src.tippableAmount),
                DisableReceiptSelection = From(src.disableReceiptSelection),
                DisableDuplicateCheck = From(src.disableDuplicateCheck),
                AutoAcceptPaymentConfirmations = From(src.autoAcceptPaymentConfirmations),
                AutoAcceptSignature = From(src.autoAcceptSignature),
                ForceOfflinePayment = From(src.forceOfflinePayment),
            };
            src.tipSuggestions?.Select(o => From(o))?.ToList()?.ForEach(o => result.TipSuggestions.Add(o));
            src.regionalExtras?.ToList()?.ForEach(kvp => result.RegionalExtras.Add(kvp.Key, kvp.Value));
            return result;
        }

        public static grpc.VaultedCard From(payments.VaultedCard src)
        {
            if (src == null) return null;
            return new grpc.VaultedCard
            {
                First6 = From(src.first6),
                Last4 = From(src.last4),
                CardholderName = From(src.cardholderName),
                ExpirationDate = From(src.expirationDate),
                Token = From(src.token),
            };
        }

        public static grpc.VerifySignatureRequest From(remotepay.VerifySignatureRequest src)
        {
            if (src == null) return null;
            return new grpc.VerifySignatureRequest
            {
                Payment = From(src.Payment),
                Signature = From(src.Signature),
            };
        }

        public static grpc.VoidReason From(order.VoidReason src)
        {
            switch (src)
            {
                case order.VoidReason.USER_CANCEL:
                    return grpc.VoidReason.UserCancel;
                case order.VoidReason.TRANSPORT_ERROR:
                    return grpc.VoidReason.TransportError;
                case order.VoidReason.REJECT_SIGNATURE:
                    return grpc.VoidReason.RejectSignature;
                case order.VoidReason.REJECT_PARTIAL_AUTH:
                    return grpc.VoidReason.RejectPartialAuth;
                case order.VoidReason.NOT_APPROVED:
                    return grpc.VoidReason.NotApproved;
                case order.VoidReason.FAILED:
                    return grpc.VoidReason.Failed;
                case order.VoidReason.AUTH_CLOSED_NEW_CARD:
                    return grpc.VoidReason.AuthClosedNewCard;
                case order.VoidReason.DEVELOPER_PAY_PARTIAL_AUTH:
                    return grpc.VoidReason.DeveloperPayPartialAuth;
                case order.VoidReason.REJECT_DUPLICATE:
                    return grpc.VoidReason.RejectDuplicate;
                case order.VoidReason.REJECT_OFFLINE:
                    return grpc.VoidReason.RejectOffline;
                default:
                    return grpc.VoidReason.Unknown;
            }
        }
    }
}
