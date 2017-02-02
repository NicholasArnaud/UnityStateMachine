using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private readonly StatePatternEnemy enemy;


    public ChaseState(StatePatternEnemy statepatternenemy)
    {
        enemy = statepatternenemy;
    }

    private void Look()
    {
        RaycastHit hit;
        Vector3 enemyToTarget = (enemy.chaseTarget.position + enemy.offset)-enemy.eyes.transform.position;
        if (Physics.Raycast(enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
            enemy.chaseTarget = hit.transform;
        else
            ToAlertState();
    }

    public void OnTriggerEnter(Collider other)
    {
       
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
       Debug.Log("Can't transition to the same state");
    }

    public void ToPatrolState()
    {
      Debug.Log("Can't transition to this state");
    }

    private void Chase()
    {
        enemy.meshRendererFlag.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();

    }


    void IEnemyState.UpdateState()
    {
        Look();
        Chase();
    }

}