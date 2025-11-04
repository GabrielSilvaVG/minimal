
using minimal.Dominio.Entidades;
using minimal.Dominio.Interfaces;
using minimal.Infraestrutura.Db;

namespace minimal.Dominio.Servicos
{
    public class VeiculoServico(DbContexto contexto) : IVeiculoServico
    {
        private readonly DbContexto _contexto = contexto;

        public void Apagar(Veiculo veiculo)
        {
            _contexto.Veiculos.Remove(veiculo);
            _contexto.SaveChanges();
        }

        public void Atualizar(Veiculo veiculo)
        {
            _contexto.Veiculos.Update(veiculo);
            _contexto.SaveChanges();
        }

        public Veiculo? BuscaPorId(int id)
        {
            return _contexto.Veiculos.Find(id);
        }

        public void Incluir(Veiculo veiculo)
        {
            _contexto.Veiculos.Add(veiculo);
            _contexto.SaveChanges();
        }

        public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
        {
            var query = _contexto.Veiculos.AsQueryable();//asqueryable transforma em consulta

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => v.Nome.Contains(nome));
            }

            if(pagina != null)
                query = query.Skip(((int)pagina - 1) * 10).Take(10);

            return query.ToList();
        }
    }
} 