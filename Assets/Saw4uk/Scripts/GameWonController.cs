using System;
using UnityEngine;
using UnityEngine.Events;

namespace Saw4uk.Scripts
{
    public class GameWonController : MonoBehaviour
    {
        [SerializeField] private Generator firstGenerator;
        [SerializeField] private Generator secondGenerator;
        [SerializeField] private Generator thirdGenerator;

        public UnityEvent gameWon;
        
        private float activatedGeneratorsAmount;
        
        private float ActivatedGeneratorsAmount
        {
            get => activatedGeneratorsAmount;
            set
            {
                activatedGeneratorsAmount = value;
                if (activatedGeneratorsAmount == 3)
                {
                    gameWon.Invoke();
                    Debug.Log("won");
                }
            }
        }
        private void Awake()
        {
            firstGenerator.Activated += GeneratorOnActivated;
            secondGenerator.Activated += GeneratorOnActivated;
            thirdGenerator.Activated += GeneratorOnActivated;
        }

        private void GeneratorOnActivated()
        {
            ActivatedGeneratorsAmount += 1;
        }
    }
}