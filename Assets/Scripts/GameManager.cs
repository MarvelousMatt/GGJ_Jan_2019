using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    string input;
    public enum AttackStyle {above,sideL,sideR,arcingL,arcingR,noDamage,extra1,extra2,extra3}//Wiggly handled elsewhere
    public AttackStyle style;

    public GameObject[] letterObjects;

    char[] letters;

    public float letterSpacing = 1;
    public float wordSpacing = 3;

    public int waveNo = 0;
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

    public GameObject overlay;

    public Canvas canv;

    Bed bed;

    void Awake()
    {
        letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ,.!?'".ToLower().ToCharArray();
        bed = GameObject.FindGameObjectWithTag("Bed").GetComponent<Bed>();
        ButtonCall(true);
        DoWave(waveNo);
    }

    private void Update()
    {
        if (!waveStarted)
        {
            overlay.tag = "Overlay";
            canv.enabled = true;

        }
        else
        {
            overlay.tag = "GameController";
            canv.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StopAllCoroutines();
            waveNo++;
            waveStarted = false;
            ButtonCall(true);
        }
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
            GameObject[] l = GameObject.FindGameObjectsWithTag("Damage");
            GameObject[] o = GameObject.FindGameObjectsWithTag("Projectile");
            GameObject[] p = GameObject.FindGameObjectsWithTag("Player");


            foreach (GameObject thing in l)
            {
                Destroy(thing);
            }

            foreach (GameObject thing in o)
            {
                Destroy(thing);
            }

            foreach (GameObject thing in p)
            {
                Destroy(thing);
            }

            bed.health = bed.maxHealth;
            GetComponent<PlaceObject>().ResetITemAmounts();

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
            case 1:
                StartCoroutine(Wave1());
                break;
            case 2:
                StartCoroutine(Wave2());
                break;
            case 3:
                StartCoroutine(Wave3());
                break;
            case 4:
                StartCoroutine(Wave4());
                break;
            case 5:
                StartCoroutine(Wave5());
                break;
            case 6:
                SceneManager.LoadScene("Win");
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
                return "Ahh... I think I've found a new home. ";
            case 1:
                return "Your fear is... potent. ";
            case 2:
                return "Pungent. ";
            case 3:
                return "Delectable. ";
            case 4:
                return "Tell me, child, why do you fear the dark? ";
            case 5:
                return "Aren't you a little old to be scared of shadows? ";
            case 6:
                return "Although, now that I'm here... ";
            case 7:
                return "I suppose you have a reason to be fearful. ";

            //drop letters here

            case 8:
                return "Oh I'm sorry. I jumped the gun a tad. ";
            case 9:
                return "I'm afraid it is in my nature, you see. ";
            case 10:
                return "Your suffering is my sustenance. ";
            case 11:
                return "You can hardly blame me for that, can you? ";
            case 12:
                return "But enough dawdling. To business. ";
            case 13:
                return "I was thinking of moving into your wardrobe... ";
            case 14:
                return "but I think I'll leave such cramped quarters... ";
            case 15:
                return "to a being of less refined tastes. ";
            case 16:
                return "How about... under your bed? ";
            case 17:
                return "Yes, I think that suits me rather nicely. ";

            //have words attack player and teddy/defences spawn in

            case 18:
                return "Oh? What's this? It seems we have a squatter. ";
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
                return "You're comfortable, aren’t you? ";
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
                return "if you're so intent on defending this child's home... ";
            case 30:
                return "how do you plan on defending yours? ";

            //player prepares for 1st wave

            //1ST WAVE
            case 31:
                return "Coward ";
            case 32:
                return "Pathetic ";
            case 33:
                return "Inconsequential ";
            case 34:
                return "Insignificant ";
            case 35:
                return "Pitiful ";


            //2ND WAVE
            case 36:
                return "Useless ";
            case 37:
                return "Worthless ";
            case 38:
                return "Wretched ";
            case 39:
                return "Weak ";
            case 40:
                return "Disappointment ";
            case 41:
                return "Idiot ";
            case 42:
                return "Moron ";


            //3RD WAVE
            case 43:
                return "What does home mean to you, child? ";
            case 44:
                return "Is it the smell of your mother’s cooking? ";
            case 45:
                return "The comfort of your warm bed? ";
            case 46:
                return "Your fruitless,  ";
            case 47:
                return "infantile attempts... ";
            case 48:
                return "to find security in a world... ";
            case 49:
                return "full of anxieties and uncertainties? ";
            case 50:
                return "I don't have a home, you know. ";
            case 51:
                return "You're both being very selfish. ";
            case 52:
                return "Don't you think I am entitled to such things? ";
            case 53:
                return "Why ";
            case 54:
                return "Don't ";
            case 55:
                return "You ";
            case 56:
                return "Let ";
            case 57:
                return "Me ";
            case 58:
                return "Take ";
            case 59:
                return "Yours? ";


            //4TH WAVE
            case 60:
                return "Crybaby ";
            case 61:
                return "Stupid ";
            case 62:
                return "Ugly ";
            case 63:
                return "Failure ";
            case 64:
                return "Mistake ";
            case 65:
                return "Loner ";
            case 66:
                return "Suffering ";
            case 67:
                return "Darkness ";
            case 68:
                return "Grief ";
            case 69:
                return "Why ";
            case 70:
                return "Won't ";
            case 71:
                return "You ";
            case 72:
                return "Die? ";

            //5TH WAVE
            case 73:
                return "This is decidedly not worth the effort. ";
            case 74:
                return "B U T . I . W O N ' T . S T O P . N O W . ";
            case 75:
                return "STICKS ";
            case 76:
                return "AND ";
            case 77:
                return "STONES ";
            case 78:
                return "MAY ";
            case 79:
                return "BREAK ";
            case 80:
                return "MY ";
            case 81:
                return "BONES ";
            case 82:
                return "B U T ";
            case 83:
                return "W O R D S ";
            case 84:
                return "W I L L ";
            case 85:
                return "N E V E R";
            case 86:
                return "H U R T ";
            case 87:
                return "M E ";


            //PLAYER CHATS SHIT

            case 88:
                return "You're still alive? ";
            case 89:
                return "I had no idea you were so strong. ";
            case 90:
                return "...Fine then.  ";
            case 91:
                return "I relent.  ";
            case 92:
                return "Keep your home as it is, 'squatter'.  ";
            case 93:
                return "I'll find somewhere else to go. ";
            case 94:
                return "If home means 'safety'... ";
            case 95:
                return "Then I guess you both get to keep it. ";
            case 96:
                return "...For now. ";


                //LIGHTS ON





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
        BeginTyping(7, AttackStyle.above, 0.05f);
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
        BeginTyping(17, AttackStyle.above, 0.05f);
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
        BeginTyping(27, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(28, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(29, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(4.5f);
        BeginTyping(30, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(4.5f);


        waveNo++;
        waveStarted = false;
        ButtonCall(true);
    }

    IEnumerator Wave1()
    {
        waveStarted = true;

        Debug.Log("Hello world");
        yield return new WaitForSeconds(1);
        //ur stuff

        BeginTyping(31, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(32, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(33, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(34, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(35, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(2f);


        waveNo++;
        waveStarted = false;
        ButtonCall(true);
    }

    IEnumerator Wave2()
    {
        waveStarted = true;

        Debug.Log("Hello world");
        yield return new WaitForSeconds(1);
        //ur stuff

        BeginTyping(36, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(37, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(38, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(39, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(40, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(41, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(42, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(2f);


        waveNo++;
        waveStarted = false;
        ButtonCall(true);
    }

    IEnumerator Wave3()
    {
        waveStarted = true;

        Debug.Log("Hello world");
        yield return new WaitForSeconds(1);
        //ur stuff

        BeginTyping(43, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(44, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(45, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(46, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(47, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(48, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(49, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(50, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(51, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(52, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(53, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(54, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(55, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(56, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(57, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(58, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(59, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);


        waveNo++;
        waveStarted = false;
        ButtonCall(true);
    }

    IEnumerator Wave4()
    {
        waveStarted = true;

        Debug.Log("Hello world");
        yield return new WaitForSeconds(1);
        //ur stuff

        BeginTyping(60, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(1f);
        BeginTyping(61, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(1f);
        BeginTyping(62, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(1f);
        BeginTyping(63, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(1f);
        BeginTyping(64, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(1f);
        BeginTyping(65, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(1f);
        BeginTyping(66, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(1f);
        BeginTyping(67, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(1f);
        BeginTyping(68, AttackStyle.sideR, 0.05f);
        yield return new WaitForSeconds(1f);
        BeginTyping(69, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(70, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(71, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(72, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(2f);


        waveNo++;
        waveStarted = false;
        ButtonCall(true);
    }

    IEnumerator Wave5()
    {
        waveStarted = true;

        Debug.Log("Hello world");
        yield return new WaitForSeconds(1);
        //ur stuff

        BeginTyping(73, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(74, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(75, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(76, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(77, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(78, AttackStyle.arcingL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(79, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(80, AttackStyle.arcingR, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(81, AttackStyle.sideL, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(82, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(83, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(84, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(85, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(86, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(87, AttackStyle.above, 0.05f);
        yield return new WaitForSeconds(2f);

        //player enters new text here

        BeginTyping(88, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(89, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(90, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(91, AttackStyle.extra3, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(92, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(93, AttackStyle.extra1, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(94, AttackStyle.extra2, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(95, AttackStyle.extra3, 0.05f);
        yield return new WaitForSeconds(2f);
        BeginTyping(96, AttackStyle.noDamage, 0.05f);
        yield return new WaitForSeconds(2f);



        waveNo++;
        waveStarted = false;
        ButtonCall(true);
    }

    IEnumerator DestroyTimer(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


}
