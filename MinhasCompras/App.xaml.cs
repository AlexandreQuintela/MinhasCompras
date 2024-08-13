using MinhasCompras.Helpers;

namespace MinhasCompras
{
    public partial class App : Application
    {
        static SqLiteDatabaseHelper _db;
        public static SqLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_minhas_compras.db3");
                    _db = new SqLiteDatabaseHelper(path);
                }
                return _db;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
