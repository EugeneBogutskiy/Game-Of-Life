using UnityEngine;

public class Cell : MonoBehaviour
{
    public Renderer rend;

    public bool isAlive; // живая клетка или нет
    public bool isNextAlive; // живая клетка или нет в слудующей итерации

    // расположение клетки по индексу y и x в двумерном массиве
    public int yIndex;
    public int xIndex;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // при нажатии на клетку меняем ее состояние
    void OnMouseDown()
    {
        if (isAlive)
        {
            rend.material.color = Color.white;
            isAlive = false;
        }
        else
        {
            rend.material.color = Color.black;
            isAlive = true;
        }
    }

    public void IsAlive(bool state)
    {
        if (state)
        {
            rend.material.color = Color.black;
            isAlive = true;
        }
        else
        {
            rend.material.color = Color.white;
            isAlive = false;
        }
    }
}