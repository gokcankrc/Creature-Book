using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ky;
using UnityEngine.EventSystems;

public class TooltipManager : Singleton<TooltipManager>, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField]GameObject tooltipBox;
   [SerializeField]float tooltipOffset;
   [SerializeField]TMP_Text tooltipText,tooltipText2;
   [SerializeField]Button evoButton;
   Creature selectedCreature;

   public bool mouseIsOver;
   public void ActivateOnPosition(Entity selectedEntity){
	   if (selectedEntity is not Creature selected)
	   {
		   return;
	   }
		/*used to receive all this data as method arguments, now we just retreive all from the Creature Class*/
		Vector3 position = selected.transform.position;

		this.tooltipBox.transform.position = position+ new Vector3 (this.tooltipOffset,0,0);
		this.tooltipBox.SetActive(true);
		this.SetToolTips(selected.GenerateTooltipText());
		this.selectedCreature = selected;
		this.evoButton.interactable = selected.CanEvolve(false);
   }
   void SetToolTips(string [] newStrings){
	   this.tooltipText.SetText(newStrings[0]);
	   this.tooltipText2.SetText(newStrings[1]);
   }
   void Update (){
	   /*Fire2 is RMB (for some reason)*/
	   if (Input.GetButtonDown("Fire2")) {
			this.Hide();
	   }
	   if (Input.GetMouseButtonDown(0) && !mouseIsOver)
	   {
		   Hide();
	   }
   }

   public void Hide(){
	   this.tooltipBox.SetActive(false);
   }
   public void EvolveSelected(){
		this.selectedCreature.Evolve();
		this.evoButton.interactable = this.selectedCreature.CanEvolve(false);
   }

   public void OnPointerEnter(PointerEventData eventData)
   {
	   mouseIsOver = true;
   }
   public void OnPointerExit(PointerEventData eventData)
   {
	   mouseIsOver = false;
   }
}
