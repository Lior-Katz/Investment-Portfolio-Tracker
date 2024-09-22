namespace PortfolioTracker.ViewModels;

internal class AddTransactionDialogViewModel : ViewModelBase
{
    private string _assetType;

    private string _market = "";

    private decimal _payoutCommission;

    // TODO: figure out binding
    private int _payoutPeriod;

    private decimal _payoutTax;

    private decimal _payoutYield;

    private string _sector = "";

    public decimal PayoutYield
    {
        get => _payoutYield;
        set
        {
            _payoutYield = value;
            OnPropertyChanged(nameof(PayoutYield));
        }
    }

    public decimal PayoutTax
    {
        get => _payoutTax;
        set
        {
            _payoutTax = value;
            OnPropertyChanged(nameof(PayoutTax));
        }
    }

    public decimal PayoutCommission
    {
        get => _payoutCommission;
        set
        {
            _payoutCommission = value;
            OnPropertyChanged(nameof(PayoutCommission));
        }
    }

    public int PayoutPeriod
    {
        get => _payoutPeriod;
        set
        {
            _payoutPeriod = value;
            OnPropertyChanged(nameof(PayoutPeriod));
        }
    }

    public string AssetType
    {
        get => _assetType;
        set
        {
            _assetType = value;
            OnPropertyChanged(nameof(AssetType));
        }
    }

    public string Sector
    {
        get => _sector;
        set
        {
            _sector = value;
            OnPropertyChanged(nameof(Sector));
        }
    }

    public string Market
    {
        get => _market;
        set
        {
            _market = value;
            OnPropertyChanged(nameof(Market));
        }
    }
}
