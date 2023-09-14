using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]

public class BallAreaTechnic
{
    public Animator BallAreaLift;
    public TextMeshProUGUI numText;
    public int NeededBall;
    public GameObject[] Balls;
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Picker;
    [SerializeField] private GameObject[] PickerArms;
    [SerializeField] private GameObject[] BonusBalls;
    bool isHaveArm;
    [SerializeField] private GameObject BallControlObject;
    public bool isPickerMoving;
    
    int BallCount;
    int SumofCheckPoints;
    int standCheckPointIndex;
    float FingerPosX;
    
    [SerializeField] private List<BallAreaTechnic> ballAreaTechnics = new List<BallAreaTechnic>();    

    void Start()
    {
        isPickerMoving = true;
        SumofCheckPoints = ballAreaTechnics.Count -1;
        for (int i =0; i< ballAreaTechnics.Count ; i++)
        {
            ballAreaTechnics[i].numText.text = BallCount +"/"+ ballAreaTechnics[i].NeededBall;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPickerMoving)
        {
            Picker.transform.position += 5f * Time.deltaTime* Picker.transform.forward;
            if(Time.timeScale != 0f)
            { 
                if(Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y
                    ,10f));
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            FingerPosX = touchPosition.x - Picker.transform.position.x;
                            break;
                        case TouchPhase.Moved:
                            if(touchPosition.x - FingerPosX > -1.3f || touchPosition.x-FingerPosX < 1.3f)
                            {
                                Picker.transform.position = Vector3.Lerp(Picker.transform.position,new Vector3(touchPosition.x - FingerPosX, 
                                    Picker.transform.position.y,Picker.transform.position.z),3f);
                            }
                            break;        
                    }
                }
            }
        }
    }
    public void LimitReached()
    {
        if(isHaveArm)
        {
            PickerArms[0].SetActive(false);
            PickerArms[1].SetActive(false);
        }
        isPickerMoving = false;
        Invoke("StageControl",2f);
        Collider[] HitColl = Physics.OverlapBox(BallControlObject.transform.position,BallControlObject.transform.localScale/2,
            Quaternion.identity);
        int i = 0;
        while(i<HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,.8f),ForceMode.Impulse);
            i++;
        }
    }
    public void CountBalls()
    {
        BallCount++;
        ballAreaTechnics[standCheckPointIndex].numText.text = BallCount +"/" + ballAreaTechnics[standCheckPointIndex].NeededBall;
    }
    void StageControl()
    {
        if(BallCount >= ballAreaTechnics[standCheckPointIndex].NeededBall)
        {
            ballAreaTechnics[standCheckPointIndex].BallAreaLift.Play("Lift");
            foreach(var item in ballAreaTechnics[standCheckPointIndex].Balls)
            {
                item.SetActive(false);
            }
            if(standCheckPointIndex == SumofCheckPoints)
            {
                Debug.Log("Game Is Over.");
                Time.timeScale = 0f;
            }
            else
            {
                standCheckPointIndex++;
                BallCount =0;
                if(isHaveArm)
                {
                    PickerArms[0].SetActive(true);
                    PickerArms[1].SetActive(true);
                }
            }
            BallCount=0;
        }
        else
        {
            Debug.Log("Lose");
        }
    }
    public void GetArms()
    {
        isHaveArm =true;
        PickerArms[0].SetActive(true);
        PickerArms[1].SetActive(true);
    }
    public void ActiveBonusBalls(int BonusBallIndex)
    {
        BonusBalls[BonusBallIndex].SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(BallControlObject.transform.position,BallControlObject.transform.localScale);
    }
}
