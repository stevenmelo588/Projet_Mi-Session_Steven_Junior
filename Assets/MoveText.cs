using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SurviveTheRust
{
    public class MoveText : MonoBehaviour
    {
        [SerializeField] private TMP_Text ButtonText;

        public void MoveTextWithButton()
        {
            StartCoroutine(MoveBTNTextCoroutine());
            //ButtonText.transform.position = new(transform.position.x, transform.position.y + 2, 0);
        }

        IEnumerator MoveBTNTextCoroutine()
        {
            ButtonText.transform.position = new(transform.position.x, transform.position.y - 4, 0);
            yield return new WaitForSeconds(.000000000001f);
            ButtonText.transform.position = new(transform.position.x, transform.position.y + 4, 0);
        }
    }
}
