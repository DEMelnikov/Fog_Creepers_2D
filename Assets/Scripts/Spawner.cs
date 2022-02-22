using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject _runePrefab;
    [SerializeField] private GameObject _heroPrefab;
    [SerializeField] private int _heroesLimit = 3;

    //private List<GameObject> _runes;

    private int _runeRandomPositionLimitX = 9;
    private int _runeRandomPositionLimitYMax = 7;
    private int _runeRandomPositionLimitYMin = -3;
    private int _runesLimit = 3;

    //private int _heroLowestPosition = -5;

    //private Random _random = new Random();

    private void Awake()
    {
        Vector3 randomPosition = new Vector3 (0,0,0);

        for (int i = 0; i < _runesLimit; i++)
        {
            //randomPosition = GenerateRandomVector3();

            Instantiate(_runePrefab, GenerateRandomVector3(), Quaternion.identity).name = "Rune";
        }

        //randomPosition = GenerateRandomVector3(_runeRandomPositionLimitX * -1, _runeRandomPositionLimitX,
        //    _heroLowestPosition, _heroLowestPosition++);

        //GameObject newHero;
        for (int i = 0; i < _heroesLimit; i++)
        {
            GameObject newHero = Instantiate(_heroPrefab, GenerateRandomVector3(), Quaternion.identity);
            //newHero.transform.position = GenerateRandomVector3();
            newHero.name = "Hero";
        }

        GameObject mageHero = Instantiate(_heroPrefab, GenerateRandomVector3(), Quaternion.identity);
        //mageHero.transform.position = GenerateRandomVector3();
        mageHero.GetComponentInChildren<HeroAI>().SetMage();
        //mageHero.GetComponentInChildren<Animator>().Con;
        mageHero.name = "Mage";


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GenerateRandomVector3()

    {
        Vector3 randomPosition = new Vector3(0, 0, 0);
        randomPosition.x = Random.Range(_runeRandomPositionLimitX * -1, _runeRandomPositionLimitX);
        randomPosition.y = Random.Range(_runeRandomPositionLimitYMin, _runeRandomPositionLimitYMax);
        randomPosition.z = 0;

        return randomPosition;
    }

    //public V

}
