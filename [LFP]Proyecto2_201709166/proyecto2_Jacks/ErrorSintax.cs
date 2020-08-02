using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2_Jacks
{
    class ErrorSintax
    {
        private String error;
        private Descripcion descripcion;

        public enum Descripcion
        {
            ERROR_SINTACTICO

        }

        public ErrorSintax(String error, Descripcion desc)
        {
            this.error = error;
            this.descripcion = desc;
        }
        public String getError()
        {
            return error;
        }
        public String Getdescripcion()
        {
            switch (descripcion)
            {
                case Descripcion.ERROR_SINTACTICO:
                    return "Error Sintatico";
                default:
                    return "Desconocido";
            }
        }
    }

    

}
