namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        private const int Expected = 0;
        private neuesprojekt.test_Drehbuch t = new neuesprojekt.test_Drehbuch();
        private string currentPath;
        private string pathToTest;
        private string pathToFile;


        public void start()
        {
            currentPath = t.initializeTests();
            pathToTest = Path.Combine(currentPath, "TestDir");
            pathToFile = Path.Combine(pathToTest, "Mathe.csv");


            if (!Directory.Exists(pathToTest))
                Directory.CreateDirectory(pathToTest);
        }
        [TestMethod]
        public void TestMethod1()
        {
            start();
            var result = t.test1(pathToTest); ;
            Assert.AreEqual(Expected, result);
        }
        [TestMethod]
        public void TestMethod2()
        {
            start();
            var result = t.test2(pathToFile);
            Assert.AreEqual(Expected, result);
        }
        [TestMethod]
        public void TestMethod3()
        {
            start();
            var result = t.test3(pathToFile);
            Assert.AreEqual(Expected, result);
        }
        [TestMethod]
        public void TestMethod4()
        {
            var result = t.test4(); ;
            Assert.AreEqual(Expected, result);
            stop();
        }


        public void stop() {
            t.stopTests();
        }
    }
}