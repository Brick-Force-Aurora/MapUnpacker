namespace MapUnpacker
{
    static class Global
    {

        public static bool skipNoGeometry = true;

        public static Form1 form;

        public static void Print(string text)
        {
            form.PrintOutput(text);
        }
    }
}
