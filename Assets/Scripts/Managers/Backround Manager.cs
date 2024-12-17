using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ky;
using System.Linq;
public class BackroundManager : Singleton<BackroundManager>
{
    [SerializeField]BackgroundImage grayscale;
    [SerializeField]Transform grayscaleContainer;
    [SerializeField]GameObject [] prefabArr;

    void Start (){
        this.Setup(0);
    }

    public void TurnOnColoredParts(int lit){
        this.grayscale.TurnOnColoredParts(lit);
    }

    public void Setup (GameObject grayscalePrefab){
        Destroy(this.grayscale);
        this.grayscale = Instantiate (grayscalePrefab,Vector3.zero,Quaternion.identity,this.grayscaleContainer).GetComponent<BackgroundImage>();
        this.grayscale.ResetPosition();
        this.grayscale.TurnOnColoredParts(0);
    }
    public void Setup (int id){
        if (id < this.prefabArr.Count()){
            Destroy(this.grayscale);
            this.grayscale = Instantiate (this.prefabArr[id],Vector3.zero,Quaternion.identity,this.grayscaleContainer).GetComponent<BackgroundImage>();
            this.grayscale.ResetPosition();
            this.grayscale.TurnOnColoredParts(0);
        }
        else {
            print ("PD");
        }
        
    }
}
