using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CalculatorWithHistory.Data;
using CalculatorWithHistory.Models;
using Debug = System.Diagnostics.Debug;

namespace CalculatorWithHistory.ViewModel;
public partial class HistoryViewModel : ObservableObject
{
    private readonly HistoryService _historyService;
    public ObservableCollection<HistoryExpressionModel> History { get; set; } = new ObservableCollection<HistoryExpressionModel>();

    public HistoryViewModel(HistoryService historyService)
    {
        _historyService =historyService;
    }

    //[ObservableProperty]
    //ObservableCollection<string> items;

    [ObservableProperty]
    public string text;

    [RelayCommand]
    public async void Add()
    {

        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        await _historyService.AddExpresssion(new Models.HistoryExpressionModel
        {
            Expression = text
    });
        Debug.WriteLine(text);

        //items.Add(text);
        //Debug.WriteLine(text);

        //text= string.Empty;
    }
    /*   [RelayCommand]
       async void DeleteAll()
       {
           if (items.Count == 0)
           {
               await Application.Current.MainPage.DisplayAlert("No History", "No History to Clear.", "Ok");
           }
           else
           {
               bool x = await Application.Current.MainPage.DisplayAlert("Question?", "Would you like to Clear  the History", "Yes", "No");
               if (x)
                   items.Clear();
           }
       } */

    [RelayCommand]
    public async void GetHistory()
    {
        History.Clear();
        var HistoryList = await _historyService.GetHistory();
        if (HistoryList?.Count > 0)
        {
            //studentList = studentList.OrderBy(f => f.FullName).ToList();
            foreach (var expr in HistoryList)
            {
                History.Add(expr);
            }
           
        }
    }

    [RelayCommand]
   public async void DisplayAction(HistoryExpressionModel historyExpressionModel)
    {
        var response = await AppShell.Current.DisplayActionSheet("Delete Expression",null,null,"Delete" );
       
        if (response == "Delete")
        {
            var delResponse = await _historyService.DeleteExpression(historyExpressionModel);
            if (delResponse > 0)
            {
                GetHistory();
            }
        }
    }

    [RelayCommand]
    public async void DeleteExpresssion(HistoryExpressionModel historyExpressionModel)
    {

        /*         bool x = await Application.Current.MainPage.DisplayAlert("Question?", "Would you like to Clear the Expression?", "Yes", "No");
             if (x)
             {

                 var delResponse = await _historyService.DeleteExpression(historyExpressionModel);

                 if (delResponse > 0)
                 {
                     GetHistory();
                 }
             } */
        bool response =  await Application.Current.MainPage.DisplayAlert("Question?", "Would you like to Clear the Expression?", "Yes", "No");
        if (response)
        {
            var delResponse = await _historyService.DeleteExpression(historyExpressionModel);
       
                GetHistory();
            
        }
    }

    [RelayCommand]
    public async void DeleteAll()
    {
        if (History.Count == 0)
        {
            await Application.Current.MainPage.DisplayAlert("No History", "No History to Clear.", "Ok");
        }
        else
        {
            bool x = await Application.Current.MainPage.DisplayAlert("Question?", "Would you like to Clear all the History?", "Yes", "No");
            if (x)
            {
                await _historyService.DeleteTable();
                GetHistory();
            }
                
        }
          
    }

}