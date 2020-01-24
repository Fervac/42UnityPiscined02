using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHP : MonoBehaviour
{
	public float hp;

    void Start()
    {
		if (this.CompareTag("orcFarm"))
		{
			hp = 30;
		}
		if (this.CompareTag("orcFishery"))
		{
			hp = 30;
		}
		if (this.CompareTag("orcTower"))
		{
			hp = 30;
		}
		if (this.CompareTag("orcFactory"))
		{
			hp = 40;
		}
		if (this.CompareTag("orcSpawn"))
		{
			hp = 200;
		}
    }
}
