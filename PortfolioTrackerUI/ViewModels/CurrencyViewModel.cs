namespace PortfolioTracker.ViewModels;
public class CurrencyViewModel : ViewModelBase
{

	public enum Currnecy
	{
		USD,
		NIS,
		EUR
	}

	private Currnecy _selectedCurrency = Currnecy.USD;
	public Currnecy SelectedCurrency
	{
		get
		{
			return _selectedCurrency;
		}
		set
		{
			_selectedCurrency = value;
			OnPropertyChanged(nameof(SelectedCurrency));
		}
	}
}
