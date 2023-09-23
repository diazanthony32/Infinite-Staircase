using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameManager gameManager;

    [Space(10)]

    // used for seed generation
    [SerializeField] private string seed;
    [SerializeField] private bool useSeed;

    // the offsets platforms will be moved when getting a new position
    private const float PLATFORM_PADDING = 0.1f;
    public float MOVE_X { get; private set; }
    public float MOVE_Y { get; private set; }

    // the percentage of having the next platform go in the same direction as the previous platform
    private const float CHANCE_TO_REPEAT = 0.65f;

    // the smaller the number, the closer the player can be to the top of the staircase without rearranging
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 12.5f;

    // for platform management and recycling
    private const int platformCount = 35;
    [SerializeField] private List<GameObject> platformTypes;
    public List<GameObject> ActivePlatforms { get; private set; } = new List<GameObject>();


    // Start is called before the first frame update
    void Awake()
    {
        SetPlacementDistances();
        DetermineSeededGeneration();
        InitalizePlatforms();
    }


    // Update is called once per frame
    void Update()
    {
        // if the player position is less than the desired spacing, move the first platform up
        if (Vector3.Distance(gameManager.player.transform.position, ActivePlatforms[^1].transform.position) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            RearragePlatform();
        }
    }


    // Gets the scale of the platforms and sets it as the minimum platform displacement distances
    void SetPlacementDistances()
    {
        if (platformTypes.Count > 0)
        {
            Transform platform = platformTypes[0].gameObject.transform;

            MOVE_X = (platform.localScale.x + PLATFORM_PADDING);
            MOVE_Y = (platform.localScale.y + PLATFORM_PADDING);
        }
        else
        {
            Debug.LogError("No platforms added into the platform types list in \"LevelGenerator.cs\"!");
        }
    }


    // determines if the level is to be generated using a seed or have it randomly generate one
    private void DetermineSeededGeneration()
    {
        // if a seed is provided and we want to generate the leel via a seed, then activiate the seed
        if (seed != "" && useSeed)
        {
            Random.InitState(int.Parse(seed));
        }
        // randomly generates a seed from 1-1000000000 if no seed is given
        else
        {
            int randSeed = Random.Range(1, 1000000000);
            Random.InitState(randSeed);

            seed = randSeed.ToString();
            Debug.Log(seed);
        }
    }


    // Initializes the starting platform creation of the level
    private void InitalizePlatforms()
    {
        // Spawn the set amount of platforms set by the platformCount variable
        for (int i = ActivePlatforms.Count; i < platformCount; i++)
        {
            NewPlatform();
        }
    }


    // Handles the creation of the and placement of platforms
    private void NewPlatform()
    {
        // If there are no platforms in the active platforms list, set the spawn position to 0,0,0
        Vector3 _initPosition = ActivePlatforms.Count > 0 ? ActivePlatforms[^1].transform.position : Vector3.zero;

        // spawn in a new platform prefab from the platform types list
        GameObject newPlatform = Instantiate(platformTypes[Random.Range(0, platformTypes.Count)], _initPosition, Quaternion.identity);

        newPlatform.transform.position = GetNewPlatformPosition();
        ActivePlatforms.Add(newPlatform);
    }


    // rearrages the platform from the oldest(first) on the list back to the newest(last) on the list, then giving it a new position
    private void RearragePlatform()
    {
        // selects the oldest platform on the list and removes it from the active platform list
        GameObject movingPlatform = ActivePlatforms[0];
        ActivePlatforms.RemoveAt(ActivePlatforms.IndexOf(movingPlatform));

        movingPlatform.transform.position = GetNewPlatformPosition();
        ActivePlatforms.Add(movingPlatform);
    }


    // rules set to decide the platforms new position
    private Vector3 GetNewPlatformPosition()
    {
        float _randNum = Random.Range(0.0f, 1.0f);

        switch (ActivePlatforms.Count)
        {
            // first platform created stays at 0,0,0
            case 0:
                return Vector3.zero;

            // second platform has a 50/50 chance of going left or right
            case 1:
                return new Vector3(_randNum <= 50 ? -MOVE_X : MOVE_X, MOVE_Y, 0.0f);

            // following platforms have a higher chance to continue in the direction that the prev platform is already headed
            case > 1:
                Vector3 _lastPlatPos = ActivePlatforms[^1].transform.position;
                Vector3 _prevPlatPos = ActivePlatforms[^2].transform.position;

                return new Vector3(_lastPlatPos.x + ((_lastPlatPos.x > _prevPlatPos.x ? 1 : -1) * (_randNum <= CHANCE_TO_REPEAT ? MOVE_X : -MOVE_X)), _lastPlatPos.y + MOVE_Y, 0);
        }

        Debug.LogError("No case found for Platform #" + ActivePlatforms.Count + ". I don't know how or why...");
        return Vector3.zero;
    }
}
