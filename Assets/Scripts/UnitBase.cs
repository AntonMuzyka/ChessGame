using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    Player,
    Enemy
}

public class UnitBase : MonoBehaviour
{
    public List<Vector2> MoveVariants;
    public List<Vector2> AttackVariants;

    public Side UnitSide;

    public int Health = 10;
    public int Damage = 2;

    [SerializeField] private GridIteract _gridIteract;

    [HideInInspector] public List<Cell> Variants = new List<Cell>();

    [SerializeField] private Cell _currentCell;

    public void GoToCell(Cell cell)
    {
        if (cell.Unit == null)
        {
            Move(cell);
        }
        else
        {
            if (DamageUnit(cell.Unit))
            {
                Move(cell);
            }
        }

        _gridIteract.DeselectCell(cell);
        _gridIteract.ClearVariants(Variants);
    }
    
    private void Move(Cell cell)
    {
        _gridIteract.GridRepository.SelectedCell.Unit = null;

        transform.position = new Vector3(cell.transform.position.x, transform.position.y, cell.transform.position.z);

        cell.Unit = this;
        _currentCell = cell;
    }
    
    private bool DamageUnit(UnitBase enemy)
    {
        enemy.Health -= Damage;

        if (enemy.Health <= 0)
        {
            enemy.Die();

            return true;
        }

        return false;
    }

    private void Die()
    {
        _currentCell.Unit = null;

        Destroy(gameObject);
    }

    [ContextMenu("Initialize")]
    public void InitializeCell()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            if (hit.collider.GetComponent<Cell>())
            {
                transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);

                hit.collider.GetComponent<Cell>().Unit = this;

                var cell = hit.collider.GetComponent<Cell>();

                _gridIteract = cell.GridIteract;
                _currentCell = cell;
                _gridIteract = cell.GridIteract;
            }
        }
    }
}
