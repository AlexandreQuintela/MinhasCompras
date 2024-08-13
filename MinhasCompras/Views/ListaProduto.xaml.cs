using MinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista;
    }

    // Sempre é chamado quando uma tela é chamada
    protected async override void OnAppearing()
    {
        lista.Clear();
        List<Produto> listaTemp = await App.Db.GetAll();
        listaTemp.ForEach(x => lista.Add(x));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
            throw;
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;
        lista.Clear();
        List<Produto> listaTemp = await App.Db.Search(q);
        listaTemp.ForEach(x => lista.Add(x));
    }

    private void ToolbarItem_Somar(object sender, EventArgs e)
    {
        double soma = lista.Sum(x => x.Total);
        string msg = $"O total é {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private void MenuItem_Remover(object sender, EventArgs e)
    {

    }
}