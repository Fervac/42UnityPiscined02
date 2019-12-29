using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
	public Animator animator;
	public float speed = 1.5f;
	private Vector3 target;
	private Vector3 tmp;
	private SpriteRenderer sr;
	Vector3 dir;
	AudioSource audioS;
	public AudioClip[] audioClips;
	GameObject Manager;
	manager script;
	public bool attack = false;
	public GameObject cible;

    void Start ()
	{
		Manager = GameObject.Find("Manager");
		target = transform.position;
		sr = GetComponent<SpriteRenderer>();
		audioS = GetComponent<AudioSource>();
		script = Manager.GetComponent<manager>();
	}

     void Update ()
	 {
		if(script.goodguys.Contains(this.gameObject))
		{
			if (Input.GetMouseButtonDown(0))
			{
				audioS.clip = audioClips[Random.Range(0, audioClips.Length)];
				//audioS.Play();
				target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
	            target.z = transform.position.z;
				dir = target - transform.position;
				if (dir.x < 0)
					sr.flipX = true;
				else
					sr.flipX = false;
	         }
	         transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
			 animator.SetFloat("Horizontal", dir.x);
			 animator.SetFloat("Vertical", dir.y);
			 tmp = target - transform.position;
			 animator.SetFloat("Magnitude", tmp.magnitude);

			 if (attack == true && cible)
			 	attackMethod();
		 }
		 else
		 {
			 animator.SetFloat("Horizontal", 0);
			 animator.SetFloat("Vertical", 0);
			 animator.SetFloat("Magnitude", 0);
			 target = transform.position;
		 }

		 if (cible)
		 {
			 if (cible.GetComponent<buildingHP>().hp <= 0)
			 {
	 			Destroy(cible);
				addSpawnTime();
	 			cible = null;
	 			attack = false;
	 			animator.SetFloat("Attack", 0f);
			}
		}
     }

	 void attackMethod()
	 {
		 //transform.position = Vector3.MoveTowards(transform.position, cible.transform.position, speed * Time.deltaTime);
		 float dist = Vector3.Distance(cible.transform.position, transform.position);
		 if (dist < .5f)
		 {
			 animator.SetFloat("Attack", 1f);
			 StartCoroutine(damage());
		 }
	 }

	 IEnumerator damage()
	 {
		yield return new WaitForSeconds(2);
		if (cible)
		{
			cible.GetComponent<buildingHP>().hp -= 10;
			Debug.Log(cible.name + " " + cible.GetComponent<buildingHP>().hp + " HP left");
		}
	 }

	 void addSpawnTime()
	 {
		 if (cible.name == "orcFarm" || cible.name == "orcFishery"
		 	|| cible.name == "orcTower" || cible.name == "orcFactory"
				|| cible.name == "orcSpawn")
		{
			GameObject tmp = GameObject.Find("orcSpawn");
			tmp.GetComponent<spawning>().spawnTime += 2.5f;
		}

		if (cible.name == "humanFarm" || cible.name == "humanFishery"
		   || cible.name == "humanTower" || cible.name == "humanFactory"
			   || cible.name == "humanSpawn")
	   {
		   GameObject tmp = GameObject.Find("humanSpawn");
		   tmp.GetComponent<spawning>().spawnTime += 2.5f;
	   }
	}
}
