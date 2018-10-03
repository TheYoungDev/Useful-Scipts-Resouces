# Useful Scripts

This repo contains scripts that could be reused for a varitey of projects.

## Local Scripts

The follow scripts are for local execution and do not contain functionaility for networked applications.

### General Scripts
Scripts that can be useful in most projects:


* CameraExploreMovement:

	- Script that allows camera to move freely with the use of mouse and wasd. Great for exploreing scenes.
	
* ConsoleCommandManager:
	
	- Script that allows the user to enter commands during runtime to execute fuctions and access/edit object/project information. 

* TestFunction:
	
	- Use to test ConsoleCommandManager.cs

### GUI
Menu, Hud, General GUI Scripts:

* CrossHair:
	
	- Draw CrossHair on the center of the users screen.

* MainMenu:
	
	- Script to Manage a Main Menu. 

* Pause_Menu:
	
	- Similar to MainMenu.cs but with added features to pause the game state.


### Actions

Event Based Scripts:


* DieOnTrigger.cs
	
	- Trigger Player Death Event if triggered.
	
* GravityGenerator.cs
	
	- Gravity Beam that disables gravity and pushes the physics based object in a certian direction.
	
* HotColdEffect.cs
	
	- Modifies the physical attributes of an object by making it more "bouncy" or "slipery".
	
* JumpPad.cs
	
	- Launches object with desired force in a desired direction.
	
* Laser.cs
	
	- Manages Laser beam allows redirection/activation using the laser.
	
* LocalMindControl.cs
	
	- Controls Enemy AI Objects/bots mimic players movement.
	
* MoveingPlatform.cs
	
	- Platform that moves between checkpoints players will stick to the platform aslong as it doenst have the Icy attribute.
	
* OldRewindManager.cs
	
	- Manages the history of an object and allows the player to freeze the object in time aswell as rewind its position/rotation.
	
* OnCollisionDestory.cs
	
	- Destory Object on Collision
	
* OnTriggerKill.cs
	
	- Trigger Death() when triggered.
	
* OpenDoor.cs
	
	- Track switches and open/close a door if all triggers are activiated/deactiviated
	
* PortalAbility.cs
	
	- Create and Move portals where the player aims their cursor.
	
* PortalScript.cs
	
	- Link Portals to one another as to allow the player to teleport to the other portal when touched.
	
* SeamlessPortal.cs
	
	- Creates/render texture during runtime and uses players position/rotation to display the information on the other side of the portal.	

* RedirectLaser.cs
	
	- Redirect Lasers beam to continue it in another direction.
	
* TriggerCheckPoint.cs
	
	- Triggers a checkpoint inwhich the player will return to if they die.
	
* TriggerEvent.cs
	
	- Triggers events like opening doors activiating switches reaching a certain destination/time.
	
* EnemyAI.cs
	
	- Enemy AI script that controls the AIs behaviour.
		- ranaged or meele
		- attack range
		- chase range
		- normal pathing/patroling
		- vision range i.e can only see players if in 180deg field of view.

## Network Scripts

The follow scripts are for network execution and do not contain functionaility for local applications.



### General
Scripts that can be useful in most  network projects:

* AssignAuth:
	
	- Assign Network Authority Over an object to all the player to determine the state of the object across the network.

* DontDestoryOnLoad:
	
	- Mantain scripts/objects when transitioning to a different scene.

* NetworkDataSync:
	
	- Sync Data between Clients
	
* NetworkDisable:
	
	- Disable Objects/scripts if the client does not own the objects/scripts.
	
* NetworkPlayer:
	
	- Similar to NetworkDisable but is speafic to player data.
	
* NetworkPlayerManager

	- Manages Network Player allows player to create/control objects with Authority.
	
* PlayerConnectionObject

	- Old NetworkPlayerManager slight differences.


### Actions
Event Based Scripts:


* NetworkObject:

	- Allow any player to pickup and throw any object with the related script.
	
* NetworkPortal:
	
	- Networked portal ability.
	- Portals can be used by anyone but are linked based on client that shot them.

* NetworkRewindManger:
	
	- Rewind/freeze Time for certain objects across all clients.

### Low Level Scripts

Network Packet Management Scripts: The follow scripts assume an authuritive network inwhich data from th client to the server and vice versa, but information is never set between clients.


* NetworkPacketManager.cs
	
	- Manage Recieved / Sent Protobuf packets.
	- Configure server tick rate
	- Creates a queue of recieved packages
	- packets are managed using timestamps.
	- Serialize and Deserialize data
	- Queue and dequeue packets when packets are recieved and managed.
	
* NetworkPacketController.cs
	
	- Define protobuff structure for recieved/sent packets.
	- Use timestamps to determine order/discard outdated information
	- Configure what actions are taken when packets are recieved.




