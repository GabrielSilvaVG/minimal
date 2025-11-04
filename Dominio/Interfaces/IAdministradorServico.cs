using minimal.Dominio.DTOs;
using minimal.Dominio.Entidades;

namespace minimal.Dominio.Interfaces
{
    public interface IAdministradorServico
    {
       Administrador? Login(LoginDTO loginDto);
    }
}