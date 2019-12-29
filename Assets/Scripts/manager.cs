using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
	public List<GameObject> goodguys = new List<GameObject>();
	public float buildinghp = 100;

    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
			{
				if (hit.collider.gameObject.tag == "footman")
				{
					if (Input.GetKey(KeyCode.LeftControl) == true)
					{
						if(goodguys.Contains(hit.collider.gameObject))
						{
							Debug.Log("already selected");
						}
						else
							goodguys.Add(hit.collider.gameObject);
					}
					else
					{
						if (goodguys.Count == 0)
							goodguys.Add(hit.collider.gameObject);
						else
						{
							goodguys.Clear();
							goodguys.Add(hit.collider.gameObject);
						}
					}
				}
				else
				{
					if (goodguys.Count != 0)
						attack(hit.collider.gameObject);
				}
			}

    	}
		if (Input.GetMouseButtonDown(1))
			goodguys.Clear();
	}

	void attack(GameObject cible)
	{
		foreach (GameObject goodguy in goodguys)
		{
			goodguy.GetComponent<controller>().cible = cible;
			goodguy.GetComponent<controller>().attack = true;
		}
	}
}
