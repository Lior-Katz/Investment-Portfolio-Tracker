using System;

namespace PortfolioTracker.ViewModels;

public class CurrencyModel
{
    public enum Currency
    {
        USD,
        NIS,
        EUR
    }

    public CurrencyModel(string currencyString)
    {
        if (Enum.TryParse(currencyString, out Currency currency))
        {
            SelectedCurrency = currency;
        }
    }

    public CurrencyModel(int index = 0)
    {
        SelectedCurrency = (Currency)index;
    }

    public Currency SelectedCurrency { get; set; } = Currency.USD;

    public override string ToString()
    {
        return SelectedCurrency.ToString();
    }
}
