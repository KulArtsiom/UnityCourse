using System.Collections;
using UnityEngine;

public class ObjectsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private bool isRandom;
    [SerializeField] private float spawnTime = 1f;
    private int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObjectCreatorProccess());
    }

    private IEnumerator ObjectCreatorProccess()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            if (isRandom)
            {
                current = Random.Range(0, prefabs.Length);
            }

            GameObject obj = Instantiate(prefabs[current]);
            obj.transform.position = this.transform.position;
            if (isRandom)
            {
                continue;
            }

            current++;
            if (current >= prefabs.Length)
            {
                current = 0;
            }
        }
        // ReSharper disable once IteratorNeverReturns
    }
}