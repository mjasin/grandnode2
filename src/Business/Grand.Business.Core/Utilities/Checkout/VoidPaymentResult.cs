﻿using Grand.Domain.Payments;

namespace Grand.Business.Core.Utilities.Checkout;

/// <summary>
///     Represents a VoidPaymentResult
/// </summary>
public class VoidPaymentResult
{
    /// <summary>
    ///     Ctor
    /// </summary>
    public VoidPaymentResult()
    {
        Errors = new List<string>();
    }

    /// <summary>
    ///     Gets a value indicating whether request has been completed successfully
    /// </summary>
    public bool Success => Errors.Count == 0;

    /// <summary>
    ///     Errors
    /// </summary>
    public IList<string> Errors { get; set; }

    /// <summary>
    ///     Gets or sets a payment transaction status after processing
    /// </summary>
    public TransactionStatus NewTransactionStatus { get; set; } = TransactionStatus.Pending;

    /// <summary>
    ///     Add error
    /// </summary>
    /// <param name="error">Error</param>
    public void AddError(string error)
    {
        Errors.Add(error);
    }
}