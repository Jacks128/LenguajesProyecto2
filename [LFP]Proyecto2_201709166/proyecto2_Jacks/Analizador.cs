using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace proyecto2_Jacks
{
    class Analizador
    {
        private int estado;
        private int inicial;
        private static int id;
        private static int columna = 0;
        private static int fila = 1;
        private String auxlex;
        private String error;
        DateTime fecha = DateTime.Now;

        public static string[] pal_reserv = { "int", "float", "char","string","String","bool","class","static","void","Main","args","Console","WriteLine","graficarVector","if","else","switch","case",
            "break", "default", "for","while" };
        public Boolean reservadas(String c)
        {
            for (int i = 0; i < pal_reserv.Length; i++)
            {
                if (String.Compare( c, pal_reserv[i], true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Scanear(String Entrada)
        {
            Entrada = Entrada + '#';
            estado = 0;
            id = 0;
            inicial = 100;
            auxlex = "";
            error = "";
            char c;
            for (int i = 0; i <= Entrada.Length - 1; i++)
            {
                c = Entrada.ElementAt(i);
                columna++;
                switch (estado)
                {
                    case 0:/*Estado Inicial y simbolos*/

                        if (char.IsLetter(c))/* palabra reservada y id*/
                        {
                            estado = 1;
                            auxlex += c;
                        }
                        else if (c == '"')/*Cadena*/
                        {
                            estado = 3;
                            auxlex += c;
                        }
                        else if (char.IsDigit(c))/*Numero*/
                        {
                            estado = 4;
                            auxlex += c;
                        }
                        else if (c == '/')/*comentarios*/
                        {
                            estado = 5;
                            auxlex += c;
                        }
                        else if (c == '\'')/*Caracter*/
                        {
                            estado = 9;
                            auxlex += c;
                        }
                        
                        else if (c == '\r' || c == '\t' || c == '\b' || Char.IsWhiteSpace(c) || c == '\f')
                        {

                            //estado = 0;
                            fila++;
                            columna = 0;
                        }
                        else if (c == '\n')
                        {
                            columna = 0;
                        }

                        else if (c == '{')
                        {
                            auxlex += c;
                            id = 7;
                            agregarToken(Token.Tipo.LLAVE_IZQ);
                        }
                        else if (c == '}')
                        {
                            auxlex += c;
                            id = 8;
                            agregarToken(Token.Tipo.LLAVE_DER);
                        }
                        else if (c == '[')
                        {
                            auxlex += c;
                            id = 9;
                            agregarToken(Token.Tipo.CORCHETE_IZQ);
                        }
                        else if (c == ']')
                        {
                            auxlex += c;
                            id = 10;
                            agregarToken(Token.Tipo.CORCHETE_DER);
                        }
                        else if (c == '(')
                        {
                            auxlex += c;
                            id = 11;
                            agregarToken(Token.Tipo.PARENTESIS_IZQ);
                        }
                        else if (c == ')')
                        {
                            auxlex += c;
                            id = 12;
                            agregarToken(Token.Tipo.PARANTESIS_DER);
                        }
                        else if (c == '=')
                        {
                            auxlex += c;
                            estado = 11;
                            //id = 13;
                            //agregarToken(Token.Tipo.SIGNO_IGUAL);
                        }
                        
                        else if (c.CompareTo(';') == 0)
                        {
                            auxlex += c;
                            id = 14;
                            agregarToken(Token.Tipo.PUNTO_COMA);
                        }

                        else if (c == '.')
                        {
                            auxlex += c;
                            id = 15;
                            agregarToken(Token.Tipo.PUNTO);
                        }
                        else if (c.CompareTo(',')==0)
                        {
                            auxlex += c;
                            id = 16;
                            agregarToken(Token.Tipo.COMA);
                        }
                        else if (c== '>')
                        {
                            auxlex += c;
                            estado = 13;
                            //id = 17;
                           // agregarToken(Token.Tipo.MAYOR_QUE);
                        }
                        else if (c == '<')
                        {
                            auxlex += c;
                            estado = 12;
                           // id = 18;
                            //gregarToken(Token.Tipo.MENOR_QUE);
                        }
                        else if (c == '+')
                        {
                            auxlex += c;
                            estado = 15;
                            //id = 19;
                            //agregarToken(Token.Tipo.SIGNO_MAS);
                        }
                        else if (c == '-')
                        {
                            auxlex += c;
                            estado = 14;
                            //id = 20;
                            //agregarToken(Token.Tipo.SIGNO_MENOS);
                        }
                        else if (c == '*')
                        {
                            auxlex += c;
                            id = 21;
                            agregarToken(Token.Tipo.SIGNO_POR);
                        }
                        else if (c.CompareTo('/')==0)
                        {
                            auxlex += c;
                            id = 22;
                            agregarToken(Token.Tipo.SIGNO_DIVIDIR);
                        }
                        else if (c.CompareTo(':') == 0)
                        {
                            auxlex += c;
                            id = 12;
                            agregarToken(Token.Tipo.DOS_PUNTOS);
                        }
                        else if (c.CompareTo('!') == 0) //diferendte
                        {
                            auxlex += c;
                            estado = 10;
                            
                        }
                       

                        else if (c.CompareTo('@') == 0)
                        {
                            auxlex += c;
                            error += c;
                            agregarError(Error.Descripcion.CARACTER_DESCONOCIDO);
                        }
                       
                        else if (c.CompareTo('?') == 0)
                        {
                            auxlex += c;
                            error += c;
                            agregarError(Error.Descripcion.CARACTER_DESCONOCIDO);
                        }
                        else
                        {
                            if (c.CompareTo('#') == 0 && i == Entrada.Length - 1)
                            {
                                Console.WriteLine("Hemos concluido el analisis con exito");
                                estado = 0;
                            }
                            else
                            {
                                Console.WriteLine("Error lexico en " + c);
                                estado = 0;
                                error += c;
                                agregarError(Error.Descripcion.CARACTER_DESCONOCIDO);


                            }
                        }

                        break;

                    case 1: //reservadas
                        if (char.IsLetter(c)||c=='_')
                        {
                            estado = 1;
                            auxlex += c;
                        }
                        else if (char.IsDigit(c))
                        {
                            estado = 2;
                            auxlex += c;
                          
                        }else{
                            estado = 0;
                            switch (auxlex) {
                                case "int":
                                    agregarToken(Token.Tipo.RES_INT);
                                    break;
                                case "float":
                                    agregarToken(Token.Tipo.RES_FLOAT);
                                    break;
                                case "char":
                                    agregarToken(Token.Tipo.RES_CHAR);
                                    break;
                                case "string":
                                    agregarToken(Token.Tipo.RES_STRING);
                                    break;
                                case "String":
                                    agregarToken(Token.Tipo.RES_STRING);
                                    break;
                                case "bool":
                                    agregarToken(Token.Tipo.RES__BOOL);
                                    break;
                                case "int[]":
                                    agregarToken(Token.Tipo.ARREGLO_INT);
                                    break;
                               
                                case "class":
                                    agregarToken(Token.Tipo.RES_CLASS);
                                    break;
                                case "static":
                                    agregarToken(Token.Tipo.RES_STATIC);
                                    break;
                                case "void":
                                    agregarToken(Token.Tipo.RES_VOID);
                                    break;
                               
                                case "args":
                                    agregarToken(Token.Tipo.RES_ARGS);
                                    break;
                                case "Console":
                                    agregarToken(Token.Tipo.RES_CONSOLE);
                                    break;
                                case "WriteLine":
                                    agregarToken(Token.Tipo.RES_WRITELINE);
                                    break;
                                case "graficarVector":
                                    agregarToken(Token.Tipo.RES_GRAFICARVECTOR);
                                    break;
                                case "if":
                                    agregarToken(Token.Tipo.RES_IF);
                                    break;
                                case "else":
                                    agregarToken(Token.Tipo.RES_ELSE);
                                    break;
                                case "switch":
                                    agregarToken(Token.Tipo.RES_SWITCH);
                                    break;
                                case "case":
                                    agregarToken(Token.Tipo.RES_CASE);
                                    break;
                                case "break":
                                    agregarToken(Token.Tipo.RES_BREAK);
                                    break;
                                case "default":
                                    agregarToken(Token.Tipo.RES_DEFAULT);
                                    break;
                                case "for":
                                    agregarToken(Token.Tipo.RES_FOR);
                                    break;
                                case "while":
                                    agregarToken(Token.Tipo.RES_WHILE);
                                    break;
                                case "new":
                                    agregarToken(Token.Tipo.RES_NEW);
                                    break;
                                default:
                                    agregarToken(Token.Tipo.ID);
                                   
                                    break;
                                   

                            }i--;
                          

                            // Console.WriteLine("Hemos concluido el analisis con exito");

                        }
                        break;

                    case 2://identidicadors
                         if (char.IsLetterOrDigit(c)||c=='_')
                        {
                            auxlex += c;
                            id = 2;
                            estado = 2;
                            //agregarToken(Token.Tipo.ID);
                        
                        
                        }else
                        {
                            agregarToken(Token.Tipo.ID);
                           i--;
                        }
                        break;

                    case 3://cadena
                        if (char.IsLetter(c))
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (char.IsDigit(c))
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '@')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '!')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                       

                        else if (c == '\\')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '$')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '%')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '&')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '(')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == ')')
                        {
                            auxlex += c;
                            estado = 3;
                        }

                        else if (c == ':')
                        {
                            auxlex += c;
                            estado = 3;
                        }

                        else if (c == '.')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == ',')
                        {
                            auxlex += c;
                            estado = 3;
                        }

                        else if (c == '?')
                        {
                            auxlex += c;
                            estado = 3;
                        }

                        else if (c == '¿')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '<')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '>')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '#')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '+')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '-')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '*')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '/')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '=')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c=='\n')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '\r' || c == '\t' || c == '\b' || Char.IsWhiteSpace(c) || c == '\f')
                        {
                            auxlex += c;
                            estado = 3;
                        }
                        else if (c == '"')
                        {
                            auxlex += c;
                            id = 3;
                            agregarToken(Token.Tipo.CADENA);
                        }
                        else
                        {
                            if (c.CompareTo('#') == 0 && i == Entrada.Length - 1)
                            {
                                Console.WriteLine("Hemos concluido el analisis con exito");
                            }
                            else
                            {
                                Console.WriteLine("Error lexico en " + c);
                                estado = 0;
                                error += c;
                                agregarError(Error.Descripcion.CARACTER_DESCONOCIDO);
                            }
                        }

                        break;

                    case 4://numeros
                        if (char.IsDigit(c))
                        {
                            auxlex += c;
                            estado = 4;

                        }
                        else if (c == '.')
                        {
                            auxlex += c;
                            estado = 17;
                        }
                        else
                        {

                            id = 4;
                            agregarToken(Token.Tipo.NUMERO);

                            i--;



                        }
                        
                        break;

                    case 5:
                        if (c=='/')
                        {
                             estado = 6;
                            auxlex += c;
                           //comentario de una linea
                        }
                        else if (c=='*')/*comentairo mayor*/
                        {
                            auxlex += c;
                            estado = 7;

                        }
                        else
                        {
                            agregarToken(Token.Tipo.SIGNO_DIVIDIR);
                            i--;
                            
                        }

                        break;

                    case 6:  //comentario de una sola linea
                       if (c == '\n')
                        {
                           // auxlex += c;
                            id = 5;
                            agregarToken(Token.Tipo.COMENTARIO_LINEAL);
                            i--;
                        }
                        else
                        {
                            estado = 6;
                            auxlex += c;
                        }

                        break;

                    case 7:  // comentario largo
                        if (c == '*')
                        {
                          auxlex += c;
                           
                            estado = 8;
                           // agregarToken(Token.Tipo.);
                        }
                        else
                        {
                            estado = 7;
                            auxlex += c;
                        }
                        break;
                    case 8:
                        if (c == '/')
                        {
                           auxlex += c;
                           id = 5;
                            estado = 16;
                        }
                        else
                        {
                            if (c.CompareTo('#') == 0 && i == Entrada.Length - 1)
                            {
                                Console.WriteLine("Error lexico en " + c);
                                estado = 0;
                                error += c;
                                agregarError(Error.Descripcion.CARACTER_DESCONOCIDO);
                            }
                            else
                            {
                                Console.WriteLine("Error lexico en " + c);
                                estado = 0;
                                error += c;
                                agregarError(Error.Descripcion.CARACTER_DESCONOCIDO);
                                // Console.WriteLine("Hemos concluido el analisis con exito");
                            }
                        }
                        break;

                    case 9:
                        if (char.IsLetter(c))
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (char.IsDigit(c))
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '@')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '/')
                        {
                            auxlex += c;
                            estado = 7;
                        }

                        else if (c == '\\')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '$')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '%')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '&')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '(')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == ')')
                        {
                            auxlex += c;
                            estado = 7;
                        }

                        else if (c == ':')
                        {
                            auxlex += c;
                            estado = 7;
                        }

                        else if (c == '.')
                        {
                            auxlex += c;
                            estado = 7;
                        }

                        else if (c == '?')
                        {
                            auxlex += c;
                            estado = 7;
                        }

                        else if (c == '¿')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '<')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '>')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '#')
                        {
                            auxlex += c;
                            estado = 7;
                        }

                        else if (Char.IsWhiteSpace(c))
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '\r' || c == '\t' || c == '\b' || Char.IsWhiteSpace(c) || c == '\f')
                        {
                            auxlex += c;
                            estado = 7;
                        }
                        else if (c == '\'')
                        {
                            auxlex += c;
                            id = 6;
                            //estado = 8;
                             agregarToken(Token.Tipo.CARACTER);
                        }
                        else
                        {
                            if (c.CompareTo('#') == 0 && i == Entrada.Length - 1)
                            {
                                Console.WriteLine("Hemos concluido el analisis con exito");
                            }
                            else
                            {
                                Console.WriteLine("Error lexico en " + c);
                                estado = 0;
                                error += c;
                                agregarError(Error.Descripcion.CARACTER_DESCONOCIDO);
                            }
                        }


                        break;

                    case 10:
                        if (c=='=')
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.DIFERENTE);
                        }
                        else
                        {
                            if (c.CompareTo('#') == 0 && i == Entrada.Length - 1)
                            {
                                Console.WriteLine("Hemos concluido el analisis con exito");
                            }
                            else
                            {
                                // Console.WriteLine("Error lexico en " + c);
                                estado = 0;
                            }
                        }
                        break;

                    case 11:
                        if (c == '=')
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.IGUAL_RELACIONAL);
                        }
                        else {
                            //auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_IGUAL);
                            i--;
                            

                        }
                        break;

                    case 12:
                        if (c == '=')
                        {
                            auxlex +=c;
                            agregarToken(Token.Tipo.MENOR_IGUAL);
                            
                        }
                        else
                        {
                            //auxlex += c;
                            agregarToken(Token.Tipo.MENOR_QUE);
                            i--;
                        }
                       
                        break;

                    case 13:
                        if (c == '=')
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.MAYOR_IGUAL);
                        }
                        else 
                        {
                            //auxlex += c;
                            agregarToken(Token.Tipo.MAYOR_QUE);
                            i--;
                           
                        }
                        break;

                    case 14:
                        if (c == '-')
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.DISMINUCION);
                        }
                        else
                        {
                           // auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_MENOS);
                            i--;
                        }
                        break;
                    case 15:
                        if (c == '+')
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.AUMENTO);
                        }
                        else
                        {
                           // auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_MAS);
                            i--;
                            
                        }
                        break;
                    case 16:
                        agregarToken(Token.Tipo.COMENTARIO_MULTILINE);
                        i--;
                        break;

                    case 17:
                        if (char.IsLetterOrDigit(c))
                        {
                            auxlex += c;
                            estado = 18;
                        }
                        break;
                    case 18:
                        if (char.IsDigit(c))
                        {
                            auxlex += c;
                            estado = 18;
                        }
                        else {
                            agregarToken(Token.Tipo.NUMERO_FLOAT);
                            i--;
                        }
                        


                        break;
                }
            }
        }
        public void agregarToken(Token.Tipo tipo)
        {
            Manejador.Obtenerllamado().getsalidaToken().Add(new Token(auxlex, tipo, fila, columna, id));
            auxlex = "";
            estado = 0;
        }

        public void agregarError(Error.Descripcion desc)
        {
            Manejador.Obtenerllamado().getsalidaError().Add(new Error(error, desc, fila, columna));
            error = "";
            estado = 0;
        }


        public void TokenHTML()
        {


            // MessageBox.Show();
            String pagina;
            pagina = "<html>" +
            "<body bgcolor= #F5D0A9>" +
            "<h1 align='center'><U>TABLA DE TOKENS</U></h1></br>" +
             "<h2 align='right'><U>Fecha y hora "+ fecha +": </U></h2></br>" +
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

            for (int i = 0; i < Manejador.Obtenerllamado().getsalidaToken().Count; i++)
            {
                Token lista = Manejador.Obtenerllamado().getsalidaToken().ElementAt(i);
                t = "";
                t = "<tr>" +
                "<td><strong>" + (i + 1).ToString() +
                "</strong></td>" +
                "<td>" + lista.GetTipo() +
                "</td>" +
                "<td>" + lista.getAuxlex() +
                "</td>" +
                "<td>" + lista.GetColumna() +
                "</td>" +
                "<td>" + lista.GetFila() +
                "</td>" +
                 "<td>" + lista.GetId() +
                "</td>" +
                "</tr>";
                cadena = cadena + t;
            }
            pagina = pagina + cadena +
            "</table>" +
            "</body>" +
            "</html>";
            File.WriteAllText("Tokens.html", pagina);
            System.Diagnostics.Process.Start("Tokens.html");
        }

        public void ErrorHTML()
        {
            String pagina1;
            pagina1 = "<html>" +
            "<body bgcolor=#F5D0A9>" +
            "<h1 align='center'><U>TABLA DE ERRORES</U></h1></br>" +
            "<table cellpadding='20' border = '1' align='center'>" +
            "<tr>" +
            "<td bgcolor= #FF0040><strong>No." + "</strong></td>" +
            "<td bgcolor= #FF0040><strong>Tipo" + "</strong></td>" +
            "<td bgcolor= #FF0040><strong>Lexema" + "</strong></td>" +
            "<td bgcolor= #FF0040><strong>Columna" + "</strong></td>" +
            "<td bgcolor= #FF0040><strong>Fila" + "</strong></td>" +
            "</tr>";
            String cadena1 = "";
            String t;

            for (int i = 0; i < Manejador.Obtenerllamado().getsalidaError().Count; i++)
            {
                Error lista = Manejador.Obtenerllamado().getsalidaError().ElementAt(i);
                t = "";
                t = "<tr>" +
                "<td><strong>" + (i + 1).ToString() +
                "</strong></td>" +
                "<td>" + lista.Getdescripcion() +
                "</td>" +
                "<td>" + lista.getError() +
                "</td>" +
                "<td>" + lista.Getcolumna() +
                "</td>" +
                "<td>" + lista.Getfila() +
                "</td>" +
                "</tr>";
                cadena1 = cadena1 + t;
            }
            pagina1 = pagina1 + cadena1 +
            "</table>" +
            "</body>" +
            "</html>";
            File.WriteAllText("Errores.html", pagina1);
            System.Diagnostics.Process.Start("Errores.html");
        }


    }




}
