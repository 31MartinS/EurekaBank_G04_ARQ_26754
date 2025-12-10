using Microsoft.EntityFrameworkCore;
using Eurabank_Restfull_SOAP_G04.Data;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public class ContadorService : IContadorService
    {
        private readonly CalculatorDbContext _context;

        public ContadorService(CalculatorDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerarCodigoAsync(string tabla)
        {
            var contador = await _context.Contadores
                .FirstOrDefaultAsync(c => c.VchConttabla == tabla);

            if (contador == null)
            {
                contador = new Contador
                {
                    VchConttabla = tabla,
                    IntContitem = 1,
                    IntContlongitud = 8
                };
                _context.Contadores.Add(contador);
            }
            else
            {
                contador.IntContitem++;
            }

            await _context.SaveChangesAsync();

            return contador.IntContitem.ToString().PadLeft(contador.IntContlongitud, '0');
        }
    }
}
