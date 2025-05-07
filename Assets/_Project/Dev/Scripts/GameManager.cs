using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject nextDropDisplay;
    [SerializeField] GameObject[] dropPrefabs;
    [SerializeField] Player player;

    GameObject nextDrop;

    private void Start()
    {
        ChooseRandomDrop();
    }

    private void ChooseRandomDrop()
    {
        int _randomIndex = Random.Range(0, dropPrefabs.Length);
        nextDrop = dropPrefabs[_randomIndex];

        if (nextDropDisplay != null)
        {
            nextDropDisplay.GetComponent<Image>().sprite = nextDrop.GetComponent<SpriteRenderer>().sprite;
            nextDropDisplay.SetActive(true);
        }
    }

    public GameObject GetNextDrop()
    {
        return nextDrop;
    }

    public void UpdateNextDrop()
    {
        ChooseRandomDrop();
    }
}
