using AVL;

namespace TestAVL
{
    [TestClass]
    public class AVLTreeTests
    {
        [TestMethod]
        //Debe contener el elemto
        public void Insert_SingleElement()
        {
            // Arrange
            var tree = new AVLTree();

            // Act
            tree.Insert(10);

            // Assert
            Assert.AreEqual("10", GetInOrder(tree));
        }

        [TestMethod]
        //Mantine el orden correcto
        public void Insert_MultipleElements()
        {
            // Arrange
            var tree = new AVLTree();

            // Act
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(30);
            tree.Insert(40);
            tree.Insert(50);
            tree.Insert(25);

            // Assert
            Assert.AreEqual("10 20 25 30 40 50", GetInOrder(tree));
        }

        [TestMethod]
        //Elimina el elemento
        public void Delete_LeafNode()
        {
            // Arrange
            var tree = new AVLTree();
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(5);

            // Act
            tree.Delete(20);

            // Assert
            Assert.AreEqual("5 10", GetInOrder(tree));
        }

        [TestMethod]
        //Mantiene el orden correcto
        public void Delete_NodeWithOneChild()
        {
            // Arrange
            var tree = new AVLTree();
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(5);
            tree.Insert(15);

            // Act
            tree.Delete(20);

            // Assert
            Assert.AreEqual("5 10 15", GetInOrder(tree));
        }

        [TestMethod]
        //Mantiene el orden correcto
        public void Delete_NodeWithTwoChildren()
        {
            // Arrange
            var tree = new AVLTree();
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(5);
            tree.Insert(15);
            tree.Insert(25);

            // Act
            tree.Delete(20);

            // Assert
            Assert.AreEqual("5 10 15 25", GetInOrder(tree));
        }

        [TestMethod]
        public void Delete_NonExistentElement()
        {
            // Arrange
            var tree = new AVLTree();
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(5);
            string original = GetInOrder(tree);

            // Act
            tree.Delete(99);

            // Assert
            Assert.AreEqual(original, GetInOrder(tree));
        }

        [TestMethod]
        public void Traversal_InOrder()
        {
            // Arrange
            var tree = new AVLTree();
            int[] elements = { 50, 30, 70, 20, 40, 60, 80 };
            foreach (var e in elements)
                tree.Insert(e);

            // Act & Assert
            Assert.AreEqual("20 30 40 50 60 70 80", GetInOrder(tree));
        }

        [TestMethod]
        public void Traversal_PreOrder()
        {
            // Arrange
            var tree = new AVLTree();
            tree.Insert(50);
            tree.Insert(30);
            tree.Insert(70);
            tree.Insert(20);
            tree.Insert(40);

            // Act & Assert
            Assert.AreEqual("50 30 20 40 70", GetPreOrder(tree));
        }

        [TestMethod]
        public void Traversal_PostOrder()
        {
            // Arrange
            var tree = new AVLTree();
            tree.Insert(50);
            tree.Insert(30);
            tree.Insert(70);
            tree.Insert(20);
            tree.Insert(40);

            // Act & Assert
            Assert.AreEqual("20 40 30 70 50", GetPostOrder(tree));
        }

        [TestMethod]
        public void Insert_DuplicateElements()
        {
            // Arrange
            var tree = new AVLTree();

            // Act
            tree.Insert(10);
            tree.Insert(10);
            tree.Insert(10);

            // Assert
            Assert.AreEqual("10", GetInOrder(tree));
        }

   
        private string GetInOrder(AVLTree tree)
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                tree.PrintInOrder();
                return consoleOutput.GetOutput().Replace("Recorrido InOrden: ", "").Trim();
            }
        }

        private string GetPreOrder(AVLTree tree)
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                tree.PrintPreOrder();
                return consoleOutput.GetOutput().Replace("Recorrido PreOrden: ", "").Trim();
            }
        }

        private string GetPostOrder(AVLTree tree)
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                tree.PrintPostOrder();
                return consoleOutput.GetOutput().Replace("Recorrido PostOrden: ", "").Trim();
            }
        }
    }

    
    public class ConsoleOutput : IDisposable
    {
        private readonly System.IO.StringWriter stringWriter;
        private readonly System.IO.TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new System.IO.StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
}