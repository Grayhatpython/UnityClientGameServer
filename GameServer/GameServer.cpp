﻿#include "pch.h"
#include "ThreadManager.h"
#include "Service.h"
#include "ClientPacketHandler.h"
#include "GameServer.h"
#include "ClientSession.h"
#include "Room.h"
#include "ClientSessionManager.h"
#include "Monitoring.h"
#include "GameObjectManager.h"
#include "WorldManager.h"
#include "DataManager.h"

GameServer::~GameServer()
{

}

bool GameServer::Initialize()
{
	ClientPacketHandler::Initialize();

	GClientSessionManager = new ClientSessionManager();
	
	GGameObjectManager = new GameObjectManager();
	GGameObjectManager->Initialize();

	GDataManager = new DataManager();
	GDataManager->LoadData();

	GWorldManager = new WorldManager();

	GRoom = make_shared<Room>();

	_service = MakeShared<ServerService>(
		NetAddress(L"127.0.0.1", 7777),
		MakeShared<IocpCore>(),
		MakeShared<ClientSession>, // TODO : SessionManager 등
		100);

	ASSERT_CRASH(_service);

	return true;
}

bool GameServer::Start()
{
	ASSERT_CRASH(_service->Start());

	return true;
}

void GameServer::DoProcessJob()
{
	Monitoring m;
	{
		GThreadManager->Launch([&m]()
			{
				m.Print();
				std::this_thread::sleep_for(1s);
			}, L"Monitoring Thread");
	}

	for (int32 i = 0; i < 2; i++)
	{
		//	Ref Count No Add
		GThreadManager->Launch([this]()
			{
				LEndTickCount = ::GetTickCount64() + PROCESS_TICK;

				//	I/O ( JobQueue Push MultiThread )
				//	Game Logic ( JobQueue Execute -> One Thread )
				if(_service)
					_service->GetIocpCore()->Dispatch(10);

				//	Global Reserve Jobs Push JobQueue
				//ThreadManager::DistributeReserveJobs();

				//	Game Logic
				//ThreadManager::DoGlobalJobQueueExecute();

			},L"Worker Thread");
	}

	//	Thread Join
	//GThreadManager->Join();
	while (_stopped == false)
	{
		GRoom->Update();
		
		if (GetAsyncKeyState(VK_ESCAPE) & 0x8000)
		{
			Close();
			break;
		}

		std::this_thread::yield();
	}
}

void GameServer::Close()
{
	_stopped = true;

	delete GGameObjectManager;
	GGameObjectManager = nullptr;

	delete GDataManager;
	GDataManager = nullptr;

	delete GWorldManager;
	GWorldManager = nullptr; 

	_service->CloseService();
	GSendBufferManager->Close();

	//	TEMP
	GRoom = nullptr;

	delete GClientSessionManager;
	GClientSessionManager = nullptr;

	_service = nullptr;

	GCoreGlobal->Clear();
	delete GCoreGlobal;
	GCoreGlobal = nullptr;

	google::protobuf::ShutdownProtobufLibrary();
}
