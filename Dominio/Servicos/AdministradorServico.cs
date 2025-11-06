using minimal.Dominio.DTOs;
using minimal.Dominio.Entidades;
using minimal.Dominio.Interfaces;
using minimal.Infraestrutura.Db;

namespace minimal.Dominio.Servicos
{
    public class AdministradorServico(DbContexto contexto) : IAdministradorServico
    {
        private readonly DbContexto _contexto = contexto;

        public Administrador? BuscaPorId(int id)
        {
            return _contexto.Administradores.Where(a => a.Id == id).FirstOrDefault();
        }
    
        public Administrador Incluir(Administrador administrador)
        {
            _contexto.Administradores.Add(administrador);
            _contexto.SaveChanges();
            return administrador;
        }

        public Administrador? Login(LoginDTO loginDto)
        {
            var adm = _contexto.Administradores.Where(a => a.Email == loginDto.Email && a.Senha == loginDto.Senha).FirstOrDefault();

            return adm;

        }

        public List<Administrador> Todos(int? pagina)
        {

            var query = _contexto.Administradores.AsQueryable();//asqueryable transforma em consulta

            if(pagina != null)
                query = query.Skip(((int)pagina - 1) * 10).Take(10);

            return query.ToList();
        }
    }
}