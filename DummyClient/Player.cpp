#include "pch.h"
#include "Player.h"

Player::Player()
{
	//	TODO : �޸�Ǯ
	_positionInfo = new Protocol::PositionInfo();
}

Player::~Player()
{
	delete _positionInfo;
}