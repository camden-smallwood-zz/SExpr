using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SExpr
{
    public class SExprReader
    {
        /// <summary>
        /// The binary reader which reads from the expression data stream.
        /// </summary>
        public BinaryReader Reader { get; }

        public SExprReader(Stream stream)
        {
            Reader = new BinaryReader(stream);
        }

        /// <summary>
        /// Expression syntax delimiters.
        /// </summary>
        public const string Delimiters = "()\"'`,;";

        public bool LastTokenValid = false;
        public string LastToken = "";

        public void SetLastToken(string token)
        {
            LastToken = token;
            LastTokenValid = true;
        }

        public string ReadSExprToken()
        {
            var result = "";

            if (LastTokenValid)
            {
                result = LastToken;

                LastToken = "";
                LastTokenValid = false;

                return result;
            }

            if (Reader.BaseStream.Position >= Reader.BaseStream.Length)
                return null;

            var c = '\0';
            while ((Reader.BaseStream.Position < Reader.BaseStream.Length) && char.IsWhiteSpace(c = Reader.ReadChar())) ;

            if (Reader.BaseStream.Position >= Reader.BaseStream.Length)
                return null;

            if (Delimiters.Contains(c))
                return c.ToString();

            result += c;

            while (Reader.BaseStream.Position < Reader.BaseStream.Length)
            {
                c = Reader.ReadChar();

                if (Delimiters.Contains(c) || char.IsWhiteSpace(c))
                {
                    Reader.BaseStream.Position--;
                    break;
                }
                
                result += c;
            }
            
            return result;
        }

        public ISExpr ReadSExprCons()
        {
            var token = ReadSExprToken();

            switch (token)
            {
                case ")":
                    return default(SExprNil);

                case ".":
                    {
                        var node = ReadSExprNode();
                        
                        if ((token = ReadSExprToken()) == null || token != ")")
                            throw new Exception(token != null ? $"Syntax error: {token}" : "End of file");

                        return node;
                    }

                default:
                    break;
            }

            SetLastToken(token);

            return new SExprCons
            {
                Car = ReadSExprNode() ?? throw new Exception("End of file"),
                Cdr = ReadSExprCons() ?? throw new Exception("End of file")
            };
        }

        public SExprString ReadSExprString()
        {
            var result = "";

            for (char c; (c = Reader.ReadChar()) != '"'; result += c) ;

            return new SExprString
            {
                Value = result
            };
        }

        public ISExpr ReadSExprQuote()
        {
            return new SExprCons
            {
                Car = new SExprSymbol { Value = "quote" },
                Cdr = new SExprCons
                {
                    Car = ReadSExprNode() ?? throw new Exception("End of file"),
                    Cdr = new SExprNil()
                }
            };
        }

        public ISExpr ReadSExprQuasiQuote()
        {
            return new SExprCons
            {
                Car = new SExprSymbol { Value = "quasiquote" },
                Cdr = new SExprCons
                {
                    Car = ReadSExprNode() ?? throw new Exception("End of file"),
                    Cdr = new SExprNil()
                }
            };
        }

        public ISExpr ReadSExprUnquote()
        {
            return new SExprCons
            {
                Car = new SExprSymbol { Value = "unquote" },
                Cdr = new SExprCons
                {
                    Car = ReadSExprNode() ?? throw new Exception("End of file"),
                    Cdr = new SExprNil()
                }
            };
        }

        public ISExpr ReadSExprNode()
        {
        begin:
            if (Reader.BaseStream.Position >= Reader.BaseStream.Length)
                return null;

            var token = ReadSExprToken();

            if (token == null)
                return null;

            switch (token)
            {
                case "(":
                    return ReadSExprCons();

                case "\"":
                    return ReadSExprString();

                case "'":
                    return ReadSExprQuote();

                case "`":
                    return ReadSExprQuasiQuote();

                case ",":
                    return ReadSExprUnquote();

                case ";":
                    for (char c; (Reader.BaseStream.Position < Reader.BaseStream.Length) && ((c = Reader.ReadChar()) != '\r' && c != '\n');) ;
                    goto begin;

                default:
                    if (bool.TryParse(token, out var boolean))
                        return new SExprBoolean { Value = boolean };
                    else if (long.TryParse(token, out var integer))
                        return new SExprInteger { Value = integer };
                    else if (double.TryParse(token, out var real))
                        return new SExprReal { Value = real };
                    else
                        return new SExprSymbol { Value = token };
            }
        }

        public List<ISExpr> ReadSExprNodes()
        {
            var nodes = new List<ISExpr>();

            while (Reader.BaseStream.Position < Reader.BaseStream.Length)
            {
                var node = ReadSExprNode();

                if (node == null)
                    break;

                nodes.Add(node);
            }

            return nodes;
        }
    }
}