using System;
using UnityEngine;

public interface Actor
{
	void MoveEnemy();
    int GetHealth();
	float GetMoveTime();
    void LoseHealth(int loss);
    GameObject getObject();
}