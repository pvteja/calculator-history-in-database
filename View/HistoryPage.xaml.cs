using CalculatorWithHistory.ViewModel;

namespace CalculatorWithHistory.View;

public partial class HistoryPage : ContentPage
{
    private HistoryViewModel _viewMode;

    public HistoryPage(HistoryViewModel vm )
	{
		InitializeComponent();
        _viewMode = vm;
        BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewMode.GetHistoryCommand.Execute(null);
    }
}