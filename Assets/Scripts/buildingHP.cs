using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingHP : MonoBehaviour
{
	public float hp;

    void Start()
    {
		if (this.gameObject.name == "orcFarm")
			hp = 30;
		if (this.gameObject.name == "orcFishery")
			hp = 30;
		if (this.gameObject.name == "orcTower")
			hp = 30;
		if (this.gameObject.name == "orcFactory")
			hp = 40;
		if (this.gameObject.name == "orcSpawn")
			hp = 200;
    }
}
