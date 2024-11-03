namespace CSLox
{
    class Interpreter : Expr.Visitor<object?>
    {
        internal void interpret(Expr expression)
        {
            try
            {
                object? value = evaluate(expression);
                Console.WriteLine(stringify(value));
            }
            catch (RuntimeError error)
            {
                ErrorReporter.defaultReporter.runtimeError(error);
            }
        }

        public object? visitLiteralExpr(Expr.Literal expr)
        {
            return expr.value;
        }

        public object? visitGroupingExpr(Expr.Grouping expr)
        {
            return evaluate(expr.expression);
        }

        public object? visitUnaryExpr(Expr.Unary expr)
        {
            object? right = evaluate(expr.right);

            switch (expr.op.type)
            {
                case TokenType.BANG:
                    return !isTruthy(right);
                case TokenType.MINUS:
                    checkNumberOperand(expr.op, right);
                    return -(double)right;
            }

            // Unreachable.
            return null;
        }

        public object? visitBinaryExpr(Expr.Binary expr)
        {
            object? left = evaluate(expr.left);
            object? right = evaluate(expr.right);

            switch (expr.op.type)
            {
                case TokenType.MINUS:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left - (double)right;
                case TokenType.SLASH:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left / (double)right;
                case TokenType.STAR:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left * (double)right;
                case TokenType.PLUS:
                    if (left is double && right is double)
                    {
                        return (double)left + (double)right;
                    }

                    if (left is string && right is string)
                    {
                        return (string)left + (string)right;
                    }
                    throw new RuntimeError(expr.op,
                                "Operands must be two numbers or two strings.");
                case TokenType.GREATER:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left > (double)right;
                case TokenType.GREATER_EQUAL:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left >= (double)right;
                case TokenType.LESS:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left < (double)right;
                case TokenType.LESS_EQUAL:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left <= (double)right;
                case TokenType.BANG_EQUAL: return !isEqual(left, right);
                case TokenType.EQUAL_EQUAL: return isEqual(left, right);
            }

            // Unreachable.
            return null;
        }

        private object? evaluate(Expr expr)
        {
            return expr.accept(this);
        }

        private bool isTruthy(object? obj)
        {
            if (obj == null) return false;
            if (obj is bool) return (bool)obj;
            return true;
        }

        private bool isEqual(object? a, object? b)
        {
            if (a == null && b == null) return true;

            return a == b;
        }
        private void checkNumberOperand(Token op, object? operand)
        {
            if (operand is double d) return;
            throw new RuntimeError(op, "Operand must be a number.");
        }

        private void checkNumberOperands(Token op,
                                 object? left, object? right)
        {
            if (left is double a && right is double b) return;

            throw new RuntimeError(op, "Operands must be numbers.");
        }

        private string stringify(object? obj)
        {
            if (obj == null) return "nil";

            if (obj is double d)
            {
                string text = d.ToString();
                if (text.EndsWith(".0"))
                {
                    text = text.Substring(0, text.Length - 2);
                }
                return text;
            }

            return obj.ToString();
        }
    }
}
