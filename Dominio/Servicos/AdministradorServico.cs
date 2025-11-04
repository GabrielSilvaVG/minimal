using minimal.Dominio.DTOs;
using minimal.Dominio.Entidades;
using minimal.Dominio.Interfaces;
using minimal.Infraestrutura.Db;

namespace minimal.Dominio.Servicos
{
    public class AdministradorServico(DbContexto contexto) : IAdministradorServico
    {
        private readonly DbContexto _contexto = contexto;

        public Administrador? Login(LoginDTO loginDto)
        {
            var adm = _contexto.Administradores.Where(a => a.Email == loginDto.Email && a.Senha == loginDto.Senha).FirstOrDefault();

            return adm;

        }
    }
}