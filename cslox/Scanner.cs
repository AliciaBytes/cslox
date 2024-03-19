namespace CSLox
{
    class Scanner
    {
        private readonly string source;
        private readonly List<Token> tokens = new List<Token>();

        public Scanner(string source)
        {
            this.source = source;
        }

        List<Token> scanTokens()
        {
            while (!isAtEnd())
            {
                // We are at the beginning of the next lexeme.
                start = current;
                scanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }
    }
}
