#pragma once

/*
	Game World ����
*/

struct Node
{
	bool isWalkable = false;
};

class WorldManager
{
public:

private:
	vector<vector<Node>>	_world;
	//	TEMP
	float					_nodeSize = 0.f;

	//	2D ���� -> 3D�� ����
	//	Y -> ���� Z
	int						_worldSizeX = 0;
	int						_worldSizeY = 0;

	float					_worldMinPositionX = 0.f;
	float					_worldMaxPositionX = 0.f;
	float					_worldMinPositionY = 0.f;
	float					_worldMaxPositionY = 0.f;
};

extern WorldManager* GWorldManager;

