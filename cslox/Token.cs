using System.Runtime.ConstrainedExecution;

namespace CSLox
{
    class Token
    {
        internal readonly TokenType type;
        internal readonly string lexeme;
        internal readonly object? literal;
        internal readonly int line;

        public Token(TokenType type, string lexeme, object? literal, int line)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;
        }

        public override string ToString()
        {
            return $"{type} {lexeme} {literal}";
        }
    }
}
