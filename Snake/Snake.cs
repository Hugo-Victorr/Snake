using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake

{
    internal class Snake
    {
        //ATRIBUTO: tamanho da cobr4a, pois ela possui um tamanho q vai aumentando e ocupa mais de um espaço
        public int Length { get; private set; }

        //ATRIBUTO: pegar localização; informando que éum array
        public Point [] Location { get; private set; } //Point pega a posição em X e Y

        //Construtor
        public Snake()
        {
            Location = new Point[28*28]; 
            Reset();
        }

        //Metodo quando o jogo recomeçar
        public void Reset()
        {
            Length = 5;//tamanho inicial da cobra
            for( int i = 0; i < Length; i++)
            {
                //Completa o tamanho inicial da cobra, onde o tamanho inicial é 5,
                //sendo o primeiro a cabeça e o resto o corpo
                Location[i].X = 12;
                Location[i].Y = 12;
            }
        }

        //metdo que faz com que a cabeça não se separe do corpo; faz com quie todas as partes 
        public void Follow()
        {
            for (int i = Length -1; i > 0; i--)
            {
                Location[i] = Location[i - 1];
            }
        }

        //Os metodos a seguir irão movimentar a cabeça, por consequencia do metodos "Follow" o corpo vem junto
        public void Up()
        {
            Follow();
            Location[0].Y--;
            if(Location [0].Y < 0)
            {
                Location[0].Y += 28;   
            }
        }

        public void Down()
        {
            Follow();
            Location[0].Y++;
            if (Location[0].Y > 27)
            {
                Location[0].Y -= 28;
            }
        }

        public void Left()
        {
            Follow();
            Location[0].X--;
            if (Location[0].X < 0)
            {
                Location[0].X += 28;
            }
        }

        public void Rigth()
        {
            Follow();
            Location[0].X++;
            if (Location[0].X > 27)
            {
                Location[0].X -= 28;
            }
        }

        public void Eat()
        {
            Length++;

        }
    }

}
