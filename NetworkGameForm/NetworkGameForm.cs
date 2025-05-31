using NetworkGame;
using NetworkGameForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle.NetworkGame
{
    public partial class NetworkGame : Form
    {
        // Кнопки главного меню
        private Button btnCreateGame;
        private Button btnJoinGame;
        private Button btnRotateShips;
        private Button btnExit;

        public NetworkGame()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Настройка формы
            this.Text = "Сетевой Морской бой";
            this.Size = new System.Drawing.Size(400, 300);

            // Кнопка "Создать игру (сервер)"
            btnCreateGame = new Button
            {
                Text = "Создать игру (Сервер)",
                Location = new System.Drawing.Point(100, 50),
                Size = new System.Drawing.Size(200, 40)
            };
            btnCreateGame.Click += BtnCreateGame_Click;

            // Кнопка "Присоединиться (Клиент)"
            btnJoinGame = new Button
            {
                Text = "Присоединиться (Клиент)",
                Location = new System.Drawing.Point(100, 100),
                Size = new System.Drawing.Size(200, 40)
            };
            btnJoinGame.Click += BtnJoinGame_Click;


            // Кнопка "Выход"
            btnExit = new Button
            {
                Text = "Выход в главное меню",
                Location = new System.Drawing.Point(100, 150),
                Size = new System.Drawing.Size(200, 40)
            };
            btnExit.Click += BtnExit_Click;

            // Добавляем кнопки на форму
            this.Controls.Add(btnCreateGame);
            this.Controls.Add(btnJoinGame);
            this.Controls.Add(btnRotateShips);
            this.Controls.Add(btnExit);
        }

        private void BtnCreateGame_Click(object sender, EventArgs e)
        {
            // Запуск в режиме сервера
            var gameForm = new OnlineGameForm(true);
            gameForm.Show();
            this.Hide();
        }

        private void BtnJoinGame_Click(object sender, EventArgs e)
        {
            // Запуск в режиме клиента
            var gameForm = new OnlineGameForm(false);
            gameForm.Show();
            this.Hide();
        }

        private void BtnRotateShips_Click(object sender, EventArgs e)
        {
            // Логика поворота будет в игровой форме
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            // Возврат в главное меню
            this.Close();
        }
    }
}
