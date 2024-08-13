using SQLite;

namespace MinhasCompras.Models;

public class Produto
{
    string descricao;
    double quantidade, preco;

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Descricao
    {
        get => descricao;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A descrição não pode ser nula, vazia ou conter apenas espaços em branco.");
            descricao = value;
        }
    }
    public double Quantidade
    {
        get => quantidade;
        set
        {
            if (value <= 0)
                throw new ArgumentException("A quantidade não pode ser menor ou igual a zero.");
            quantidade = value;
        }
    }
    public double Preco
    {
        get => preco;
        set
        {
            if (value <= 0)
                throw new ArgumentException("O preço não pode ser menor ou igual a zero.");
            preco = value;
        }
    }
    public double Total { get => Quantidade * Preco; }
}
