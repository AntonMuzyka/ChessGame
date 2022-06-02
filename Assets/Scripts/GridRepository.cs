using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRepository : MonoBehaviour
{
    public List<Cell> Cells = new List<Cell>(); /*для збереження клітинок*/
    public Cell SelectedCell; /*для виділеної клітинки*/
}
