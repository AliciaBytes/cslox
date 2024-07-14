namespace CSLox
{
    class ErrorReporter
    {
        public static ErrorReporter defaultReporter = new ErrorReporter();

        public bool hadError { get; set; }

        public ErrorReporter()
        {
            hadError = false;
        }
        internal void Error(int line, string message)
        {
            report(line, "", message);
        }

        private void report(int line, string where, string message)
        {
            Console.Error.WriteLine($"[line {line}] Error{where}: {message}");
            hadError = true;
        }

        internal void Error(Token token, string message)
        {
            if (token.type == TokenType.EOF)
            {
                report(token.line, " at end", message);
            }
            else
            {
                report(token.line, " at '" + token.lexeme + "'", message);
            }
        }
    }
}
