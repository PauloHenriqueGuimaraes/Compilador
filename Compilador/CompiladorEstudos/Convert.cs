using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorEstudos
{
    public class Convert
    {
        bool next;
        List<Token> token;
        public Convert(List<Token> token, bool next)
        {
            this.token = token;
            this.next = next;
        }

        public bool Converts()
        {
            if (next == true)
            {
                foreach (var token in token)
                {
                    switch (token.Value)
                    {
                        case "real":
                            token.Value = "double ";
                            break;
                        case "literal":
                            token.Value = "char ";
                            break;
                        case "decimal":
                            token.Value = "float ";
                            break;
                        case "caractere":
                            token.Value = "char ";
                            break;
                        case "logico":
                            token.Value = "bool ";
                            break;
                        case "inteiro":
                            token.Value = "int ";
                            break;
                        case "<-":
                            token.Value = "= ";
                            break;
                        case "se":
                            token.Value = "if "; ;
                            break;
                        case "senao":
                            token.Value = "else ";
                            break;
                        case "senaose":
                            token.Value = "else if ";
                            break;
                        case "para":
                            token.Value = "for ";
                            break;
                        case "enquanto":
                            token.Value = "while ";
                            break;
                        case "escreva":
                            token.Value = "printf ";
                            break;
                        case "entao":
                            token.Value = " ";
                            break;
                        case "leia":
                            token.Value = "scanf ";
                            break;
                        case "verdadeiro":
                            token.Value = "true ";
                            break;
                        case "falso":
                            token.Value = "false ";
                            break;
                        case "igual":
                            token.Value = " == ";
                            break;

                        case "maior":
                            token.Value = " > ";
                            break;

                        case "menor":
                            token.Value = " < ";
                            break;

                        case "diferente":
                            token.Value = " != ";
                            break;

                        case "ou":
                            token.Value = " || ";
                            break;

                        case "ee":
                            token.Value = " && ";
                            break;

                        case "menorigual":
                            token.Value = " <= ";
                            break;

                        case "maiorigual":
                            token.Value = " >= ";
                            break;
                        case "%d":
                            token.Value = "%d";
                            break;
                        case "%f":
                            token.Value = "%f";
                            break;
                        case "%s":
                            token.Value = "%s";
                            break;

                        case "%lf":
                            token.Value = "%lf";
                            break;
                        default:
                            //Console.WriteLine($"Token desconhecido ou não tratado");
                            break;
                    }
                }


                string caminhoArquivo = "C:\\temp\\compilado.txt";

                try
                {
                    using (StreamWriter escritor = new StreamWriter(caminhoArquivo))
                    {
                        escritor.WriteLine("#include<stdio.h>");
                        escritor.WriteLine("#include<stdlib.h>");
                        escritor.WriteLine("#include<locale.h>\n");

                        escritor.WriteLine("int main(){\n");
                        escritor.WriteLine("setlocale(LC_ALL, \"Portuguese\");\n");
                        foreach (var item in token)
                        {
                            escritor.Write(item.Value);
                        }
                        escritor.WriteLine("}");
                    }

                    Console.WriteLine("Arquivo compilado !");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);

                    foreach (var token in token)
                    {
                        Console.WriteLine(token.Value);
                    }
                    return true;
                }

                return true;
            }
            else
            {
                Console.WriteLine("Arquivo não convertido!!!");
                Console.ReadKey();
                return false;
            }
        }
    }
}