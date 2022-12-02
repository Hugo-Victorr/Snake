using System;
using System.Collections.Generic;
using System.Drawing; //Utilizado para acrescentar ferramentas graficas
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //utilizado para arescentar tipos "keys" por exemplo

namespace Snake

{
    internal class Game
    {
        //indentifica qual tecla foi digitada no teclado
        public Keys Direction {get; set;}

        //identifica qual direção a cobra vai de acorod com a tecla digitada
        public Keys Arrow { get; set; }

        //propriedades criadas para alterar os valores no formulario por referencia
        private Timer Frame { get; set; }

        private Label LbPontuacao { get; set; }

        private Panel PnTela { get; set; }

        //propriedade para amazenar a pontuação
        private int pontos = 0;

        //classe Food
        private Food Food;

        //classe Snake
        private Snake Snake;

        //ATRIBUTOS GRAFICOS
        //trabalha comos pixels da tela. Vai definir as dimensoes da tela 
        private Bitmap offScreenBitmap;//define o inicio da tela

        //Gerar os componentes em si
        private Graphics bitmapGraph;

        //limpa a tela e atualiza com a nova comida e a nova posição da cobra
        private Graphics screenGraph;

        //CONSTRUTOR
        //Passar valores por referencia
        public Game(ref Timer timer, ref Label label, ref Panel panel)
        {
            PnTela = panel; //passa os valores de refencia para as variaveis originais
            Frame = timer; //passa os valores de refencia para as variaveis originais
            LbPontuacao = label; //passa os valores de refencia para as variaveis originais

            offScreenBitmap = new Bitmap(428, 428);//cria a tela na medida certa

            Snake = new Snake(); //cria a cobra
            Food = new Food(); //cria a comida

            //setar direção inicial que a conra vai anda, no caso para a esquerda
            Direction = Keys.Left;
            Arrow = Direction;
        
        }

        public void StartGame()
        {
            Snake.Reset();//define o tamanho da cobra padrão de inicio do jogo

            Food.CreateFood(); // cria a comida inicial

            Direction = Keys.Left; //Define a direção padrão de inicio do jogo

            bitmapGraph = Graphics.FromImage(offScreenBitmap); //iniciar os graficos

            screenGraph = PnTela.CreateGraphics();//passa a referencia para onde a tela deve ser renderizada, no caso no "PnTela"

            Frame.Enabled = true; //inicia a contagem do Timer, cotando 100 milesimos por segundo

        }

        //a logica do jogo rodará aqui

        public void Tick()
        {
            //verificar se a direção digitada é valida
            //se a direção selecionada for contraria ao movimento atual, esse movimento é invalida, portanto não será executado 
            if((Arrow == Keys.Left) && (Direction != Keys.Right) ||
            (Arrow == Keys.Right) && (Direction != Keys.Left) ||
            (Arrow == Keys.Up) && (Direction != Keys.Down) ||
            (Arrow == Keys.Down) && (Direction != Keys.Up))
            {
                Direction = Arrow;
            }
            
            //fazer a cobra se mover de fato
            switch ( Direction)
            {
                case Keys.Left: 
                    Snake.Left(); //chama o metodo ja criado na classe Snake onde efetua o movimento da cobra
                    break;

                case Keys.Right:
                    Snake.Rigth(); //chama o metodo ja criado na classe Snake onde efetua o movimento da cobra
                    break;

                case Keys.Up:
                    Snake.Up(); //chama o metodo ja criado na classe Snake onde efetua o movimento da cobra
                    break;

                case Keys.Down:
                    Snake.Down(); //chama o metodo ja criado na classe Snake onde efetua o movimento da cobra
                    break;
            }

            //Renderizar os pixels de "movimento" da cobra, preenchendo o novo pixel e esvaziando o pixel antigo
            bitmapGraph.Clear(Color.White);

            //cria a maça
            bitmapGraph.DrawImage(Properties.Resources.maça, (Food.Location.X * 15), (Food.Location.Y * 15), 15, 15);

            //verifica se a cobra colidiu com o corpo
            bool gameOver = false;

            for(int i = 0; i < Snake.Length; i++)
            {
                if(i == 0)
                {
                    //Pinta o pixel da cobra de preto se i for a posição zero, para diferenciar como sendo a cabeça
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#000000")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);
                }
                else
                {
                    //pinta o pixel de outra cor caso nao seja a posição zero (a cabeça da cobra)
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#4F4F4F")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);
                }

                //logica para saber se a cabeça colidiu com o corpo
                if((Snake.Location[i] == Snake.Location[0]) && (i > 0)) //indetifica se a cabeça esta no mesmo pixel da cabeça esta na mesma Location que outra parte do corpo,
                                                                       //e isso so acontecerá se o "i" for maior que zero, pois se não, o jogo vai  detectar que a cabeça esta batedo na cabeça
                {
                    gameOver = true;//indica que o jogo acabou
                }
            }

            //resertar o offScrreen, fazer ele voltar pra parte de cima
            screenGraph.DrawImage(offScreenBitmap, 0, 0);

            CheckCollision(); //verifica se a cobra colidiu com o corpo
            if (gameOver == true)
            {
                GameOver();
            }
        }

        //metodo que verifica se a cobra colidiu com a comida
        public void CheckCollision()
        {
            if(Snake.Location[0] == Food.Location)
            {
                Snake.Eat(); //chama o metodo que quando a cobra come, ela aumenta de tamanho
                Food.CreateFood();//metodo de criar uma nova comida em um novo espaço
                pontos += 9; //incremento de pontos de 9 a 9
                LbPontuacao.Text = "PONTOS: " + pontos; //concatena o texto "PONTOS" com o valor da pontuação
            }
        }

        //metodo para proceder o final do jogo
        public void GameOver()
        {
            Frame.Enabled = false;//para o contador
            bitmapGraph.Dispose();//limpa da memoria a parte grafica criada
            screenGraph.Dispose();//limpa a tela
            LbPontuacao.Text = "PONTOS: 0";
            pontos = 0;
            MessageBox.Show("Game Over");
        }

    }
}
