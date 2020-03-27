namespace TaschenrechnerCore
{
    public class TaschenrechnerLogik
    {
        public static int Zahl1 { get; set; }
        public static int Zahl2 { get; set; }
        public static int Result { get; set; }

        public static void AddTwoNumbers()
        {
            Result = Zahl1 + Zahl2;
        }
    }
}