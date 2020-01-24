using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
	public GameObject humanPrefab;
    public GameObject orcPrefab;
	Vector3 offset;
	public float spawnTime;

	GameObject prefab;

	void Start()
	{
		spawnTime = 10f;
		if (this.gameObject.name == "humanSpawn")
		{
			prefab = humanPrefab;
		}

		if (this.gameObject.name == "orcSpawn")
		{
			prefab = orcPrefab;
		}

		offset = new Vector3(0f, 1.5f, 0f);
		offset = transform.position - offset;

		StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
    {
		while (true)
		{
			Instantiate(prefab, offset, Quaternion.identity);
        	yield return new WaitForSeconds(spawnTime);
		}
    }
}
