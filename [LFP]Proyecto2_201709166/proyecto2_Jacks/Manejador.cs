using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2_Jacks
{
    class Manejador
    {
        List<Token> salidaToken;
        List<Error> salidaError;
        List<ErrorSintax> ErrorSintax;
        private static Manejador llamado;

        public static Manejador Obtenerllamado()
        {
            if (llamado == null)
            {
                llamado = new Manejador();
            }
            return llamado;
        }
        public Manejador()
        {
            salidaToken = new List<Token>();

            salidaError = new List<Error>();
            ErrorSintax = new List<ErrorSintax>();
           
        }

        public List<Token> getsalidaToken()
        {
            return salidaToken;
        }


        public List<Error> getsalidaError()
        {
            return salidaError;
        }

        public List<ErrorSintax> getErrorSintax()
        {
            return ErrorSintax;
        }
    }
}
