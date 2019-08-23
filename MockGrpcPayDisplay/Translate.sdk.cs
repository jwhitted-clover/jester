using System;
using System.Linq;
using System.Reflection;
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
        public static payments.AccountType From(grpc.AccountType src)
        {
            switch (src)
            {
                case grpc.AccountType.Credit:
                    return payments.AccountType.CREDIT;
                case grpc.AccountType.Debit:
                    return payments.AccountType.DEBIT;
                case grpc.AccountType.Checking:
                    return payments.AccountType.CHECKING;
                case grpc.AccountType.Savings:
                    return payments.AccountType.SAVINGS;
                default:
                    return default(payments.AccountType);
            }
        }

        public static payments.AdditionalChargeAmount From(grpc.AdditionalChargeAmount src)
        {
            if (src == null) return null;
            return new payments.AdditionalChargeAmount
            {
                amount = From(src.Amount),
                id = From(src.Id),
                type = From(src.Type),
            };
        }

        public static payments.AdditionalChargeType From(grpc.AdditionalChargeType src)
        {
            switch (src)
            {
                case grpc.AdditionalChargeType.Interac:
                    return payments.AdditionalChargeType.INTERAC;
                default:
                    return default(payments.AdditionalChargeType);
            }
        }

        public static payments.AppTracking From(grpc.AppTracking src)
        {
            if (src == null) return null;
            return new payments.AppTracking
            {
                applicationID = From(src.ApplicationId),
                applicationName = From(src.ApplicationId),
                applicationVersion = From(src.ApplicationVersion),
                creditRef = From(src.CreditRef),
                creditRefundRef = From(src.CreditRefundRef),
                developerAppId = From(src.DeveloperAppId),
                paymentRef = From(src.PaymentRef),
                refundRef = From(src.RefundRef),
                sourceSDK = From(src.SourceSdk),
                sourceSDKVersion = From(src.SourceSdkVersion),
            };
        }

        public static payments.AVSResult From(grpc.AvsResult src)
        {
            switch (src)
            {
                case grpc.AvsResult.Success:
                    return payments.AVSResult.SUCCESS;
                case grpc.AvsResult.ZipCodeMatch:
                    return payments.AVSResult.ZIP_CODE_MATCH;
                case grpc.AvsResult.ZipCodeMatchAddressNotChecked:
                    return payments.AVSResult.ZIP_CODE_MATCH_ADDRESS_NOT_CHECKED;
                case grpc.AvsResult.AddressMatch:
                    return payments.AVSResult.ADDRESS_MATCH;
                case grpc.AvsResult.AddressMatchZipNotChecked:
                    return payments.AVSResult.ADDRESS_MATCH_ZIP_NOT_CHECKED;
                case grpc.AvsResult.NeitherMatch:
                    return payments.AVSResult.NEITHER_MATCH;
                case grpc.AvsResult.ServiceFailure:
                    return payments.AVSResult.SERVICE_FAILURE;
                case grpc.AvsResult.ServiceUnavailable:
                    return payments.AVSResult.SERVICE_UNAVAILABLE;
                case grpc.AvsResult.NotChecked:
                    return payments.AVSResult.NOT_CHECKED;
                case grpc.AvsResult.ZipCodeNotMatchedAddressNotChecked:
                    return payments.AVSResult.ZIP_CODE_NOT_MATCHED_ADDRESS_NOT_CHECKED;
                case grpc.AvsResult.AddressNotMatchedZipCodeNotChecked:
                    return payments.AVSResult.ADDRESS_NOT_MATCHED_ZIP_CODE_NOT_CHECKED;
                default:
                    return default(payments.AVSResult);
            }
        }

        public static payments.CardEntryType From(grpc.CardEntryType src)
        {
            switch (src)
            {
                case grpc.CardEntryType.Swiped:
                    return payments.CardEntryType.SWIPED;
                case grpc.CardEntryType.Keyed:
                    return payments.CardEntryType.KEYED;
                case grpc.CardEntryType.Voice:
                    return payments.CardEntryType.VOICE;
                case grpc.CardEntryType.Vaulted:
                    return payments.CardEntryType.VAULTED;
                case grpc.CardEntryType.OfflineSwiped:
                    return payments.CardEntryType.OFFLINE_SWIPED;
                case grpc.CardEntryType.OfflineKeyed:
                    return payments.CardEntryType.OFFLINE_KEYED;
                case grpc.CardEntryType.EmvContact:
                    return payments.CardEntryType.EMV_CONTACT;
                case grpc.CardEntryType.EmvContactless:
                    return payments.CardEntryType.EMV_CONTACTLESS;
                case grpc.CardEntryType.MsdContactless:
                    return payments.CardEntryType.MSD_CONTACTLESS;
                case grpc.CardEntryType.PinpadManualEntry:
                    return payments.CardEntryType.PINPAD_MANUAL_ENTRY;
                default:
                    return default(payments.CardEntryType);
            }
        }

        public static payments.CardTransaction From(grpc.CardTransaction src)
        {
            if (src == null) return null;
            var result = new payments.CardTransaction
            {
                paymentRef = From(src.PaymentRef),
                creditRef = From(src.CreditRef),
                cardType = From(src.CardType),
                entryType = From(src.EntryType),
                first6 = From(src.First6),
                last4 = From(src.Last4),
                type = From(src.Type),
                authCode = From(src.AuthCode),
                referenceId = From(src.ReferenceId),
                transactionNo = From(src.TransactionNo),
                state = From(src.State),
                begBalance = From(src.BegBalance),
                endBalance = From(src.EndBalance),
                avsResult = From(src.AvsResult),
                cardholderName = From(src.CardholderName),
                token = From(src.Token),
                vaultedCard = From(src.VaultedCard),
            };
            result.extra = src.Extra.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return result;
        }

        public static payments.CardTransactionState From(grpc.CardTransactionState src)
        {
            switch (src)
            {
                case grpc.CardTransactionState.Pending:
                    return payments.CardTransactionState.PENDING;
                case grpc.CardTransactionState.Closed:
                    return payments.CardTransactionState.CLOSED;
                default:
                    return default(payments.CardTransactionState);
            }
        }

        public static payments.CardTransactionType From(grpc.CardTransactionType src)
        {
            switch (src)
            {
                case grpc.CardTransactionType.Auth:
                    return payments.CardTransactionType.AUTH;
                case grpc.CardTransactionType.PreAuth:
                    return payments.CardTransactionType.PREAUTH;
                case grpc.CardTransactionType.PreAuthCapture:
                    return payments.CardTransactionType.PREAUTHCAPTURE;
                case grpc.CardTransactionType.Adjust:
                    return payments.CardTransactionType.ADJUST;
                case grpc.CardTransactionType.Void:
                    return payments.CardTransactionType.VOID;
                case grpc.CardTransactionType.VoidReturn:
                    return payments.CardTransactionType.VOIDRETURN;
                case grpc.CardTransactionType.Return:
                    return payments.CardTransactionType.RETURN;
                case grpc.CardTransactionType.Refund:
                    return payments.CardTransactionType.REFUND;
                case grpc.CardTransactionType.NakedRefund:
                    return payments.CardTransactionType.NAKEDREFUND;
                case grpc.CardTransactionType.GetBalance:
                    return payments.CardTransactionType.GETBALANCE;
                case grpc.CardTransactionType.BatchClose:
                    return payments.CardTransactionType.BATCHCLOSE;
                case grpc.CardTransactionType.Activate:
                    return payments.CardTransactionType.ACTIVATE;
                case grpc.CardTransactionType.BalanceLock:
                    return payments.CardTransactionType.BALANCE_LOCK;
                case grpc.CardTransactionType.Load:
                    return payments.CardTransactionType.LOAD;
                case grpc.CardTransactionType.Cashout:
                    return payments.CardTransactionType.CASHOUT;
                case grpc.CardTransactionType.CashoutActiveStatus:
                    return payments.CardTransactionType.CASHOUT_ACTIVE_STATUS;
                case grpc.CardTransactionType.Redemption:
                    return payments.CardTransactionType.REDEMPTION;
                case grpc.CardTransactionType.RedemptionUnlock:
                    return payments.CardTransactionType.REDEMPTION_UNLOCK;
                case grpc.CardTransactionType.Reload:
                    return payments.CardTransactionType.RELOAD;
                default:
                    return default(payments.CardTransactionType);
            }
        }

        public static payments.CardType From(grpc.CardType src)
        {
            switch (src)
            {
                case grpc.CardType.Visa:
                    return payments.CardType.VISA;
                case grpc.CardType.Mc:
                    return payments.CardType.MC;
                case grpc.CardType.Amex:
                    return payments.CardType.AMEX;
                case grpc.CardType.Discover:
                    return payments.CardType.DISCOVER;
                case grpc.CardType.DinersClub:
                    return payments.CardType.DINERS_CLUB;
                case grpc.CardType.Jcb:
                    return payments.CardType.JCB;
                case grpc.CardType.Maestro:
                    return payments.CardType.MAESTRO;
                case grpc.CardType.Solo:
                    return payments.CardType.SOLO;
                case grpc.CardType.Laser:
                    return payments.CardType.LASER;
                case grpc.CardType.ChinaUnionPay:
                    return payments.CardType.CHINA_UNION_PAY;
                case grpc.CardType.CarteBlanche:
                    return payments.CardType.CARTE_BLANCHE;
                case grpc.CardType.Unknown:
                    return payments.CardType.UNKNOWN;
                case grpc.CardType.GiftCard:
                    return payments.CardType.GIFT_CARD;
                case grpc.CardType.Ebt:
                    return payments.CardType.EBT;
                case grpc.CardType.Interac:
                    return payments.CardType.INTERAC;
                case grpc.CardType.Other:
                    return payments.CardType.OTHER;
                default:
                    return default(payments.CardType);
            }
        }

        public static payments.CashAdvanceCustomerIdentification From(grpc.CashAdvanceCustomerIdentification src)
        {
            if (src == null) return null;
            return new payments.CashAdvanceCustomerIdentification
            {
                idType = From(src.IdType),
                serialNumber = From(src.SerialNumber),
                maskedSerialNumber = From(src.MaskedSerialNumber),
                encryptedSerialNumber = From(src.EncryptedSerialNumber),
                expirationDate = From(src.ExpirationDate),
                issuingState = From(src.IssuingState),
                issuingCountry = From(src.IssuingCountry),
                customerName = From(src.CustomerName),
                addressStreet1 = From(src.AddressStreet1),
                addressStreet2 = From(src.AddressStreet2),
                addressCity = From(src.AddressCity),
                addressState = From(src.AddressState),
                addressZipCode = From(src.AddressZipCode),
                addressCountry = From(src.AddressCountry),
            };
        }

        public static payments.CashAdvanceExtra From(grpc.CashAdvanceExtra src)
        {
            if (src == null) return null;
            return new payments.CashAdvanceExtra
            {
                cashAdvanceCustomerIdentification = From(src.CashAdvanceCustomerIdentification),
                cashAdvanceSerialNum = From(src.CashAdvanceSerialNum),
                paymentRef = From(src.PaymentRef),
            };
        }

        public static transport.Challenge From(grpc.Challenge src)
        {
            if (src == null) return null;
            return new transport.Challenge
            {
                message = From(src.Message),
                type = From(src.Type),
                reason = From(src.Reason),
            };
        }

        public static transport.ChallengeType From(grpc.ChallengeType src)
        {
            switch (src)
            {
                case grpc.ChallengeType.DuplicateChallenge:
                    return transport.ChallengeType.DUPLICATE_CHALLENGE;
                case grpc.ChallengeType.OfflineChallenge:
                    return transport.ChallengeType.OFFLINE_CHALLENGE;
                default:
                    return default(transport.ChallengeType);
            }
        }

        public static remotepay.ConfirmPaymentRequest From(grpc.ConfirmPaymentRequest src)
        {
            if (src == null) return null;
            var result = new remotepay.ConfirmPaymentRequest
            {
                Payment = From(src.Payment),
            };
            result.Challenges = src.Challenges.Select(o => From(o)).ToList();
            return result;
        }

        public static payments.DataEntryLocation From(grpc.DataEntryLocation src)
        {
            switch (src)
            {
                case grpc.DataEntryLocation.OnScreen:
                    return payments.DataEntryLocation.ON_SCREEN;
                case grpc.DataEntryLocation.OnPaper:
                    return payments.DataEntryLocation.ON_PAPER;
                case grpc.DataEntryLocation.None:
                    return payments.DataEntryLocation.NONE;
                default:
                    return default(payments.DataEntryLocation);
            }
        }

        public static payments.DataEntryLocation From(grpc.DataEntryLocation? src) => From(src.GetValueOrDefault());

        public static payments.DCCInfo From(grpc.DccInfo src)
        {
            if (src == null) return null;
            return new payments.DCCInfo
            {
                inquiryRateId = From(src.InquiryRateId),
                dccApplied = From(src.DccApplied),
                foreignCurrencyCode = From(src.ForeignCurrencyCode),
                foreignAmount = From(src.ForeignAmount),
                exchangeRate = From(src.ExchangeRate),
                marginRatePercentage = From(src.MarginRatePercentage),
                exchangeRateSourceName = From(src.ExchangeRateSourceName),
                ExchangeRateSourceTimeStamp = From(src.ExchangeRateSourceTimeStamp),
                paymentRef = From(src.PaymentRef),
                creditRef = From(src.CreditRef),
            };
        }

        public static remotepay.DeviceInfo From(grpc.DeviceInfo src)
        {
            if (src == null) return null;
            return new remotepay.DeviceInfo
            {
                Name = From(src.Name),
                Serial = From(src.Serial),
                Model = From(src.Model),
                SupportsAcks = From(src.SupportsAcks),
            };
        }

        public static payments.GermanInfo From(grpc.GermanInfo src)
        {
            if (src == null) return null;
            return new payments.GermanInfo
            {
                cardTrack2 = From(src.CardTrack2),
                cardSequenceNumber = From(src.CardSequenceNumber),
                transactionCaseGermany = From(src.TransactionCaseGermany),
                transactionTypeGermany = From(src.TransactionTypeGermany),
                terminalID = From(src.TerminalId),
                traceNumber = From(src.TraceNumber),
                oldTraceNumber = From(src.OldTraceNumber),
                receiptNumber = From(src.ReceiptNumber),
                transactionAID = From(src.TransactionAid),
                transactionMSApp = From(src.TransactionMsApp),
                transactionScriptResults = From(src.TransactionScriptResults),
                receiptType = From(src.ReceiptType),
                customerTransactionDOLValues = From(src.CustomerTransactionDolValues),
                merchantTransactionDOLValues = From(src.MerchantTransactionDolValues),
                merchantJournalDOL = From(src.MerchantJournalDol),
                merchantJournalDOLValues = From(src.MerchantJournalDolValues),
                configMerchantId = From(src.ConfigMerchantId),
                configProductLabel = From(src.ConfigProductLabel),
                hostResponseAidParBMP53 = From(src.HostResponseAidParBmp53),
                hostResponsePrintDataBM60 = From(src.HostResponsePrintDataBm60),
                sepaElvReceiptFormat = From(src.SepaElvReceiptFormat),
                sepaElvExtAppLabel = From(src.SepaElvExtAppLabel),
                sepaElvPreNotification = From(src.SepaElvPreNotification),
                sepaElvMandate = From(src.SepaElvMandate),
                sepaElvCreditorId = From(src.SepaElvCreditorId),
                sepaElvMandateId = From(src.SepaElvMandateId),
                sepaElvIban = From(src.SepaElvIban),
                paymentRef = From(src.PaymentRef),
                creditRef = From(src.CreditRef),
                refundRef = From(src.RefundRef),
                creditRefundRef = From(src.CreditRefundRef),
            };
        }

        public static customers.IdentityDocument From(grpc.IdentityDocument src)
        {
            if (src == null) return null;
            return new customers.IdentityDocument
            {
                Number = From(src.Number),
                Type = From(src.Type),
            };
        }

        public static payments.IdType From(grpc.IdType src)
        {
            switch (src)
            {
                case grpc.IdType.DriversLicense:
                    return payments.IdType.DRIVERS_LICENSE;
                case grpc.IdType.Passport:
                    return payments.IdType.PASSPORT;
                default:
                    return default(payments.IdType);
            }
        }

        public static transport.InputOption From(grpc.InputOption src)
        {
            if (src == null) return null;
            return new transport.InputOption
            {
                description = From(src.Description),
                keyPress = From(src.KeyPress),
            };
        }

        public static transport.KeyPress From(grpc.KeyPress src)
        {
            switch (src)
            {
                case grpc.KeyPress.None:
                    return transport.KeyPress.NONE;
                case grpc.KeyPress.Enter:
                    return transport.KeyPress.ENTER;
                case grpc.KeyPress.Esc:
                    return transport.KeyPress.ESC;
                case grpc.KeyPress.Backspace:
                    return transport.KeyPress.BACKSPACE;
                case grpc.KeyPress.Tab:
                    return transport.KeyPress.TAB;
                case grpc.KeyPress.Star:
                    return transport.KeyPress.STAR;
                case grpc.KeyPress.Button1:
                    return transport.KeyPress.BUTTON_1;
                case grpc.KeyPress.Button2:
                    return transport.KeyPress.BUTTON_2;
                case grpc.KeyPress.Button3:
                    return transport.KeyPress.BUTTON_3;
                case grpc.KeyPress.Button4:
                    return transport.KeyPress.BUTTON_4;
                case grpc.KeyPress.Button5:
                    return transport.KeyPress.BUTTON_5;
                case grpc.KeyPress.Button6:
                    return transport.KeyPress.BUTTON_6;
                case grpc.KeyPress.Digit1:
                    return transport.KeyPress.DIGIT_1;
                case grpc.KeyPress.Digit2:
                    return transport.KeyPress.DIGIT_2;
                case grpc.KeyPress.Digit3:
                    return transport.KeyPress.DIGIT_3;
                case grpc.KeyPress.Digit4:
                    return transport.KeyPress.DIGIT_4;
                case grpc.KeyPress.Digit5:
                    return transport.KeyPress.DIGIT_5;
                case grpc.KeyPress.Digit6:
                    return transport.KeyPress.DIGIT_6;
                case grpc.KeyPress.Digit7:
                    return transport.KeyPress.DIGIT_7;
                case grpc.KeyPress.Digit8:
                    return transport.KeyPress.DIGIT_8;
                case grpc.KeyPress.Digit9:
                    return transport.KeyPress.DIGIT_9;
                case grpc.KeyPress.Digit0:
                    return transport.KeyPress.DIGIT_0;
                default:
                    return default(transport.KeyPress);
            }
        }

        public static payments.LineItemPayment From(grpc.LineItemPayment src)
        {
            if (src == null) return null;
            return new payments.LineItemPayment
            {
                id = From(src.Id),
                lineItemRef = From(src.LineItemRef),
                paymentRef = From(src.PaymentRef),
                percentage = From(src.Percentage),
                binName = From(src.BinName),
                refunded = From(src.Refunded),
            };
        }

        public static remotepay.MerchantInfo From(grpc.MerchantInfo src)
        {
            if (src == null) return null;
            return new remotepay.MerchantInfo
            {
                Device = From(src.Device),
                merchantID = From(src.MerchantId),
                merchantName = From(src.MerchantName),
                merchantMId = From(src.MerchantMId),
                supportsPreAuths = From(src.SupportsPreAuths),
                supportsVaultCards = From(src.SupportsVaultCards),
                supportsManualRefunds = From(src.SupportsManualRefunds),
                supportsTipAdjust = From(src.SupportsTipAdjust),
                supportsRemoteConfirmation = From(src.SupportsRemoteConfirmation),
                supportsNakedCredit = From(src.SupportsNakedCredit),
                supportsMultiPayToken = From(src.SupportsMultiPayToken),
                supportsAcknowledgement = From(src.SupportsAcknowledgement),
                supportsVoidPaymentResponse = From(src.SupportsVoidPaymentResponse),
                supportsPreAuth = From(src.SupportsPreAuth),
                supportsAuth = From(src.SupportsAuth),
                supportsVaultCard = From(src.SupportsVaultCard),
            };
        }

        public static payments.Payment From(grpc.Payment src)
        {
            if (src == null) return null;
            var result = new payments.Payment
            {
                amount = From(src.Amount),
                appTracking = From(src.AppTracking),
                cardTransaction = From(src.CardTransaction),
                cashAdvanceExtra = From(src.CashAdvanceExtra),
                cashbackAmount = From(src.CashbackAmount),
                cashTendered = From(src.CashTendered),
                clientCreatedTime = From(src.ClientCreatedTime),
                createdTime = From(src.CreatedTime),
                dccInfo = From(src.DccInfo),
                device = From(src.Device),
                employee = From(src.Employee),
                externalPaymentId = From(src.ExternalPaymentId),
                externalReferenceId = From(src.ExternalReferenceId),
                germanInfo = From(src.GermanInfo),
                id = From(src.Id),
                merchant = From(src.Merchant),
                modifiedTime = From(src.ModifiedTime),
                note = From(src.Note),
                offline = From(src.Offline),
                order = From(src.Order),
                result = From(src.Result),
                serviceCharge = From(src.ServiceCharge),
                signatureDisclaimer = From(src.SignatureDisclaimer),
                taxAmount = From(src.TaxAmount),
                tender = From(src.Tender),
                tipAmount = From(src.TipAmount),
                transactionInfo = From(src.TransactionInfo),
                transactionSettings = From(src.TransactionSettings),
                voidReason = From(src.VoidReason),
            };
            result.additionalCharges = src.AdditionalCharges.Select(o => From(o)).ToList();
            result.lineItemPayments = src.LineItemPayments.Select(o => From(o)).ToList();
            result.refunds = src.Refunds.Select(o => From(o)).ToList();
            result.taxRates = src.TaxRates.Select(o => From(o)).ToList();
            return result;
        }

        public static payments.Result From(grpc.PaymentResult src)
        {
            switch (src)
            {
                case grpc.PaymentResult.Success:
                    return payments.Result.SUCCESS;
                case grpc.PaymentResult.Fail:
                    return payments.Result.FAIL;
                case grpc.PaymentResult.Initiated:
                    return payments.Result.INITIATED;
                case grpc.PaymentResult.Voided:
                    return payments.Result.VOIDED;
                case grpc.PaymentResult.Voiding:
                    return payments.Result.VOIDING;
                case grpc.PaymentResult.Auth:
                    return payments.Result.AUTH;
                case grpc.PaymentResult.AuthCompleted:
                    return payments.Result.AUTH_COMPLETED;
                default:
                    return default(payments.Result);
            }
        }

        public static payments.PaymentTaxRate From(grpc.PaymentTaxRate src)
        {
            if (src == null) return null;
            return new payments.PaymentTaxRate
            {
                id = From(src.Id),
                paymentRef = From(src.PaymentRef),
                name = From(src.Name),
                rate = From(src.Rate),
                isDefault = From(src.IsDefault),
                taxableAmount = From(src.TaxableAmount),
            };
        }

        public static transport.Signature2.Point From(grpc.Point src)
        {
            if (src == null) return null;
            return new transport.Signature2.Point
            {
                x = From(src.X),
                y = From(src.Y),
            };
        }

        public static base_.Reference From(grpc.Reference src)
        {
            if (src == null) return null;
            return new base_.Reference
            {
                id = From(src.Id),
            };
        }

        public static payments.Refund From(grpc.Refund src)
        {
            if (src == null) return null;
            var result = new payments.Refund
            {
                id = From(src.Id),
                orderRef = From(src.OrderRef),
                device = From(src.Device),
                amount = From(src.Amount),
                taxAmount = From(src.TaxAmount),
                createdTime = From(src.CreatedTime),
                clientCreatedTime = From(src.ClientCreatedTime),
                payment = From(src.Payment),
                employee = From(src.Employee),
                overrideMerchantTender = From(src.OverrideMerchantTender),
                serviceChargeAmount = From(src.ServiceChargeAmount),
            };
            result.lineItems = src.LineItems.Select(o => From(o)).ToList();
            result.taxableAmountRates = src.TaxableAmountRates.Select(o => From(o)).ToList();
            return result;
        }

        public static remotepay.ResponseCode From(grpc.ResponseCode src)
        {
            switch (src)
            {
                case grpc.ResponseCode.Success:
                    return remotepay.ResponseCode.SUCCESS;
                case grpc.ResponseCode.Fail:
                    return remotepay.ResponseCode.FAIL;
                case grpc.ResponseCode.Unsupported:
                    return remotepay.ResponseCode.UNSUPPORTED;
                case grpc.ResponseCode.Cancel:
                    return remotepay.ResponseCode.CANCEL;
                case grpc.ResponseCode.Error:
                    return remotepay.ResponseCode.ERROR;
                default:
                    return default(remotepay.ResponseCode);
            }
        }

        public static payments.ReversalReason From(grpc.ReversalReason src)
        {
            switch (src)
            {
                case grpc.ReversalReason.ChipDecline:
                    return payments.ReversalReason.CHIP_DECLINE;
                case grpc.ReversalReason.CardholderCancellation:
                    return payments.ReversalReason.CARDHOLDER_CANCELLATION;
                case grpc.ReversalReason.CommunicationError:
                    return payments.ReversalReason.COMMUNICATION_ERROR;
                case grpc.ReversalReason.OtherReason:
                    return payments.ReversalReason.OTHER_REASON;
                default:
                    return default(payments.ReversalReason);
            }
        }

        public static remotepay.SaleRequest From(grpc.SaleRequest src)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(src);
            var attempt = Newtonsoft.Json.JsonConvert.DeserializeObject<remotepay.SaleRequest>(json);

            if (src == null) return null;
            var result = new remotepay.SaleRequest
            {
                DisableCashback = From(src.DisableCashback),
                TaxAmount = From(src.TaxAmount),
                TippableAmount = From(src.TipAmount),
                AllowOfflinePayment = From(src.AllowOfflinePayment),
                ApproveOfflinePaymentWithoutPrompt = From(src.ApproveOfflinePaymentWithoutPrompt),
                TipAmount = From(src.TipAmount),
                TipMode = From<remotepay.TipMode>(src.TipMode),
                ForceOfflinePayment = From(src.ForceOfflinePayment),
                // TransactionRequest
                SignatureThreshold = From(src.Base.SignatureThreshold),
                SignatureEntryLocation = From(src.Base.SignatureEntryLocation),
                AutoAcceptSignature = From(src.Base.AutoAcceptSignature),
                // BaseTransactionRequest
                DisablePrinting = From(src.Base.Base.DisablePrinting),
                CardNotPresent = From(src.Base.Base.CardNotPresent),
                DisableRestartTransactionOnFail = From(src.Base.Base.DisableRestartTransactionOnFail),
                Amount = From(src.Base.Base.Amount),
                CardEntryMethods = From<int?>(src.Base.Base.CardEntryMethods),
                VaultedCard = From(src.Base.Base.VaultedCard),
                ExternalId = From(src.Base.Base.ExternalId),
                Type = From(src.Base.Base.Type),
                DisableDuplicateChecking = From(src.Base.Base.DisableDuplicateChecking),
                DisableReceiptSelection = From(src.Base.Base.DisableReceiptSelection),
                AutoAcceptPaymentConfirmations = From(src.Base.Base.AutoAcceptPaymentConfirmations),
            };
            result.TipSuggestions = src.Base.TipSuggestions.Select(o => From(o)).ToList();
            src.Base.Base.Extras.ToList().ForEach(kvp => result.Extras.Add(kvp.Key, kvp.Value));
            src.Base.Base.RegionalExtras.ToList().ForEach(kvp => result.RegionalExtras.Add(kvp.Key, kvp.Value));
            // HACK: BaseRequest.RequestId is protected (and doesn't look like it is used anywhere?!)
            result.GetType().GetProperty("RequestId", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(result, src.Base?.Base?.Base?.RequestId);
            return result;
        }

        public static remotepay.SaleResponse From(grpc.SaleResponse src)
        {
            if (src == null) return null;
            return new remotepay.SaleResponse
            {
                // PaymentResponse
                Payment = From(src.Base.Payment),
                Signature = From(src.Base.Signature),
                // BaseResponse
                Message = From(src.Base.Base.Message),
                Reason = From(src.Base.Base.Reason),
                Result = From(src.Base.Base.Result),
                Success = From(src.Base.Base.Success),
            };
        }

        public static payments.SelectedService From(grpc.SelectedService src)
        {
            switch (src)
            {
                case grpc.SelectedService.None:
                    return payments.SelectedService.NONE;
                case grpc.SelectedService.Payment:
                    return payments.SelectedService.PAYMENT;
                case grpc.SelectedService.Refund:
                    return payments.SelectedService.REFUND;
                case grpc.SelectedService.Cancellation:
                    return payments.SelectedService.CANCELLATION;
                case grpc.SelectedService.PreAuth:
                    return payments.SelectedService.PRE_AUTH;
                case grpc.SelectedService.UpdatePreAuth:
                    return payments.SelectedService.UPDATE_PRE_AUTH;
                case grpc.SelectedService.PaymentCompletion:
                    return payments.SelectedService.PAYMENT_COMPLETION;
                case grpc.SelectedService.CashAdvance:
                    return payments.SelectedService.CASH_ADVANCE;
                case grpc.SelectedService.DeferredPayment:
                    return payments.SelectedService.DEFERRED_PAYMENT;
                case grpc.SelectedService.DeferredPaymentCompletion:
                    return payments.SelectedService.DEFERRED_PAYMENT_COMPLETION;
                case grpc.SelectedService.VoiceAuthorization:
                    return payments.SelectedService.VOICE_AUTHORISATION;
                case grpc.SelectedService.CardholderDetection:
                    return payments.SelectedService.CARDHOLDER_DETECTION;
                default:
                    return default(payments.SelectedService);
            }
        }

        public static payments.ServiceChargeAmount From(grpc.ServiceChargeAmount src)
        {
            if (src == null) return null;
            return new payments.ServiceChargeAmount
            {
                id = From(src.Id),
                name = From(src.Name),
                amount = From(src.Amount),
                paymentRef = From(src.PaymentRef),
            };
        }

        public static transport.Signature2 From(grpc.Signature src)
        {
            if (src == null) return null;
            var result = new transport.Signature2
            {
                height = From(src.Height),
                width = From(src.Width),
            };
            result.strokes = src.Strokes.Select(o => From(o)).ToList();
            return result;
        }

        public static payments.SignatureDisclaimer From(grpc.SignatureDisclaimer src)
        {
            if (src == null) return null;
            var result = new payments.SignatureDisclaimer
            {
                disclaimerText = From(src.DisclaimerText),
                paymentRef = From(src.PaymentRef),
            };

            result.disclaimerValues = src.DisclaimerValues.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return result;
        }

        public static transport.Signature2.Stroke From(grpc.Stroke src)
        {
            if (src == null) return null;
            var result = new transport.Signature2.Stroke();
            result.points = src.Points.Select(o => From(o)).ToList();
            return result;
        }

        public static payments.TaxableAmountRate From(grpc.TaxableAmountRate src)
        {
            if (src == null) return null;
            return new payments.TaxableAmountRate
            {
                id = From(src.Id),
                name = From(src.Name),
                taxableAmount = From(src.TaxableAmount),
                rate = From(src.Rate),
                taxAmount = From(src.TaxAmount),
                isVat = From(src.IsVat),
            };
        }

        public static TResult From<TResult>(grpc.TipMode src)
        {
            if (typeof(TResult) == typeof(payments.TipMode))
            {
                switch (src)
                {
                    case grpc.TipMode.TipProvided:
                        return (TResult)(object)payments.TipMode.TIP_PROVIDED;
                    case grpc.TipMode.OnScreenBeforePayment:
                        return (TResult)(object)payments.TipMode.ON_SCREEN_BEFORE_PAYMENT;
                    case grpc.TipMode.OnScreenAfterPayment:
                        return (TResult)(object)payments.TipMode.ON_SCREEN_AFTER_PAYMENT;
                    case grpc.TipMode.OnPaper:
                        return (TResult)(object)payments.TipMode.ON_PAPER;
                    case grpc.TipMode.NoTip:
                        return (TResult)(object)payments.TipMode.NO_TIP;
                    default:
                        return default(TResult);
                }
            }
            else if (typeof(TResult) == typeof(remotepay.TipMode))
            {
                switch (src)
                {
                    case grpc.TipMode.TipProvided:
                        return (TResult)(object)remotepay.TipMode.TIP_PROVIDED;
                    case grpc.TipMode.OnScreenBeforePayment:
                        return (TResult)(object)remotepay.TipMode.ON_SCREEN_BEFORE_PAYMENT;
                    case grpc.TipMode.NoTip:
                        return (TResult)(object)remotepay.TipMode.NO_TIP;
                    default:
                        return default(TResult);
                }
            }
            else throw new NotSupportedException();

        }

        public static merchant.TipSuggestion From(grpc.TipSuggestion src)
        {
            if (src == null) return null;
            return new merchant.TipSuggestion
            {
                id = From(src.Id),
                name = From(src.Name),
                percentage = From(src.Percentage),
                isEnabled = From(src.IsEnabled),
            };
        }

        public static base_.Tender From(grpc.Tender src)
        {
            if (src == null) return null;
            return new base_.Tender
            {
                id = From(src.Id),
                editable = From(src.Editable),
                labelKey = From(src.LabelKey),
                label = From(src.Label),
                opensCashDrawer = From(src.OpensCashDrawer),
                supportsTipping = From(src.SupportsTipping),
                enabled = From(src.Enabled),
                visible = From(src.Visible),
                instructions = From(src.Instructions),
            };
        }

        public static payments.TxFormat From(grpc.TransactionFormat src)
        {
            switch (src)
            {
                case grpc.TransactionFormat.Default:
                    return payments.TxFormat.DEFAULT;
                case grpc.TransactionFormat.Nexo:
                    return payments.TxFormat.NEXO;
                default:
                    return default(payments.TxFormat);
            }
        }

        public static payments.TransactionInfo From(grpc.TransactionInfo src)
        {
            if (src == null) return null;
            return new payments.TransactionInfo
            {
                languageIndicator = From(src.LanguageIndicator),
                transactionLocale = From(src.TransactionLocale),
                accountSelection = From(src.AccountSelection),
                paymentRef = From(src.PaymentRef),
                creditRef = From(src.CreditRef),
                refundRef = From(src.RefundRef),
                creditRefundRef = From(src.CreditRefundRef),
                fiscalInvoiceNumber = From(src.FiscalInvoiceNumber),
                installmentsQuantity = From(src.InstallmentsQuantity),
                installmentsPlanCode = From(src.InstallmentsPlanCode),
                installmentsPlanId = From(src.InstallmentsPlanId),
                installmentsPlanDesc = From(src.InstallmentsPlanDesc),
                cardTypeLabel = From(src.CardTypeLabel),
                stan = From(src.Stan),
                identityDocument = From(src.IdentityDocument),
                batchNumber = From(src.BatchNumber),
                receiptNumber = From(src.ReceiptNumber),
                reversalStan = From(src.ReversalStan),
                reversalMac = From(src.ReversalMac),
                reversalMacKsn = From(src.ReversalMacKsn),
                terminalIdentification = From(src.TerminalIdentification),
                merchantIdentifier = From(src.MerchantIdentifier),
                merchantNameLocation = From(src.MerchantNameLocation),
                maskedTrack2 = From(src.MaskedTrack2),
                receiptExtraData = From(src.ReceiptExtraData),
                selectedService = From(src.SelectedService),
                transactionResult = From(src.TransactionResult),
                transactionTags = From(src.TransactionTags),
                txFormat = From(src.TxFormat),
                panMask = From(src.PanMask),
                transactionSequenceCounter = From(src.TransactionSequenceCounter),
                applicationPanSequenceNumber = From(src.ApplicationPanSequenceNumber),
                reversalReason = From(src.ReversalReason),
                isTokenBasedTx = From(src.IsTokenBasedTx),
                origTransactionSequenceCounter = From(src.OrigTransactionSequenceCounter),
                transactionSequenceCounterUpdate = From(src.TransactionSequenceCounterUpdate),
            };
        }

        public static payments.TransactionResult From(grpc.TransactionResult src)
        {
            switch (src)
            {
                case grpc.TransactionResult.Approved:
                    return payments.TransactionResult.APPROVED;
                case grpc.TransactionResult.Declined:
                    return payments.TransactionResult.DECLINED;
                case grpc.TransactionResult.Aborted:
                    return payments.TransactionResult.ABORTED;
                case grpc.TransactionResult.VoiceAuthorization:
                    return payments.TransactionResult.VOICE_AUTHORISATION;
                case grpc.TransactionResult.PaymentPartOnly:
                    return payments.TransactionResult.PAYMENT_PART_ONLY;
                case grpc.TransactionResult.PartiallyApproved:
                    return payments.TransactionResult.PARTIALLY_APPROVED;
                case grpc.TransactionResult.None:
                    return payments.TransactionResult.NONE;
                default:
                    return default(payments.TransactionResult);
            }
        }

        public static payments.TransactionSettings From(grpc.TransactionSettings src)
        {
            if (src == null) return null;
            var result = new payments.TransactionSettings
            {
                cardEntryMethods = From(src.CardEntryMethods),
                disableCashBack = From(src.DisableCashBack),
                cloverShouldHandleReceipts = From(src.CloverShouldHandleReceipts),
                forcePinEntryOnSwipe = From(src.ForcePinEntryOnSwipe),
                disableRestartTransactionOnFailure = From(src.DisableRestartTransactionOnFailure),
                allowOfflinePayment = From(src.AllowOfflinePayment),
                approveOfflinePaymentWithoutPrompt = From(src.ApproveOfflinePaymentWithoutPrompt),
                signatureThreshold = From(src.SignatureThreshold),
                signatureEntryLocation = From(src.SignatureEntryLocation),
                tipMode = From<payments.TipMode>(src.TipMode),
                tippableAmount = From(src.TippableAmount),
                disableReceiptSelection = From(src.DisableReceiptSelection),
                disableDuplicateCheck = From(src.DisableDuplicateCheck),
                autoAcceptPaymentConfirmations = From(src.AutoAcceptPaymentConfirmations),
                autoAcceptSignature = From(src.AutoAcceptSignature),
                forceOfflinePayment = From(src.ForceOfflinePayment),
            };
            result.tipSuggestions = src.TipSuggestions.Select(o => From(o)).ToList();
            result.regionalExtras = src.RegionalExtras.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return result;
        }

        public static transport.PayIntent.TransactionType From(grpc.TransactionType src)
        {
            switch (src)
            {
                case grpc.TransactionType.Payment:
                    return transport.PayIntent.TransactionType.PAYMENT;
                case grpc.TransactionType.Credit:
                    return transport.PayIntent.TransactionType.CREDIT;
                case grpc.TransactionType.Auth:
                    return transport.PayIntent.TransactionType.AUTH;
                case grpc.TransactionType.Data:
                    return transport.PayIntent.TransactionType.DATA;
                case grpc.TransactionType.BalanceInquiry:
                    return transport.PayIntent.TransactionType.BALANCE_INQUIRY;
                case grpc.TransactionType.CapturePreaut:
                    return transport.PayIntent.TransactionType.CAPTURE_PREAUTH;
                default:
                    return default(transport.PayIntent.TransactionType);
            }
        }

        public static payments.VaultedCard From(grpc.VaultedCard src)
        {
            if (src == null) return null;
            return new payments.VaultedCard
            {
                first6 = From(src.First6),
                last4 = From(src.Last4),
                cardholderName = From(src.CardholderName),
                expirationDate = From(src.ExpirationDate),
                token = From(src.Token),
            };
        }

        public static remotepay.VerifySignatureRequest From(grpc.AcceptSignatureRequest src)
        {
            if (src == null) return null;
            return new remotepay.VerifySignatureRequest
            {
                Payment = From(src.Payment),
                Signature = null,
            };
        }

        public static remotepay.VerifySignatureRequest From(grpc.RejectSignatureRequest src)
        {
            if (src == null) return null;
            return new remotepay.VerifySignatureRequest
            {
                Payment = From(src.Payment),
                Signature = null,
            };
        }

        public static remotepay.VerifySignatureRequest From(grpc.VerifySignatureRequest src)
        {
            if (src == null) return null;
            return new remotepay.VerifySignatureRequest
            {
                Payment = From(src.Payment),
                Signature = From(src.Signature),
            };
        }

        public static order.VoidReason From(grpc.VoidReason src)
        {
            switch (src)
            {
                case grpc.VoidReason.UserCancel:
                    return order.VoidReason.USER_CANCEL;
                case grpc.VoidReason.TransportError:
                    return order.VoidReason.TRANSPORT_ERROR;
                case grpc.VoidReason.RejectSignature:
                    return order.VoidReason.REJECT_SIGNATURE;
                case grpc.VoidReason.RejectPartialAuth:
                    return order.VoidReason.REJECT_PARTIAL_AUTH;
                case grpc.VoidReason.NotApproved:
                    return order.VoidReason.NOT_APPROVED;
                case grpc.VoidReason.Failed:
                    return order.VoidReason.FAILED;
                case grpc.VoidReason.AuthClosedNewCard:
                    return order.VoidReason.AUTH_CLOSED_NEW_CARD;
                case grpc.VoidReason.DeveloperPayPartialAuth:
                    return order.VoidReason.DEVELOPER_PAY_PARTIAL_AUTH;
                case grpc.VoidReason.RejectDuplicate:
                    return order.VoidReason.REJECT_DUPLICATE;
                case grpc.VoidReason.RejectOffline:
                    return order.VoidReason.REJECT_OFFLINE;
                default:
                    return default(order.VoidReason);
            }
        }
    }
}
