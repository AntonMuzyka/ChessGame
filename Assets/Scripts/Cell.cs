using UnityEngine;

public enum State /*стадії клітки*/
{
    Standart,
    Selected,
    Variant
}
public class Cell : MonoBehaviour
{
    public Color StandartColor; /*стандартний колір клітки*/
    public Color HoverColor; //колір клітинки при наведенні
    public Color HoverMoveColor; /*колір клітинки коли по ній можна пройти*/
    public Color HoverAttackColor; /*колір клітинки коли на ній ворог в діапазоні атаки*/

    public Color SelectedColor;
    public Color MoveColor;
    public Color AttackColor;

    public MeshRenderer MeshRenderer;

    public UnitBase Unit; /*юніт на клітинці*/
    [HideInInspector] public Vector2 Position; /*для зміни позиції клітинки*/
    [HideInInspector] public State State; /*для збереження стану клітинки*/

    public GridIteract GridIteract;

    public void ChangeColor(Color color) //метод для зміни кольору 
    {
        MeshRenderer.material.color = color;
    }

    private void OnMouseEnter()
    {
        if (State == State.Standart)
        {
            ChangeColor(HoverColor); /*зміна кольору клітинки принаведенні миші*/
        }
        else if (State == State.Variant)
        {
            if (Unit == null)
            {
                ChangeColor(HoverMoveColor);
            }
            else
            {
                ChangeColor(HoverAttackColor);
            }
        }
    }

    private void OnMouseDown() /*при натисканні на клітинку*/
    {
        if (State == State.Selected)
        {
            GridIteract.DeselectCell(this); //прибрати виділення
        }
        else if (State == State.Variant)
        {
            GridIteract.GridRepository.SelectedCell.Unit.GoToCell(this);
        }
        else if (State == State.Standart)
        {
            if (GridIteract.GridRepository.SelectedCell != null)
            {
                GridIteract.DeselectCell(GridIteract.GridRepository.SelectedCell);
            } //виділяти клітинку

            GridIteract.SelectCell(this);
        }
    }

    private void OnMouseExit()
    {
        if (State == State.Standart)
        {
            ChangeColor(StandartColor); /*повернення стандартного кольору коли клітинка не виділена*/
        }
        else if (State == State.Variant)
        {
            if (Unit == null)
            {
                ChangeColor(MoveColor);
            }
            else
            {
                ChangeColor(AttackColor);
            }
        }
    }
}
