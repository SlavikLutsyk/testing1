using MySql.Data.MySqlClient;
using AnalaizerClassLibrary;
using GraphInterface;


namespace AnalaizerTests
{
    [TestClass]
    public class AnalaizerUnitTest
    {
        string connectionString = "server=localhost;port=3306;database=sample;uid=root;password=10Gejhupov!";
        [TestMethod]
        public void TestInsertSymbolMethod()
        {
            string res;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // DbConnection that is already opened
                using (CalculatorDbContext context = new CalculatorDbContext(connection, false))
                {
                    var list = context.InsertSymbolSetValues;
                    foreach (var item in list)
                    {
                        Console.WriteLine(item);
                        res = AnalaizerClass.InsertSymbol(item.InputString, item.Symbol[0], item.Position);
                        Console.WriteLine(res);
                        if (item.TestEqual) Assert.AreEqual(item.ExpectedResult, res);
                        else Assert.AreNotEqual(item.ExpectedResult, res);
                    }
                }

            }
        }
        [TestMethod]
        public void TestIsOperatorMethod()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                connection.Open();
                // DbConnection that is already opened
                using (CalculatorDbContext context = new CalculatorDbContext(connection, false))
                {
                    var list = context.IsOperatorSetValues;
                    foreach (var item in list)
                    {

                        if (item.TestTrue) Assert.IsTrue(AnalaizerClass.IsOperator(item.Operator));
                        else Assert.IsFalse(AnalaizerClass.IsOperator(item.Operator));
                    }
                }
            }
        }
    }
}