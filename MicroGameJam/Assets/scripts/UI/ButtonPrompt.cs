using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UI
{
    public class ButtonPrompt : MonoBehaviour
    {
        public GameObject buttonPromptPrefab;
        public Sprite sprite;
        public Vector3 position;
        private GameObject _instantiatedObject;


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _instantiatedObject = Instantiate(buttonPromptPrefab, position + transform.position, Quaternion.identity);
                var image = _instantiatedObject.GetComponentInChildren<SpriteRenderer>();
                image.sprite = sprite;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_instantiatedObject != null)
            {
                Destroy(_instantiatedObject);
            }
        }

        
    }
}