using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundImage : MonoBehaviour
{
	[SerializeField]GameObject[] imgArr;
	[SerializeField]Button [] btnArray;
    public void TurnOnColoredParts(int lit){
		int current = 0;
		foreach (GameObject  img in this.imgArr){
			if (current < lit){
				img.SetActive(true);
				this.btnArray[current].interactable = false;
			}
			else
			{
				btnArray[current].interactable = current == lit;

				img.SetActive(false);
			}
			current++;
		}
		foreach (Button b in this.btnArray){
			b.gameObject.SetActive(true);
		}
	}
	public void ResetPosition (){
		this.transform.localPosition = Vector3.zero;
	}
	public void NextMission (){
		foreach (Button b in this.btnArray){
			b.gameObject.SetActive(false);
		}
		EnemyManager.I.SetNextEnemy(EncounterManager.I.GetNextEnemy());
        CombatManager.I.StartCombat();
	}
}
