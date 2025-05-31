using NetworkGameForm;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetworkGame { 
public partial class OnlineGameForm : Form
{
    private bool isServer; // Сервер или клиент
    private GameServer server; // Объект сервера
    private GameClient client; // Объект клиента

    // Игровые поля (10x10)
    private Button[,] playerGrid; // Ваши корабли
    private Button[,] enemyGrid;  // Корабли противника
    private Button btnRotate;     // Кнопка поворота

    public OnlineGameForm(bool isServer)
    {
        this.isServer = isServer;
        InitializeComponents();

        if (isServer)
        {
            // Запускаем сервер
            server = new GameServer();
            server.Start();
        }
        else
        {
            // Подключаемся как клиент
            client = new GameClient();
            client.Connect("127.0.0.1"); // Подключаемся к localhost
        }
    }

    private void InitializeComponents()
    {
        // Настройка формы
        this.Text = isServer ? "Морской бой - Сервер" : "Морской бой - Клиент";
        this.Size = new Size(800, 600);

        // Создаем игровые поля
        CreateGrids();

        // Кнопка поворота
        btnRotate = new Button
        {
            Text = "Повернуть (R)",
            Location = new Point(50, 400),
            Size = new Size(100, 40)
        };
        btnRotate.Click += BtnRotate_Click;
        this.Controls.Add(btnRotate);
    }

    private void CreateGrids()
    {
        playerGrid = new Button[10, 10];
        enemyGrid = new Button[10, 10];

        // Ваше поле (размещение кораблей)
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                playerGrid[i, j] = new Button
                {
                    Size = new Size(30, 30),
                    Location = new Point(50 + j * 32, 50 + i * 32),
                    Tag = new Point(i, j) // Для идентификации клетки
                };
                playerGrid[i, j].Click += PlayerGrid_Click;
                this.Controls.Add(playerGrid[i, j]);
            }
        }

        // Поле противника (атаки)
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                enemyGrid[i, j] = new Button
                {
                    Size = new Size(30, 30),
                    Location = new Point(400 + j * 32, 50 + i * 32),
                    Tag = new Point(i, j)
                };
                enemyGrid[i, j].Click += EnemyGrid_Click;
                this.Controls.Add(enemyGrid[i, j]);
            }
        }
    }

    private void PlayerGrid_Click(object sender, EventArgs e)
    {
        // Размещение кораблей
        var button = (Button)sender;
        var position = (Point)button.Tag;

        // Логика размещения корабля...
    }

    private void EnemyGrid_Click(object sender, EventArgs e)
    {
        // Атака по противнику
        var button = (Button)sender;
        var position = (Point)button.Tag;

        // Отправляем сообщение о выстреле...
    }

    private void BtnRotate_Click(object sender, EventArgs e)
    {
        // Меняем ориентацию корабля (горизонтальная/вертикальная)
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // При закрытии формы останавливаем сервер/отключаем клиент
        if (isServer && server != null)
            server.Stop();

        if (!isServer && client != null)
            client.Disconnect();

        base.OnFormClosing(e);
    }
}
}