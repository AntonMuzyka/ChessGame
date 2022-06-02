using UnityEngine;

public enum State /*���䳿 �����*/
{
    Standart,
    Selected,
    Variant
}
public class Cell : MonoBehaviour
{
    public Color StandartColor; /*����������� ���� �����*/
    public Color HoverColor; //���� ������� ��� ��������
    public Color HoverMoveColor; /*���� ������� ���� �� �� ����� ������*/
    public Color HoverAttackColor; /*���� ������� ���� �� �� ����� � ������� �����*/

    public Color SelectedColor;
    public Color MoveColor;
    public Color AttackColor;

    public MeshRenderer MeshRenderer;

    public UnitBase Unit; /*��� �� �������*/
    [HideInInspector] public Vector2 Position; /*��� ���� ������� �������*/
    [HideInInspector] public State State; /*��� ���������� ����� �������*/

    public GridIteract GridIteract;

    public void ChangeColor(Color color) //����� ��� ���� ������� 
    {
        MeshRenderer.material.color = color;
    }

    private void OnMouseEnter()
    {
        if (State == State.Standart)
        {
            ChangeColor(HoverColor); /*���� ������� ������� ����������� ����*/
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

    private void OnMouseDown() /*��� ��������� �� �������*/
    {
        if (State == State.Selected)
        {
            GridIteract.DeselectCell(this); //�������� ��������
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
            } //������� �������

            GridIteract.SelectCell(this);
        }
    }

    private void OnMouseExit()
    {
        if (State == State.Standart)
        {
            ChangeColor(StandartColor); /*���������� ������������ ������� ���� ������� �� �������*/
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
