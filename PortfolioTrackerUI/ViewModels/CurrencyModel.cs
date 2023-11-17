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

	private Currency _selectedCurrency = Currency.USD;
	public Currency SelectedCurrency
	{
		get
		{
			return _selectedCurrency;
		}
		set
		{
			_selectedCurrency = value;
		}
	}

	public CurrencyModel(string currencyString)
	{

		if (Enum.TryParse(currencyString, out Currency currency))
		{
			_selectedCurrency = currency;
		}
	}

	public CurrencyModel(int index = 0)
	{
		_selectedCurrency = (Currency) index;
	}
	public override string ToString() => _selectedCurrency.ToString();
}
