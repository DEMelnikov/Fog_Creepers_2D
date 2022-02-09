using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject _runePrefab;
    [SerializeField] private GameObject _heroPrefab;

    //private List<GameObject> _runes;

    private int _runeRandomPositionLimitX = 9;
    private int _runeRandomPositionLimitYMax = 7;
    private int _runeRandomPositionLimitYMin = -3;
    private int _runesLimit = 3;

    private int _heroLowestPosition = -5;

    //private Random _random = new Random();

    private void Awake()
    {
        Vector3 randomPosition = new Vector3 (0,0,0);

        for (int i = 0; i < _runesLimit; i++)
        {
            randomPosition  = GeneratRandomVector3(_runeRandomPositionLimitX * -1, _runeRandomPositionLimitX,
                _runeRandomPositionLimitYMin, _runeRandomPositionLimitYMax);

            Instantiate(_runePrefab, randomPosition, Quaternion.identity).name = "Rune";
        }

        randomPosition = GeneratRandomVector3(_runeRandomPositionLimitX * -1, _runeRandomPositionLimitX,
            _heroLowestPosition, _heroLowestPosition++);

        Instantiate(_heroPrefab, randomPosition, Quaternion.identity).name = "Hero";

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GeneratRandomVector3(int minX, int maxX, int minY, int maxY)

    {
        Vector3 randomPosition = new Vector3(0, 0, 0);
        randomPosition.x = Random.Range(_runeRandomPositionLimitX * -1, _runeRandomPositionLimitX);
        randomPosition.y = Random.Range(_runeRandomPositionLimitYMin, _runeRandomPositionLimitYMax);
        randomPosition.z = 0;

        return randomPosition;
    }

}
