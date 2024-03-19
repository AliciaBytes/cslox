namespace CSLox
{
    class ErrorReporter
    {
        public bool hadError { get; set; }

        public ErrorReporter()
        {
            hadError = false;
        }
        internal static void Error(int line, string message)
        {
            report(line, "", message);
        }

        private static void report(int line, string where, string message)
        {
            Console.Error.WriteLine($"[line {line}] Error{where}: {message}");
        }
    }
}
