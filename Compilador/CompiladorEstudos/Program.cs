using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    Responsáveis: Davi Sacheto
                  Paulo Henrique Guimarães
                  Richard Katsumata
                  Stefane Aglae Aquino
 */

namespace CompiladorEstudos
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando análise do código...\n\n");
            Console.ReadKey();

            Console.WriteLine("Iniciando análise léxica do cógigo...\n\n");
            Console.ReadKey();
            var lexer = new Lexer();
            var tokens = lexer.Tokenize();

            Console.WriteLine("Iniciando análise sintática e semântica do código...\n\n");
            Console.ReadKey();
            Sintatica sintatica = new Sintatica();
            bool res = sintatica.Compi(tokens);

            Console.WriteLine("Convertendo o código para C...\n\n");
            Console.ReadKey();
            Convert c = new Convert(tokens, res);
            c.Converts();

            Console.ReadLine();
        }
    }
}
