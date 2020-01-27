namespace Static
{
    class CounterStatic
    {
        public int counter => counterInternal;

        public static int counterInternal = 0;

        public void Add(int i)
        {
            counterInternal += i;

            // CS0176 C# Member cannot be accessed with an instance reference; qualify it with a type name instead
            // this.counterInternal += 1;
        }
    }
}