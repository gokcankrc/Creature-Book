using System.Collections.Generic;
using Ky;
using UnityEngine;
public class CreatureSlotReferencer : Singleton<CreatureSlotReferencer>
{
    public List<CreatureSlot> slots;

    public CreatureSlot GetNearestSlot (Vector3 pos){
        CreatureSlot result = null;
        float minDistance = 9999f;
        
        foreach (CreatureSlot slot in this.slots){
            if ((pos-slot.transform.position).magnitude < minDistance){
                minDistance = (pos-slot.transform.position).magnitude;
                result = slot;
            }
        }
        return result;
    }
}