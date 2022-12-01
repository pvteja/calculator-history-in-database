# Project-3
Youtube Link: https://youtu.be/3jH-j3L3xqA

Github Repo:https://github.com/vedasree-kommindala/Project-3

A. Vedasree Kommindala
1. Created Tabbed Pages for Calculator and History - d2c6f0a5b4081426d5ee6aefd1de5f92040ec378

![image](https://user-images.githubusercontent.com/114097793/202527181-3346bd78-6d6a-475f-9e69-95862ea68443.png)

```
    <TabBar>
        <Tab Title="Calculator Page">
            <ShellContent ContentTemplate="{DataTemplate  local:MainPage}" />
        </Tab>
        <Tab Title="History">
            <ShellContent ContentTemplate= "{DataTemplate view:HistoryPage}" />
        </Tab>
    </TabBar> 
```

2. Created the Label-'result text' and adding binding for it - d275bf13b0a9795156bbc4c07ac5b0686dd40f9b
```

  <Label x:Name="tempResult" IsVisible="false"
               Text="{Binding Text, Mode=TwoWay}"/>
```
```
 void OnCalculate(object sender, EventArgs e)
    {
      //
            this.tempResult.Text = $"{firstNumber} {mathOperator} {secondNumber}" +" = "+result.ToTrimmedString(decimalFormat);
      //
    }
```

3. Changed the UI of History Page - 9352aebc5f604e7b59c1252416ee9556cc7d9828

![image](https://user-images.githubusercontent.com/114097793/202527287-2924ece4-aae1-49e3-9f0d-40d6e56f8b50.png)

```
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalculatorWithHistory.View.HistoryPage"
             xmlns:viewmodel="clr-namespace:CalculatorWithHistory.ViewModel"
             x:DataType="viewmodel:HistoryViewModel"
             BackgroundImageSource="background.png"
             >
             //
             //
              <Grid RowDefinitions="*,auto">
        <CollectionView Grid.Row="0" ItemsSource="{Binding Items}"  >
            <CollectionView.ItemTemplate>
                <DataTemplate x:DataType="{x:Type x:String}">

                    <Grid Padding="10">
                        <Frame BackgroundColor="Transparent">

                            <Label Grid.Row="0" FontSize="25" TextColor="White" Text="{Binding .}"/>


                        </Frame>
                        <!--  <Frame HeightRequest="100">
                            <Label Text="{Binding id}" FontSize="20"  />
                        </Frame>
                        <Frame HeightRequest="1">
                            <Label Text="{Binding question}" FontSize="20"  /> 
                        </Frame> -->
                    </Grid>

                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>

        <ImageButton Source="trash.jpg" Command="{Binding DeleteAllCommand}"
                    HorizontalOptions="EndAndExpand"
                    VerticalOptions="EndAndExpand" HeightRequest="50" WidthRequest="50" />
    </Grid>
              //
 </ContentPage>
```
4. Added Functionality to delete history using RelayCommand in ViewModel - 439933192c47e863fa69280971831f9945514622

```
 [RelayCommand]
    async void DeleteAll()
    {
      
           
                items.Clear();
        }
    }
}
```
```
 <ImageButton Source="trash.jpg" Command="{Binding DeleteAllCommand}"
                    HorizontalOptions="EndAndExpand"
                    VerticalOptions="EndAndExpand" HeightRequest="50" WidthRequest="50" />
```
5. Added PopUp to confirm Delete History - 0407ebdca5c36e75c7d2912b2d92634e63225fd7

![image](https://user-images.githubusercontent.com/114097793/202527748-6b2f9a8d-1784-49b6-952e-578dbdfb17e2.png)

```
 [RelayCommand]
    async void DeleteAll()
    {

     
            bool x = await Application.Current.MainPage.DisplayAlert("Question?", "Would you like to Clear  the History", "Yes", "No");
            if (x)
                items.Clear();

    }
}
```
6. Updated code, restored packages - 



B. Venkata Pruthvi Teja
1. Created HistoryViewModel as part of MVVM architecture - e20c3b74947e5f1fec93486eaf2b9d22e08b3c93
```
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CalculatorWithHistory.ViewModel;

public partial class HistoryViewModel : ObservableObject
{

    public HistoryViewModel()
    {
        items=new ObservableCollection<string>();
    }
}
```
2. Created CollectionView element in HistoryPage to fetch bind data - ba38c05581115b8ba220f9884846012575b986c3
```
 <CollectionView  ItemsSource="{Binding Items}"  >
            <CollectionView.ItemTemplate>
                <DataTemplate x:DataType="{x:Type x:String}">

                    <Grid>
                        <Frame BackgroundColor="Transparent">

                            <Label Text="{Binding .}"/>


                        </Frame>
           
                    </Grid>

                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>
```
3. Created the Observable collection for history items         
   and added RelayCommand for Add Items method and binded         - 998d36bfd6fcfa901c71fd71936d0484504cc810
   it to onCalculate button(Functionality to fetch history)
   
```   
namespace CalculatorWithHistory.ViewModel;

public partial class HistoryViewModel : ObservableObject
{

    public HistoryViewModel()
    {
        items=new ObservableCollection<string>();
    }

    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty]
    public string text;

    [RelayCommand]
    void Add()

    {

        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }
        items.Add(text);
        Debug.WriteLine(text);

        text= string.Empty;
    }
   
}
```
4. Corrected the code to display on History Page - 998d36bfd6fcfa901c71fd71936d0484504cc810
```
  <Label x:Name="tempResult" IsVisible="false"
               Text="{Binding Text, Mode=TwoWay}"/>

        <Button Text="=" Grid.Row="5" Grid.Column="3" 
               Pressed="OnCalculate" Command="{Binding AddCommand}" />
 ```
5. Added PopUp to prevent delete empty history - 8e320310e4a5919f1a03e947c551d1a31cf0d084

![image](https://user-images.githubusercontent.com/114097793/202527858-5681ba08-0e95-4215-bd12-590d66537543.png)

```
 [RelayCommand]
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
    }
}
```



C. Praneeth R
1. Forked the base Calculator Project - 74c9cf24a1e8cb8afc89a7fb8b595985502d6fd3
2. Created a new History Page in View - d7a8901add68265c089fa575763b607a682133d3
```
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalculatorWithHistory.View.HistoryPage"
             xmlns:viewmodel="clr-namespace:CalculatorWithHistory.ViewModel"
             x:DataType="viewmodel:HistoryViewModel"
             BackgroundImageSource="background.png"
             >
</ContentPage>
```
3. Installed CommunityToolkit.mvvm package and debugged the errors - e32041c6e0b6a40ce4fb6be9df9431ae8a6e31f4
4. Added the Delete History button to History Page - 7631118ff2dda6b5c1755418e2fb98e7001b8756

```
 <ImageButton Source="trash.jpg" Command="{Binding DeleteAllCommand}"
                    HorizontalOptions="EndAndExpand"
                    VerticalOptions="EndAndExpand" HeightRequest="50" WidthRequest="50" />
```

