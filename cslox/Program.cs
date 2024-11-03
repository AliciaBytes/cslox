namespace CSLox
{
    class Program
    {
        private readonly static Interpreter interpreter = new Interpreter();
        public static int Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: jlox [script]");
                return 64;
            }
            else if (args.Length == 1)
            {
                runFile(args[0]);
            }
            else
            {
                runPrompt();
            }
            return 0;
        }

        // Read a whole file into memory and run it.
        private static void runFile(string path)
        {
            string source = File.ReadAllText(path);
            run(source);
        }

        // Read from stdin line by line and run each line.
        private static void runPrompt()
        {
            while (true)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (line == null) break;
                run(line);
                ErrorReporter.defaultReporter.hadError = false;
            }
        }

        private static void run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.scanTokens();

            // For now just print each of the tokens.
            Parser parser = new Parser(tokens);
            Expr expression = parser.parse();

            // Stop if there was a syntax error.
            if (ErrorReporter.defaultReporter.hadError) Environment.Exit(65);
            if (ErrorReporter.defaultReporter.hadRuntimeError) Environment.Exit(70);

            interpreter.interpret(expression);
        }
    }
}
