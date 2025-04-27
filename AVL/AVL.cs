using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    public class AVLNode
    {
        public int Key { get; set; }
        public int Height { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }

        public AVLNode(int key)
        {
            Key = key;
            Height = 1;
            Left = Right = null;
        }
    }

    public class AVLTree
    {
        private AVLNode root;

        private int Height(AVLNode node)
        {
            return node == null ? 0 : node.Height;
        }

        private int GetBalance(AVLNode node)
        {
            if (node == null)
                return 0;
            return Height(node.Right) - Height(node.Left);
        }

        private AVLNode RightRotate(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private AVLNode LeftRotate(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public void Insert(int key)
        {
            root = InsertRecursive(root, key);
        }

        private AVLNode InsertRecursive(AVLNode node, int key)
        {
            if (node == null)
                return new AVLNode(key);

            if (key < node.Key)
                node.Left = InsertRecursive(node.Left, key);
            else if (key > node.Key)
                node.Right = InsertRecursive(node.Right, key);
            else
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            return BalanceNode(node);
        }

        public void Delete(int key)
        {
            root = DeleteRecursive(root, key);
        }

        private AVLNode DeleteRecursive(AVLNode node, int key)
        {
            if (node == null)
                return node;

            if (key < node.Key)
                node.Left = DeleteRecursive(node.Left, key);
            else if (key > node.Key)
                node.Right = DeleteRecursive(node.Right, key);
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    AVLNode temp = node.Left ?? node.Right;

                    if (temp == null)
                    {
                        temp = node;
                        node = null;
                    }
                    else
                        node = temp;
                }
                else
                {
                    AVLNode temp = MinValueNode(node.Right);
                    node.Key = temp.Key;
                    node.Right = DeleteRecursive(node.Right, temp.Key);
                }
            }

            if (node == null)
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            return BalanceNode(node);
        }

        private AVLNode BalanceNode(AVLNode node)
        {
            int balance = GetBalance(node);

            if (balance < -1 && GetBalance(node.Left) <= 0)
                return RightRotate(node);

            if (balance < -1 && GetBalance(node.Left) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            if (balance > 1 && GetBalance(node.Right) >= 0)
                return LeftRotate(node);

            if (balance > 1 && GetBalance(node.Right) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private AVLNode MinValueNode(AVLNode node)
        {
            AVLNode current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        // Métodos de recorrido simplificados
        public void PrintInOrder()
        {
            Console.Write("Recorrido InOrden: ");
            InOrder(root);
            Console.WriteLine();
        }

        public void PrintPreOrder()
        {
            Console.Write("Recorrido PreOrden: ");
            PreOrder(root);
            Console.WriteLine();
        }

        public void PrintPostOrder()
        {
            Console.Write("Recorrido PostOrden: ");
            PostOrder(root);
            Console.WriteLine();
        }

        private void InOrder(AVLNode node)
        {
            if (node != null)
            {
                InOrder(node.Left);
                Console.Write(node.Key + " ");
                InOrder(node.Right);
            }
        }

        private void PreOrder(AVLNode node)
        {
            if (node != null)
            {
                Console.Write(node.Key + " ");
                PreOrder(node.Left);
                PreOrder(node.Right);
            }
        }

        private void PostOrder(AVLNode node)
        {
            if (node != null)
            {
                PostOrder(node.Left);
                PostOrder(node.Right);
                Console.Write(node.Key + " ");
            }
        }
    }
}
