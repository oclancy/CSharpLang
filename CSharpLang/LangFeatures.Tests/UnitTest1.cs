using LangFeatures;
namespace LangFeatures.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            Console.WriteLine(list);

            list.Append(1);

            Console.WriteLine(list);

            list.Append(2);
            list.Append(3);
            list.Append(2);

            Console.WriteLine(list);

            list.Remove(2);

            Console.WriteLine(list);

            list.Insert(5,1);

            Console.WriteLine(list);

            list.Remove(1);
            
            Console.WriteLine(list);

            list.Reverse();
            Console.WriteLine(list);
        }

        
    }
}