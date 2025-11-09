namespace Proyectofinal.Data
{
    public class Conexion
    {
        private string cadenaSQL = string.Empty;

        public Conexion()
        {


            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            cadenaSQL = builder.GetSection("ConnectionStrings:MyConnection").Value;
        }

        public string getCadenaSQL()
        {
            return cadenaSQL;
        }
    }
}
