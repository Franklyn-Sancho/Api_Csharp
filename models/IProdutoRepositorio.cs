public interface IProdutoRepositorio {
    IEnumerable<Produto> getAll();
    Produto GetProduto(int id);
    Produto Add(Produto item);
    void Remove(int id);
    bool Update(Produto item);
}