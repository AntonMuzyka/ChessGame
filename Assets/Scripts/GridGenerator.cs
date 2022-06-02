using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize; /*������� ���� �� x,y*/
    [SerializeField] private Cell _cellPrefab; /*������ �������*/
    [SerializeField] private float _offset; /*������ �� ���������*/
    [SerializeField] private Transform _parent; /*���� �� ����� ����������� �����*/
    [SerializeField] private GridRepository _gridRepository;
    [SerializeField] private GridIteract _gridIteract;

    [ContextMenu("Generate grid")]
    private void GenerateGrid() /*������� ����� ��������� ����*/
    {
        _gridRepository.Cells.Clear();
        _cellPrefab.GridIteract = _gridIteract;

        var cellSize = _cellPrefab.GetComponent<MeshRenderer>().bounds.size; /*�������� ����� ����*/

        for (int x = 0; x < _gridSize.x; x++) /*��������� ���� ����*/
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                var position = new Vector3(x * (cellSize.x + _offset), 0, y * (cellSize.z + _offset)); /*������� ������� � ����������� �������*/

                var cell = Instantiate(_cellPrefab, position, Quaternion.identity, _parent); //���� �������

                cell.name = $"X: {x} Y: {y}";
                cell.Position = new Vector2(x, y);

                _gridRepository.Cells.Add(cell);
            }
        }
    }
}
