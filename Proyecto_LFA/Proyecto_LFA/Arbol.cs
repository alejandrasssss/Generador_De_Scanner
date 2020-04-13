using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    class Arbol
    {
        public static List<string> sets;
        public static string ExpresionRegular;
        public static List<string> metaCaracteres = new List<string> { "?", "+", "*", "|", "." };
        public static int numSet;
        public static List<string> setsPostOrden;
        public static List<string> ordenEstados;
        public static Dictionary<string, string> follow;
        public static Dictionary<string, Dictionary<string, string>> estados;
        public static List<string> listaEspera;

        public static void ReiniciarArbol()
        {
            sets = new List<string>();
            ExpresionRegular = "";
            numSet = 1;
            setsPostOrden = new List<string>();
            ordenEstados = new List<string>();
            follow = new Dictionary<string, string>();
            estados = new Dictionary<string, Dictionary<string, string>>();
            listaEspera = new List<string>();
        }

        public static Nodo ShuntingYard()
        {
            var S = new Stack<Nodo>();
            var T = new Stack<string>();

            var actual = "";

            while (ExpresionRegular.Length != 0)
            {
                int caracterNum = 0;
                if (char.IsUpper(ExpresionRegular[0]))
                {
                    while (caracterNum < ExpresionRegular.Length)
                    {
                        if (char.IsUpper(ExpresionRegular[caracterNum]))
                        {
                            caracterNum++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if (ExpresionRegular[0] == '\'' && sets.Contains(ExpresionRegular.Substring(0, 3)))
                {
                    caracterNum = 3;
                }
                else if (ExpresionRegular[0] == '\'' && !sets.Contains(ExpresionRegular.Substring(0, 3)))
                {
                    caracterNum = 3;
                    sets.Add(ExpresionRegular.Substring(0, caracterNum));
                }
                else
                {
                    caracterNum = 1;
                }

                actual = ExpresionRegular.Substring(0, caracterNum);
                ExpresionRegular = ExpresionRegular.Substring(caracterNum);

                if (sets.Contains(actual) || actual == "$")
                {
                    var nuevo = new Nodo();
                    nuevo.caracter = actual;
                    S.Push(nuevo);
                }
                else if (actual == "(")
                {
                    T.Push(actual);
                }
                else if (actual == ")")
                {
                    while (T.Count > 0 && T.Peek() != "(")
                    {
                        var nuevo = new Nodo();
                        nuevo.caracter = T.Pop();
                        nuevo.der = S.Pop();
                        nuevo.izq = S.Pop();
                        S.Push(nuevo);
                    }
                    T.Pop();
                }
                else if (metaCaracteres.Contains(actual))
                {
                    int posMeta = metaCaracteres.IndexOf(actual);
                    if (posMeta == 0 || posMeta == 1 || posMeta == 2)
                    {
                        var nodo = new Nodo();
                        nodo.caracter = actual;
                        nodo.izq = S.Pop();
                        S.Push(nodo);
                    }
                    else if (T.Count != 0)
                    {
                        while (T.Peek() != "(" && posMeta <= metaCaracteres.IndexOf(T.Peek()))
                        {
                            var nuevo = new Nodo();
                            nuevo.caracter = T.Pop();
                            nuevo.der = S.Pop();
                            nuevo.izq = S.Pop();
                            S.Push(nuevo);
                        }
                    }
                    if (posMeta == 3 || posMeta == 4)
                    {
                        T.Push(actual);
                    }
                }
                actual = "";
            }
            while (T.Count > 0)
            {
                var nuevo = new Nodo();
                nuevo.caracter = T.Pop();
                nuevo.der = S.Pop();
                nuevo.izq = S.Pop();
                S.Push(nuevo);
            }
            return S.Pop();
        }

        static string union(string izq, string der)
        {
            string[] partesIzq = izq.Split(',');
            string[] partesDer = der.Split(',');

            List<int> aux = new List<int>();

            foreach (var item in partesIzq)
            {
                if (item != "")
                {
                    aux.Add(Convert.ToInt32(item));
                }
            }

            foreach (var item in partesDer)
            {
                if (item != "" && !aux.Contains(Convert.ToInt32(item)))
                {
                    aux.Add(Convert.ToInt32(item));
                }
            }

            aux.Sort();
            string nuevo = "";

            foreach (var item in aux)
            {
                nuevo += Convert.ToString(item) + ",";
            }
            
            return nuevo;
        }

        static void AgregarRepetidos(string linea1, string linea2)
        {
            string[] partes1 = linea1.Split(',');
            string[] partes2 = linea2.Split(',');

            foreach (var item in partes1)
            {
                if (item != "")
                {
                    if (follow.ContainsKey(item))
                    {
                        string[] parteAux = follow[item].Split(',');
                        List<int> aux = new List<int>();

                        foreach (var item2 in parteAux)
                        {
                            if (item2 != "")
                            {
                                aux.Add(Convert.ToInt32(item2));
                            }
                        }

                        foreach (var item2 in partes2)
                        {
                            if (!aux.Contains(Convert.ToInt32(item2)))
                            {
                                aux.Add(Convert.ToInt32(item2));
                            }
                        }
                        aux.Sort();
                        string nuevo = "";
                        foreach (var item2 in aux)
                        {
                            nuevo += Convert.ToString(item2) + ",";
                        }
                        follow[item] = nuevo;
                    }
                    else
                    {
                        follow.Add(item, linea2);
                    }
                }
            }
        }

        public static void postOrden(Nodo actual)
        {
            if (actual != null)
            {
                postOrden(actual.izq);
                postOrden(actual.der);

                if (actual.caracter == "|")
                {
                    actual.First = union(actual.izq.First, actual.der.First);

                    actual.Last = union(actual.izq.Last, actual.der.Last);

                    if (actual.izq.N || actual.der.N)
                    {
                        actual.N = true;
                    }
                }
                else if (actual.caracter == ".")
                {
                    if (actual.izq.N)
                    {
                        actual.First = union(actual.izq.First, actual.der.First);
                    }
                    else
                    {
                        actual.First = actual.izq.First;
                    }

                    if (actual.der.N)
                    {
                        actual.Last = union(actual.izq.Last, actual.der.Last);
                    }
                    else
                    {
                        actual.Last = actual.der.Last;
                    }

                    if (actual.izq.N && actual.der.N)
                    {
                        actual.N = true;
                    }

                    AgregarRepetidos(actual.izq.Last, actual.der.First);
                }
                else if (actual.caracter == "?" || actual.caracter == "+" || actual.caracter == "*")
                {
                    actual.First = actual.izq.First;
                    actual.Last = actual.izq.Last;

                    if (actual.caracter == "*" || actual.caracter == "?")
                    {
                        actual.N = true;
                    }

                    if (actual.caracter == "*" || actual.caracter == "+")
                    {
                        AgregarRepetidos(actual.izq.Last, actual.izq.First);
                    }
                }
                else
                {
                    actual.First = Convert.ToString(numSet);
                    actual.Last = Convert.ToString(numSet);

                    if (actual.caracter == "$")
                    {
                        follow.Add(Convert.ToString(numSet), "-");
                    }
                    else if (!ordenEstados.Contains(actual.caracter))
                    {
                        ordenEstados.Add(actual.caracter);
                    }

                    setsPostOrden.Add(actual.caracter);
                    numSet++;
                }

                Console.WriteLine("");
                Console.WriteLine("SIMBOLO");
                Console.WriteLine(actual.caracter);
                Console.WriteLine("FIRST");
                Console.WriteLine(actual.First);
                Console.WriteLine("LAST");
                Console.WriteLine(actual.Last);
                Console.WriteLine("NULLABLE");
                Console.WriteLine(actual.N.ToString());
            }
        }

        public static void MostrarFollow()
        {
            List<int> listaOrdenada = new List<int>();
            foreach (var item in follow)
            {
                listaOrdenada.Add(Convert.ToInt32(item.Key));
            }

            listaOrdenada.Sort();
            Dictionary<string, string> aux = new Dictionary<string, string>();

            foreach (var item in listaOrdenada)
            {
                aux.Add(Convert.ToString(item), follow[Convert.ToString(item)]);
            }

            follow = aux;

            foreach (var item in follow)
            {
                Console.WriteLine("");
                Console.WriteLine("SIMBOLO");
                Console.WriteLine(item.Key);
                Console.WriteLine("FOLLOW");
                Console.WriteLine(item.Value);
            }
        }

        public static void Estados(Nodo principal)
        {
            listaEspera.Add(principal.First);

            while (listaEspera.Count > 0)
            {
                string[] partes = listaEspera[0].Split(',');
                Dictionary<string, string> nuevo = new Dictionary<string, string>();
                foreach (var item in ordenEstados)
                {
                    List<int> aux = new List<int>();
                    foreach (var item2 in partes)
                    {
                        if (item2 != "" &&  setsPostOrden[Convert.ToInt32(item2) - 1] == item)
                        {
                            string[] partes1 = follow[item2].Split(',');
                            foreach (var item3 in partes1)
                            {
                                if (item3 != "" && !aux.Contains(Convert.ToInt32(item3)))
                                {
                                    aux.Add(Convert.ToInt32(item3));
                                }
                            }
                        }
                    }
                    string nuevoEstado = "";
                    aux.Sort();
                    foreach (var item2 in aux)
                    {
                        nuevoEstado += Convert.ToString(item2) + ",";
                    }
                    nuevo.Add(item, nuevoEstado);
                    if (nuevoEstado != "" && !estados.ContainsKey(nuevoEstado) && !listaEspera.Contains(nuevoEstado))
                    {
                        listaEspera.Add(nuevoEstado);
                    }
                }
                estados.Add(listaEspera[0], nuevo);
                listaEspera.RemoveAt(0);
            }

            foreach (var item in estados)
            {
                Console.WriteLine("");
                Console.WriteLine("ESTADO");
                Console.WriteLine(item.Key);
                foreach (var item2 in item.Value)
                {
                    Console.WriteLine(item2.Key);
                    if (item2.Value == "")
                    {
                        Console.WriteLine("-");
                    }
                    else
                    {
                        Console.WriteLine(item2.Value);
                    }
                }
            }
        }
    }
}
