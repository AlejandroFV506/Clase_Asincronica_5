using AVL;

class Program
{
    static void Main(string[] args)
    {
        AVLTree tree = new AVLTree();

        Console.WriteLine("Insertando valores: 10, 20, 30, 40, 50, 25");
        int[] keys = { 10, 20, 30, 40, 50, 25 };
        foreach (int key in keys)
        {
            tree.Insert(key);
        }

        // Mostrar recorridos
        tree.PrintInOrder();
        tree.PrintPreOrder();
        tree.PrintPostOrder();

        Console.WriteLine("\nEliminando 30...");
        tree.Delete(30);
        tree.PrintInOrder();

        Console.WriteLine("Eliminando 40...");
        tree.Delete(40);
        tree.PrintInOrder();
    }
}
