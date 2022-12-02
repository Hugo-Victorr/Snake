using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake

{
    internal class Food
    {
        //pegar a localização da maça; set é private pq as outras classes não podrao setar um valor, apenas ler o valor
        //"get e set" para a Location virar uma propriedade
        public Point Location { get; private set;}

        //criar de fato a comida para a cobra
        public void CreateFood()
        {
            Random rnd = new Random(); //vai sortear o valor tanto no eixo X quanto no Y

            Location = new Point(rnd.Next(0, 27), rnd.Next(0, 27));
        }
    }
}
