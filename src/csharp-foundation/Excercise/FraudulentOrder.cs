namespace csharp_foundation.Excercise
{
    public class FraudulentOrder
    {
        public void Find()
        {
            // string[] fraudulentOrderIDs = new string[3];

            // fraudulentOrderIDs[0] = "A123";
            // fraudulentOrderIDs[1] = "B456";
            // fraudulentOrderIDs[2] = "C789";
            // // fraudulentOrderIDs[3] = "D000";

            string[] fraudulentOrderIDs = { "A123", "B456", "C789" };

            Console.WriteLine($"First: {fraudulentOrderIDs[0]}");
            Console.WriteLine($"Second: {fraudulentOrderIDs[1]}");
            Console.WriteLine($"Third: {fraudulentOrderIDs[2]}");

            fraudulentOrderIDs[0] = "F000";

            Console.WriteLine($"Reassign First: {fraudulentOrderIDs[0]}");

            Console.WriteLine($"There are {fraudulentOrderIDs.Length} fraudulent orders to process.");

            //Excercise
            string[] orderIds = { "B123", "C234", "A345", "C15", "B177", "G3003", "C235", "B179" };

            foreach (string id in orderIds)
            {
                if (id.StartsWith("B"))
                {
                    Console.WriteLine(id);
                }
            }
        }
    }
}