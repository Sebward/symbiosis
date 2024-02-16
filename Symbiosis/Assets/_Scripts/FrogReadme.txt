RigidBody movement
frog{
	set up:
	for on_ground detection, must set the ground to the tag: midground
	for in_water detection, must set the water's collider to "IsTrigger", and set its tag to "Water"

	notes:
	The frog uses a physics material 2D to maximize the friction. The material is under Prefab folder.
} 


tongue{
	set up:
	A seperate prefab called "FrogTongue"
	in the script "frog_tongue", attach the mousePos, the mousePos is under the prefab "Frog"
	the tongue's tag is set to "Tongue", different from the frog.
	its polygon collider need to be set to "IsTrigger"

	0215 update:
	set the non-stick to the tag "NonStick"

	notes:
	the tongue needs one and only one camera, and the camera's tag need to be "MainCamera"
	
	
}
