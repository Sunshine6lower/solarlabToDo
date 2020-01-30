namespace solarlabToDo
{
    internal static class Core
    {
        internal static void Init()
        {
            DB.InitDB();
        
            UI.StartUI();
        }
    }
}