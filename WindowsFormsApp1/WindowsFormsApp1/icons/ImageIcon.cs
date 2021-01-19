using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class ImageIcon
    {
        private static Image Man = Image.FromFile(@"..\..\icons\icon_man.png");
        private static Image Woman = Image.FromFile(@"..\..\icons\icon_woman.png");

        private static Image Deer = Image.FromFile(@"..\..\icons\icon_deer.png");
        private static Image Mouse = Image.FromFile(@"..\..\icons\icon_mouse.png");
        private static Image Rabbit = Image.FromFile(@"..\..\icons\icon_rabbit.png");
        private static Image Bear = Image.FromFile(@"..\..\icons\icon_bear.png");
        private static Image Pig = Image.FromFile(@"..\..\icons\icon_pig.png");
        private static Image Raccoon = Image.FromFile(@"..\..\icons\icon_raccoon.png");
        private static Image Fox = Image.FromFile(@"..\..\icons\icon_fox.png");
        private static Image Lion = Image.FromFile(@"..\..\icons\icon_lion.png");
        private static Image Wolf = Image.FromFile(@"..\..\icons\icon_wolf.png");

        private static Image Apple = Image.FromFile(@"..\..\icons\icon_apple.png");
        private static Image Carrot = Image.FromFile(@"..\..\icons\icon_carrot.png");
        private static Image Oat = Image.FromFile(@"..\..\icons\icon_oat.png");

        private static Image Shard = Image.FromFile(@"..\..\icons\icon_shard.png");
        private static Image ShardCold = Image.FromFile(@"..\..\icons\icon_shard_cold.png");

        private static Image HousePart = Image.FromFile(@"..\..\icons\icon_house_part.png");
        private static Image BarnPart = Image.FromFile(@"..\..\icons\icon_barn_part.png");

        public static Image GetIcon(string entityName)
        {
            switch (entityName)
            {
                case "Man":
                    return Man;
                case "Woman":
                    return Woman;

                case "Deer":
                    return Deer;
                case "Mouse":
                    return Mouse;
                case "Rabbit":
                    return Rabbit;
                case "Bear":
                    return Bear;
                case "Pig":
                    return Pig;
                case "Raccoon":
                    return Raccoon;
                case "Fox":
                    return Fox;
                case "Lion":
                    return Lion;
                case "Wolf":
                    return Wolf;

                case "Apple":
                    return Apple;
                case "Carrot":
                    return Carrot;
                case "Oat":
                    return Oat;

                case "HousePart":
                    return HousePart;
                case "BarnPart":
                    return BarnPart;

                case "Shard":
                    return Shard;
                case "ShardCold":
                    return ShardCold;

                default:
                    return null;
            }
        }
            
    }
}
