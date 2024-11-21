using ConsoleTools;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using System.Net.Sockets;
using System.Xml.Linq;
namespace RealEstate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            var fileMenu = new ConsoleMenu(args, level:1);
            //fileMenu.Add("Load from XML file", );
            //fileMenu.Add("Manual load", );
        

            var customerMenu = new ConsoleMenu(args, level: 1);
        

            var propertyMenu = new ConsoleMenu(args, level: 1);
            //    propertyMenu.Add("Refresh properties", () => refreshProp.Show());
        

            var contractMenu = new ConsoleMenu(args, level: 1);
        

            var districtMenu = new ConsoleMenu(args, level: 1);
            //    districtMenu.Add("1. District", );
        

            var queryMenu = new ConsoleMenu(args, level: 1);
            //    queryMenu.Add("Properties for sale", );
            //    queryMenu.Add("Properties for rent", );
            //    queryMenu.Add("Top 10 properties", );
            //    queryMenu.Add("Avarage price", );
        

            var Menu = new ConsoleMenu(args, level: 0);
            Menu.Add("File", ()=>fileMenu.Show());
            Menu.Add("Customers", () => customerMenu.Show());
            Menu.Add("Properties", () => propertyMenu.Show());
            Menu.Add("Contrancts", () => contractMenu.Show());
            Menu.Add("Districts", () => districtMenu.Show());
            Menu.Add("Queries", () => queryMenu.Show());
            Menu.Add("Exit", ConsoleMenu.Close);
            Menu.Show();
            */
            
            Contract contracts = new Contract();
            contracts.Load();
            
            Customer customers = new Customer();
            customers.Load();
            
            Property properties = new Property();
            properties.Load();
        }
    }
}
