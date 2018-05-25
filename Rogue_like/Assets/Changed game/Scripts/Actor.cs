using System;

public interface Actor
{
	void MoveEnemy();
    int GetHealth();
	float GetMoveTime();
    void LoseHealth(int loss);
}