namespace CSLox
{
    abstract class Expr
    {
        internal interface Visitor<R>
        {
            R visitAssignExpr(Assign expr);
            R visitBinaryExpr(Binary expr);
            R visitCallExpr(Call expr);
            R visitGetExpr(Get expr);
            R visitGroupingExpr(Grouping expr);
            R visitLiteralExpr(Literal expr);
            R visitLogicalExpr(Logical expr);
            R visitSetExpr(Set expr);
            R visitSuperExpr(Super expr);
            R visitThisExpr(This expr);
            R visitUnaryExpr(Unary expr);
            R visitVariableExpr(Variable expr);
        }

        // Nested Expr classes here...
        internal class Binary : Expr
        {
            Binary(Expr left, Token op, Expr right)
            {
                this.left = left;
                this.op = op;
                this.right = right;
            }

            internal override R accept<R>(Visitor<R> visitor)
            {
                return visitor.visitBinaryExpr(this);
            }

            internal readonly Expr left;
            internal readonly Token op;
            internal readonly Expr right;
        }

        internal class Grouping : Expr
        {
            Grouping(Expr expression)
            {
                this.expression = expression;
            }

            internal override R accept<R>(Visitor<R> visitor)
            {
                return visitor.visitGroupingExpr(this);
            }

            internal readonly Expr expression;
        }

        internal class Literal : Expr
        {
            Literal(object value)
            {
                this.value = value;
            }

            @
            internal override R accept<R>(Visitor<R> visitor)
            {
                return visitor.visitLiteralExpr(this);
            }

            internal readonly object value;
        }

        internal class Unary : Expr
        {
            Unary(Token op, Expr right)
            {
                this.op = op;
                this.right = right;
            }

            internal override R accept<R>(Visitor<R> visitor)
            {
                return visitor.visitUnaryExpr(this);
            }

            internal readonly Token op;
            internal readonly Expr right;
        }

        internal abstract R accept<R>(Visitor<R> visitor);
    }
}
