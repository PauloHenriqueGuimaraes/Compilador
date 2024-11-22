using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintatica
{
    public class Sintatica
    {
        CompiladorEstudos.Program program;
        public Sintatica() { }

        public string Comp(CompiladorEstudos.Token token)
        {
            try
            {
                
                return "Certo";
            }
            catch (Exception)
            {

                throw new Exception("Error");
            }
        }

    }
}
