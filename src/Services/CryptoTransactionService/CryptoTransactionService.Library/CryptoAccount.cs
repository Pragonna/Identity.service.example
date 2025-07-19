using System.Runtime.CompilerServices;
using CryptoTransactionService.Library.Events;
using CryptoTransactionService.Library.Models;
using EventBus.EventBus.Base.Abstraction;
using EventBus.EventBus.Base.Events;
using Nethereum.Signer;

namespace CryptoTransactionService.Library;

public class CryptoAccount
{
    private readonly IEventBus _eventBus;
    public Guid GuidId { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public bool IsActive { get; set; }
    public DateTime TransferAt { get; set; }

    private CryptoAccount(IEventBus eventBus)
    {
        _eventBus = eventBus;
        GuidId = Guid.NewGuid();
    }

    public static CryptoAccount Create(IEventBus eventBus, decimal balance, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentNullException(nameof(currency));

        var instance = new CryptoAccount(eventBus);
        instance.Balance = balance;
        instance.Currency = currency;
        instance.IsActive = true;

        return instance;
    }

    public async Task TransferCrypto(decimal amount, string currency, CryptoWallet cryptoWallet, string toAddress)
    {
        if (currency != Currency) throw new ArgumentException("Currency is not supported", nameof(currency));
        if (Balance < amount) throw new ArgumentException("Insufficient balance");

        // Cold wallet connection via external signer
        var web3 = new Web3(cryptoWallet.ExternalSigner); // Cold wallet connection via external signer
        var transactionReceipt = await web3.Eth.GetEtherTransferService()
            .TransferEtherAndWaitForReceiptAsync(toAddress, amount);

        if (transactionReceipt.Status.Value == 1)
        {
            Balance -= amount;
            TransferAt = DateTime.UtcNow;
            _eventBus.Publish(new CryptoAccountDataIntegrationEvent(new Balance(Balance, currency)));
        }
        else
        {
            throw new Exception("Transaction failed on blockchain");
        }
    }

    private bool VerifySignature(string walletAddress, string message, string signature)
    {
        var signer = new EthereumMessageSigner();
        var recoveredAddress = signer.EncodeUTF8AndEcRecover(message, signature);
        return recoveredAddress.Equals(walletAddress, StringComparison.OrdinalIgnoreCase);
    }
}