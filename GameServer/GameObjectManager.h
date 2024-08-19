#pragma once

class GameObjectManager
{
public:
	static PlayerRef CreatePlayer(ClientSessionRef session);

private:
	static Atomic<uint32> S_autoincrementId;
};

