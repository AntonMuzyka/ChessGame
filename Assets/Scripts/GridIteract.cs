using System.Collections.Generic;
using UnityEngine;

public class GridIteract : MonoBehaviour
{
    public GridRepository GridRepository;

   public void SelectCell (Cell cell)
    {
        GridRepository.SelectedCell = cell;
        cell.State = State.Selected;
        cell.ChangeColor(cell.SelectedColor);

        if (cell.Unit != null)
        {
            cell.Unit.Variants = CalculateVariants(cell, cell.Unit.MoveVariants, cell.Unit.AttackVariants); //пошук ходів

            ShowVariants(cell.Unit.Variants);
        }
    }

    public void DeselectCell(Cell cell)
    {
        GridRepository.SelectedCell.State = State.Standart;
        GridRepository.SelectedCell.ChangeColor(GridRepository.SelectedCell.StandartColor); /*зміна назв кольорів*/
        GridRepository.SelectedCell = null;

        if (cell.Unit != null)
        {
            ClearVariants(cell.Unit.Variants); //почистити варіанти ходів
            cell.Unit.Variants.Clear();
        }
    }

    public List<Cell> CalculateVariants(Cell unitcell, List<Vector2> movevariants, List<Vector2> attackvariants)
    {
        var variants = new List<Cell>();
        Cell cell;

        foreach (Vector2 offset in movevariants)
        {
            cell = FindCellByPosition(offset, unitcell, true);

            if (cell != null)
            {
                variants.Add(cell);
            }
        }

        foreach(Vector2 offset in attackvariants)
        {
            cell = FindCellByPosition(offset, unitcell, false);

            if (cell != null)
            {
                variants.Add(cell);
            }
        }

        return variants;
    }
    
    public Cell FindCellByPosition(Vector2 offset, Cell UnitCell, bool MoveVariant)
    {
        if (MoveVariant)
        {
            return GridRepository.Cells.Find(cell => cell.Position == UnitCell.Position + offset && cell.Unit == null);
        }
        else
        {
            return GridRepository.Cells.Find(cell => cell.Position == UnitCell.Position + offset && cell.Unit != null && cell.Unit.UnitSide != UnitCell.Unit.UnitSide);
        }
    }

    public void ShowVariants(List<Cell> variants) /*Відображення варіатів ходу, аргумент список з збереженими варіантами ходу*/
    {
        foreach (Cell variant in variants)
        {
            variant.State = State.Variant;

            if (variant.Unit == null)
            {
                variant.ChangeColor(variant.MoveColor);
            }
            else
            {
                variant.ChangeColor(variant.AttackColor);
            }
        }
    }

    public void ClearVariants(List<Cell> variants) /*аргумент варіантс з поточними варіантами ходу*/
    {
        foreach (var variant in variants)
        {
            variant.State = State.Standart;
            variant.ChangeColor(variant.StandartColor);
        }
    }
}
