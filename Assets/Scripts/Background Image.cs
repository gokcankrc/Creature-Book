using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundImage : MonoBehaviour
{
	[SerializeField]GameObject[] imgArr;
    public void TurnOnColoredParts(int lit){
		int current = 0;
		foreach (GameObject  img in this.imgArr){
			if (current < lit){
				img.SetActive(true);
			}
			else{
				img.SetActive(false);
			}
			current++;
		}
	}
	public void ResetPosition (){
		this.transform.localPosition = Vector3.zero;
	}
}
