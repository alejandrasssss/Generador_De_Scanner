using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_LFA
{
    class Lectura
    {
        //Elimina espacios inecesarios
        public static string limpiarLinea(string txt)
        {
            char[] caracteresLimpiar = new char[2] { ' ', '\t' };
            return txt.Trim(caracteresLimpiar);
        }

        //Evalua cada set
        public static string sets(string set, int numLinea)
        {
            int caracterNum = 0;
            int conteo = 0;
            while (caracterNum < set.Length)
            {
                if (char.IsUpper(set[caracterNum]))
                {
                    caracterNum++;
                }
                else
                {
                    break;
                }
            }

            if (caracterNum == 0)
            {
                Console.WriteLine("ERROR " + numLinea + " LINEA");
            }
            else
            {
                if (Arbol.sets.Contains(set.Substring(0, caracterNum)))
                {
                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                }
                else
                {
                    Arbol.sets.Add(set.Substring(0, caracterNum));
                    set = limpiarLinea(set.Substring(caracterNum));
                    caracterNum = 0;
                    conteo = 0;
                    if (set.Length == 0 && set[caracterNum] != '=')
                    {
                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                        return "0";
                    }
                    else
                    {
                        set = limpiarLinea(set.Substring(1));
                        while (set.Length != 0)
                        {
                            if (conteo == 0)
                            {
                                if (set.Length >= 1 && set[0] == '\'')
                                {
                                    set = set.Substring(1);

                                    if (set.Length >= 1 && set[0] > 31 && set[0] < 256)
                                    {
                                        set = set.Substring(1);
                                        if (set.Length >= 1 && set[0] == '\'')
                                        {
                                            set = set.Substring(1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                            return "0";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                        return "0";
                                    }

                                    if (set.Length >= 3 && set.Substring(0, 3) == "..'")
                                    {
                                        set = set.Substring(3);

                                        if (set.Length >= 1 && set[0] > 31 && set[0] < 256)
                                        {
                                            set = set.Substring(1);
                                            if (set.Length >= 1 && set[0] == '\'')
                                            {
                                                set = set.Substring(1);
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                return "0";
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                            return "0";
                                        }
                                    }

                                    conteo = 1;
                                }
                                else if (set.Length >= 3 && set.Substring(0, 3) == "CHR")
                                {
                                    set = set.Substring(3);
                                    if (set.Length >= 1 && set[0] == '(')
                                    {
                                        set = set.Substring(1);
                                        while (set.Length > caracterNum && set.Length != 0)
                                        {
                                            if (char.IsDigit(set[caracterNum]))
                                            {
                                                caracterNum++;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        if (caracterNum == 0)
                                        {
                                            Console.WriteLine("ERROR: " + numLinea);
                                            return "0";
                                        }
                                        else
                                        {
                                            set = set.Substring(caracterNum);
                                            caracterNum = 0;
                                            if (set.Length >= 1 && set[0] == ')')
                                            {
                                                set = set.Substring(1);
                                                conteo = 1;
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR: " + numLinea);
                                                return "0";
                                            }
                                        }

                                        if (set.Length >= 2 && set.Substring(0, 2) == "..")
                                        {
                                            set = set.Substring(2);
                                            if (set.Length >= 3 && set.Substring(0, 3) == "CHR")
                                            {
                                                set = set.Substring(3);
                                                if (set.Length >= 1 && set[0] == '(')
                                                {
                                                    set = set.Substring(1);
                                                    caracterNum = 0;
                                                    while (set.Length != 0)
                                                    {
                                                        if (set.Length > caracterNum && char.IsDigit(set[caracterNum]))
                                                        {
                                                            caracterNum++;
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    if (caracterNum == 0)
                                                    {
                                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                        return "0";
                                                    }
                                                    else
                                                    {
                                                        set = set.Substring(caracterNum);
                                                        if (set.Length >= 1 && set[0] == ')')
                                                        {
                                                            set = limpiarLinea(set.Substring(1));
                                                            conteo = 1;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                            return "0";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                    return "0";
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                return "0";
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                            return "0";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                        return "0";
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                                    return "0";
                                }
                            }
                            else
                            {
                                if (set.Length >= 2 && set[0] == '+')
                                {
                                    set = limpiarLinea(set.Substring(1));
                                    conteo = 0;
                                }
                                else
                                {
                                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                                    return "0";
                                }
                            }
                        }
                        if (conteo == 0)
                        {
                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                            return "0";
                        }
                    }
                }
            }

            return set;
        }

        //Evalua cada token
        public static string tokens(string token, int numLinea)
        {
            int caracterNum = 0;
            int conteo = 0;
            if (token.Length <= 4 && token.Substring(0, 5) != "TOKEN")
            {
                Console.WriteLine("ERROR " + numLinea + " LINEA");
                return "0";
            }
            else
            {
                token = token.Substring(5);
                if (token.Length >= 1 && (token[0] == ' ' || token[0] == '\t'))
                {
                    token = limpiarLinea(token.Substring(1));
                    while (token.Length > caracterNum && token.Length != 0)
                    {
                        if (char.IsDigit(token[caracterNum]))
                        {
                            caracterNum++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (caracterNum == 0)
                    {
                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                        return "0";
                    }
                    else
                    {
                        token = limpiarLinea(token.Substring(caracterNum));

                        if (token.Length == 0 && token[0] != '=')
                        {
                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                            return "0";
                        }
                        else
                        {
                            token = limpiarLinea(token.Substring(1));


                            while (token.Length != 0)
                            {
                                if (token.Length >= 1 && token[0] == '{')
                                {
                                    token = limpiarLinea(token.Substring(1));
                                    if (token.Length  >= 12 && token.Substring(0, 12) == "RESERVADAS()")
                                    {
                                        token = limpiarLinea(token.Substring(12));
                                        if (token == "}")
                                        {
                                            token = limpiarLinea(token.Substring(1));
                                        }
                                        else
                                        {
                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                            return "0";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                        return "0";
                                    }
                                }
                                else if (conteo == 1 && token.Length >= 1 && (token[0] == '+' || token[0] == '*' || token[0] == '?'))
                                {
                                    Arbol.ExpresionRegular = Arbol.ExpresionRegular.TrimEnd('.') + token.Substring(0, 1) + ".";
                                    token = limpiarLinea(token.Substring(1));
                                    conteo = 2;
                                }
                                else if ((conteo == 1 || conteo == 2 )&& token.Length >= 1 && token[0] == '|')
                                {
                                    Arbol.ExpresionRegular = Arbol.ExpresionRegular.TrimEnd('.') + token.Substring(0, 1);
                                    token = limpiarLinea(token.Substring(1));
                                    if (token.Length >= 1)
                                    {
                                        if (char.IsUpper(token[0]))
                                        {
                                            caracterNum = 0;
                                            while (caracterNum < token.Length)
                                            {
                                                if (char.IsUpper(token[caracterNum]))
                                                {
                                                    caracterNum++;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            if (caracterNum == 0)
                                            {
                                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                return "0";
                                            }
                                            else
                                            {
                                                if (!Arbol.sets.Contains(token.Substring(0, caracterNum)))
                                                {
                                                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                    return "0";
                                                }
                                                Arbol.ExpresionRegular = Arbol.ExpresionRegular + token.Substring(0, caracterNum) + ".";
                                                token = limpiarLinea(token.Substring(caracterNum));
                                                conteo = 1;
                                            }
                                        }
                                        else if (token[0] == '\'')
                                        {
                                            string actual = token.Substring(0, 1);
                                            token = token.Substring(1);
                                            if (token.Length >= 1 && token[0] > 31 && token[0] < 256)
                                            {
                                                actual += token.Substring(0, 1);
                                                token = token.Substring(1);
                                                if (token.Length >= 1 && token[0] == '\'')
                                                {
                                                    actual += token.Substring(0, 1);
                                                    Arbol.ExpresionRegular = Arbol.ExpresionRegular + actual + ".";
                                                    token = limpiarLinea(token.Substring(1));
                                                    conteo = 1;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                    return "0";
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                return "0";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                        return "0";
                                    }

                                }
                                else if (token.Length >= 1 && token[0] == '(')
                                {
                                    Arbol.ExpresionRegular = Arbol.ExpresionRegular + token.Substring(0, 1);
                                    token = limpiarLinea(token.Substring(1));
                                    conteo = 0;
                                    bool termino = false;
                                    while (token.Length != 0)
                                    {
                                        if ((conteo == 1 || conteo == 2) && token.Length >= 1 && token[0] == ')')
                                        {
                                            Arbol.ExpresionRegular = Arbol.ExpresionRegular.TrimEnd('.') + token.Substring(0, 1) + ".";
                                            token = limpiarLinea(token.Substring(1));
                                            conteo = 1;
                                            termino = true;
                                            break;
                                        }
                                        else if (conteo == 1 && token.Length >= 1 && (token[0] == '+' || token[0] == '*' || token[0] == '?'))
                                        {
                                            Arbol.ExpresionRegular = Arbol.ExpresionRegular.TrimEnd('.') + token.Substring(0, 1) + ".";
                                            token = limpiarLinea(token.Substring(1));
                                            conteo = 2;
                                        }
                                        else if ((conteo == 1 || conteo == 2) && token.Length >= 1 && token[0] == '|')
                                        {
                                            Arbol.ExpresionRegular = Arbol.ExpresionRegular.TrimEnd('.') + token.Substring(0, 1);
                                            token = limpiarLinea(token.Substring(1));
                                            if (token.Length >= 1)
                                            {
                                                if (char.IsUpper(token[0]))
                                                {
                                                    caracterNum = 0;
                                                    while (caracterNum < token.Length)
                                                    {
                                                        if (char.IsUpper(token[caracterNum]))
                                                        {
                                                            caracterNum++;
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    if (caracterNum == 0)
                                                    {
                                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                        return "0";
                                                    }
                                                    else
                                                    {
                                                        if (!Arbol.sets.Contains(token.Substring(0, caracterNum)))
                                                        {
                                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                            return "0";
                                                        }
                                                        Arbol.ExpresionRegular = Arbol.ExpresionRegular + token.Substring(0, caracterNum) + ".";
                                                        token = limpiarLinea(token.Substring(caracterNum));
                                                        conteo = 1;
                                                    }
                                                }
                                                else if (token[0] == '\'')
                                                {
                                                    var actual = token.Substring(0, 1);
                                                    token = token.Substring(1);
                                                    if (token.Length >= 1 && token[0] > 31 && token[0] < 256)
                                                    {
                                                        actual += token.Substring(0, 1);
                                                        token = token.Substring(1);
                                                        if (token.Length >= 1 && token[0] == '\'')
                                                        {
                                                            actual += token.Substring(0, 1);
                                                            Arbol.ExpresionRegular = Arbol.ExpresionRegular + actual + ".";
                                                            token = limpiarLinea(token.Substring(1));
                                                            conteo = 1;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                            return "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                        return "0";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                return "0";
                                            }
                                        }
                                        else if (conteo == 0 || conteo == 1 || conteo == 2)
                                        {
                                            if (token.Length >= 1)
                                            {
                                                if (char.IsUpper(token[0]))
                                                {
                                                    caracterNum = 0;
                                                    while (caracterNum < token.Length)
                                                    {
                                                        if (char.IsUpper(token[caracterNum]))
                                                        {
                                                            caracterNum++;
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    if (caracterNum == 0)
                                                    {
                                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                        return "0";
                                                    }
                                                    else
                                                    {
                                                        if (!Arbol.sets.Contains(token.Substring(0, caracterNum)))
                                                        {
                                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                            return "0";
                                                        }
                                                        Arbol.ExpresionRegular = Arbol.ExpresionRegular + token.Substring(0, caracterNum) + ".";
                                                        token = limpiarLinea(token.Substring(caracterNum));
                                                        conteo = 1;
                                                    }
                                                }
                                                else if (token[0] == '\'')
                                                {
                                                    var actual = token.Substring(0, 1);
                                                    token = token.Substring(1);
                                                    if (token.Length >= 1 && token[0] > 31 && token[0] < 256)
                                                    {
                                                        actual += token.Substring(0, 1);
                                                        token = token.Substring(1);
                                                        if (token.Length >= 1 && token[0] == '\'')
                                                        {
                                                            actual += token.Substring(0, 1);
                                                            Arbol.ExpresionRegular = Arbol.ExpresionRegular + actual + ".";
                                                            token = limpiarLinea(token.Substring(1));
                                                            conteo = 1;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                            return "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                        return "0";
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                                            return "0";
                                        }
                                    }
                                    if (!termino)
                                    {
                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                        return "0";
                                    }
                                }
                                else if (conteo == 0 || conteo == 1 || conteo == 2)
                                {
                                    if (token.Length >= 1)
                                    {
                                        if (char.IsUpper(token[0]))
                                        {
                                            caracterNum = 0;
                                            while (caracterNum < token.Length)
                                            {
                                                if (char.IsUpper(token[caracterNum]))
                                                {
                                                    caracterNum++;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            if (caracterNum == 0)
                                            {
                                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                return "0";
                                            }
                                            else
                                            {
                                                if (!Arbol.sets.Contains(token.Substring(0, caracterNum)))
                                                {
                                                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                    return "0";
                                                }
                                                Arbol.ExpresionRegular = Arbol.ExpresionRegular + token.Substring(0, caracterNum) + ".";
                                                token = limpiarLinea(token.Substring(caracterNum));
                                                conteo = 1;
                                            }
                                        }
                                        else if (token[0] == '\'')
                                        {
                                            var actual = token.Substring(0, 1);
                                            token = token.Substring(1);
                                            if (token.Length >= 1 && token[0] > 31 && token[0] < 256)
                                            {
                                                actual += token.Substring(0, 1);
                                                token = token.Substring(1);
                                                if (token.Length >= 1 && token[0] == '\'')
                                                {
                                                    actual += token.Substring(0, 1);
                                                    Arbol.ExpresionRegular = Arbol.ExpresionRegular + actual + ".";
                                                    token = limpiarLinea(token.Substring(1));
                                                    conteo = 1;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                    return "0";
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                return "0";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                                        return "0";
                                    }
                                    
                                }
                            }
                        }
                        if (conteo == 0)
                        {
                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                            return "0";
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                }
            }
            return token;
        }

        //Evalua cada action
        public static string actions(string action, int numLinea)
        {
            int caracterNum = 0;
            while (action.Length != 0)
            {
                if (action.Length > caracterNum && char.IsDigit(action[caracterNum]))
                {
                    caracterNum++;
                }
                else
                {
                    break;
                }
            }
            if (caracterNum == 0)
            {
                Console.WriteLine("ERROR " + numLinea + " LINEA");
                return "0";
            }
            else
            {
                action = limpiarLinea(action.Substring(caracterNum));
                caracterNum = 0;
                if (action.Length == 0 && action[caracterNum] != '=')
                {
                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                    return "0";
                }
                else
                {
                    action = limpiarLinea(action.Substring(1));

                    if (action.Length == 0 && action[0] != '\'')
                    {
                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                        return "0";
                    }
                    else
                    {
                        action = action.Substring(1);
                        caracterNum = 0;
                        while (caracterNum < action.Length)
                        {
                            if (char.IsUpper(action[caracterNum]))
                            {
                                caracterNum++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (caracterNum == 0)
                        {
                            Console.WriteLine("ERROR " + numLinea + " LINEA");
                        }
                        else
                        {
                            action = limpiarLinea(action.Substring(caracterNum));
                            if (action.Length == 0 && action[caracterNum] != '\'')
                            {
                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                return "0";
                            }
                            else
                            {
                                action = limpiarLinea(action.Substring(1));
                            }
                        }
                    }
                }
            }
            return action;
        }

        public static string errors(string error, int numLinea)
        {

            if (error.Length >= 5 && error.Substring(0, 5) == "ERROR")
            {
                error = limpiarLinea(error.Substring(5));
                int caracterNum = 0;
                if (error.Length == 0 && error[caracterNum] != '=')
                {
                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                    return "0";
                }
                else
                {
                    error = limpiarLinea(error.Substring(1));
                    caracterNum = 0;
                    while (error.Length != 0)
                    {
                        if (error.Length > caracterNum && char.IsDigit(error[caracterNum]))
                        {
                            caracterNum++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (caracterNum == 0)
                    {
                        Console.WriteLine("ERROR " + numLinea + " LINEA");
                        return "0";
                    }
                    else
                    {
                        error = limpiarLinea(error.Substring(caracterNum));
                    }
                }
            }
            else
            {
                Console.WriteLine("ERROR " + numLinea + " LINEA");
                return "0";
            }
            
            return error;
        }
    }
}
