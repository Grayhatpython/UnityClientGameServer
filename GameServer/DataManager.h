#pragma once

/*
	Game Data ����
*/

class DataManager
{
public:
	static void LoadData();

private:
	static void LoadMapData();

};

extern DataManager* GDataManager;

