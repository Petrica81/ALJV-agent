using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject _brickPrefab;
    private int _rows = 6;
    private int _columns = 10;
    private float _horizontalSpacing = 1.5f;
    private float _verticalSpacing = 1f;
    
    public int BricksNumber { get; set; }

    public void Spawn()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        BricksNumber = _rows * _columns;
        Vector3 offset = new Vector3(-_columns / 2 * _horizontalSpacing + _horizontalSpacing / 2, _rows / 2 * _verticalSpacing - _verticalSpacing / 2, 0);
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                Vector3 position = new Vector3(j * _horizontalSpacing, -i * _verticalSpacing, 0) + offset;
                GameObject brick = Instantiate(_brickPrefab, transform.position + position, Quaternion.identity, transform);
            }
        }
    }
}
