using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject cellPrefab;
    TimeController timeController;

    GameObject[,] cells = new GameObject[35, 50];

    int height = 35;
    int width = 50;
    float spacing = 0.146f; // расстояние между клетками
    public int population; // номер популяции

    void Start()
    {
        InitGameField(); // инициализируем игровое поле
        timeController = GetComponent<TimeController>();
    }

    void FixedUpdate()
    {
        CheckEachCell();
        DrawNewPopulation();
    }

    private void InitGameField()
    {
        // расставляем клетки по игровому полю в двумерном массиве
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = new Vector3(x * spacing, y * -spacing, 0);
                GameObject cloneCellPrefab = Instantiate(cellPrefab, pos, Quaternion.identity) as GameObject;

                // заполняем массив клетками
                cells[y, x] = cloneCellPrefab;

                // передаем в клетку ее расположение в поле и массиве
                cloneCellPrefab.GetComponent<Cell>().yIndex = y;
                cloneCellPrefab.GetComponent<Cell>().xIndex = x;
            }
        }
    }

    // проверяет состояние каждой клетки в каждом поколении
    private void CheckEachCell()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // проверка состояния клетки и подсчет количества живых соседей
                bool isAlive = cells[y, x].GetComponent<Cell>().isAlive;
                int count = GetAliveNeighbors(y, x);
                bool result = false;

                // здесь применяем правила игры к конкретной клетке
                if (isAlive && count < 2)
                    result = false;
                if (isAlive && (count == 2 || count == 3))
                    result = true;
                if (isAlive && count > 3)
                    result = false;
                if (!isAlive && count == 3)
                    result = true;

                // присваиваем клетке ее будущее состояние, не меняя текущее
                // затем в слудующей итерации будем считывать это состояние и передавать в функцию
                cells[y, x].GetComponent<Cell>().isNextAlive = result;
            }
        }
    }

    // считает сколько соседей текущей клетки в данном поколении живы
    private int GetAliveNeighbors(int y, int x)
    {
        // счетчик живых соседей
        int count = 0;

        // справа от клетки
        if (cells[y, x].GetComponent<Cell>().xIndex != width - 1)
            if (cells[y, x + 1].GetComponent<Cell>().isAlive)
                count++;

        // слева от клетки
        if (cells[y, x].GetComponent<Cell>().xIndex != 0)
            if (cells[y, x - 1].GetComponent<Cell>().isAlive)
                count++;

        // сверху клетки
        if (cells[y, x].GetComponent<Cell>().yIndex != 0)
            if (cells[y - 1, x].GetComponent<Cell>().isAlive)
                count++;

        // снизу клетки
        if (cells[y, x].GetComponent<Cell>().yIndex != height - 1)
            if (cells[y + 1, x].GetComponent<Cell>().isAlive)
                count++;

        // справа сверху по диагонали
        if (cells[y, x].GetComponent<Cell>().yIndex != 0 && cells[y, x].GetComponent<Cell>().xIndex != width - 1)
            if (cells[y - 1, x + 1].GetComponent<Cell>().isAlive)
                count++;

        // слева сверху по диагонали
        if (cells[y, x].GetComponent<Cell>().yIndex != 0 && cells[y, x].GetComponent<Cell>().xIndex != 0)
            if (cells[y - 1, x - 1].GetComponent<Cell>().isAlive)
                count++;

        // справа снизу по диагонали
        if (cells[y, x].GetComponent<Cell>().yIndex != height - 1 && cells[y, x].GetComponent<Cell>().xIndex != width - 1)
            if (cells[y + 1, x + 1].GetComponent<Cell>().isAlive)
                count++;

        // слева снизу по диагонали
        if (cells[y, x].GetComponent<Cell>().yIndex != height - 1 && cells[y, x].GetComponent<Cell>().xIndex != 0)
            if (cells[y + 1, x - 1].GetComponent<Cell>().isAlive)
                count++;

        return count;
    }

    // проходит по массиву клеток и применяет к текущему состоянию рассчитанное будущее состояние
    private void DrawNewPopulation()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                cells[y, x].GetComponent<Cell>().IsAlive(cells[y, x].GetComponent<Cell>().isNextAlive);
            }
        }

        population++;
    }

    // рисуем кнопки
    void OnGUI()
    {
        if (GUI.Button(new Rect(50, 15, 80, 40), "Play/Pause"))
            timeController.Pause();

        if (GUI.Button(new Rect(150, 15, 80, 40), "Reset"))
            SceneManager.LoadScene("SampleScene");

        GUI.TextArea(new Rect(250, 15, 160, 40), "Population: \n" + population);
    }
}
