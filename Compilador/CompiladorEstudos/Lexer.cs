using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorEstudos
{
    public class Lexer
    {
        private  string _input;
        private int _position;

        StreamReader sr = new StreamReader("C:\\temp\\codigo.txt");
        public Lexer()
        {
            _input = sr.ReadLine();
            _position = 0;
        }

        
        private char CurrentChar => _position < _input.Length ? _input[_position] : '\0';

        private void Advance() => _position++;

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (_input != null)
            {
                while (CurrentChar != '\0')
                {
                    if (char.IsWhiteSpace(CurrentChar))
                    {
                        Advance();
                        continue;
                    }

                    if (char.IsDigit(CurrentChar))
                    {
                        tokens.Add(GenerateNumberToken());
                        continue;
                    }

                    if (char.IsLetter(CurrentChar))
                    {
                        tokens.Add(GenerateIdentifierToken());
                        continue;
                    }

                    switch (CurrentChar)
                    {
                        case '+':
                            tokens.Add(new Token(TokenType.Plus, CurrentChar.ToString()));
                            Advance();
                            break;
                        case '-':
                            tokens.Add(new Token(TokenType.Minus, CurrentChar.ToString()));
                            Advance();
                            break;
                        case '*':
                            tokens.Add(new Token(TokenType.Multiply, CurrentChar.ToString()));
                            Advance();
                            break;
                        case '/':
                            tokens.Add(new Token(TokenType.Divide, CurrentChar.ToString()));
                            Advance();
                            break;

                        case '(':
                            tokens.Add(new Token(TokenType.Parentheses, CurrentChar.ToString()));
                            Advance();
                            break;

                        case ')':
                            tokens.Add(new Token(TokenType.Parentheses, CurrentChar.ToString()));
                            Advance();
                            break;

                        case '{':
                            tokens.Add(new Token(TokenType.Bracket, CurrentChar.ToString()));
                            Advance();
                            break;

                        case '}':
                            tokens.Add(new Token(TokenType.Bracket, CurrentChar.ToString()));
                            Advance();
                            break;

                        case ';':
                            tokens.Add(new Token(TokenType.Semicolon, CurrentChar.ToString()));
                            Advance();
                            break;

                        case '"':
                            tokens.Add(new Token(TokenType.DoubleQuotes, CurrentChar.ToString()));
                            Advance();
                            break;
                      
                        case '.':
                            tokens.Add(new Token(TokenType.Pointer, CurrentChar.ToString()));
                            Advance();
                            break;

                        case '\'':
                            tokens.Add(new Token(TokenType.Quotes, CurrentChar.ToString()));
                            Advance();
                            break;

                        case ',':
                            tokens.Add(new Token(TokenType.Comma, CurrentChar.ToString()));
                            Advance();
                            break;

                        case '&':
                            tokens.Add(new Token(TokenType.Ampersand, CurrentChar.ToString()));
                            Advance();
                            break;
                        case '\\':
                            tokens.Add(new Token(TokenType.All, CurrentChar.ToString()));
                            Advance();
                            break;

                        case '<':
                            var identifier = string.Empty;
                            while (char.IsSymbol(CurrentChar) || CurrentChar.ToString() == "-")
                            {
                                identifier += CurrentChar;
                                Advance();
                            }

                            switch (identifier)
                            { 
                                case "<-":
                                    tokens.Add(new Token(TokenType.Assign, "<-"));
                                    break;
                            }
                            break;

                        case '%':
                            var typeScan = string.Empty;

                            while (CurrentChar == '%' || char.IsLetter(CurrentChar))
                            {
                                typeScan += CurrentChar;
                                Advance();
                            }

                            switch (typeScan)
                            {
                                case "%d":
                                    tokens.Add(new Token(TokenType.SC_d, typeScan));
                                    break;
                                case "%f":
                                    tokens.Add(new Token(TokenType.SC_f, typeScan));
                                    break;
                                case "%s":
                                    tokens.Add(new Token(TokenType.SC_s, typeScan));
                                    break;
                                case "%l" +
                                "f":
                                    tokens.Add(new Token(TokenType.SC_lf, typeScan));
                                    break;
                                default:
                                    tokens.Add(new Token(TokenType.Unknown, typeScan));
                                    break;
                            }
                            break;

                        default:
                            tokens.Add(new Token(TokenType.Unknown, "<-"));
                            Advance();
                            break;
                    }
                }
                _input = sr.ReadLine();
                _position = 0;
            }

            tokens.Add(new Token(TokenType.EOF, ""));
            return tokens;
        }

        private Token GenerateNumberToken()
        {
            var number = string.Empty;

            while (char.IsDigit(CurrentChar))
            {
                number += CurrentChar;
                Advance();
            }

            return new Token(TokenType.Number, number);
        }

        private Token GenerateIdentifierToken()
        {
            var identifier = string.Empty;

            while (char.IsLetterOrDigit(CurrentChar) || CurrentChar == '_')
            {
                identifier += CurrentChar;
                Advance();
            }

            switch (identifier) 
            {
                case "inteiro":
                    return new Token(TokenType.Int, identifier);
                    break;

                case "<-":
                    return new Token(TokenType.Assign, CurrentChar.ToString());
                    break;

                case "real":
                    return new Token(TokenType.Double, identifier);
                    break;

                case "caractere":
                    return new Token(TokenType.Char, identifier);
                    break;

                case "decimal":
                    return new Token(TokenType.Float, identifier);
                    break;

                case "literal":
                    return new Token(TokenType.String, identifier);
                    break;

                case "logico":
                    return new Token(TokenType.Bool, identifier);
                    break;

                case "enquanto":
                    return new Token(TokenType.While, identifier);
                    break;

                case "se":
                    return new Token(TokenType.If, identifier);
                    break;

                case "faca":
                    return new Token(TokenType.Do, identifier);
                    break;
                case "senaose":
                    return new Token(TokenType.ElseIf, identifier);
                    break;
                case "senao":
                    return new Token(TokenType.Else, identifier);
                    break;

                case "para":
                    return new Token(TokenType.For, identifier);
                    break;

                case "escreva":
                    return new Token(TokenType.Printf, identifier);
                    break;

                case "leia":
                    return new Token(TokenType.Scanf, identifier);
                    break;

                case "entao":
                    return new Token(TokenType.Then, identifier);
                    break;

                case "%s":
                    return new Token(TokenType.SC_s, identifier);
                    break;

                case "%d":
                    return new Token(TokenType.SC_d, identifier);
                    break;

                case "%f":
                    return new Token(TokenType.SC_f, identifier);
                    break;

                case "%lf":
                    return new Token(TokenType.SC_lf, identifier);
                    break;

                case "verdadeiro":
                    return new Token(TokenType.True, identifier);
                    break;

                case "falso":
                    return new Token(TokenType.False, identifier);
                    break;

                case "igual":
                    return new Token(TokenType.All, identifier);
                    break;

                case "maior":
                    return new Token(TokenType.All, identifier);
                    break;

                case "menor":
                    return new Token(TokenType.All, identifier);
                    break;

                case "diferente":
                    return new Token(TokenType.All, identifier);
                    break;

                case "ou":
                    return new Token(TokenType.All, identifier);
                    break;

                case "ee":
                    return new Token(TokenType.All, identifier);
                    break;

                case "menorigual":
                    return new Token(TokenType.All, identifier);
                    break;

                case "maiorigual":
                    return new Token(TokenType.All, identifier);
                    break;

                default:
                    return new Token(TokenType.Identifier, identifier);
                    break;

            }

            return new Token(TokenType.Identifier, identifier);
        }
    }
    public enum TokenType
    {
        Identifier,  Double,   String,  Float,  Char,  Int, Plus,  Minus,  Multiply,  Divide,  Assign,  Semicolon,  EOF,  Unknown, Number,
        If,  ElseIf,  Else,  For, While,  Do, Parentheses,  Bracket,  DoubleQuotes,   Pointer,    Quotes,  Bool,  Printf,  Scanf, Then, True,
        False,  SC_s, SC_d, SC_f, SC_lf, Ampersand,   All,   CharCom,Comma, 
    }
}
