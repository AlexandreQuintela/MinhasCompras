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
        try
        {
            lista.Clear();
            List<Produto> listaTemp = await App.Db.GetAll();
            listaTemp.ForEach(x => lista.Add(x));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
            throw;
        }
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
        try
        {
            string q = e.NewTextValue;
            lista.Clear();
            List<Produto> listaTemp = await App.Db.Search(q);
            listaTemp.ForEach(x => lista.Add(x));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
            throw;
        }
    }

    private async void ToolbarItem_Somar(object sender, EventArgs e)
    {
        try
        {
            double soma = lista.Sum(x => x.Total);
            string msg = $"O total é {soma:C}";
            await DisplayAlert("Total dos Produtos", msg, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
            throw;
        }
    }

    private async void MenuItem_Remover(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;
            if (selecionado != null)
            {
                Produto produto = selecionado.BindingContext as Produto;
                bool confirma = await DisplayAlert("Tem Certeza...", $"Remover {produto.Descricao} ?", "Sim", "Não");
                if (confirma)
                {
                    await App.Db.Delete(produto.Id);
                    // retira da observer
                    lista.Remove(produto);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
            throw;
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto produto = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = produto,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
            throw;
        }
    }
}