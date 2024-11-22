using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CompiladorEstudos
{
    public  class Sintatica
    {
        bool voidClose = false;
        bool next = true;
     
        public Sintatica() { }

        int index = 0;
        List<Token> tokens;

        public bool Compi(List<Token> tokensLexica)
        {
            tokens = tokensLexica;

            while (index  <= tokens.Count || index ==  tokens.Count)
            {
                if (next == true)
                {
                    switch (tokens[index].Type)
                    {
                        case TokenType.Identifier:
                            Identifier();
                            break;
                        case TokenType.Double:
                            DoubleFloat();
                            break;
                        case TokenType.String:
                            String();
                            break;
                        case TokenType.Float:
                            DoubleFloat();
                            break;
                        case TokenType.Char:
                            Char();
                            break;
                        case TokenType.Bool:
                            Bool();
                            break;
                        case TokenType.Int:
                            Int();
                            break;
                        case TokenType.EOF:
                            return true;
                            break;  
                        case TokenType.Unknown:
                            return true;
                            break;
                        case TokenType.Number:
                            break;
                        case TokenType.If:
                            If();
                            break;
                        case TokenType.ElseIf:
                            ElseIf();
                            break;
                        case TokenType.Else:
                            Else();
                            break;
                        case TokenType.For:
                            For();
                            break;
                        case TokenType.While:
                            While();
                            break;
                        case TokenType.Printf:
                            Printf();
                            break;
                        case TokenType.Scanf:
                            Scanf();
                            break;
                        case TokenType.Bracket:
                            index++;
                            break;
                        case TokenType.Semicolon:
                            index++;
                            if (tokens[index].Type == TokenType.EOF)
                            {
                                return true;
                            }
                            break;
                        default:
                            Console.WriteLine($"Token desconhecido ou não tratado: {tokens[index].Type}");
                            return false;

                            break;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        //Double e Float
        public bool DoubleFloat()
        {
            Console.WriteLine("Validando Variável \"real/ decimal\"...");
            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Nome da variável não declarada ou uso de uma palavra reservada.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"real/decimal\")");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;                
            }

            index++;

            if (tokens[index].Type == TokenType.Comma)
            {
                index++;
                Identifier();
                return true;
            }

            if (!(tokens[index].Type == TokenType.Assign))
            {
                tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
                index++;
                return true;
            }

            index++;

            if (tokens[index].Type != TokenType.Number)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado um número na atribuição de valor da variável.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"real/decimal\")");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Pointer)
            {
               
                tokens.Insert( index,new Token(TokenType.Semicolon, ";"));
                index++;
                return true;
            }

            index++;

            if (tokens[index].Type != TokenType.Number)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado um número depois do ponto.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"real/decimal\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.ReadKey();
                next = false;
                return false;
            }
            
            index++;
            tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
            next = true;
            return true;
        }

        //Int
        public bool Int()
        {
            Console.WriteLine("Validando Variável \"inteiro\"...");
            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Nome da variável não declarada ou uso de uma palavra reservada.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"inteiro\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next= false;
                return false;
            }

            index++;

            if (tokens[index].Type == TokenType.Comma)
            {
                index++;
                Identifier();
                return true;
            }

            if (!(tokens[index].Type == TokenType.Assign))
            {
                tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
                index++;
                return true;
            }

            index++;


            if (tokens[index].Type != TokenType.Number)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado um número na atribuição de valor da variável.");
                Console.WriteLine("*(Verifique a sua declaração de variável do tipo \"inteiro\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
               next = false;
               return false;
            }

            index++;
            tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
            index++;
            next = true;
            return true;
        }

        //Printf
        public bool Printf()
        {
        
            index++;
            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado a abertura de parênteses após a declaração da função \"escreva\"!!!");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;
            if (tokens[index].Type != TokenType.DoubleQuotes)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado a abertura de aspas duplas após a abertura de parênteses da função \"escreva\"!!!");
                Console.WriteLine("-------------------------------------------------------------------------------------------------");
                next= false;
                return false;
            }

            index++;

            while (tokens[index].Type == TokenType.Identifier || tokens[index].Type == TokenType.Unknown || tokens[index].Type == TokenType.Number || tokens[index].Type == TokenType.All || tokens[index].Type == TokenType.Plus || tokens[index].Type == TokenType.Minus || tokens[index].Type == TokenType.Multiply || tokens[index].Type == TokenType.Divide || tokens[index].Type == TokenType.Assign || tokens[index].Type == TokenType.Parentheses || tokens[index].Type == TokenType.Bracket || tokens[index].Type == TokenType.Pointer || tokens[index].Type == TokenType.Quotes || tokens[index].Type == TokenType.Comma || tokens[index].Type == TokenType.Then || tokens[index].Type == TokenType.True || tokens[index].Type == TokenType.False || tokens[index].Type == TokenType.SC_d || tokens[index].Type == TokenType.SC_f || tokens[index].Type == TokenType.SC_lf || tokens[index].Type == TokenType.SC_s || tokens[index].Type == TokenType.Ampersand )
            {
                index++;
            }


            if (tokens[index].Type != TokenType.DoubleQuotes)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado o fechamento de aspas duplas antes do fechamento de parênteses da fução \"escreva\"!!!");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
            index++;


            if (tokens[index].Type == TokenType.Comma)// parte teste
            {

                while (tokens[index].Type == TokenType.Comma)
                {
                    index++;

                    if (tokens[index].Type != TokenType.Identifier)
                    {
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine("*Erro: Esperando um nome de uma variável após a vírgula.");
                        Console.WriteLine("-------------------------------------------------------------------");
                        next = false;
                    }

                    index++;
                }
            }

            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado o fechamento de parênteses após a declaração da fução \"escreva\"!!!");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false ;
                return false;
            }

           
            index++;
            tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
            next = true;
            return true;

        }

        //Scanf
        public bool Scanf()
        {
            Console.WriteLine("Validando \"leia\"...");
            index++;

            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado a abertura de parênteses após a declaração da função \"leia\"!!!");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;
            tokens.Insert(index, new Token(TokenType.DoubleQuotes, "\""));
            index++;

            switch (tokens[index].Value)
            {
                case "%d":
                    index++;
                    break;

                case "%s":
                    index++;
                    break;

                case "%f":
                    index++;
                    break;

                case "%lf":
                    index++;
                    break;
                default:
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("*Erro: Esperado \"%d\",\"%s\",\"%f\" ou \"%lf\" após a abertura de parênteses na declaração da função \"leia\"!!!");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                    next = false;
                    return false ;
                    break;
            }

            tokens.Insert(index, new Token(TokenType.DoubleQuotes, "\""));
            index++;

            if (tokens[index].Type != TokenType.Comma)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado \",\" após  \"%d\",\"%s\"\"%f\" ou \"%lf\" na declaração da função \"leia\"!!!");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
            index++;
            tokens.Insert(index, new Token(TokenType.Ampersand, "&"));
            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado o nome de uma variável após  \",\" na declaração da função \"leia\"!!!");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
            index++;

            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado o fechamento de parênteses na declaração da função \"leia\"!!!");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
           
            index++;
            tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
            //modify
            index++;
            next = true;
            return true;

        }


        //Bool
        public bool Bool()
        {
            Console.WriteLine("Validando Variavel \"logico\"...");
            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Nome da variável não declarada ou uso de uma palavra reservada.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"logico\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type == TokenType.Comma)
            {
                index++;
                Identifier();
                return true;
            }

            if (!(tokens[index].Type == TokenType.Assign))
            {
                tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
                index++;
                return true;
            }

            index++;

            if ((tokens[index].Type != TokenType.True) && (tokens[index].Type != TokenType.False))
            {
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado os valores \"verdadeiro\" ou \"falso\" na atribuição de valor da variável.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"logico\").");
                Console.WriteLine("------------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            tokens.Insert(index, new Token(TokenType.Semicolon, ";"));

            index++;
            next = true;
            return true;
        }

        //String
        public bool String()
        {
            Console.WriteLine("Validando \"literal\"...");
            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Nome da variável não declarada ou uso de uma palavra reservada.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"literal\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.ReadKey();
                next = false;
                return false;
            }
            index++;
            tokens.Insert(index, new Token(TokenType.CharCom, "[100]"));
     
            index++;

            if (tokens[index].Type == TokenType.Comma)
            {
                index++;
                Identifier();
                return true;
            }

            if (!(tokens[index].Type == TokenType.Assign))
            {
                tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
                index++;
                return true;
            }

            index++;

            if (tokens[index].Type != TokenType.DoubleQuotes)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado a abertura de aspas duplas antes do conteúdo da variável.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"literal\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;


            while ((tokens[index].Type == TokenType.Identifier || tokens[index].Type == TokenType.Unknown || tokens[index].Type == TokenType.Number || tokens[index].Type == TokenType.All || tokens[index].Type == TokenType.Plus || tokens[index].Type == TokenType.Minus || tokens[index].Type == TokenType.Multiply || tokens[index].Type == TokenType.Divide || tokens[index].Type == TokenType.Assign || tokens[index].Type == TokenType.Parentheses || tokens[index].Type == TokenType.Bracket || tokens[index].Type == TokenType.Pointer || tokens[index].Type == TokenType.Quotes || tokens[index].Type == TokenType.Comma || tokens[index].Type == TokenType.Then || tokens[index].Type == TokenType.True || tokens[index].Type == TokenType.False || tokens[index].Type == TokenType.SC_d || tokens[index].Type == TokenType.SC_f || tokens[index].Type == TokenType.SC_s || tokens[index].Type == TokenType.Ampersand))
            { 
                index++;
            }

            if (tokens[index].Type != TokenType.DoubleQuotes)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado o fechamento de aspas duplas após do conteúdo da variável.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"literal\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;
            tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
            index++;
            next = true;
            return true;
        }

        //Char
        public bool Char()
        {
            Console.WriteLine("Validando \"caractere\"...");
            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Nome da variável não declarada ou uso de uma palavra reservada.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"caractere\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;
            if (tokens[index].Type == TokenType.Comma)
            {
                index++;
                Identifier();
                return true;
            }

            if (!(tokens[index].Type == TokenType.Assign))
            {
                tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
                index++;
                return true;
            }

            index++;

            if (tokens[index].Type != TokenType.Quotes)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado a abertura de aspas simples antes do conteúdo da variável.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"caractere\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Identifier && !(tokens[index].Value.Length == 1))
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado um conteúdo dentro das aspas para a atribuiçao de valor para a variável ou o valor comtém mais de um caractere.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"caractere\").");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Quotes)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperado o fechamento de aspas simples após do conteúdo da variável.");
                Console.WriteLine("*(Verifique a sua declaração de variável \"caractere\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
            index++;
            tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
            index++;
            next = true;
            return true;
        }

        //Void
        public bool Void()
        {
            Console.WriteLine("Validando Vazio");
            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                throw new Exception("Erro: Nome da variável não declarada.");
            }

            index++;

            if (tokens[index].Type != TokenType.Parentheses)
            {
                throw new Exception("Erro: Esperado abre parênteses.");
            }

            index++;

            while ((tokens[index].Type == TokenType.Double) || (tokens[index].Type == TokenType.Int) || (tokens[index].Type == TokenType.Float) || (tokens[index].Type == TokenType.Bool) || (tokens[index].Type == TokenType.Float) || (tokens[index].Type == TokenType.Char) || (tokens[index].Type == TokenType.String))
            {
                index++;

                if (tokens[index].Type != TokenType.Identifier)
                {
                    throw new Exception("Erro: Nome da variável não declarada.");
                }

                index++;

                if (tokens[index].Type != TokenType.Comma && tokens[index].Type == TokenType.Parentheses)
                {
                    break;
                }

                index++;
            }


            if (tokens[index].Type != TokenType.Parentheses)
            {
                throw new Exception("Erro: Esperado o fechamento de parênteses");
            }

            index++;

            if (tokens[index].Type != TokenType.Bracket)
            {
                throw new Exception("Erro: Esperado abre chaves.");
            }
            index++;
            voidClose = true;

            next = true;
            return true;
        }

        //If
        public bool If()
        {
            Console.WriteLine("Validando \"se\"...");
            index++;
            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando a abertura de parênteses após \"se\".");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando um nome de variável após a abertura do parênteses.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de condição \"se\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
            
            index++;

            while (tokens[index].Type == TokenType.All)
            {
                index++;
                if (tokens[index].Type != TokenType.Identifier && tokens[index].Type != TokenType.Number)
                {
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                    Console.WriteLine("*Erro: Esperando um operador lógico válido seguido de uma variável.");
                    Console.WriteLine("*(Verifique a declaração de sua estrutura de condição \"se\").");
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                    next = false;
                    return false;
                }
                index++;
            }

            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando o fechamento de parênteses.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de condição \"se\").");
                Console.WriteLine("--------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Bracket)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando a abertura após o fechamento de parênteses\"se\".");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de condição \"se\").");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
            index++;


            return true;
        }

        //Else if
        public bool ElseIf()
        {
            Console.WriteLine("Validando \"senaose\"...");
            index++;
            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando a abertura de parênteses após \"senaose\".");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando um nome de variável após a abertura do parênteses.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de condição \"senaose\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            while (tokens[index].Type == TokenType.All)
            {
                index++;
                if (tokens[index].Type != TokenType.Identifier && tokens[index].Type != TokenType.Number )
                {
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                    Console.WriteLine("*Erro: Esperando um operador lógico válido seguido de uma variável.");
                    Console.WriteLine("*(Verifique a declaração de sua estrutura de condição \"senaose\").");
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                    next = false;
                    return false;
                }
                index++;
            }

            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando o fechamento de parênteses.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de condição \"senaose\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Bracket)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando a abertura após o fechamento de parênteses\"se\".");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de condição \"senaose\").");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
            index++;


            return true;
        }

        //Else
        public bool Else()
        {
            Console.WriteLine("Validando \"senao\"...");
            index++;
            if (tokens[index].Type != TokenType.Bracket)
            {
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando a abertura de chaves.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de condição \"senao\").");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
            index++;
            return true;
            
        }

        //While
        public bool While()
        {
            Console.WriteLine("Validando \"enquanto\"...");
            index++;
            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando a abertura de parênteses após \"enquanto\".");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando um nome de uma variável.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição \"enquanto\").");
                Console.WriteLine("---------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            while (tokens[index].Type == TokenType.All)
            {
                index++;
                if (tokens[index].Type != TokenType.Identifier)
                {
                    Console.WriteLine("-------------------------------------------------------------------------");
                    Console.WriteLine("*Erro: Esperando um operador lógico seguido de uma variável \"enquando\".");
                    Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição \"enquanto\").");
                    Console.WriteLine("-------------------------------------------------------------------------");
                    next = false;
                    return false;
                }
                index++;
            }

            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando o fechamento de parênteses \"enquando\".");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição \"enquanto\").");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                next = false;
                return false;
            }
            index++;

            if (tokens[index].Type != TokenType.Bracket)
            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando a abertura de chaves após o fechamento de parêntes.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"enquanto\").");
                Console.WriteLine("----------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;
            return true;
        }

        //For
        public bool For() 
        {
            Console.WriteLine("Validando \"para\"...");
            index++;
            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando a abertura de parênteses após \"para\".");
                Console.WriteLine("------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {

                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando um nome de variável após a abertura do parênteses.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Assign)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando \"<-\" para atribuição.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Number)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando um nome de variável após a abertura do parênteses.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Semicolon)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando ponto e virgula depois do número.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando um nome de variável após ponto e vírgula.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.All)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando um token de comparação.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Number)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando um número após o token de comparação.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Semicolon)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando ponto e vírgula depois do número.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Identifier)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando uma variável contador.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type == TokenType.Plus)
            {
                index++;

                if (tokens[index].Type != TokenType.Plus)
                {
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                    Console.WriteLine("*Erro: Esperando \"++\" após a variável contador.");
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                    next = false;
                    return false;
                }

            }

            if (tokens[index].Type == TokenType.Minus)
            {
                index++;

                if (tokens[index].Type != TokenType.Minus)
                {
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                    Console.WriteLine("*Erro: Esperando \"--\" após a variável contador.");
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                    next = false;
                    return false;
                }
            }

            index++;

            if (tokens[index].Type != TokenType.Parentheses)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando fechamento de parênteses.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;

            if (tokens[index].Type != TokenType.Bracket)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("*Erro: Esperando a abertura de chaves.");
                Console.WriteLine("*(Verifique a declaração de sua estrutura de repetição  \"para\").");
                Console.WriteLine("-------------------------------------------------------------------");
                next = false;
                return false;
            }

            index++;
            next = true;
            return true;
        }

        //Identifier
        public void Identifier() 
        {
            index++;

            while (tokens[index].Type == TokenType.Comma)
            {
                index++;

                if (tokens[index].Type != TokenType.Identifier)
                {
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("*Erro: Esperando um nome de uma variável depois da vírgula.");
                    Console.WriteLine("-------------------------------------------------------------------");
                    next = false;
                }
                
                index++;
            }
            
            if (tokens[index].Type == TokenType.Assign)
            {
                index++;
                if (tokens[index].Type == TokenType.Identifier || tokens[index].Type == TokenType.Number)
                {
                    index++;

                    while(tokens[index].Type == TokenType.Plus || tokens[index].Type == TokenType.Minus || tokens[index].Type == TokenType.Divide || tokens[index].Type == TokenType.Multiply)
                    {
                        index++;
                        if(tokens[index].Type != TokenType.Number && tokens[index].Type != TokenType.Identifier)
                        {
                            Console.WriteLine("-------------------------------------------------------------------");
                            Console.WriteLine("*Erro: Conta aritimética inválida.");
                            Console.WriteLine("*(Verifique a sua declaração de conta aritimética!!!).");
                            Console.WriteLine("-------------------------------------------------------------------");
                            next = false;
                        }
                        index++;
                    }

                }

            }

            
            tokens.Insert(index, new Token(TokenType.Semicolon, ";"));
            index++;
            next = true;
        }
    }
}
