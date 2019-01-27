using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    string input;
    public enum AttackStyle {above,sideL,sideR,arcingL,arcingR,noDamage,extra1,extra2,extra3}//Wiggly handled elsewhere
    public AttackStyle style;

    public GameObject[] letterObjects;

    char[] letters;

    public float letterSpacing = 1;
    public float wordSpacing = 3;

    public int waveNo;
    public int waveStage;

    public Text aboveLabel;
    public Text arcLabel;
    public Text sideLabel;

    public Vector3[] warnings;

    public GameObject tlDummy;
    public GameObject blDummy;
    public GameObject trDummy;
    public GameObject brDummy;
    public GameObject extra1;
    public GameObject extra2;
    public GameObject extra3;

    public bool waveStarted;

    public float noDamageDestroy = 1;

    public Button startWaveButton;

    void Awake()
    {
        letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ,.!?'".ToLower().ToCharArray();
        ButtonCall(true);
    }

    public void ButtonCall(bool doReset)
    {
        if (!doReset)
        {
            DoWave(waveNo);
            startWaveButton.interactable = false;
        }
        else
        {
            startWaveButton.interactable = true;
            
          
        }

        aboveLabel.text = warnings[waveNo].x.ToString();
        arcLabel.text = warnings[waveNo].y.ToString();
        sideLabel.text = warnings[waveNo].z.ToString();

    }
   
    void DoWave(int waveID)
    {
        switch (waveID)
        {
            case 0:
                StartCoroutine(Wave0());
                break;


        }


        

    }

    void BeginTyping(int stringID,AttackStyle style, float typeSpeed)
    {
        List<int> letterIDs = new List<int>();

        string text = FindText(stringID);

        text = text.ToLower();

        char[] chars = text.ToLower().ToCharArray();

        //FIND SPECIAL CHARACTERS HERE TOO

        for (int i = 0; i < chars.Length; i++)
        {

            //Finding letter id's to use for instantiation
            for (int m = 0; m < letters.Length; m++)
            {
                if (chars[i] == letters[m])
                {
                    letterIDs.Add(m);
                    break;
                }
            }

        }

        StartCoroutine(TypeSlowly(typeSpeed,style,letterIDs,chars));


    }

    string FindText(int id)
    {
        switch (id)
        {
            case 0:
                return "Ahh… I could smell your fear from a mile away ";
            case 1:
                return "Potent. ";
            case 2:
                return "Pungent. ";
            case 3:
                return "Delectable. ";
            case 4:
                return "Tell me, child, why do you fear the dark? ";
            case 5:
                return "Aren’t you a little old to be scared of shadows? ";
            case 6:
                return "Although, now that I’m here... ";
            case 7:
                return "I suppose you have a reason to be afraid. ";

            //drop letters here

            case 8:
                return "Oh I’m sorry. I jumped the gun a tad. ";
            case 9:
                return "I’m afraid it is in my nature, you see. ";
            case 10:
                return "Your suffering is my sustenance. ";
            case 11:
                return "You can hardly blame me for that, can you? ";
            case 12:
                return "But enough dawdling. To business. ";
            case 13:
                return "I was thinking of moving into your wardrobe... ";
            case 14:
                return "but I think I’ll leave such cramped quarters... ";
            case 15:
                return "to a being of less refined tastes. ";
            case 16:
                return "How about... under your bed? ";
            case 17:
                return "Yes, I think that suits me rather nicely. ";

            //have words attack player and teddy/defences spawn in

            case 18:
                return "Oh? What’s this? It seems we have a squatter. ";
            case 19:
                return "If you're a monster, you're not a very good one. ";
            case 20:
                return "The child felt right at home before I came along. ";
            case 21:
                return "I could barely tell that you were there! ";
            case 22:
                return "How embarrassing for you. ";
            case 23:
                return "You must also feel very at home here. ";
            case 24:
                return "You’re comfortable, aren’t you? ";
            case 25:
                return "Too comfortable. ";
            case 26:
                return "Pathetic in your positivity. ";
            case 27:
                return "I think it's about time you let me take your place. ";

            //nightmare attacks again but it don't work

            case 28:
                return "Tsk. Rude. Tell me... ";
            case 29:
                return "if you’re so intent on defending this child... ";
            case 30:
                return "how do you plan on defending yourself? ";

                //player prepares for 1st wave





        }

        return "Failure While Finding String";
    }

    IEnumerator TypeSlowly(float typeSpeed,AttackStyle attack, List<int> letterNums,char[] chars)
    {

        bool inWord = false;

        GameObject currentParent = null;

        Vector3 nextLetterPosition = Vector3.zero;

        GameObject spawnDummy = null;

        Rigidbody rigid = null;

        List<GameObject> parents = new List<GameObject>();

        GameObject megaParent = null;


        if(attack == AttackStyle.sideL || attack == AttackStyle.sideR || attack == AttackStyle.arcingL || attack == AttackStyle.arcingR)
        {
            megaParent = new GameObject();
        }

        

        switch (attack)
        {
            case AttackStyle.above:
                spawnDummy = tlDummy;
                break;
            case AttackStyle.sideL:
                spawnDummy = blDummy;
                break;
            case AttackStyle.sideR:
                spawnDummy = brDummy;
                break;
            case AttackStyle.noDamage:
                spawnDummy = tlDummy;
                break;
            case AttackStyle.extra1:
                spawnDummy = extra1;
                attack = AttackStyle.noDamage;
                break;
            case AttackStyle.extra2:
                spawnDummy = extra2;
                attack = AttackStyle.noDamage;
                break;
            case AttackStyle.extra3:
                spawnDummy = extra3;
                attack = AttackStyle.noDamage;
                break;
            case AttackStyle.arcingL:
                spawnDummy = blDummy;
                break;
            case AttackStyle.arcingR:
                spawnDummy = brDummy;
                break;


        }

        for (int i = 0; i < chars.Length; i++)
        {

            yield return new WaitForSeconds(typeSpeed);

            if (inWord && letterNums[i] == 26)
            {
                inWord = false;
            }

            if (!inWord)
            {
                



                if (currentParent != null && attack == AttackStyle.above)
                {
                    rigid.isKinematic = false;
                    rigid.useGravity = true;
                }

                if (attack == AttackStyle.noDamage && currentParent != null)
                {
                    StartCoroutine(DestroyTimer(currentParent, noDamageDestroy));
                }


                inWord = true;
                currentParent = new GameObject();
                currentParent.transform.position = spawnDummy.transform.position;

                if(attack == AttackStyle.sideL || attack == AttackStyle.sideR || attack == AttackStyle.arcingL || attack == AttackStyle.arcingR)
                {
                    parents.Add(currentParent);
                }


                if(attack == AttackStyle.above)
                {
                    currentParent.AddComponent<DamagingObject>().isParent = true;


                    rigid = currentParent.AddComponent<Rigidbody>();
                    rigid.useGravity = false;
                    rigid.mass = 10;
                    rigid.isKinematic = false;
                    rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                }


                


                
            }

            if (letterNums[i] != 26)
            {
                GameObject currentLetter = Instantiate(letterObjects[letterNums[i]], spawnDummy.transform.position, Quaternion.identity);

                currentLetter.transform.SetParent(currentParent.transform, true);
                currentLetter.transform.position += new Vector3(nextLetterPosition.x, 0, 0);
                
                nextLetterPosition.x -= letterSpacing;
            }
            else
            {
                nextLetterPosition.x -= wordSpacing;
            }





        }

        if (attack == AttackStyle.sideL || attack == AttackStyle.sideR || attack == AttackStyle.arcingL || attack == AttackStyle.arcingR)
        {
            for (int i = 0; i < parents.Count; i++)
            {
                parents[i].transform.SetParent(megaParent.transform);
            }

            megaParent.AddComponent<DamagingObject>().isMegaParent = true;

            Rigidbody rig = megaParent.AddComponent<Rigidbody>();
            rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                   

            if(attack == AttackStyle.sideL)
                rig.AddForce(Vector3.left * 1000);
            else if(attack == AttackStyle.sideR)
                rig.AddForce(Vector3.left * - 1 * 1000);

            if (attack == AttackStyle.arcingL)
            {
                rig.AddForce((Vector3.left + Vector3.up) * 350);
            }
            else if (attack == AttackStyle.arcingR)
            {
                rig.AddForce((Vector3.right + Vector3.up) * 350);
            }


        }
            



        if (currentParent != null && currentParent.GetComponent<Rigidbody>())
        {
            rigid.isKinematic = false;
            rigid.useGravity = true;
        }
        else if(attack == AttackStyle.noDamage)
        {
            StartCoroutine(DestroyTimer(currentParent, noDamageDestroy));
        }

    }

    IEnumerator Wave0()
    {
        waveStarted = true;
        BeginTyping(1, AttackStyle.arcingL, 0.05f);
        BeginTyping(1, AttackStyle.arcingR, 0.05f);

        BeginTyping(0, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(1, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(2, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(3, AttackStyle.extra3, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(4, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(5, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(6, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(7, AttackStyle.extra3, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(8, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(9, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(10, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(11, AttackStyle.extra3, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(12, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(13, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(14, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(15, AttackStyle.extra3, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(16, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(17, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(18, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(19, AttackStyle.extra3, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(20, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(21, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(22, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(23, AttackStyle.extra3, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(24, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(25, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(26, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(27, AttackStyle.extra3, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(28, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(29, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(30, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(4.5f);


        //waveNo++;
        waveStarted = false;
        ButtonCall(true);
    }

    IEnumerator DestroyTimer(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


}
