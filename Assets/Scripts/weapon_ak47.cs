using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class weapon_ak47 : MonoBehaviour 
{

	public GameObject shell_prefab;
	
	
	GameObject bulletout;
	
	bool click = false;
	GameObject muzzleflash;
	GameObject muzzleflashsprite;
	
	Text bulletleft;
	
	
	float waittime = 0;
	float reloadtime = 0;
	int bullets = 30;

	float muzzletime = 0;

	
	
	public Animator anim;
	GameObject crosshair;
	float croshairscale;

	void Start () 
	{  
		bulletleft = GameObject.Find ("bullet").GetComponent<Text>();
		crosshair = GameObject.Find ("crosshair");
		bulletout = GameObject.Find ("bulletout");
		
		muzzleflash = GameObject.Find ("muzzleflash");
		muzzleflashsprite = GameObject.Find("muzzleflash_sprite");
		
		muzzleflash.SetActive (false);

		click = false;
		
		//anim = GetComponent<Animator> ();

		anim.SetBool ("reload", false);
	
	}
	
	// Update is called once per frame
	void Update () 
	{		

		if (!click) 
		{
			anim.SetInteger("fire",0);
		}
		
		if (muzzletime > 0)
			muzzletime -= Time.deltaTime;
		
		if (muzzletime <= 0) {
			muzzleflash.SetActive (false);
		}
		
		if (bullets <= 0) 
		{
			if(!anim.GetBool("reload"))
			{
				anim.SetBool("reload",true);
				reloadtime = 4.7f;
			}
			
		}
		
		if (waittime > 0) 
		{
			waittime -= Time.deltaTime;
		}
		
		
		if(anim.GetBool("reload"))
		{
			if(reloadtime > 0)
			{

				reloadtime -= Time.deltaTime;
			}
			if(reloadtime <=0)
			{
				anim.SetBool("reload",false);
				bullets = 30;
			}
			
		}
		
		if(Input.GetMouseButtonDown(0))
		{
			
			click = true;
			
			
			
		}
		
		if (Input.GetMouseButtonUp (0)) 
		{
			click = false;
		}
		
		if (click) 
		{
			if(waittime <= 0 && reloadtime <= 0)
			{
				Vector3 rot = new Vector3(0,0,Random.Range(0.0f,360.0f));
				
				GameObject shell = (GameObject)Instantiate(shell_prefab, bulletout.transform.position, bulletout.transform.rotation);
				Rigidbody rb = shell.GetComponent<Rigidbody> ();
				rb.velocity = bulletout.transform.right;
				rb.velocity *= -1;

				muzzletime = 0.1f;
				muzzleflashsprite.transform.Rotate(rot);
				muzzleflash.SetActive (true);
				
				anim.SetInteger("fire",1);
				
				bullets -=1;
				waittime = 0.1f;
				croshairscale += 0.1f;


				gameObject.SendMessage("Punch",new Vector3(Random.Range(0.1f,0.5f), 
				                                           Random.Range(-0.2f,0.2f),0));
			}
			
			
		}
		
		bulletleft.text = "Bullets: " + bullets;


		UpdateCrosshair ();
	
	}

	void UpdateCrosshair()
	{
		if (croshairscale < 0.5)
			croshairscale = 0.5f;

		if (croshairscale > 1.5f)
			croshairscale = 1.5f;

		crosshair.transform.localScale = new Vector3 (croshairscale, croshairscale, 1);

		if (croshairscale > 0.5f)
			croshairscale -= 0.7f * Time.deltaTime;

	}


}
