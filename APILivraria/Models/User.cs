using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace APILivraria.Models;

public class User : Entity
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int RoleId { get; set; }
    public byte[] Salt { get; set; } = Array.Empty<byte>();
    public Role Role { get; set; } = default!;
    public Carrinho Carrinho { get; set; } = default!;
    public ICollection<EnderecoUsuario> Enderecos { get; set; } = Array.Empty<EnderecoUsuario>();

    //public IList<int> MyProperty { get; set; } = new List<int>();
    //public List<int> MyProperty1 { get; set; } = new List<int>();

    //public List<int> MyProperty4 { get; set; } = new MinhaListaPersonalizada<int>();
    //public List<int> MyProperty9 { get; set; } = new HashSet<int>();
    //public IList<int> MyProperty3 { get; set; } = new MinhaListaPersonalizada<int>();
    //public IList<int> MyProperty10 { get; set; } = new HashSet<int>();

    //public ICollection<int> MyProperty5 { get; set; } = new List<int>();
    //public ICollection<int> MyProperty11 { get; set; } = new int[3]; // CUIDADO!!!! 
    //public ICollection<int> MyProperty6 { get; set; } = new MinhaListaPersonalizada<int>();

    //public IEnumerable<int> MyProperty7 { get; set; } = new List<int>();
    //public IEnumerable<int> MyProperty8 { get; set; } = new MinhaListaPersonalizada<int>();
}

//public class MinhaListaPersonalizada<T> : IList<T>
//{
//    public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//    public int Count => throw new NotImplementedException();

//    public bool IsReadOnly => throw new NotImplementedException();

//    public void Add(T item)
//    {
//        throw new NotImplementedException();
//    }

//    public void Clear()
//    {
//        throw new NotImplementedException();
//    }

//    public bool Contains(T item)
//    {
//        throw new NotImplementedException();
//    }

//    public void CopyTo(T[] array, int arrayIndex)
//    {
//        throw new NotImplementedException();
//    }

//    public IEnumerator<T> GetEnumerator()
//    {
//        throw new NotImplementedException();
//    }

//    public int IndexOf(T item)
//    {
//        throw new NotImplementedException();
//    }

//    public void Insert(int index, T item)
//    {
//        throw new NotImplementedException();
//    }

//    public bool Remove(T item)
//    {
//        throw new NotImplementedException();
//    }

//    public void RemoveAt(int index)
//    {
//        throw new NotImplementedException();
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        throw new NotImplementedException();
//    }
//}
