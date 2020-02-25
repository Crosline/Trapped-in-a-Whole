using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CyclopAI : Enemy
{
    [SerializeField] private Transform _rayPosition;
    [SerializeField] private float _timeToWaitOnEdge;
    [SerializeField] private bool isDashingAvaible;

    [SerializeField] private float _shootingTimeout;
    [SerializeField] private float _shootingPower;

    private bool _isWaiting = false;


}