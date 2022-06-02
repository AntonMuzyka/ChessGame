using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize; /*довжина сітки по x,y*/
    [SerializeField] private Cell _cellPrefab; /*префаб клітинки*/
    [SerializeField] private float _offset; /*відступ між клітинками*/
    [SerializeField] private Transform _parent; /*обєкт на якому зберігаються обєкти*/
    [SerializeField] private GridRepository _gridRepository;
    [SerializeField] private GridIteract _gridIteract;

    [ContextMenu("Generate grid")]
    private void GenerateGrid() /*основна логіка генерації сітки*/
    {
        _gridRepository.Cells.Clear();
        _cellPrefab.GridIteract = _gridIteract;

        var cellSize = _cellPrefab.GetComponent<MeshRenderer>().bounds.size; /*отримуємо розмір сітки*/

        for (int x = 0; x < _gridSize.x; x++) /*генерація самої сітки*/
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                var position = new Vector3(x * (cellSize.x + _offset), 0, y * (cellSize.z + _offset)); /*позиція клітинки з урахуванням відступів*/

                var cell = Instantiate(_cellPrefab, position, Quaternion.identity, _parent); //обєкт клітинки

                cell.name = $"X: {x} Y: {y}";
                cell.Position = new Vector2(x, y);

                _gridRepository.Cells.Add(cell);
            }
        }
    }
}
