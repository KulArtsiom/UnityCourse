using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorStones : MonoBehaviour
{

    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private float force;
    [SerializeField] private int damage;
    [SerializeField] private 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StonesGenerate());
    }

    private IEnumerator StonesGenerate()
    {
        while (true)
        {
            var time = Random.Range(2f, 5f);
            yield return  new WaitForSeconds(time);
            var current = Random.Range(0, prefabs.Length);
            var obj = Instantiate(prefabs[current]);
            var projectileMovement = obj.GetComponent<ProjectileMovement>();
            projectileMovement.LaunchBulletBackground(force, damage, new Vector2(0f,1f));
            obj.transform.position = transform.position;
        }
    }
}
