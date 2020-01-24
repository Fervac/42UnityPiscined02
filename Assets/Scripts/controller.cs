using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public Animator animator;
	public float speed = 1.5f;
	private Vector3 target;
	private Vector3 tmp;
	private SpriteRenderer sr;
	Vector3 dir;
	AudioSource audioS;
	public AudioClip[] audioClips;
	GameObject manager;
	Manager script;
	public bool attack = false;
	public GameObject cible;

    void Start()
	{
		manager = GameObject.Find("Manager");
		target = transform.position;
		sr = GetComponent<SpriteRenderer>();
		audioS = GetComponent<AudioSource>();
		script = manager.GetComponent<Manager>();
	}

     void Update()
	 {
		if(script.goodguys.Contains(this.gameObject))
		{
			if (Input.GetMouseButtonDown(0))
			{
				audioS.clip = audioClips[Random.Range(0, audioClips.Length)];
				audioS.Play();
				target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
	            target.z = transform.position.z;
				dir = target - transform.position;
				if (dir.x < 0)
				{
					sr.flipX = true;
				}
				else
				{
					sr.flipX = false;
				}
	        }
	        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
			animator.SetFloat("Horizontal", dir.x);
			animator.SetFloat("Vertical", dir.y);
			tmp = target - transform.position;
			animator.SetFloat("Magnitude", tmp.magnitude);

			if (attack == true && cible)
			{
				AttackMethod();
			}
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
			 if (cible.GetComponent<BuildingHP>().hp <= 0)
			 {
	 			Destroy(cible);
				AddSpawnTime();
	 			cible = null;
	 			attack = false;
	 			animator.SetFloat("Attack", 0f);
			}
		}
     }

	 void AttackMethod()
	 {
		 float dist = Vector3.Distance(cible.transform.position, transform.position);
		 if (dist < .5f)
		 {
			 animator.SetFloat("Attack", 1f);
			 StartCoroutine(Damage());
		 }
	 }

	 IEnumerator Damage()
	 {
		yield return new WaitForSeconds(2);
		if (cible)
		{
			cible.GetComponent<BuildingHP>().hp -= 10;
			Debug.Log(cible.name + " " + cible.GetComponent<BuildingHP>().hp + " HP left");
		}
	 }

	 void AddSpawnTime()
	 {
		 if (cible.name == "orcFarm" || cible.name == "orcFishery"
		 	|| cible.name == "orcTower" || cible.name == "orcFactory"
				|| cible.name == "orcSpawn")
		{
			GameObject tmp = GameObject.Find("orcSpawn");
			tmp.GetComponent<Spawning>().spawnTime += 2.5f;
		}

		if (cible.name == "humanFarm" || cible.name == "humanFishery"
		   || cible.name == "humanTower" || cible.name == "humanFactory"
			   || cible.name == "humanSpawn")
	   {
		   GameObject tmp = GameObject.Find("humanSpawn");
		   tmp.GetComponent<Spawning>().spawnTime += 2.5f;
	   }
	}
}
