using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_LFA
{
    class Program
    {
        /*
         * Alejandra Samayoa
         * carnet 1278817
         * proyecto de Lenguajes Formales y Automatas
         * Primera Fase
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Arrastre el archivo, para poder determinar el error");

            //strings
            string lineaActual = "";
            //ints
            int numLinea = 0;
            int conteo = 0;
            //bools
            bool sets = false;
            bool tokens = false;
            bool actions = false;
            bool error = false;
            bool errores = false;

            using (StreamReader archivo = new StreamReader(Console.ReadLine().Trim('"')))
            {
                //Evalua que viene primero si sets o tokens
                while ((lineaActual = archivo.ReadLine()) != null && !errores)
                {
                    numLinea++;
                    lineaActual = Logica.limpiarLinea(lineaActual);
                    if (lineaActual != "")
                    {
                        if (lineaActual == "SETS")
                        {
                            sets = true;
                            break;
                        }
                        else if (lineaActual == "TOKENS")
                        {
                            tokens = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error no viene TOKENS o SETS");
                            errores = true;
                        }
                    }
                }

                //Evalua que sets este escrito correctamente si viene en el archivo
                while ((lineaActual = archivo.ReadLine()) != null && sets && !errores)
                {
                    numLinea++;
                    lineaActual = Logica.limpiarLinea(lineaActual);
                    if (lineaActual != "")
                    {
                        if (lineaActual == "TOKENS")
                        {
                            tokens = true;
                            break;
                        }
                        else if (Logica.sets(lineaActual, numLinea) == "")
                        {
                            conteo++;
                        }
                        else
                        {
                            errores = true;
                        }
                    }
                }

                //Evalua si al momento de venir sets tenga por lo menos un set
                if (conteo == 0 && sets)
                {
                    errores = true;
                    Console.WriteLine("Error");
                }
                else
                {
                    //Evalua si viene tokens
                    if (!errores)
                    {
                        if (!tokens)
                        {
                            Console.WriteLine("Error no viene TOKENS");
                        }
                        else
                        {
                            //Evalua si tokens viene correcto en el archivo
                            while ((lineaActual = archivo.ReadLine()) != null && !errores)
                            {
                                numLinea++;
                                lineaActual = Logica.limpiarLinea(lineaActual);
                                if (lineaActual != "")
                                {
                                    if (lineaActual == "ACTIONS")
                                    {
                                        actions = true;
                                        break;
                                    }
                                    else if (Logica.tokens(lineaActual, numLinea) == "")
                                    {
                                        conteo++;
                                    }
                                    else
                                    {
                                        errores = true;
                                    }
                                }
                            }

                            //Evalua si viene actions
                            if (!errores)
                            {
                                if (!actions)
                                {
                                    Console.WriteLine("Error no viene ACTIONS");
                                }
                                else
                                {
                                    bool reservadas = false;
                                    string aux = "";
                                    //Evalua si actions viene correcto en el archivo
                                    while ((lineaActual = archivo.ReadLine()) != null && !errores)
                                    {
                                        numLinea++;
                                        lineaActual = Logica.limpiarLinea(lineaActual);
                                        if (lineaActual != "")
                                        {
                                            if (lineaActual.Length >= 5 && lineaActual.Substring(0, 5) == "ERROR")
                                            {
                                                error = true;
                                                aux = lineaActual;
                                                break;
                                            }
                                            else if (lineaActual.Length >= 1 && char.IsUpper(lineaActual[0]))
                                            {
                                                int caracterNum = 0;
                                                while (caracterNum < lineaActual.Length)
                                                {
                                                    if (char.IsUpper(lineaActual[caracterNum]))
                                                    {
                                                        caracterNum++;
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }

                                                if (lineaActual.Length >= (caracterNum + 2) && lineaActual.Substring(0, caracterNum + 2) == "RESERVADAS()")
                                                {
                                                    reservadas = true;
                                                    lineaActual = Logica.limpiarLinea(lineaActual.Substring(caracterNum + 2));
                                                }
                                                else if (lineaActual.Length >= (caracterNum + 2) && lineaActual.Substring((caracterNum), 2) == "()")
                                                {
                                                    lineaActual = Logica.limpiarLinea(lineaActual.Substring(caracterNum + 2));
                                                }
                                                else
                                                {
                                                    lineaActual = "";
                                                }

                                                if (lineaActual.Length != 0)
                                                {
                                                    Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                    errores = true;
                                                }
                                                else
                                                {
                                                    while ((lineaActual = archivo.ReadLine()) != null && !errores)
                                                    {
                                                        numLinea++;
                                                        lineaActual = Logica.limpiarLinea(lineaActual);
                                                        if (lineaActual != "")
                                                        {
                                                            if (lineaActual.Length >= 1 && lineaActual[0] == '{')
                                                            {
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                                errores = true;
                                                            }
                                                        }
                                                    }

                                                    while ((lineaActual = archivo.ReadLine()) != null && !errores)
                                                    {
                                                        numLinea++;
                                                        lineaActual = Logica.limpiarLinea(lineaActual);
                                                        if (lineaActual != "")
                                                        {
                                                            if (lineaActual == "}")
                                                            {
                                                                break;
                                                            }
                                                            else if (Logica.actions(lineaActual, numLinea) == "")
                                                            {
                                                                conteo++;
                                                            }
                                                            else
                                                            {
                                                                errores = true;
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                errores = true;
                                            }
                                        }
                                    }

                                    if (!reservadas)
                                    {
                                        errores = true;
                                    }

                                    //Evalua si viene errors
                                    if (!errores)
                                    {
                                        if (!error)
                                        {
                                            Console.WriteLine("Error no viene ERROR");
                                        }
                                        else
                                        {
                                            //Evalua si tokens viene correcto en el archivo
                                            if (aux != "" && Logica.errors(lineaActual, numLinea) == "")
                                            {
                                                conteo++;
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR " + numLinea + " LINEA");
                                                errores = true;
                                            }

                                            while ((lineaActual = archivo.ReadLine()) != null && !errores)
                                            {
                                                numLinea++;
                                                lineaActual = Logica.limpiarLinea(lineaActual);
                                                if (lineaActual != "")
                                                {
                                                    if (Logica.errors(lineaActual, numLinea) == "")
                                                    {
                                                        conteo++;
                                                    }
                                                    else
                                                    {
                                                        errores = true;
                                                    }
                                                }
                                            }

                                            if (!errores)
                                            {
                                                Console.WriteLine("CORRECTO");
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                }
            }
            Console.ReadKey();
        }
    }
}
