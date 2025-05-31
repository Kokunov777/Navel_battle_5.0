using SeaBattle.SingleGame;
using System.Collections.Generic;
using System;
using SingleGameForm;

public class Bot
{
    public int[,] Field { get; }
    public List<Ship> Ships { get; }
    private Random random;

    public Bot()
    {
        Field = new int[10, 10];
        Ships = new List<Ship>();
        random = new Random();
        PlaceShipsRandom();
    }

    private void PlaceShipsRandom()
    {
        int[] shipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        foreach (int size in shipSizes)
        {
            bool placed = false;
            int attempts = 0;

            while (!placed && attempts < 100)
            {
                int x = random.Next(0, 10);
                int y = random.Next(0, 10);
                bool isHorizontal = random.Next(0, 2) == 0;

                if (CanPlaceShip(x, y, size, isHorizontal))
                {
                    PlaceShip(x, y, size, isHorizontal);
                    placed = true;
                }
                attempts++;
            }
        }
    }

    private bool CanPlaceShip(int x, int y, int size, bool isHorizontal)
    {
        if (x < 0 || y < 0) return false;
        if (isHorizontal && x + size > 10) return false;
        if (!isHorizontal && y + size > 10) return false;

        for (int i = -1; i <= size; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int checkX = isHorizontal ? x + i : x + j;
                int checkY = isHorizontal ? y + j : y + i;

                if (checkX >= 0 && checkX < 10 && checkY >= 0 && checkY < 10)
                {
                    if (Field[checkX, checkY] != 0)
                        return false;
                }
            }
        }
        return true;
    }

    private void PlaceShip(int x, int y, int size, bool isHorizontal)
    {
        for (int i = 0; i < size; i++)
        {
            int posX = isHorizontal ? x + i : x;
            int posY = isHorizontal ? y : y + i;
            Field[posX, posY] = 1;
        }
        Ships.Add(new Ship(x, y, size, isHorizontal));
    }

    public (int x, int y) MakeMove(int[,] playerField)
    {
        List<(int x, int y)> availableCells = new List<(int, int)>();

        // Собираем все доступные для выстрела клетки
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (playerField[x, y] != 2 && playerField[x, y] != 3)
                {
                    availableCells.Add((x, y));
                }
            }
        }

        // Если есть доступные клетки, выбираем случайную
        if (availableCells.Count > 0)
        {
            return availableCells[random.Next(availableCells.Count)];
        }

        // Если все клетки уже обстреляны (теоретически невозможно)
        return (-1, -1);
    }
}