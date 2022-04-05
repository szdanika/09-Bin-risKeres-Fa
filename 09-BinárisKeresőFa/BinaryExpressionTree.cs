using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_BinárisKeresőFa
{
    enum Operator { Add, Sub, Mul, Div, Pow }
    internal class BinaryExpressionTree
    {
        Node root { get; }
        private BinaryExpressionTree(Node root)
        {
            this.root = root;
        }
        public abstract class Node
        {
            protected Node(char data, Node left, Node right)
            {
                Data = data;
                Left = left;
                Right = right;
            }
            protected Node(char data) : this(data, null, null)
            {
            }

            public char Data { get; private set; }
            public Node Left { get; private set; }
            public Node Right { get; private set; }
        }
        public class OperandNode : Node
        {
            public OperandNode(char data) : base(data)
            { }
        }
        public class OperatorNode : Node
        {
            Operator op { get; }

            public OperatorNode(char data, Node left, Node Right) : base(data, left, Right)
            {
                switch(data)
                {
                    case '+': op = Operator.Add; break;
                    case '-': op = Operator.Sub; break;
                    case '*': op = Operator.Mul; break;
                    case '/': op = Operator.Div; break;
                    case '^': op = Operator.Pow; break;
                    default: break; // hiba eldobás 

                }
            }

        }

        public static BinaryExpressionTree Build(string exoression)
        {
            return Build(exoression.ToCharArray());
        }
        public static BinaryExpressionTree Build(char[] expression)
        {
            Stack<Node> verem = new Stack<Node>();
            for(int i = 0; i< expression.Length; i++)
            {
                if(char.IsNumber(expression[i]))
                {
                    OperandNode operand = new OperandNode(expression[i]);
                    verem.Push(operand);
                }else if(IsItOperator(expression[i]))
                {
                    OperatorNode uj = new OperatorNode(expression[i], verem.Pop(), verem.Pop());
                    verem.Push(uj);
                }
            }
            return  new BinaryExpressionTree(verem.Pop());

        }
        public static bool IsItOperator(char op)
        {
            return (op == '+' || op == '-' || op == '*' || op == '^' ||op =='/');
        }
        public override string ToString()
        {
            if (root == null)
                return "";
            else
                return _ToString(root);
        }
        private string _ToString(Node node)
        {
            _ToString(node.Left);
            _ToString(node.Right);
            return _ToString(node.Left) + _ToString(node.Right) + node.Data;
        }
    }
}
