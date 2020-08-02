using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace proyecto2_Jacks
{
    class Analizador_Sintac
    {
        int controlToken;
        string traduccion = "";
        Token tokenActual;
        List<Token> listToken;
        public string tra() {
            return traduccion; }

        public void parsear(List<Token> tokens) {

            this.listToken = tokens;
            controlToken = 0;
            tokenActual = listToken.ElementAt(controlToken);
            Inicio();
        }

        public void Inicio() {
            if (tokenActual.GetTipo() == "Palabra Reservada Class")
            {
                emparejar("Palabra Reservada Class");
                Identificador();
                emparejar("Llave Izquierda");
                MetodoStatic();
                emparejar("Llave Izquierda");
                Body();
                emparejar("Llave Derecha");
                emparejar("Llave Derecha");
            }

        }
        public void Identificador() {
            if (tokenActual.GetTipo() == "Identificador")
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Identificador");
            }
        }
        public void MetodoStatic() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada Static"))
            {
                emparejar("Palabra Reservada Static");
                emparejar("Palabra reservada Void");
                Identificador();
                emparejar("Parentesis izquierda");

                emparejar("Parentesis derecha");

            }
        }
        public void Tipo() {
            if (tokenActual.GetTipo().Equals("Reservada Int"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Reservada Int");

            }
            else if (tokenActual.GetTipo().Equals("Reservada Bool"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Reservada Bool");
            }
            else if (tokenActual.GetTipo().Equals("Reservada Char"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Reservada Char");

            }
            else if (tokenActual.GetTipo().Equals("Reservada String"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Reservada String");
            }
            else if (tokenActual.GetTipo().Equals("Reservada float"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Reservada Float");
            }
            else {
                
                TipoAsignacion();
            }
        }
        public void Body()
        {
            Comandos();
        }
        public void Comandos() {
            if (tokenActual.GetTipo().Equals("Reservada Int") || tokenActual.GetTipo().Equals("Reservada Float")||
                tokenActual.GetTipo().Equals("Reservada String")|| tokenActual.GetTipo().Equals("Reservada Char")||
                tokenActual.GetTipo().Equals("Reservado Bool"))
            {
 Declaracion();      
            }
                  

            Graficar_vector();
            Comentarios();
            Imprimir();
            IF();
            FOR();
            WHILE();           
            Jerarquia();
            //Subcomandos();


        }
        public void Subcomandos()
        {
           // Declaracion();
            // Comandos();
            Graficar_vector();
            Comentarios();
            Imprimir();
            IF();
            FOR();
            WHILE();
            Jerarquia();

        }
        public void Declaracion()
        {
            
            
            Tipo();
            TipoDeclaracion();

            Comandos();
        }
        public void TipoDeclaracion() {
           Ident_aux1();
            Ident_aux2();
            Identificador();
            if (tokenActual.GetTipo().Equals("Punto y Coma"))
            {

                emparejar("Punto y Coma");
                Subcomandos();
            }

            if (tokenActual.GetTipo().Equals("Corchete Izquierda"))
            {
                emparejar("Corchete Izquierda");
                emparejar("Corchete Derecha");
                Identificador();
                emparejar("Signo igual");
                Arreglo();

            }

        }
        public void Arreglo() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada New"))
            {
                emparejar("Palabra Reservada New");
                Tipo();
                emparejar("Corchete Izquierda");
                emparejar("Corchete Derecha");
                emparejar("Punto y Coma");
                Subcomandos();

            }
            else {
                if (tokenActual.GetTipo().Equals("Llave Izquierda"))
                {
                    emparejar("Llave Izquierda");
                    Valores();
                    Opcion3();
                    emparejar("Llave Derecha");
                    emparejar("Punto y Coma");
                }


            }
        }
        public void Ident_aux1() {
            Identificador();
            if (tokenActual.GetTipo().Equals("Punto y Coma"))
            {
                emparejar("Punto y Coma");
            }
            Opcion1();

        }

        public void Opcion1() {
            if (tokenActual.GetTipo().Equals("Coma"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Coma");
                Identificador();
                Opcion1();
            }
            else if (tokenActual.GetTipo().Equals("Signo igual"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Signo igual");
                Valores();
                Opcion1();
            }
            else {
                Final();
            }

        }
        public void Final() {
            if (tokenActual.GetTipo().Equals("Punto y Coma"))
            {
                emparejar("Punto y Coma");
                Subcomandos();
            }
            else { }
        }

        public void Ident_aux2()
        {
            Identificador();
            if (tokenActual.GetTipo().Equals("Signo igual"))
            {
                emparejar("Signo igual");
                Valores();
                Ident_aux2();
 
            }
            if (tokenActual.GetTipo().Equals("Coma"))
            {
                Identificador();
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Signo igual");
                Valores();
                Ident_aux2();
            }
            else {
                Final();
            }
        }
        public void Opcion2() {
            if (tokenActual.GetTipo().Equals("Coma"))
            {
                emparejar("Coma");
                Identificador();
                emparejar("Signo igual");
                Valores();
                Opcion2();

            }
            else {
                Final();
            }

        }

        public void Valores() {
            if (tokenActual.GetTipo().Equals("Numero entero"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Numero entero");
            }
            else if (tokenActual.GetTipo().Equals("Cadena"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Cadena");
            }
            else if (tokenActual.GetTipo().Equals("Numero float"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Numero float");
            }
            else if (tokenActual.GetTipo().Equals("Caracter"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Caracter");
            }
            else {
                Identificador();
            }

        }

        public void Opcion3() {
            if (tokenActual.GetTipo().Equals("Coma"))
            {
                emparejar("Coma");
                Valores();
                Opcion3();

            }
            else {
            }
        }


        public void TipoAsignacion() {
            Identificador();
            if (tokenActual.GetTipo().Equals("Signo igual"))
            {
                emparejar("Signo igual");
                AsignemosPue();
            }
            else {
                Subcomandos();
            }
        }
        public void AsignemosPue() {

            Valores();
            Opcion4();
            if (tokenActual.GetTipo().Equals("Llave Izquierda"))
            {
                emparejar("Llave Izquierda");
                Valores();
                Opcion3();
                emparejar("Llave Derecha");
                emparejar("Punto y Coma");

            }
        }

        public void Graficar_vector() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada GraficarVector"))
            {
                traduccion += "\n" + tokenActual.getAuxlex() + " ";
                emparejar("Palabra Reservada GraficarVector");
                traduccion += " " + tokenActual.getAuxlex() + " ";
                emparejar("Parentesis izquierda");
                traduccion += " " + tokenActual.getAuxlex() + " ";
                Identificador();
                traduccion += " " + tokenActual.getAuxlex() + " ";
                emparejar("Coma");
                traduccion += " " + tokenActual.getAuxlex() + " ";
                emparejar("Cadena");
                traduccion += " " + tokenActual.getAuxlex() + " ";
                emparejar("Parentesis derecha");
                Comandos();
            }
        }

        public void Opcion4() {
            if (tokenActual.GetTipo().Equals("Coma"))
            {
                emparejar("Coma");
                Identificador();
                Opcion4();
            }
            else if (tokenActual.GetTipo().Equals("Signo igual"))
            {
                emparejar("Signo igual");
                Valores();
                Opcion4();
            }
            else {
                Final();
                Subcomandos();
            }
        }

        public void Comentarios() {
            if (tokenActual.GetTipo().Equals("Comentario Lineal"))
            {
                traduccion += "\n #" + tokenActual.getAuxlex() + " ";
                emparejar("Comentario Lineal");
                Subcomandos();
            }
            else if (tokenActual.GetTipo().Equals("Comentario Multilineal"))
            {
                traduccion += "\n ..." + tokenActual.getAuxlex() + "... ";
                emparejar("Comentario Multilineal");
                Subcomandos();
            }
            else
            {

            }
        }

        public void Imprimir() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada Console"))
            {
                emparejar("Palabra Reservada Console");
                emparejar("Punto");
                emparejar("Palabra Reservada writeline");
                traduccion += "\n print" + tokenActual.getAuxlex() + " ";
                emparejar("Parentesis izquierda");
                traduccion += " " + tokenActual.getAuxlex() + " ";
                PosiblesPrint();
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Parentesis derecha");
                emparejar("Punto y Coma");
                Subcomandos();

            }
        }
        public void PosiblesPrint() {
            if (tokenActual.GetTipo().Equals("Cadena"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Cadena");
                PosiblesPrint2();
            }
            else {
                PosiblesConcatenaciones();
            }

        }

        public void PosiblesPrint2() {
            if (tokenActual.GetTipo().Equals("Signo mas"))
            {
                emparejar("Signo mas");
                PosiblesConcatenaciones();


            }
            else
            {

            }
        }

        public void PosiblesConcatenaciones() {
            Identificador();
            PosibleMatriz();
            if (tokenActual.GetTipo().Equals("Cadena"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Cadena");
                PosiblesPrint2();
            }
            else if (tokenActual.GetTipo().Equals("Numero"))
            {
                Jerarquia();
                PosiblesPrint2();
            }
            else if (tokenActual.GetTipo().Equals("Parentesis izquierda"))
            {
                Jerarquia();
                PosiblesPrint2();
            }
            else {
                Valores();
                PosiblesPrint2();
            }
        }

        public void PosibleMatriz() {
            if (tokenActual.GetTipo().Equals("Corchete Izquierda"))
            {
                emparejar("Corchete Izquierda");
                Valores();
                emparejar("Corchete Derecha");
                //Comandos();
            }
            else {
            }
        }

        public void Jerarquia() {
            if (tokenActual.GetTipo().Equals("Parentesis izquierda"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Parentesis izquieda");
                Operaciones();
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Parentesis derecha");
                PosiblesOpe();

            }
            else if (tokenActual.GetTipo().Equals("Corchete izquierda"))
            {
                if (tokenActual.GetTipo().Equals("Parentesis izquierda"))
                {
                    emparejar("Parentesis izquieda");
                    Operaciones();
                    emparejar("Parentesis derecha");
                    PosiblesOpe();

                }
                else
                {
                    emparejar("Corchete Izquieda");
                    Operaciones();
                    emparejar("Corchete Derecha");
                    PosiblesOpe();
                }
            }
            else {
                Operaciones();
            }
        }

        public void Operaciones() {
            Valores();
            Operador();
            Valores();
            PosiblesOpe();
        }

        public void PosiblesOpe() {
            if (tokenActual.GetTipo().Equals("Signo mas") || tokenActual.GetTipo().Equals("Signo menos") ||
                tokenActual.GetTipo().Equals("Signo Por") || tokenActual.GetTipo().Equals("Signo dividir"))
            {
                Operador();
                Valores();
                PosiblesOpe();
            }
            else {

            }

        }

        public void Operador() {
            if (tokenActual.GetTipo().Equals("Signo mas"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Signo mas");
            }
            else if (tokenActual.GetTipo().Equals("Signo menos"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Signo menos");
            }
            else if (tokenActual.GetTipo().Equals("Signo dividir"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Signo dividir");
            }
            else if (tokenActual.GetTipo().Equals("Signo Por"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Signo por");
            }
            else if (tokenActual.GetTipo().Equals("Signo igual relacional"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Signo igual relacional");
            }
            else if (tokenActual.GetTipo().Equals("Mayor que"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Mayor que");
            }
            else if (tokenActual.GetTipo().Equals("Menor que"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Menor que");
            }
            else if (tokenActual.GetTipo().Equals("Signo Diferente"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Signo Diferente");
            }
            else if (tokenActual.GetTipo().Equals("Mayor igual"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Mayor igual");
            }
            else if (tokenActual.GetTipo().Equals("Menor igual"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Menor igual");
            }
            else if (tokenActual.GetTipo().Equals("Disminucion"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Disminucion");
            }
            else if (tokenActual.GetTipo().Equals("Aumento"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";

                emparejar("Aumento");
            }
            else { }
        }

        public void IF() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada If"))
            {
                traduccion += "\n " + tokenActual.getAuxlex() + " )";
                emparejar("Palabra Reservada If");
                emparejar("Parentesis izquierda");
                Condicion();
                emparejar("Parentesis derecha");
                emparejar("Llave Izquierda");
                Subcomandos();
                emparejar("Llave Derecha");
                ELSE();
            }
        }
        public void Condicion() {

            Identificador();
            Operador();
            Valores();

        }

        public void ELSE() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada Else"))
            {
                traduccion += "\n " + tokenActual.getAuxlex() + " )";
                emparejar("Palabra Reservada Else");
                emparejar("Llave Izquierda");
                Subcomandos();
                emparejar("Llave Derecha");

            }
            else { }
        }

        public void FOR() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada For"))
            {
               // traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Palabra Reservada For");
                emparejar("Parentesis izquierda");
                Ini();
                emparejar("Punto y Coma");
                Condicion();
                emparejar("Punto y Coma");
                Incre_Decre();
                emparejar("Parentesis derecha");
                emparejar("Llave Izquierda");
                Subcomandos();
                emparejar("Llave Derecha");

            }
        }

        public void Ini()
        {
            Tipo();
            Identificador();
            if (tokenActual.GetTipo().Equals("Signo igual"))
            {
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Signo igual");
            }
            Valores();
        }

        public void Incre_Decre() {
            Identificador();
            TipoSigno();

        }
        public void TipoSigno() {
            if (tokenActual.GetTipo().Equals("Aumento"))
            {
                emparejar("Aumento");
            }
            else if (tokenActual.GetTipo().Equals("Disminucion"))
            {
                emparejar("Disminucion");
            }
            else { }
        }

        public void SWITCH() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada Switch"))
            {
                traduccion += "\n " + tokenActual.getAuxlex() + " )";
                emparejar("Palabra Reservada Switch");
                emparejar("Parentesis izquierda");
                Identificador();
                emparejar("Parentesis derecha");
                emparejar("Llave Izquierda");
                Cases();
                emparejar("Llave derecha");
            }
        }
        public void Cases() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada Case"))
            {
                traduccion += "2n " + tokenActual.getAuxlex() + " )";
                emparejar("Palabra Reservada Case");
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Numero entero");
                traduccion += " " + tokenActual.getAuxlex() + " )";
                emparejar("Dos puntos");
                Comandos();
                emparejar("Palabra Reservada Break");
                emparejar("Punto y Coma");


            }
            else if (tokenActual.GetTipo().Equals("Palabra Reservada Default"))
            {
                traduccion += "\n " + tokenActual.getAuxlex() + " )";
                emparejar("Palabra Reservada Default");

                emparejar("Dos puntos");
                Comandos();
                traduccion += "\n " + tokenActual.getAuxlex() + " )";
                emparejar("Palabra Reservada Break");
                emparejar("Punto y Coma");
            }
            else { }
        }

        public void WHILE() {
            if (tokenActual.GetTipo().Equals("Palabra Reservada While"))
            {

                emparejar("Palabra Reservada While");
                emparejar("Parentesis izquierda");
                Condicion();
                emparejar("Parentesis derecha");
                emparejar("Llave Izquierda");
                Comandos();
                emparejar("Llave derecha");
            }
        }

        public void agregarErrorSintax(ErrorSintax.Descripcion desc)
        {
            Manejador.Obtenerllamado().getErrorSintax().Add(new ErrorSintax(tokenActual.ToString(), desc));
            
            
        }
        public void emparejar(String tip)
        {
           if (!tokenActual.GetTipo().Equals(tip))
            {
                //ERROR si no viene lo que deberia
                Console.WriteLine("Error Sintactico" );
              
                agregarErrorSintax(ErrorSintax.Descripcion.ERROR_SINTACTICO);
               
            }
            if (!tokenActual.GetTipo().Equals("ULTIMO"))
            {
                controlToken += 1;
                tokenActual = listToken.ElementAt(controlToken);
            }
        }


        public void ErrorSintaxHTML()
        {


            // MessageBox.Show();
            String pagina;
            pagina = "<html>" +
            "<body bgcolor= #F5D0A9>" +
            "<h1 align='center'><U>TABLA DE TOKENS</U></h1></br>" +
            "<table cellpadding='20' border = '1' align='center'>" +
            "<tr>" +
            "<td bgcolor= #FF8000><strong>No." + "</strong></td>" +
            "<td bgcolor= #FF8000><strong>Tipo" + "</strong></td>" +
            "<td bgcolor= #FF8000><strong>Lexema" + "</strong></td>" +
            "<td bgcolor= #FF8000><strong>Columna" + "</strong></td>" +
            "<td bgcolor= #FF8000><strong>Fila" + "</strong></td>" +
            "<td bgcolor= #FF8000><strong>ID" + "</strong></td>" +
            "</tr>";
            String cadena = "";
            String t;

            for (int i = 0; i < Manejador.Obtenerllamado().getErrorSintax().Count; i++)
            {
                ErrorSintax lista = Manejador.Obtenerllamado().getErrorSintax().ElementAt(i);
                t = "";
                t = "<tr>" +
                "<td><strong>" + (i + 1).ToString() +
                "</strong></td>" +
                "<td>" + lista.Getdescripcion() +
                "</td>" +
                "<td>" + lista.getError() +
                "</td>" +
                 "</tr>";
                cadena = cadena + t;
            }
            pagina = pagina + cadena +
            "</table>" +
            "</body>" +
            "</html>";
            File.WriteAllText("SintaxError.html", pagina);
            System.Diagnostics.Process.Start("SintaxError.html");
        }
    }
}
