using UnityEngine;
using System.Collections;

public class flare : MonoBehaviour {

	Light l;
	float timestep = 0.15f;

	public ParticleEmitter [] particleEmitters;
	public float scale = 1;
	
	[SerializeField]
	[HideInInspector]
	private float [] minsize;
	
	[SerializeField]
	[HideInInspector]
	private float[] maxsize;
	
	[SerializeField]
	[HideInInspector]
	private Vector3 [] worldvelocity;
	
	[SerializeField]
	[HideInInspector]
	private Vector3 [] localvelocity;
	
	[SerializeField]
	[HideInInspector]
	private Vector3 [] rndvelocity;
	
	[SerializeField]
	[HideInInspector]
	private Vector3 [] scaleBackUp;
	
	[SerializeField]
	[HideInInspector]
	private bool firstUpdate = true;

	void Start () 
	{
		l = GameObject.Find("flare_light").GetComponent<Light> ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (timestep < 0) {
			l.range = Random.Range (10.0f, 20.0f);
			timestep = 0.15f;
		} else {
			timestep -= Time.deltaTime;
		}

	
	}


	[ContextMenu ("Do Something")]
	void UpdateScale () 
	{   
		int length = particleEmitters.Length;
		
		if(firstUpdate == true)
		{
			minsize = new float[length];
			maxsize = new float[length];
			worldvelocity = new Vector3[length];
			localvelocity = new Vector3[length];
			rndvelocity = new Vector3[length];
			scaleBackUp = new Vector3[length];
		}
		
		
		for (int i = 0; i < particleEmitters.Length; i++) 
		{ 
			if(firstUpdate == true){
				minsize[i] = particleEmitters[i].minSize;
				maxsize[i] = particleEmitters[i].maxSize;
				worldvelocity[i] = particleEmitters[i].worldVelocity;
				localvelocity[i] = particleEmitters[i].localVelocity;
				rndvelocity[i] = particleEmitters[i].rndVelocity;
				scaleBackUp[i] = particleEmitters[i].transform.localScale;
			}
			
			particleEmitters[i].minSize = minsize[i] * scale;
			particleEmitters[i].maxSize = maxsize[i] * scale;
			particleEmitters[i].worldVelocity = worldvelocity[i] * scale;
			particleEmitters[i].localVelocity = localvelocity[i] * scale;
			particleEmitters[i].rndVelocity = rndvelocity[i] * scale;
			particleEmitters[i].transform.localScale = scaleBackUp[i] * scale;
			
		}
		firstUpdate = false;
	}

}

