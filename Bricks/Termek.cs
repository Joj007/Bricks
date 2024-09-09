using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks
{
    class Termek
    {
        string id;
        string typeId;
        int colorId;
        string name;
        string typeName;
        string colorName;
        int categoryId;
        string categoryName;
        string status;
        int quantity;
        double price;
        string condition;

        public Termek(string id, string typeId, int colorId, string name, string typeName, string colorName, int categoryId, string categoryName, string status, int quantity, double price, string condition)
        {
            this.id = id;
            this.typeId = typeId;
            this.colorId = colorId;
            this.name = name;
            this.typeName = typeName;
            this.colorName = colorName;
            this.categoryId = categoryId;
            this.categoryName = categoryName;
            this.status = status;
            this.quantity = quantity;
            this.price = price;
            this.condition = condition;
        }

        public Termek(string[] aru)
        {
            this.id = aru[0];
            this.typeId = aru[1];
            this.colorId = int.Parse(aru[2]);
            this.name = aru[3];
            this.typeName = aru[4];
            this.colorName = aru[5];
            this.categoryId = int.Parse(aru[6]);
            this.categoryName = aru[7];
            this.status = aru[8];
            this.quantity = int.Parse(aru[9]);
            this.price = double.Parse(aru[10].Replace('.', ','));
            this.condition = aru[11];
        }

        public string Id { get => id;}
        public string TypeId { get => typeId;}
        public int ColorId { get => colorId;}
        public string Name { get => name;}
        public string TypeName { get => typeName;}
        public string ColorName { get => colorName;}
        public int CategoryId { get => categoryId;}
        public string CategoryName { get => categoryName;}
        public string Status { get => status;}
        public int Quantity { get => quantity;}
        public double Price { get => price;}
        public string Condition { get => condition;}


    }
}
