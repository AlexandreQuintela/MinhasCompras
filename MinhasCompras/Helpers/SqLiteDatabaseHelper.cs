﻿using MinhasCompras.Models;
using SQLite;

namespace MinhasCompras.Helpers;

public class SqLiteDatabaseHelper
{
    readonly SQLiteAsyncConnection _conn;
    public SqLiteDatabaseHelper(string path)
    {
        _conn = new SQLiteAsyncConnection(path);
        _conn.CreateTableAsync<Produto>().Wait();
    }
    public Task<int> Insert(Produto produto)
    {
        return _conn.InsertAsync(produto);
    }
    public Task<List<Produto>> Update(Produto produto)
    {
        string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
        return _conn.QueryAsync<Produto>(
            sql,
            produto.Descricao, produto.Quantidade, produto.Preco, produto.Id
        );
    }
    public Task<int> Delete(int id)
    {
        return _conn.Table<Produto>().DeleteAsync(p => p.Id == id);
    }
    public Task<List<Produto>> GetAll()
    {
        return _conn.Table<Produto>().ToListAsync();
    }
    public Task<List<Produto>> Search(string query)
    {
        string sql = $"select * from Produto WHERE Descricao like='%{query}%'";
        return _conn.QueryAsync<Produto>(sql);
    }
}
