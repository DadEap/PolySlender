using UnityEngine;
using System.Collections;

public class LODScript : MonoBehaviour {

	public float[] distanceRange;
	public MeshFilter[] lodModels;

	private int currentObject = -2;
	// Use this for initialization
	void Start () {
	
		for(int i = 0 ; i < this.lodModels.Length ; i++)
		{
			this.lodModels[i].renderer.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float d = Vector3.Distance (Camera.main.transform.position, this.transform.position);
		int level = -1;

		for (int i = 0; i < this.distanceRange.Length; i++)
		{
			if(d < this.distanceRange[i])
			{
				level = i;
				break;
			}
		}

		if(level < 0)
		{
			level = this.distanceRange.Length;
		}
		
		if(this.currentObject != level)
		{
			changeModel(level);
		}
	}

	void changeModel(int level)
	{
		this.lodModels[level].renderer.enabled= true;

		if (currentObject >= 0) 
		{
			this.lodModels [this.currentObject].renderer.enabled= false;
		}

		this.currentObject = level;
	}
}
