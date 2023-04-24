using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcDialogue : MonoBehaviour
{
    public GameObject DialoguePanel;
    public Text DialogueText;

    [SerializeField] ButtonEvnet sceneMove;

    public string[] dialogue;

    private int index; //which will help us find position in the string

    public GameObject continueButton;
    public float wordSpeed;
    public bool StellaIsClose;
    int num;
    private void Start()
    {
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && StellaIsClose)
        {
            if(DialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                DialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if(DialogueText.text== dialogue[index])
        {
            continueButton.SetActive(true);
        }


    }

    public void zeroText() //text �ʱ�ȭ
    {
        DialogueText.text = "";
        index = 0;
        DialoguePanel.SetActive(false);
    }
    
    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    string sceneName;
    public void NextLine()
    {
        if (dialogue[4] == "����ȯ���� ���������� �Ѿ�� �ɰž�! "&&num==4)
        {
            sceneMove.StartButton("GiovanniEventScene");
            return;
        }
        if (dialogue[4] == "������ ���������״ϱ�." && num == 4) 
        {
            sceneMove.StartButton("Scene01"); 
            return;
        }

        if (dialogue[4] == "�ٷ� ��Ƽ�� ��ư�� ��Ŭ�� ������ ���߱� ��������!" && num==4) 
        {
            sceneMove.StartButton("Intro");
            return;
        }

        num++; 

        continueButton.SetActive(false);

        if(index < dialogue.Length - 1)
        {
            index++;
            DialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StellaIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StellaIsClose = false;
            zeroText();
        }
    }

}
